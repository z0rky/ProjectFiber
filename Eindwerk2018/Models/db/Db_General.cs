using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models.db
{
    public class Db_General
    {
        //connection string "constr" staat in (root)/Web.config
        //en waarom niet hier? we gebruiken het toch niet voor entityFramework
        protected static string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        protected MySqlConnection con = new MySqlConnection(constr);
        protected const int Max_row = 100;

        //public List<var> List() { return List(0; }; //replaced now
        //public List<var> List(int Start=0) {}
        //public List<var> Search(string search) {}
        //public var Get(int id) {}
        //public void Add(model) {}
        //public void Edit(model) {}
        //public void Delete(int id) {}

        //short queries
        protected void ShortQuery(string qry)
        {
            using (con) //perhaps connection can be made once and reused?
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(qry))
                    {
                        cmd.Connection = con;
                        con.Open();
                        using (MySqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read()) { }  //nothing, perhaps not needed ?
                        }
                        con.Close();
                    }
                }
                catch (Exception e)
                {
                    //throw new System.InvalidOperationException("No connection to database");
                    Console.WriteLine("No connection to database"+e.Message); //should rethrow and handle it in the user part somwhere
                }
            }
        }

        //for return queries
        //private List<model> ListQueries(string qry) { }
    }
}