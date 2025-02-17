﻿using System.Data;
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

        public void updateAssigned(string userId, string ticketId)
        {
            Channel.updateAssigned(userId, ticketId);
        }

        public void AddAnswer(string answer, string ticketId)
        {
            Channel.AddAnswer(answer, ticketId);
        }

        public void AddWating(string ticketId, string questionTitle, string questionDescription)
        {
            Channel.AddWating(ticketId, questionTitle, questionDescription);
        }

        public bool addSecondaryAnswer(string ticketId, string answer)
        {
            return Channel.addSecondaryAnswer(ticketId, answer);
        }
    }
}
