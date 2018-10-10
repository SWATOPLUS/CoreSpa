using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreSpa.Data.Entities;
using CoreSpa.Web.Extensions;
using CoreSpa.Web.Helpers.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreSpa.Web.Controllers
{
    [Authorize(Policy = ApiPolicies.ApiUser)]
    [Route("api/[controller]/[action]")]
    public class DashboardController : Controller
    {
        private readonly ClaimsPrincipal _caller;
        private readonly ApplicationDbContext _appDbContext;

        public DashboardController(UserManager<AppUser> userManager, ApplicationDbContext appDbContext,
            IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _appDbContext = appDbContext;
        }

        // GET api/dashboard/home
        [HttpGet]
        public async Task<IActionResult> Home()
        {
            // retrieve the user info
            //HttpContext.User
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var customer = _appDbContext.Customers.Single(c => c.IdentityId == userId.Value);

            return new OkObjectResult(customer);
        }

        [HttpGet]
        public async Task<IActionResult> Profile(int customerId)
        {
            var customer = await _appDbContext.Customers.SingleAsync(x => x.CustomerId == customerId);

            return new OkObjectResult(customer);
        }

        [HttpPost]
        [Authorize(ApiPolicies.ApiUser)]
        public async Task<IActionResult> Profile([FromBody] Customer changes)
        {
            if (changes == null || changes.CustomerId == 0)
            {
                return BadRequest();
            }

            var userId = _caller.Claims.Single(c => c.Type == "id").Value;

            var customer = _appDbContext.Customers.Single(x => x.CustomerId == changes.CustomerId);

            if (!this.IsAdmin() && customer.IdentityId != userId)
            {
                return Unauthorized();
            }

            customer.ApplyChanges(changes);

            _appDbContext.Customers.Update(customer);
            _appDbContext.SaveChanges();

            return Ok();
        }
    }
}
