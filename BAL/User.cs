using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BAL
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

