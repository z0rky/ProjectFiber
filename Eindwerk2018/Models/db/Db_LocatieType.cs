using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models.db
{
    public class Db_LocatieType : Db_General
    {
        public List<LocatieType> List(int Start=0)
        {
            if (Start < 0) Start = 0;

            string query = "SELECT id, name_nl, name_fr, description_nl,description_fr FROM location_type LIMIT " + Start + ","+Max_row; //query

            return ListQueries(query);
        }

        public List<LocatieType> Search(string search)
        {
            if (search == null) return null;
            //zoekt zowel op name in NL en FR
            string query = "SELECT id, name_nl, name_fr, description_nl,description_fr FROM location_type WHERE name_nl LIKE '%" + search + "%' OR name_nl LIKE '%" + search + "%' LIMIT " + Max_row; //query

            return ListQueries(query);
        }

        public LocatieType Get(int id)
        {
            if (id == 0) return null;

            string query = "SELECT id, name_nl, name_fr, description_nl,description_fr FROM location_type WHERE id='"+ id +"' LIMIT 1"; //query

            return ListQueries(query)[0];
        }

        public int Add(LocatieType locatieType)
        {
            if (locatieType != null)
            {
                string query = "INSERT INTO location_type (name_nl, name_fr, description_nl,description_fr) VALUES ('" + locatieType.NaamNL + "','"+ locatieType.NaamFR + "','" + locatieType.DescNL + "','" + locatieType.DescFR +"')"; //query
                this.ShortQuery(query);
                return GetLastInsertedId(); //return new id
            }
            return 0;
        }

        public void Edit(LocatieType locatieType)
        {
            if (locatieType != null || locatieType.Id != 0)
            {
                string query = "UPDATE location_type SET name_nl='" + locatieType.NaamNL + "',name_fr='" + locatieType.NaamFR + "', description_nl='" + locatieType.DescNL + "', description_fr='" + locatieType.DescFR + "' WHERE id='" + locatieType.Id + "' LIMIT 1"; //query
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
        private List<LocatieType> ListQueries(string qry)
        {
            List<LocatieType> locatieTypes = new List<LocatieType>();

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
                                locatieTypes.Add(new LocatieType
                                {
                                    Id = Convert.ToInt32(sdr["id"]),
                                    NaamNL = sdr["name_nl"].ToString(),
                                    NaamFR = sdr["name_fr"].ToString(),
                                    DescNL = sdr["description_nl"].ToString(),
                                    DescFR = sdr["description_fr"].ToString()
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

            return locatieTypes;
        }
    }
}