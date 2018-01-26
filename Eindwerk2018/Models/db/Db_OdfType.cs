using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models.db
{
    public class Db_OdfType : Db_General
    {
        public List<OdfType> List() { return List(0); }

        public List<OdfType> List(int Start)
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

        public OdfType Get(int id)
        {
            if (id == 0) return null;

            string query = "SELECT id, name, description FROM ODF_type WHERE id='"+id+"' LIMIT 1"; //query

            return ListQueries(query)[0]; //sjould be only 1
        }

        public void Add(OdfType odfType)
        {
            if (odfType != null)
            {
                string query = "INSERT INTO ODF_type (name,description) VALUES ('" + odfType.Name + "','" + odfType.Description + "')"; //query
                this.ShortQuery(query);
            }
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

            return OdfTypes;
        }
    }
}