using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MoabDashboard
{
    public class ClientSDK
    {
        public async Task<string> Create(string uriString, string jsonString, string securityToken)
        {
            var uri = new Uri(uriString);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + securityToken);
                var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, stringContent);

                if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new HttpException("401");
                }

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new HttpException("404");
                }

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    throw new HttpException("400");
                }


                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }




        public async Task<string> Read(string uriString, string securityToken)
        {
            var uri = new Uri(uriString);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + securityToken);
                var response = await client.GetAsync(uri);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new HttpException("401");
                }

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new HttpException("404");
                }

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }


        public async Task<string> ReadOne(string uriString, string securityToken)
        {
            var uri = new Uri(uriString);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + securityToken);
                var response = await client.GetAsync(uri);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new HttpException("401");
                }

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new HttpException("404");
                }

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }



        public async Task<string> Delete(string uriString, string securityToken)
        {
            var uri = new Uri(uriString);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + securityToken);
                var response = await client.DeleteAsync(uri);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new HttpException("401");
                }

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new HttpException("404");
                }

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }



    }
}