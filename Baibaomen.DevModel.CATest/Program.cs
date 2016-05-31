using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using System.Net.Http;

namespace Baibaomen.DevModel.CATest
{
    class Program
    {

        static TokenResponse GetClientToken()
        {
            var client = new TokenClient(
                "https://localhost:44301/identity/connect/token",
                "silicon",
                "F621F470-9731-4A25-80EF-67A6F7C5F4B8");

            return client.RequestClientCredentialsAsync("api1").Result;
        }

        static TokenResponse GetUserToken()
        {
            var client = new TokenClient(
                "https://localhost:44301/identity/connect/token",
                "carbon",
                "21B5F798-BE55-42BC-8AA8-0025B903DC3B");

            return client.RequestResourceOwnerPasswordAsync("bob", "secret", "api1").Result;
        }

        static void CallApi(TokenResponse response)
        {
            var client = new HttpClient();
            client.SetBearerToken(response.AccessToken);

            Console.WriteLine(client.GetStringAsync("http://localhost:9889/api/test/echo").Result);
        }

        static void Main(string[] args)
        {
            //CallApi(GetClientToken());

            //CallApi(GetUserToken());

            Console.ReadLine();
        }
    }
}
