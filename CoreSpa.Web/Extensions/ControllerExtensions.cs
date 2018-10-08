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
    }
}
