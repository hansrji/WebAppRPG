using System;
using System.Security.Cryptography;
using AspNetCore.Identity.Mongo.Model;

namespace RPGWebAPI.Models.Identity
{
    //Add any custom field for a user
    public class ApplicationUser : MongoUser
    {
        public string Name { get; set; }
		public string LastName { get; set; }
        public string City { get; set; }
    }
}
