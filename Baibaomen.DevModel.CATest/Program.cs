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
                "https://localhost:44333/core/connect/token",
                "web_system",
                "0D66FECD-2D4F-4947-8483-9E561532E9C9");

            return client.RequestClientCredentialsAsync("api").Result;
        }

        static TokenResponse GetUserToken()
        {
            var client = new TokenClient(
                "https://localhost:44333/core/connect/token",
                "web_user",
                "E066B041-5C34-47E5-9581-A16A27724D0C");

            return client.RequestResourceOwnerPasswordAsync("bob", "secret", "api").Result;
        }

        static void CallApi(TokenResponse response)
        {
            var client = new HttpClient();
            client.SetBearerToken(response.AccessToken);

            //Console.WriteLine(client.GetStringAsync("http://localhost:9889/api/test/echo").Result);
            Console.WriteLine(client.GetStringAsync("http://localhost:9996/api/test/echo").Result);
        }

        static void Main(string[] args)
        {
            //CallApi(GetClientToken());

            CallApi(GetUserToken());

            Console.ReadLine();
        }
    }
}
