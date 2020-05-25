using System.Data;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace TTService
{
    [ServiceContract]
    public interface ITTService
    {
        [WebInvoke(Method = "POST", UriTemplate = "/tickets", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        int AddTicket(string author, string problem, string title);

        [WebGet(UriTemplate = "/tickets/{author}", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        DataTable GetTickets(string author);

        [WebGet(UriTemplate = "/tickets/get/{assign}", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        DataTable GetTicketsAssign(string assign);

        [WebGet(UriTemplate = "/users", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        DataTable GetUsers();

        [WebInvoke(Method = "POST", UriTemplate = "/users/add", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        void AddUser(string username);

        [WebInvoke(Method = "POST", UriTemplate = "/tickets/update/{userId}/{ticketId}", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        void updateAssigned(string userId, string ticketId);

    }
}
