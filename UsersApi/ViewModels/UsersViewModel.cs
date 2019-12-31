using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersApi.ViewModels
{
    public class UsersViewModel
    {
        [JsonProperty("Users")]
        public UserViewModel[] Users { get; set; }
    }
}
