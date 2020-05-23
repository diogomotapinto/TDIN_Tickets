using System.Data;
using System.ServiceModel;
using TTService;

namespace TTClient
{

    class TTProxy : ClientBase<ITTService>, ITTService
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

        public void AddUser(string username)
        {
            Channel.AddUser(username);
        }
    }
}
