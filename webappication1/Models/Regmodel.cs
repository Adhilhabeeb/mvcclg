using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webappication1.Models
{
    public class Regmodel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string currentpassword { get; set; }
    }

}