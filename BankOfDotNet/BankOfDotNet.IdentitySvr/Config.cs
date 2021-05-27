using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace BankOfDotNet.IdentitySvr
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetAllApiResources()
        {
            return new[]
            {
                new ApiResource ("bankOfDotNet", "Customer Api for BankOfDotNet")
                {
                    Scopes = { "bankOfDotNetApi", "bankOfDotNetAdminApi" }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "bankOfDotNetApi" }
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScope()
        {
            return new List<ApiScope>
            {
                new ApiScope("bankOfDotNetApi","Customer Api for BankOfDotNet"),
                new ApiScope("bankOfDotNetAdminApi","Administrator Api for BankOfDotNet")
            };
        }
    }
}
