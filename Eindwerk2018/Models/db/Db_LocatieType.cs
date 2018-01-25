using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models.db
{
    public class Db_LocatieType
    {
        //connection string "constr" staat in (root)/Web.config
        private string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        private const int Max_row = 100;

        public List<LocatieType> List() { return List(0); }

        public List<LocatieType> List(int Start)
        {
            List<LocatieType> locatieTypeList = new List<LocatieType>();

            if (Start < 0) Start = 0;

            using (MySqlConnection con = new MySqlConnection(constr)) //perhaps connection can be made once and reused?
            {
                string query = "SELECT id, name_nl, name_fr, desription_nl,desription_fr FROM location_type LIMIT " + Start + ","+Max_row; //query
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            locatieTypeList.Add(new LocatieType
                            {
                                Id = Convert.ToInt32(sdr["id"]),
                                NaamNL = sdr["name_nl"].ToString(),
                                NaamFR = sdr["name_fr"].ToString(),
                                DescNL = sdr["desription_nl"].ToString(),
                                DescFR = sdr["desription_fr"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return locatieTypeList;
        }

        public LocatieType Get(int id)
        {
            if (id == 0) return null;

            using (MySqlConnection con = new MySqlConnection(constr)) //perhaps connection can be made once and reused?
            {
                string query = "SELECT id, name_nl, name_fr, desription_nl,desription_fr FROM location_type WHERE id='" + id+"' LIMIT 1"; //query
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {   //only 1
                            return new LocatieType
                            {
                                Id = Convert.ToInt32(sdr["id"]),
                                NaamNL = sdr["name_nl"].ToString(),
                                NaamFR = sdr["name_fr"].ToString(),
                                DescNL = sdr["desription_nl"].ToString(),
                                DescFR = sdr["desription_fr"].ToString()
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