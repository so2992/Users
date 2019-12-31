using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UsersWeb.ViewModels;

namespace UsersWeb.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        [Route("controller/getUsersId")]
        public async Task<int[]> GetUsersId()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://localhost:8092/api/GetUsersId");

            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<IEnumerable<int>>(content);

            return result.ToArray();
        }

        [HttpGet]
        [Route("controller/getUser/{id}")]
        public async Task<UserViewModel> GetUsersId(int id)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://localhost:8092/api/getUser/" + id);

            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<UserViewModel>(content);

            return result;
        }

        [HttpPost]
        [Route("controller/updateUser")]
        public async Task<ResponseMessage> UpdateUser(UserViewModel user)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var httpContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://localhost:8092/api/updateUser/", httpContent);

            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<ResponseMessage>(content);

            return result;
        }
    }
}
