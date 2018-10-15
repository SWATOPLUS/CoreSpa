using System;
using System.Linq;
using System.Security.Claims;
using CoreSpa.Web.Helpers.Constants;
using Microsoft.AspNetCore.Mvc;

namespace CoreSpa.Web.Extensions
{
    public static class ControllerExtensions
    {
        public static bool IsAdmin(this Controller controller)
        {
            return controller.User.IsInRole(Roles.Admin);
        }

        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.Single(c => c.Type == JwtClaimIdentifiers.Id).Value;
        }

        public static int GetCustomerId(this ClaimsPrincipal claimsPrincipal)
        {
            return Convert.ToInt32(claimsPrincipal.Claims.Single(c => c.Type == JwtClaimIdentifiers.CustomerId).Value);
        }
    }
}
