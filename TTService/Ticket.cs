using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace TTService
{
    public class Ticket
    {
        private string authorName;
        private string authorEmail;
        private string title;
        private string description;
        private DateTime creation;
        private string state;
        private string id;

        public Ticket()
        {

        }
        
        public Ticket(string name, string email, string title, string desc, DateTime creation, string id)
        {
            this.AuthorName = name;
            this.AuthorEmail = email;
            this.Title = title;
            this.Description = desc;
            this.Creation = creation;
            this.id = id;
        }

        public string AuthorName { get => authorName; set => authorName = value; }
        public string AuthorEmail { get => authorEmail; set => authorEmail = value; }
        public string Title { get => title; set => title = value; }
        public DateTime Creation { get => creation; set => creation = value; }
        public string Description { get => description; set => description = value; }
        public string Id { get => id; set => id = value; }


        public string State { get => state; set => state = value; }

        public static ArrayList getTickets(DataTable dataTable)
        {
            ArrayList ticketsList = new ArrayList();


            foreach (DataRow row in dataTable.Rows)
            {
                if (row.RowState == DataRowState.Deleted) continue;
                string elemId = row["Id"].ToString();
                string elemAuthor = row["Author"].ToString();
                string elemTitle = row["Title"].ToString();
                string elemState = row["State"].ToString();
                var ticket = new Ticket(elemAuthor, "", elemTitle, "", DateTime.Now, elemId);
                ticket.State = elemState;
                ticket.Id = elemId;
                ticketsList.Add(ticket);
            }
            return ticketsList;
        }

    }
}
