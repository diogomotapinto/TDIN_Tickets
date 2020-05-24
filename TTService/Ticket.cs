using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTService
{
    class Ticket
    {
        private string authorName;
        private string authorEmail;
        private string title;
        private string description;
        private DateTime creation;

        public Ticket(string name, string email, string title, string desc, DateTime creation)
        {
            this.AuthorName = name;
            this.AuthorEmail = email;
            this.Title = title;
            this.Description = desc;
            this.Creation = creation;
        }

        public string AuthorName { get => authorName; set => authorName = value; }
        public string AuthorEmail { get => authorEmail; set => authorEmail = value; }
        public string Title { get => title; set => title = value; }
        public DateTime Creation { get => creation; set => creation = value; }
        public string Description { get => description; set => description = value; }
    }
}
