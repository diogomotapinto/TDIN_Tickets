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

        private string answer;
        private string secundaryQuestionTitle;
        private string secundaryQuestionDescription;
        private string secundaryQuestionAnswer;

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
        public string SecundaryQuestionTitle { get => secundaryQuestionTitle; set => secundaryQuestionTitle = value; }
        public string SecundaryQuestionDescription { get => secundaryQuestionDescription; set => secundaryQuestionDescription = value; }
        public string SecundaryQuestionAnswer { get => secundaryQuestionAnswer; set => secundaryQuestionAnswer = value; }
        public string Answer { get => answer; set => answer = value; }

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
                string desc = row["Description"].ToString();
                string answer = row["Answer"].ToString();
                string secTitle = row["SecundaryQuestionTitle"].ToString();
                string secDesc = row["SecundaryQuestionDesciption"].ToString();
                string secAnswer = row["SecundaryQuestionAnswer"].ToString();
                var ticket = new Ticket(elemAuthor, "", elemTitle, "", DateTime.Now, elemId);
                ticket.State = elemState;
                ticket.Id = elemId;
                ticket.Description = desc;
                ticket.Answer = answer;
                ticket.secundaryQuestionTitle = secTitle;
                ticket.secundaryQuestionDescription = secDesc;
                ticket.secundaryQuestionAnswer = secAnswer;
                ticketsList.Add(ticket);
            }
            return ticketsList;
        }

    }
}
