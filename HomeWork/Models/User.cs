using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeWork.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Details { get; set; }
    }

    public class Country
    {
        public int ID { get; set; }
        public string NameCountry { get; set; }
    }
    public class Cities
    {
        public int ID { get; set; }
        public Country Country_ID { get; set; }
        public string Name { get; set; }
    }
}