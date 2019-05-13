using System;

namespace TodoList.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string ContactNo { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
