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

            string query = "SELECT l.id, l.name, l.GPS_Longitude, l.GPS_Latidude,l.Lcode,l.infrabel_terein,l.location_type,lt.name_nl,l.lijnnr,l.bk,a.street,a.nr,a.postcode,a.city FROM location AS l LEFT JOIN adres AS a ON l.id=a.id, location_type AS lt WHERE l.location_type=lt.id LIMIT " + Start + ","+Max_row; //query

            return ListQueries(query);
        }

        public List<Locatie> SearchNaam(string search)
        {
            if (search == null) return null;
            string query = "SELECT l.id, l.name, l.GPS_Longitude, l.GPS_Latidude,l.Lcode,l.infrabel_terein,l.location_type,lt.name_nl,l.lijnnr,l.bk,a.street,a.nr,a.postcode,a.city FROM location AS l LEFT JOIN adres AS a ON l.id=a.id, location_type AS lt " +
                           "WHERE l.name LIKE '%" + search + "%' AND l.location_type=lt.id LIMIT " + Max_row; //query

            return ListQueries(query);
        }

        // query nog aanpassen
        public List<Locatie> SearchPostCode(int search)
        {
            if (search == 0) return null;
            string query = "SELECT l.id, l.name, l.GPS_Longitude, l.GPS_Latidude,l.Lcode,l.infrabel_terein,l.location_type,lt.name_nl,l.lijnnr,l.bk,a.street,a.nr,a.postcode,a.city FROM location AS l LEFT JOIN adres AS a ON l.id=a.id, location_type AS lt " +
                           "WHERE a.postcode LIKE '%" + search + "%' AND l.location_type=lt.id LIMIT " + Max_row; //query

            return ListQueries(query);
        }
        // query nog aanpassen
        public List<Locatie> SearchPlaats(string search)
        {
            if (search == null) return null;
            string query = "SELECT l.id, l.name, l.GPS_Longitude, l.GPS_Latidude,l.Lcode,l.infrabel_terein,l.location_type,lt.name_nl,l.lijnnr,l.bk,a.street,a.nr,a.postcode,a.city FROM location AS l LEFT JOIN adres AS a ON l.id=a.id, location_type AS lt  " +
                           "WHERE a.city LIKE '%" + search + "%' AND l.location_type=lt.id LIMIT " + Max_row; //query

            return ListQueries(query);
        }
        // query nog aanpassen
        public List<Locatie> SearchGPS(double search1, double search2)
        {
            if (search1 == 0 || search2 == 0) return null;
            string query = "SELECT l.id, l.name, l.GPS_Longitude, l.GPS_Latidude,l.Lcode,l.infrabel_terein,l.location_type,lt.name_nl,l.lijnnr,l.bk,a.street,a.nr,a.postcode,a.city FROM location AS l LEFT JOIN adres AS a ON l.id=a.id, location_type AS lt  " +
                           "WHERE l.GPS_Longitude BETWEEN '" + search1 + "%' AND '" + search2 + "%' OR l.GPS_Latidude BETWEEN '" + search1 + "%' AND '" + search2 + "%' AND l.location_type=lt.id LIMIT " + Max_row; //query

            return ListQueries(query);
        }


        /*check id name exists, except own id (when filled in)*/
        public Boolean CheckName(string search, int id = 0)
        {
            if (search == null) return true;
            string query = "SELECT l.id, l.name, l.GPS_Longitude, l.GPS_Latidude,l.Lcode,l.infrabel_terein,l.location_type,lt.name_nl,l.lijnnr,l.bk,a.street,a.nr,a.postcode,a.city FROM location AS l LEFT JOIN adres AS a ON l.id=a.id, location_type AS lt  WHERE l.name='" + search + "' AND l.location_type=lt.id";
            if (id > 0) query += " AND l.id!='" + id + "'";
            query += " LIMIT " + Max_row; //query

            if (ListQueries(query).Count() == 0) return false;
            return true;
        }

        public Locatie Get(int id)
        {
            if (id == 0) return null;

            string query = "SELECT l.id, l.name, l.GPS_Longitude, l.GPS_Latidude,l.Lcode,l.infrabel_terein,l.location_type,lt.name_nl,l.lijnnr,l.bk,a.street,a.nr,a.postcode,a.city FROM location AS l LEFT JOIN adres AS a ON l.id=a.id, location_type AS lt WHERE l.id='" + id +"' AND l.location_type=lt.id LIMIT 1"; //Including id to complete the normal class

            return ListQueries(query)[0];
        }

        public int Add(Locatie locatie)
        {
            if (locatie != null)
            {
                string query = "INSERT INTO location ( name, GPS_Longitude, GPS_Latidude,Lcode,infrabel_terein,location_type,lijnnr,bk) VALUES ('" + locatie.LocatieNaam + "','" + locatie.GpsLong + "','" + locatie.GpsLat + "','" + locatie.Lcode + "','" + locatie.LocatieInfrabel + "','" + locatie.LocatieTypeId + "','"+locatie.LijnNr+ "','" + locatie.BK + "')"; //query
                this.ShortQuery(query);
                int newId = GetLastInsertedId();
                //add address
                query = "INSERT INTO adres (id,street,nr,postcode,city) VALUES ('" + newId + "','" + locatie.Straat + "','" + locatie.HuisNr + "','" + locatie.PostCode + "','" + locatie.Plaats + "')"; //query
                this.ShortQuery(query);
                return newId; //return new id
            }
            return 0;
        }

        public void Edit(Locatie locatie)
        {
            if (locatie != null || locatie.Id != 0)
            {
                string query = "UPDATE location SET name='" + locatie.LocatieNaam + "', GPS_Longitude='" + locatie.GpsLong + "', GPS_Latidude='" + locatie.GpsLat + "',Lcode='"+ locatie.Lcode +"', infrabel_terein='" + locatie.LocatieInfrabel + "', location_type='" + locatie.LocatieTypeId + "' WHERE id='" + locatie.Id + "',lijnnr='" + locatie.LijnNr + "',l.bk='" + locatie.BK + "' LIMIT 1"; //query
                this.ShortQuery(query);
                //should also edit adres
                query = "UPDATE adres SET street='" + locatie.Straat + "', nr='" + locatie.HuisNr+ "', postcode='" + locatie.PostCode + "', city='" + locatie.Plaats+ "' WHERE id='" + locatie.Id + "' LIMIT 1";
                //better delete address and add again?
                this.ShortQuery(query);
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
                                    GpsLong = MyConvertDouble(sdr["GPS_Longitude"].ToString().Replace('.', ',')), //covert problem ?, gaat punten en commas zijn
                                    GpsLat = MyConvertDouble(sdr["GPS_Latidude"].ToString().Replace('.', ',')),
                                    LocatieInfrabel = Convert.ToBoolean(sdr["infrabel_terein"]),
                                    LocatieTypeId = Convert.ToInt32(sdr["location_type"]),
                                    LocatieTypeName = sdr["name_nl"].ToString(),
                                    Straat = sdr["street"].ToString(),
                                    HuisNr = sdr["nr"].ToString(),
                                    PostCode = MyConvertInt(sdr["postcode"].ToString()),
                                    Plaats = sdr["city"].ToString()
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