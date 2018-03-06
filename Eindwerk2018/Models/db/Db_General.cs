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
            con = new MySqlConnection(constr);
            using (con) //perhaps connection can be made once and reused?
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(qry))
                    {
                        cmd.Connection = con;
                        con.Open();
                        MySqlDataReader sdr = cmd.ExecuteReader();
                        con.Close();
                    }
                }
                catch (Exception e)
                {
                    //throw new System.InvalidOperationException("No connection to database");
                    Console.WriteLine("No connection to database" + e.Message); //should rethrow and handle it in the user part somwhere
                }
            }
        }

        //for return queries
        //private List<model> ListQueries(string qry) { }

        protected int MyConvertInt(String var)
        {
            int nummer = 0;
            if (var.Equals("")) nummer = 0; //int null give a problem
            else nummer = Convert.ToInt32(var);

            return nummer;
        }

        protected Double MyConvertDouble(String var)
        {
            Double nummer;
            if (var.Equals("")) nummer = 0; //int null give a problem
            else nummer = Convert.ToDouble(var);

            return nummer;
        }

        protected String MySqlDate(DateTime time)
        {
            return time.Year + "-" + time.Month + "-" + time.Day;
        }

        protected int GetLastInsertedId()
        {
            int lastId = 0;

            //if (con == null) con = new MySqlConnection(constr);
            con = new MySqlConnection(constr);
            using (con) //perhaps connection can be made once and reused?
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand("SELECT LAST_INSERT_ID() AS id"))
                    {
                        cmd.Connection = con;
                        con.Open();
                        MySqlDataReader sdr = cmd.ExecuteReader();
                        if (sdr.Read()) lastId = MyConvertInt(sdr["id"].ToString());
                        con.Close();
                    }
                }
                catch (Exception e)
                {
                    //throw new System.InvalidOperationException("No connection to database");
                    Console.WriteLine("No connection to database" + e.Message); //should rethrow and handle it in the user part somwhere
                }
            }

            return lastId;
        }

        protected int CountSelectedRows()
        {
            int rows = 0;

            //if (con == null) con = new MySqlConnection(constr);
            con = new MySqlConnection(constr); //gewoon opnieuw
            using (con)
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand("SELECT FOUND_ROWS() AS rows"))
                    {
                        cmd.Connection = con;
                        con.Open();
                        MySqlDataReader sdr = cmd.ExecuteReader();
                        rows = MyConvertInt(sdr["rows"].ToString());
                        con.Close();
                    }
                }
                catch (Exception e)
                {
                    //throw new System.InvalidOperationException("No connection to database");
                    Console.WriteLine("No connection to database" + e.Message); //should rethrow and handle it in the user part somwhere
                }
            }

            return rows;
        }
    }
}