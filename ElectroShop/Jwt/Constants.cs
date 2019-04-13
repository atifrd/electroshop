
namespace ElectroShop.Helpers
{
    public static class Constants
    {
        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id" , Email = "email" , Language = "language";
            }

            public static class JwtClaims
            {
                public const string NoneAccess = "none_access";
                public const string ApiAccess = "api_access";
                public const string BuyerAccess = "buyer_access";
                public const string SellerAccess = "seller_access";
                public const string AdminAccess = "admin_access";
            }
        }
    }
}
