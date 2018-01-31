using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models.db
{
    public class Db_Color: Db_General
    {
        public List<Color> List() { return List(0); }

        public List<Color> List(int Start)
        {
            if(Start < 0) Start = 0;

            string query = "SELECT id, name_en, name_nl,name_fr FROM fiber_color LIMIT "+Start+","+Max_row; //query

            return ListQueries(query);
        }

        public List<Color> Search(string search)
        {
            if (search == null) return null;
            string query = "SELECT id, name_en, name_nl,name_fr FROM fiber_color WHERE name_en LIKE '%" + search + "%' OR name_nl LIKE '%" + search + "%' OR name_fr LIKE '%" + search + "%' LIMIT " + Max_row; //query

            return ListQueries(query);
        }

        public Color Get(int id)
        {
            if (id == 0) return null;

            string query = "SELECT id,name_en, name_nl,name_fr FROM fiber_color WHERE id='" + id + "' LIMIT 1"; //query

            return ListQueries(query)[0];
        }

        public void Add(Color color)
        {
            if (color != null)
            {
                string query = "INSERT INTO fiber_color (name_en,name_nl,name_fr) VALUES ('" + color.NameEn + "','" + color.NameNl + "','" + color.NameFr + "')"; //query
                this.ShortQuery(query);
            }
        }

        public void Edit(Color color)
        {
            if (color != null || color.Id != 0)
            {
                string query = "UPDATE fiber_color SET name_en='" + color.NameEn + "',  name_nl='" + color.NameNl + "',  name_fr='" + color.NameFr + "' WHERE id='" + color.Id + "' LIMIT 1"; //query
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
        private List<Color> ListQueries(string qry)
        {
            List<Color> colors = new List<Color>();

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
                            colors.Add(new Color
                            {
                                Id = Convert.ToInt32(sdr["id"]),
                                NameEn = sdr["name_en"].ToString(),
                                NameNl = sdr["name_nl"].ToString(),
                                NameFr = sdr["name_fr"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return colors;
        }
    }
}