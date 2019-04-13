
using System.Security.Claims;
using System.Threading.Tasks;

namespace ElectroShop.Jwt
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id , bool isSuperVisor , string language , bool isMaster ,bool isActiveMaster ,  bool isActive , bool IsTranslator ,bool IsActiveTranslator);
    }
}
