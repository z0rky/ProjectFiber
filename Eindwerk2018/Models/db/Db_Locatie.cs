using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models.db
{
    public class Db_Locatie : Db_General
    {
        public List<Locatie> List(int Start=0)
        {
            if (Start < 0) Start = 0;

            string query = "SELECT id, name, GPS_Longitude, GPS_Latidude,Lcode,infrabel_terein, location_type FROM location LIMIT " + Start + ","+Max_row; //query

            return ListQueries(query);
        }

        public List<Locatie> Search(string search)
        {
            if (search == null) return null;
            string query = "SELECT id, name, GPS_Longitude, GPS_Latidude,Lcode,infrabel_terein, location_type FROM location WHERE name LIKE '%" + search + "%' LIMIT " + Max_row; //query

            return ListQueries(query);
        }

        public Locatie Get(int id)
        {
            if (id == 0) return null;

            string query = "SELECT id, name, GPS_Longitude, GPS_Latidude,Lcode,infrabel_terein, location_type FROM location WHERE id='" + id+"' LIMIT 1"; //Including id to complete the normal class

            return ListQueries(query)[0];
        }

        public void Add(Locatie locatie)
        {
            if (locatie != null)
            {  //voorlopig nog geen auto-create id, dus nog geen add, zal het vanavond aanpassen
                string query = "INSERT INTO location ( name, GPS_Longitude, GPS_Latidude,Lcode,infrabel_terein, location_type) VALUES ('" + locatie.LocatieNaam + "','" + locatie.GpsLong + "','" + locatie.GpsLat + "','Null','" + locatie.LocatieInfrabel + "','" + locatie.LocatieTypeId + "',)"; //query
                this.ShortQuery(query);
                //should also add adres
            }
        }

        public void Edit(Locatie locatie)
        {
            if (locatie != null || locatie.Id != 0)
            {
                string query = "UPDATE location SET name='" + locatie.LocatieNaam + "', GPS_Longitude='" + locatie.GpsLong + "', GPS_Latidude='" + locatie.GpsLat + "',infrabel_terein='" + locatie.LocatieInfrabel + "', location_type='" + locatie.LocatieTypeId + "' WHERE id='" + locatie.Id + "' LIMIT 1"; //query
                this.ShortQuery(query);
                //should also edit adres
            }
        }

        public void Delete(int id)
        {
            if (id != 0)
            {
                //delete, set bit ?
            }
        }


        //for return queries
        private List<Locatie> ListQueries(string qry)
        {
            List<Locatie> locaties = new List<Locatie>();

            using (con) //con in Db_general
            {
                using (MySqlCommand cmd = new MySqlCommand(qry))
                {
                    try
                    {
                        cmd.Connection = con;
                        con.Open();
                        using (MySqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                locaties.Add(new Locatie
                                {
                                    Id = Convert.ToInt32(sdr["id"]),
                                    LocatieNaam = sdr["name"].ToString(),
                                    GpsLong = Convert.ToDouble(sdr["GPS_Longitude"].ToString().Replace('.',',')), //covert problem ?, gaat punten en commas zijn
                                    GpsLat = Convert.ToDouble(sdr["GPS_Latidude"].ToString().Replace('.',',')),
                                    LocatieInfrabel = Convert.ToBoolean(sdr["infrabel_terein"]),
                                    LocatieTypeId = Convert.ToInt32(sdr["location_type"])
                                });
                            }
                        }
                        con.Close();
                    }
                    catch (Exception e)
                    {
                        //throw new System.InvalidOperationException("No connection to database");
                        Console.WriteLine("No connection to database. "+e.Message); //should rethrow and handle it in the user part somewhere
                    }
                }
            }

            return locaties;
        }
    }
}