using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReCaptchaDemo.Models
{
    public class Employee
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string ReCaptchaCode { get; set; }
    }
}