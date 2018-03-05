using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models.db
{
    public class Db_History : Db_General
    {
        public List<History> List(int Start=0)
        {
            if(Start < 0) Start = 0;

            string query = "SELECT datum,table,id,user,text FROM history LIMIT "+Start+","+Max_row; //query

            return ListQueries(query);
        }

        public List<History> Search(string table, int id)
        {
            if (table == null || id == 0) return null;
            string query = "SELECT datum,table,id,user,text FROM history WHERE table='"+table+"' AND id='"+id+"' ORDER BY datum DESC LIMIT " + Max_row; //query

            return ListQueries(query);
        }

        public void Add(History history)
        {
            if (history != null)
            {
                //SqlCommand com = new SqlCommand("SELECT lastName , phoneNo , creditCardNo , dateOfBirth  FROM UserExtendedDataSet WHERE UserId = @UserId", con);
                //com.Parameters.Add("@UserId", SqlDbType.NVarChar).Value = getUserId();


                string query = "INSERT INTO history (datum,table,id,user,text) VALUES ('" + MySqlDate(history.CreatieDatum) + "','" + history.Table + "','" + history.Id + "','" + history.User + "','" + history.Text + "')"; //query
                this.ShortQuery(query);
            }
        }

        //for return queries
        private List<History> ListQueries(string qry)
        {
            List<History> history = new List<History>();

            using (con) //con in Db_general
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(qry))
                    {
                        cmd.Connection = con;
                        con.Open();
                        using (MySqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                history.Add(new History
                                {
                                    CreatieDatum = Convert.ToDateTime(sdr["date_creation"]),
                                    Table = sdr["reference"].ToString(),
                                    Id = Convert.ToInt32(sdr["reference"]),
                                    User = Convert.ToInt32(sdr["reference"]),
                                    Text = sdr["reference"].ToString()
                                });
                            }
                        }
                        con.Close();
                    }
                }
                catch (Exception e)
                {
                    //throw new System.InvalidOperationException("No connection to database");
                    Console.WriteLine("No connection to database. " + e.Message); //should rethrow and handle it in the user part somewhere
                }
            }

            return history;
        }
    }
}