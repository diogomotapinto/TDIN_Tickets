using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TTService
{
    public class TTService : ITTService
    {
        readonly string database;

        TTService()
        {
            string connection = ConfigurationManager.ConnectionStrings["TTs"].ConnectionString;
            database = String.Format(connection, AppDomain.CurrentDomain.BaseDirectory);
        }

        public void AddAnswer(string answer, string ticketId)
        {
            DataTable result = new DataTable("TTickets");

            using (SqlConnection c = new SqlConnection(database))
            {
                try
                {
                    c.Open();
                    string sql = "Update TTickets SET Answer=@answer, State='solved' Where Id=@ticketId";

                    SqlCommand cmd = new SqlCommand(sql, c);
                    cmd.Parameters.AddWithValue("@answer", answer);
                    cmd.Parameters.AddWithValue("@ticketId", ticketId);
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                }
                finally
                {
                    c.Close();
                }
            }
        }

        public int AddTicket(string author, string problem, string title)
        {
            int id = 0;

            using (SqlConnection c = new SqlConnection(database))
            {
                try
                {
                    c.Open();
                    string sql = "insert into TTickets(Author, Problem, Title, Date, Answer, Status) values (@a1, @p1, @t1, @d1,'', 1)"; // injection protection
                    SqlCommand cmd = new SqlCommand(sql, c);                                                       // injection protection
                    DateTime time = DateTime.Now;
                    string format = "yyyy-MM-dd HH:mm:ss";
                    cmd.Parameters.AddWithValue("@d1", time.ToString(format));
                    cmd.Parameters.AddWithValue("@a1", author);                                                    // injection protection
                    cmd.Parameters.AddWithValue("@p1", problem);                                                   // injection protection
                    cmd.Parameters.AddWithValue("@t1", title);
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "select max(Id) from TTickets";
                    id = (int)cmd.ExecuteScalar();
                }
                catch (SqlException)
                {
                }
                finally
                {
                    c.Close();
                }
            }
            return id;
        }

        public Boolean AddUser(string username, string role)
        {
            int id = 0;
            using (SqlConnection c = new SqlConnection(database))
            {
                try
                {
                    c.Open();
                    string sql = "insert into Employees(Name, Role) values (@a1, @r1)"; // injection protection
                    SqlCommand cmd = new SqlCommand(sql, c);                                                       // injection protection
                    cmd.Parameters.AddWithValue("@a1", username);
                    cmd.Parameters.AddWithValue("@r1", role);
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "select max(Id) from Employees";
                    id = (int)cmd.ExecuteScalar();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("falha ao adicionar");
                    return false;
                }
                finally
                {
                    c.Close();

                }
            }

            return true;

        }

        public void AddWating(string ticketId)
        {
            return;
        }

        public DataTable GetTickets(string author)
        {
            DataTable result = new DataTable("TTickets");

            using (SqlConnection c = new SqlConnection(database))
            {
                try
                {
                    c.Open();
                    string sql = "select Id, Description, Title, State, Date, Answer from TTickets where Author=@a1";
                    SqlCommand cmd = new SqlCommand(sql, c);
                    cmd.Parameters.AddWithValue("@a1", author);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(result);
                }
                catch (SqlException ex)
                {
                }
                finally
                {
                    c.Close();
                }
            }
            return result;
        }

        public DataTable GetTicketsAssign(string assign)
        {
            Console.WriteLine(assign);
            DataTable result = new DataTable("TTickets");
            using (SqlConnection c = new SqlConnection(database))
            {
                try
                {
                    c.Open();
                    string sql = "select * from TTickets";
                    SqlCommand cmd = new SqlCommand(sql, c);
                    cmd.Parameters.AddWithValue("@a1", assign);
                    cmd.Parameters.AddWithValue("@a2", "unassigned");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(result);
                }
                catch (SqlException ex)
                {
                }
                finally
                {
                    c.Close();
                }
            }
            return result;
        }

        public DataTable GetUsers()
        {
            DataTable result = new DataTable("Users");

            using (SqlConnection c = new SqlConnection(database))
            {
                try
                {
                    c.Open();
                    string sql = "select Id, Name, Role, Email from Employees";
                    SqlCommand cmd = new SqlCommand(sql, c);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(result);
                }
                catch (SqlException)
                {
                }
                finally
                {
                    c.Close();
                }
            }
            return result;
        }

        public void updateAssigned(string userId, string ticketId)
        {
            DataTable result = new DataTable("TTickets");

            using (SqlConnection c = new SqlConnection(database))
            {
                try
                {
                    c.Open();
                    string sql = "Update TTickets SET State=@userId Where Id=@ticketId";

                    SqlCommand cmd = new SqlCommand(sql, c);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@ticketId", ticketId);
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                }
                finally
                {
                    c.Close();
                }
            }
        }
    }
}
