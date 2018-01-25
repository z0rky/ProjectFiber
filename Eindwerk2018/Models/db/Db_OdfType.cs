using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models.db
{
    public class Db_OdfType
    {
        //connection string "constr" staat in (root)/Web.config
        private string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        private const int Max_row = 100;

        public List<OdfType> List() { return List(0); }

        public List<OdfType> List(int Start)
        {
            List<OdfType> OdfTypes = new List<OdfType>();

            if (Start < 0) Start = 0;

            using (MySqlConnection con = new MySqlConnection(constr)) //perhaps connection can be made once and reused?
            {
                string query = "SELECT id, name, description FROM ODF_type LIMIT " + Start + ","+Max_row; //query
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            OdfTypes.Add(new OdfType
                            {
                                Id = Convert.ToInt32(sdr["id"]),
                                Name = sdr["name"].ToString(),
                                Description = sdr["description"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return OdfTypes;
        }

        public OdfType Get(int id)
        {
            if (id == 0) return null;

            using (MySqlConnection con = new MySqlConnection(constr)) //perhaps connection can be made once and reused?
            {
                string query = "SELECT id, name, description FROM ODF_type WHERE id='"+id+"' LIMIT 1"; //query
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {   //only 1
                            return new OdfType
                            {
                                Id = Convert.ToInt32(sdr["id"]),
                                Name = sdr["name"].ToString(),
                                Description = sdr["description"].ToString()
                            };
                        }
                    }
                    con.Close();
                }
            }

            return null;
        }
    }
}