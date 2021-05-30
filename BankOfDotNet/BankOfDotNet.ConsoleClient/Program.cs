using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BankOfDotNet.ConsoleClient
{
    class Program
    {
        public static void Main(string[] args) => ResourceOwnerPasswordMainAsync().GetAwaiter().GetResult();

        private static async Task ResourceOwnerPasswordMainAsync()
        {
            HttpClient httpClient = new HttpClient();

            var discoRo = await httpClient.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (discoRo.IsError)
            {
                Console.WriteLine(discoRo.Error);
                return;
            }


            //Grab a bearer token using Resource owner password
            var tokenResponseRo = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = "http://localhost:5000/connect/token",
                GrantType = "password",

                ClientId = "ro.client",
                ClientSecret = "secret",

                Scope = "bankOfDotNetApi",

                UserName = "Bob",
                Password = "password"
            });

            if (tokenResponseRo.IsError)
            {
                Console.WriteLine(tokenResponseRo.Error);
                return;
            }

            Console.WriteLine(tokenResponseRo.Json);
            Console.WriteLine("\n\n");

            //Consume our Customer API
            await ConsumeCustomerApi(tokenResponseRo.AccessToken);
        }

        private static async Task ClientCredentialsMainAsync()
        {
            HttpClient httpClient = new HttpClient();

            //discover all the endpoints using metadata of identity server
            var disco = await httpClient.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            //Grab a bearer token
            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = "http://localhost:5000/connect/token",
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "bankOfDotNetApi"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            //Consume our Customer API
            await ConsumeCustomerApi(tokenResponse.AccessToken);
        }

        private static async Task ConsumeCustomerApi(string accessToken)
        {
            //Consume our Customer API
            var client = new HttpClient();
            client.SetBearerToken(accessToken);

            var customerInfo = new StringContent(
                JsonConvert.SerializeObject(new { Id = 10, FirstName = "Manish", LastName = "Narayan" }),
                Encoding.UTF8, "application/json");

            var createCustomerResponse = await client.PostAsync("http://localhost:6000/api/customer", customerInfo);

            if (!createCustomerResponse.IsSuccessStatusCode)
            {
                Console.WriteLine(createCustomerResponse.StatusCode);
            }

            var getCustomerResponse = await client.GetAsync("http://localhost:6000/api/customer");
            if (!getCustomerResponse.IsSuccessStatusCode)
            {
                Console.WriteLine(getCustomerResponse.StatusCode);
            }
            else
            {
                var content = await getCustomerResponse.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

            Console.Read();
        }
    }
}
