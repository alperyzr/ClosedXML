using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClosedXML.Web.Helper
{
    public static class HttpPostHelper<TResponse>
    {
        public static async Task<TResponse> PostDataAsync(IHttpContextAccessor httpContextAccessor, IHttpClientFactory clientFactory, string path, object request, bool useProperCase = false, bool UseAuthorization = true)
        {
            using var client = clientFactory.CreateClient();
            if (UseAuthorization)
            {
                var token = httpContextAccessor.HttpContext.Request.Cookies["Token"];
                if (token != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());
            }
            var response = await client.PostAsync(path,
                new StringContent(
                    JsonConvert.SerializeObject(request),
                    Encoding.UTF8,
                    "application/json")).ConfigureAwait(false);
            var data = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            //convert snakecase result to proper case properties
            if (useProperCase)
                data = JsonHelper.ConvertJson(data, new ProperCaseFromSnakeCaseNamingStrategy());

            var res = JsonConvert.DeserializeObject<TResponse>(data);



            return res;
        }
    }
}
