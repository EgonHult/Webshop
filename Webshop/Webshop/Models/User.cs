﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop.Models
{
    public class User : IdentityUser
    {
        //public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string PhoneNumber { get; set; }
        //public string Email { get; set; }
        public string StreetAddress { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        //public string Password { get; set; }
        public List<Order> Orders { get; set; }

    }
}
