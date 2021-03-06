﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models.db
{
    public class Db_OdfType : Db_General
    {
        public List<OdfType> List(int Start=0)
        {
            if (Start < 0) Start = 0;

            string query = "SELECT id, name, description FROM ODF_type LIMIT " + Start + ","+Max_row; //query

            return ListQueries(query);
        }

        public List<OdfType> Search(string search)
        {
            if (search == null) return null;
            string query = "SELECT id, name, description FROM ODF_type WHERE name LIKE '%" + search + "%' LIMIT " + Max_row; //query

            return ListQueries(query);
        }

        /*check id name exists, except own id (when filled in)*/
        public Boolean CheckName(string search, int id = 0)
        {
            if (search == null) return true;
            string query = "SELECT id, name, description FROM ODF_type WHERE name='" + search + "'";
            if (id > 0) query += " AND id!='" + id + "'";
            query += " LIMIT " + Max_row; //query

            if (ListQueries(query).Count() == 0) return false;
            return true;
        }

        public OdfType Get(int id)
        {
            if (id == 0) return null;

            string query = "SELECT id, name, description FROM ODF_type WHERE id='"+id+"' LIMIT 1"; //query

            return ListQueries(query)[0]; //sjould be only 1
        }

        public int Add(OdfType odfType)
        {
            if (odfType != null)
            {
                string query = "INSERT INTO ODF_type (name,description) VALUES ('" + odfType.Name + "','" + odfType.Description + "')"; //query
                this.ShortQuery(query);
                return GetLastInsertedId(); //return new id
            }
            return 0;
        }

        public void Edit(OdfType odfType)
        {
            if (odfType != null || odfType.Id != 0)
            {
                string query = "UPDATE ODF_type SET name='" + odfType.Name + "', description='" + odfType.Description + "' WHERE id='" + odfType.Id + "' LIMIT 1"; //query
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
        private List<OdfType> ListQueries(string qry)
        {
            List<OdfType> OdfTypes = new List<OdfType>();

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
                catch (Exception e)
                {
                    //throw new System.InvalidOperationException("No connection to database");
                    Console.WriteLine("No connection to database. " + e.Message); //should rethrow and handle it in the user part somewhere
                }
            }

            return OdfTypes;
        }
    }
}