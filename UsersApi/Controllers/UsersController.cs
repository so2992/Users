using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using UsersApi.ViewModels;

namespace UsersApi.Controllers
{
    [ApiController]

    public class UsersController
    {
        [HttpGet]
        [Route("api/GetUser/{userId}")]
        public UserViewModel GetUser(int userId)
        {
            using (StreamReader reader = new StreamReader(@"..\..\..\..\FakeUsers.json"))
            {
                string json = reader.ReadToEnd();
                UsersViewModel users = JsonConvert.DeserializeObject<UsersViewModel>(json);

                return users.Users.Where(x => x.UserId == userId).FirstOrDefault();
            }
        }

        [HttpGet]
        [Route("api/GetUsersId")]
        public IEnumerable<int> GetUsersId()
        {
            using (StreamReader reader = new StreamReader(@"..\..\..\..\FakeUsers.json"))
            {
                string json = reader.ReadToEnd();
                UsersViewModel users = JsonConvert.DeserializeObject<UsersViewModel>(json);

                return users.Users.Select(x => x.UserId).ToList();
            }
        }

        [HttpPost]
        [Route("api/UpdateUser")]
        public ResponseMessage UpdateUser(UserViewModel userToUpdate)
        {
            using (StreamReader reader = new StreamReader(@"..\..\..\..\FakeUsers.json"))
            {
                string json = reader.ReadToEnd();
                UsersViewModel users = JsonConvert.DeserializeObject<UsersViewModel>(json);
                reader.Close();

                if (userToUpdate.Username == null || userToUpdate.FirstName == null || userToUpdate.LastName == null || userToUpdate.DateOfBirth == null)
                {
                    return new ResponseMessage() { IsSuccessfull = false, Message = "User details cannot be empty!" };
                }

                foreach (var user in users.Users)
                {
                    if (user.UserId == userToUpdate.UserId)
                    {
                        if (user.Username == userToUpdate.Username && user.FirstName == userToUpdate.FirstName && user.LastName == userToUpdate.LastName && user.DateOfBirth == userToUpdate.DateOfBirth)
                        {
                            return new ResponseMessage() { IsSuccessfull = false, Message = "User details not modified!" };
                        }
                        else
                        {
                            user.Username = userToUpdate.Username;
                            user.FirstName = userToUpdate.FirstName;
                            user.LastName = userToUpdate.LastName;
                            user.DateOfBirth = userToUpdate.DateOfBirth;
                        }
                    }
                }

                json = JsonConvert.SerializeObject(users, Formatting.Indented);
                File.WriteAllText(@"..\..\..\..\FakeUsers.json", json);
            }

            return new ResponseMessage() { IsSuccessfull = true, Message = "User updated successfully!" };
        }
    }
}
