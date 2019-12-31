using Newtonsoft.Json;
using System;

namespace UsersApi.ViewModels
{
    public class UserViewModel
    {
        [JsonProperty("Id")]
        public int UserId { get; set; }

        [JsonProperty("UserName")]
        public string Username { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("DateOfBirth")]
        public DateTime DateOfBirth { get; set; }
    }
}
