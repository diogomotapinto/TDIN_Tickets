using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTService
{
    public class User
    {
        private int id;
        private string username;
        private string role;

        public User(int id, string username, string role)
        {
            this.Id = id;
            this.Username = username;
            this.Role = role;
        }

        public string Role { get => role; set => role = value; }
        public string Username { get => username; set => username = value; }
        public int Id { get => id; set => id = value; }
    }
}
