using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CoreSpa.Data.Entities;
using CoreSpa.Web.Helpers;
using CoreSpa.Web.Helpers.Constants;
using CoreSpa.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreSpa.Web.Controllers
{
    [Route("api/[controller]")] 
    public class AccountsController : Controller
    {
        //todo: get in from more safer place
        private const string AdminToken = "ynukUhtif9gCHvE8hc7LVUMEgrNRpPkx";

        private readonly ApplicationDbContext _appDbContext;
        private readonly ClaimsPrincipal _caller;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AccountsController(
            IHttpContextAccessor httpContextAccessor,
            UserManager<AppUser> userManager, IMapper mapper, ApplicationDbContext appDbContext)
        {
            _caller = httpContextAccessor.HttpContext.User;

            _userManager = userManager;
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<AppUser>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            await _appDbContext.Customers.AddAsync(new Customer { IdentityId = userIdentity.Id, Location = model.Location });
            await _appDbContext.SaveChangesAsync();

            return new OkObjectResult("Account created");
        }

        // GET api/accounts/make-me-admin
        [HttpGet("make-me-admin/{token}")]
        [Authorize(ApiPolicies.ApiUser)]
        public async Task<IActionResult> MakeMeAdmin(string token)
        {
            if (token != AdminToken)
            {
                return Unauthorized();
            }

            var userId = _caller.Claims.Single(c => c.Type == "id").Value;
            var appUser = _appDbContext.Users.FirstOrDefault(x => x.Id == userId);

            if (appUser == null)
            {
                return new BadRequestObjectResult("Error getting user information");
            }

            await _userManager.AddToRoleAsync(appUser, Roles.Admin);

            return new OkObjectResult("You are admin now! Please, re-login to get update your clams");
        }

        [HttpPost("make-admin/{id}/{status}")]
        [Authorize(Policy = ApiPolicies.ApiAdmin)]
        public async Task<IActionResult> MakeAdmin(int id, bool status)
        {
            var userId = _appDbContext.Customers.FirstOrDefault(x => x.CustomerId == id)?.IdentityId;
            var appUser = _appDbContext.Users.FirstOrDefault(x => x.Id == userId);

            if (appUser == null)
            {
                return new BadRequestObjectResult("Error getting user information");
            }

            if (status)
            {
                await _userManager.AddToRoleAsync(appUser, Roles.Admin);
            }
            else
            {
                await _userManager.RemoveFromRoleAsync(appUser, Roles.Admin);
            }

            return Ok();
        }
    }
}
