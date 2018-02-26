using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/*Moet nog aangepast worden*/

namespace Eindwerk2018.Models.db
{
    public class Db_KabelType : Db_General
    {
        public List<KabelType> List(int Start=0)
        {
            if(Start < 0) Start = 0;

            string query = "SELECT id, name_nl,name_fr FROM kabel_type LIMIT "+Start+","+Max_row; //query

            return ListQueries(query);
        }

        public List<KabelType> Search(string search)
        {
            if (search == null) return null;
            string query = "SELECT  id, name_nl,name_fr FROM kabel_type WHERE name_nl LIKE '%" + search + "%' OR name_fr LIKE '%" + search + "%'  LIMIT " + Max_row; //query

            return ListQueries(query);
        }

        public KabelType Get(int id)
        {
            if (id == 0) return null;

            string query = "SELECT id, name_nl,name_fr FROM kabel_type WHERE id='" + id + "' LIMIT 1"; //query

            return ListQueries(query)[0];
        }

        public void Add(KabelType kabelType)
        {
            if (kabelType != null)
            {
                string query = "INSERT INTO kabel_type (name_nl,name_fr) VALUES ('" + kabelType.NameNL + "','" + kabelType.NameFR + "')"; //query
                this.ShortQuery(query);
            }
        }

        public void Edit(KabelType kabelType)
        {
            if (kabelType != null || kabelType.Id != 0)
            {
                string query = "UPDATE kabel_type SET name_nl='" + kabelType.NameNL + "',name_fr='" + kabelType.NameFR + "' WHERE id='" + kabelType.Id + "' LIMIT 1"; //query
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
        private List<KabelType> ListQueries(string qry)
        {
            List<KabelType> kabelTypes = new List<KabelType>();

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
                                kabelTypes.Add(new KabelType
                                {
                                    Id = Convert.ToInt32(sdr["id"]),
                                    NameNL = sdr["name_nl"].ToString(),
                                    NameFR = sdr["name_fr"].ToString()
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

            return kabelTypes;
        }
    }
}