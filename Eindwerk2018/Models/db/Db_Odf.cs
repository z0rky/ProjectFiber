using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models.db
{
    public class Db_Odf
    {
        //connection string "constr" staat in (root)/Web.config
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        const int Max_row = 100;

        public List<Odf> List() { return List(0); }

        public List<Odf> List(int Start)
        {
            List<Odf> ODFs = new List<Odf>();

            if(Start < 0) Start = 0;

            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "SELECT id, locatie_id, type_id, name FROM ODF LIMIT "+Start+","+Max_row; //query
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            ODFs.Add(new Odf
                            {
                                Id = Convert.ToInt32(sdr["id"]),
                                Location_id = Convert.ToInt32(sdr["locatie_id"]),
                                Type_id = Convert.ToInt32(sdr["type_id"]),
                                Name = sdr["name"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return ODFs;
        }

        public Odf Get(int id)
        {
            if (id == 0) return null;

            using (MySqlConnection con = new MySqlConnection(constr)) //perhaps connection can be made once and reused?
            {
                string query = "SELECT id, name, description FROM ODF_type WHERE id='" + id + "' LIMIT 1"; //query
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {   //only 1
                            return new Odf
                            {
                                Id = Convert.ToInt32(sdr["id"]),
                                Name = sdr["name"].ToString()
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