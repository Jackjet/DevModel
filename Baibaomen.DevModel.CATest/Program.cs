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
                "web_api",
                "60DAA737-95F7-4910-BD7F-E01B6B2AB8E2");

            return client.RequestClientCredentialsAsync("api1").Result;
        }

        static TokenResponse GetUserToken()
        {
            var client = new TokenClient(
                "https://localhost:44301/identity/connect/token",
                "web_user",
                "C4878BC2-B315-49CC-B6BD-BAA325C8A902");

            return client.RequestResourceOwnerPasswordAsync("bob", "secret", "profile").Result;
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
