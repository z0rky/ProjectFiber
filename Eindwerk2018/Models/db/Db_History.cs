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
        public List<History> List() { return List(0); }

        public List<History> List(int Start)
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
                string query = "INSERT INTO history (datum,table,id,user,text) VALUES ('" + history.CreatieDatum + "','" + history.Table + "','" + history.Id + "','" + history.User + "','" + history.Text + "')"; //query
                this.ShortQuery(query);
            }
        }

        //for return queries
        private List<History> ListQueries(string qry)
        {
            List<History> history = new List<History>();

            using (MySqlConnection con = new MySqlConnection(constr)) //perhaps connection can be made once and reused?
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

            return history;
        }
    }
}