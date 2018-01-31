using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models.db
{
    public class Db_SectieType : Db_General
    {
        public List<SectieType> List() { return List(0); }

        public List<SectieType> List(int Start)
        {
            if(Start < 0) Start = 0;

            string query = "SELECT id, name,description, virtual FROM section_type LIMIT " + Start+","+Max_row; //query

            return ListQueries(query);
        }

        public List<SectieType> Search(string search)
        {
            if (search == null) return null;
            string query = "SELECT id, name,description, virtual FROM section_type WHERE name LIKE '%" + search + "%' LIMIT " + Max_row; //query

            return ListQueries(query);
        }

        public SectieType Get(int id)
        {
            if (id == 0) return null;

            string query = "SELECT id, name,description,virtual FROM section_type WHERE id='" + id + "' LIMIT 1"; //query

            return ListQueries(query)[0];
        }

        public void Add(SectieType sectieType)
        {
            if (sectieType != null)
            {
                string query = "INSERT INTO section_type (name,description,virtual) VALUES ('" + sectieType.Naam + "','" + sectieType.Beschrijving+ "','" + sectieType.Virtueel+ "')"; //query
                this.ShortQuery(query);
            }
        }

        public void Edit(SectieType sectieType)
        {
            if (sectieType != null || sectieType.Id != 0)
            {
                string query = "UPDATE kabel SET name='" + sectieType.Naam + "', description='" + sectieType.Beschrijving + "', virtual'" + sectieType.Virtueel + "' WHERE id='" + sectieType.Id + "' LIMIT 1"; //query
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
        private List<SectieType> ListQueries(string qry)
        {
            List<SectieType> kabels = new List<SectieType>();

            using (MySqlConnection con = new MySqlConnection(constr)) //perhaps connection can be made once and reused?
            {
                using (MySqlCommand cmd = new MySqlCommand(qry))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            kabels.Add(new SectieType
                            {
                                Id = Convert.ToInt32(sdr["id"]),
                                Naam = sdr["name"].ToString(),
                                Beschrijving = sdr["description"].ToString(),
                                Virtueel = Convert.ToBoolean(sdr["virtual"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            return kabels;
        }
    }
}