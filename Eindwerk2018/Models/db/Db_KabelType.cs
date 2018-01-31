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
        public List<Kabel> List() { return List(0); }

        public List<Kabel> List(int Start)
        {
            if(Start < 0) Start = 0;

            string query = "SELECT id, name_nl,name_fr FROM kabel_type LIMIT "+Start+","+Max_row; //query

            return ListQueries(query);
        }

        public List<Kabel> Search(string search)
        {
            if (search == null) return null;
            string query = "SELECT  id, name_nl,name_fr FROM kabel_type WHERE name_nl LIKE '%" + search + "%' OR name_fr LIKE '%" + search + "%'  LIMIT " + Max_row; //query

            return ListQueries(query);
        }

        public Kabel Get(int id)
        {
            if (id == 0) return null;

            string query = "SELECT id, name_nl,name_fr FROM kabel_type WHERE id='" + id + "' LIMIT 1"; //query

            return ListQueries(query)[0];
        }

        public void Add(Kabel kabelType)
        {
            if (kabelType != null)
            {
                string query = "INSERT INTO kabel_type (name_nl,name_fr) VALUES ('" + kabelType.Naam + "','" + kabelType.Naam + "')"; //query
                this.ShortQuery(query);
            }
        }

        public void Edit(Kabel kabelType)
        {
            if (kabelType != null || kabelType.Id != 0)
            {
                string query = "UPDATE kabel SET name_nl='" + kabelType.Naam + "',name_fr='" + kabelType.Naam + "' WHERE id='" + kabelType.Id + "' LIMIT 1"; //query
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
        private List<Kabel> ListQueries(string qry)
        {
            List<Kabel> kabelTypes = new List<Kabel>();

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
                            kabelTypes.Add(new Kabel
                            {
                                Id = Convert.ToInt32(sdr["id"])
                                //NaamNL = sdr["name_nl"].ToString(),
                                //NaamFR = sdr["name_fr"].ToString(),
                            });
                        }
                    }
                    con.Close();
                }
            }

            return kabelTypes;
        }
    }
}