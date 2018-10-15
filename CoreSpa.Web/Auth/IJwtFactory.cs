using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreSpa.Web.Auth
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string userId, int customerId, bool isAdmin);
    }
}
