using System.Data;
using System.ServiceModel;
using TTService;

namespace TTService
{

    public class TTProxy : ClientBase<ITTService>, ITTService
    {
        public DataTable GetUsers()
        {
            return Channel.GetUsers();
        }

        public DataTable GetTickets(string author)
        {
            return Channel.GetTickets(author);
        }

        public int AddTicket(string author, string desc, string title)
        {
            return Channel.AddTicket(author, desc, title);
        }

        public bool AddUser(string username, string role)
        {
            return Channel.AddUser(username, role);
        }

        public DataTable GetTicketsAssign(string assign)
        {
            return Channel.GetTicketsAssign(assign);
        }
    }
}
