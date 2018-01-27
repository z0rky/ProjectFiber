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
        protected string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        protected const int Max_row = 100;

        //public List<var> List() { return List(0; };
        //public List<var> List(int Start) {}
        //public List<var> Search(string search) {}
        //public var Get(int id) {}
        //public void Add(model) {}
        //public void Edit(model) {}
        //public void Delete(int id) {}

        //short queries
        protected void ShortQuery(string qry)
        {
            using (MySqlConnection con = new MySqlConnection(constr)) //perhaps connection can be made once and reused?
            {
                using (MySqlCommand cmd = new MySqlCommand(qry))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read()) { }  //nothing
                    }
                    con.Close();
                }
            }
        }

        //for return queries
        //private List<model> ListQueries(string qry) { }
    }
}