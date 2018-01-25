using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models.db
{
    public class Db_Locatie
    {
        //connection string "constr" staat in (root)/Web.config
        private string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        private const int Max_row = 100;

        public List<Locatie> List() { return List(0); }

        public List<Locatie> List(int Start)
        {
            List<Locatie> OdfTypes = new List<Locatie>();

            if (Start < 0) Start = 0;

            using (MySqlConnection con = new MySqlConnection(constr)) //perhaps connection can be made once and reused?
            {
                string query = "SELECT id, name, GPS_Longitude, GPS_Latidude,Lcode,infrabel_terein, location_type FROM location LIMIT " + Start + ","+Max_row; //query
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            OdfTypes.Add(new Locatie
                            {
                                Id = Convert.ToInt32(sdr["id"]),
                                LocatieNaam = sdr["name"].ToString(),
                                //GpsLong = Convert.ToDouble(sdr["GPS_Longitude"]), //covert problem ?, gaat punten en commas zijn
                                //GpsLat = Convert.ToDouble(sdr["GPS_Latidude"]),
                                LocatieInfrabel = Convert.ToBoolean(sdr["infrabel_terein"]),
                                LocatieTypeId = Convert.ToInt32(sdr["location_type"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            return OdfTypes;
        }

        public Locatie Get(int id)
        {
            if (id == 0) return null;

            using (MySqlConnection con = new MySqlConnection(constr)) //perhaps connection can be made once and reused?
            {
                string query = "SELECT id, name, GPS_Longitude, GPS_Latidude,Lcode,infrabel_terein, location_type FROM location WHERE id='" + id+"' LIMIT 1"; //query
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {   //only 1
                            return new Locatie
                            {
                                Id = Convert.ToInt32(sdr["id"]),
                                LocatieNaam = sdr["name"].ToString(),
                                GpsLong = Convert.ToInt32(sdr["GPS_Longitude"]),
                                GpsLat = Convert.ToInt32(sdr["GPS_Latidude"]),
                                LocatieInfrabel = Convert.ToBoolean(sdr["infrabel_terein"]),
                                LocatieTypeId = Convert.ToInt32(sdr["location_type"])
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