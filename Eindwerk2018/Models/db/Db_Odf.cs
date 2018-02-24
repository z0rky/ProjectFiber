using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models.db
{
    public class Db_Odf : Db_General
    {
        public List<Odf> List(int Start=0)
        {
            if(Start < 0) Start = 0;

            string query = "SELECT id, location_id, type_id, name FROM ODF LIMIT "+Start+","+Max_row; //query

            return ListQueries(query);
        }

        public List<Odf> Search(string search)
        {
            if (search == null) return null;
            string query = "SELECT id, location_id, type_id, name FROM ODF WHERE name LIKE '%" + search + "%' LIMIT " + Max_row; //query

            return ListQueries(query);
        }

        public Odf Get(int id)
        {
            if (id == 0) return null;

            string query = "SELECT id, location_id, type_id, name FROM ODF WHERE id='" + id + "' LIMIT 1"; //query

            return ListQueries(query)[0];
        }

        public void Add(Odf odf)
        {
            if (odf != null)
            {
                string query = "INSERT INTO ODF (location_id, type_id, name) VALUES ('" + odf.Location_id + "','" + odf.Type_id + "','" + odf.Name + "')"; //query
                this.ShortQuery(query);
            }
        }

        public void Edit(Odf odf)
        {
            if (odf != null || odf.Id != 0)
            {
                string query = "UPDATE ODF SET name='" + odf.Name + "', location_id='" + odf.Location_id + "', type_id='" + odf.Type_id + "' WHERE id='" + odf.Id + "' LIMIT 1"; //query
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
        private List<Odf> ListQueries(string qry)
        {
            List<Odf> odfs = new List<Odf>();

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
                                odfs.Add(new Odf
                                {
                                    Id = Convert.ToInt32(sdr["id"]),
                                    Location_id = Convert.ToInt32(sdr["location_id"]),
                                    Type_id = Convert.ToInt32(sdr["type_id"]),
                                    Name = sdr["name"].ToString()
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

            return odfs;
        }
    }
}