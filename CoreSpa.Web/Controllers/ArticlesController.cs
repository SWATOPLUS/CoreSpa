using CoreSpa.Data.Entities;
using CoreSpa.Web.Extensions;
using CoreSpa.Web.Helpers.Constants;
using CoreSpa.Web.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace CoreSpa.Web.Controllers
{
    [Route("api/[controller]/[action]")] 
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly ClaimsPrincipal _caller;

        public ArticlesController
        (
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext appDbContext
        )
        {
            _caller = httpContextAccessor.HttpContext.User;
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult ListArticle([DataSourceRequest]DataSourceRequest request)
        {
            return Json(GetArticleSummaries().ToDataSourceResult(request));
        }

        [HttpGet]
        public IActionResult ListTag()
        {
            var list = _appDbContext.Tags.ToArray();

            return Json(list);
        }

        [HttpPost]
        [Authorize(ApiPolicies.ApiUser)]
        public IActionResult AddOrUpdateArticle([FromBody]ArticleEditModel model)
        {
            var customerId = _caller.GetCustomerId();

            if (model.CustomerId == 0)
            {
                model.CustomerId = customerId;
            }

            if (model.CustomerId != customerId && !this.IsAdmin())
            {
                return Unauthorized();
            }

            var tagIds = GetTagsIds(model.TagsDisplayNames).Distinct();

            var entry = _appDbContext.Articles.Add(new Article
            {
                ArticleId = model.ArticleId,
                Category = model.Category,
                Content = model.Content,
                CustomerId = model.CustomerId,
                Summary = model.Summary,
                Title = model.Title,
                Tags = tagIds.Select(x => new ArticleAndTag {ArticleTagId = x}).ToArray()
            });

            _appDbContext.SaveChanges();

            return Ok(new {entry.Entity.ArticleId});
        }


        private IEnumerable<int> GetTagsIds(IEnumerable<string> displayNames)
        {
            var tags = displayNames.Select(x => new ArticleTag
            {
                DisplayName = x,
                NormalizedName = x.Trim().ToLower()
            })
                .ToArray();
            
            var normalizedNames = tags
                .Select(x => x.NormalizedName)
                .ToArray();

            var oldTags = _appDbContext.Tags
                .Where(x => normalizedNames.Contains(x.NormalizedName))
                .ToArray();

            var oldTagsNormalizedNames = oldTags
                .Select(x => x.NormalizedName)
                .ToArray();

            var newTags = tags
                .Where(x => !oldTagsNormalizedNames.Contains(x.NormalizedName))
                .ToArray();

            _appDbContext.Tags.AddRange(newTags);
            _appDbContext.SaveChangesAsync();

            return oldTags.Select(x => x.ArticleTagId)
                .Concat(newTags.Select(x => x.ArticleTagId));
        }

        private IQueryable<ArticleSummary> GetArticleSummaries()
        {
            return _appDbContext.Articles
                .Include(x => x.Customer)
                .Include(x => x.Comments)
                .Include(x => x.Rates)
                .Include(x => x.Tags)
                .ThenInclude(x => x.Tag)
                .Select(x => new ArticleSummary
                {
                    ArticleId = x.ArticleId,
                    Category = x.Category,
                    CustomerId = x.CustomerId,
                    CustomerDisplayName = x.Customer.DisplayName,
                    CommentsCount = x.Comments.Count,
                    RatesAverage = x.Rates.Select(r => r.Mark).DefaultIfEmpty(0).Average(),
                    RatesCount = x.Rates.Count,
                    Summary = x.Summary,
                    Tags = x.Tags.Select(t => t.Tag).ToArray(),
                    Title = x.Title
                });
        }
    }
}
