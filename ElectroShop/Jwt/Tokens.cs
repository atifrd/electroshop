

using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ElectroShop.Jwt;
using ElectroShop.Models;
using Newtonsoft.Json;

namespace ElectroShop.Helpers
{
  public class Tokens
  {
    public static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, string profileImage, string fullName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
    {
      var response = new
      {
        id = identity.Claims.Single(c => c.Type == "id").Value,
        localId = identity.Claims.Single(c => c.Type == "language").Value,
        auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
        expires_in = (int)jwtOptions.ValidFor.TotalSeconds,
        image_prof = profileImage,
        full_name = fullName
      };

      return JsonConvert.SerializeObject(response, serializerSettings);
    }
  }
}
