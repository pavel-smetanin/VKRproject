using Microsoft.AspNetCore.Http;
using MySqlX.XDevAPI.Common;
using System.Reflection.PortableExecutable;
using VKRproject.Models;
using VKRproject.Tools;

namespace VKRproject.Modules
{
    public class ClientSearchModule
    {
        private string tabClients = "clients_archive";
        public List<Client> SearchClient(string lastname, string firstname = "", string patrname = "", string birthDate = "0000.00.00")
        {
            List<Client> results = new List<Client>();
            string sql = $"SELECT * FROM {tabClients} WHERE last_name = '{lastname}' OR first_name = '{firstname}' OR patr_name = '{patrname}' OR birth_date = '{birthDate}';";
            results = GetClientsListFromDb(sql);
            return results;
        }
        public List<Client> SearchClient(int id)
        {
            List<Client> results = new List<Client>();
            string sql = $"SELECT * FROM {tabClients} WHERE ID = {id};";
            results = GetClientsListFromDb(sql);
            return results;
        }
        public List<Client> SearchClient(string email, string phone)
        {
            List<Client> results = new List<Client>();
            string sql = $"SELECT * FROM {tabClients} WHERE email = '{email}' OR phone = '{phone}';";
            results = GetClientsListFromDb(sql);
            return results;
        }
        public List<TourStat> GetTourStatsByClientId(int clientId)
        {
            List<TourStat> results = new List<TourStat>();
            string sql = $"SELECT t.ID, t.country_id, t.nights_count, t.adults_count, t.child_count, t.price, o.client_rate " +
                $"FROM tours_archive t JOIN orders_archive o ON t.ID = o.tour_id WHERE o.client_id = {clientId};";
            DbTool.OpenDbConnection();
            var reader = DbTool.ExcecuteQueryWithResult(sql);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TourStat tourStat = new TourStat();
                    tourStat.TourId = reader[0].ToString();
                    tourStat.CountryId = Int32.Parse(reader[1].ToString());
                    tourStat.NightsCount = Int32.Parse(reader[2].ToString());
                    tourStat.AdultsCount = Int32.Parse(reader[3].ToString());
                    tourStat.ChildCount = Int32.Parse(reader[4].ToString());
                    tourStat.Price = Int32.Parse(reader[5].ToString());
                    tourStat.Rate = float.Parse(reader[6].ToString());
                    results.Add(tourStat);
                }
            }
            reader.Close();
            DbTool.CloseDbConnection();
            return results;
        }
        private List<Client> GetClientsListFromDb(string sql)
        {
            List<Client> results = new List<Client>();
            DbTool.OpenDbConnection();
            var reader = DbTool.ExcecuteQueryWithResult(sql);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Client client = new Client();
                    client.ID = Int32.Parse(reader[0].ToString());
                    client.LastName = reader[1].ToString();
                    client.FirstName = reader[2].ToString();
                    client.PatrName = reader[3].ToString();
                    client.BirthDate = DateOnly.FromDateTime(DateTime.Parse(reader[4].ToString()));
                    client.Email = reader[5].ToString();
                    client.Phone = reader[6].ToString();
                    results.Add(client);
                }
            }
            reader.Close();
            DbTool.CloseDbConnection();
            return results;
        }
    }
}
