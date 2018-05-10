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

            string query = "SELECT o.id, o.location_id, l.name AS locatie_name, o.type_id, ot.name AS type_name, o.name FROM ODF AS o, ODF_type AS ot, location AS l WHERE o.type_id=ot.id AND o.location_id=l.id ORDER BY o.id DESC LIMIT " + Start+","+Max_row; //query

            return ListQueries(query);
        }

        public List<Odf> Search(string search)
        {
            if (search == null) return null;
            string query = "SELECT o.id, o.location_id, l.name AS locatie_name, o.type_id, ot.name AS type_name, o.name FROM ODF AS o, ODF_type AS ot, location AS l WHERE o.name LIKE '%" + search + "%' AND o.type_id=ot.id AND o.location_id=l.id LIMIT " + Max_row; //query

            return ListQueries(query);
        }

        public List<Odf> Search(int odfId) //Search for connected Odf's (via section), and location?
        {
            if (odfId == 0) return null;
            /*string query = "SELECT o.id, o.location_id,'' AS locatie_name, o.type_id, '' AS type_name, o.name FROM ODF AS o,sections AS s WHERE s.odf_start='" + odfId + "' AND s.odf_end=o.id " +
                " UNION SELECT o.id, o.location_id,'' AS locatie_name, o.type_id, '' AS type_name, o.name FROM ODF AS o,sections AS s WHERE s.odf_end='" + odfId + "' AND s.odf_start=o.id " +
                "LIMIT " + Max_row; //only kabels of odf
            */
            //query that includes location
            string query = "SELECT o.id, o.location_id, o.type_id, o.name FROM ODF AS o,sections AS s WHERE s.odf_end IN(SELECT ob.id FROM ODF AS oa, ODF AS ob WHERE oa.id= '27843' AND oa.location_id= ob.location_id) AND s.odf_start = o.id"+
                " UNION SELECT o.id, o.location_id, o.type_id, o.name FROM ODF AS o,sections AS s WHERE s.odf_end IN(SELECT ob.id FROM ODF AS oa, ODF AS ob WHERE oa.id= '27843' AND oa.location_id= ob.location_id) AND s.odf_start = o.id"+
                " LIMIT " + Max_row;

            return ListQueries(query);
        }

        public Boolean CheckName(string search, int id=0)
        {
            if (search == null) return true;
            string query = "SELECT o.id, o.location_id, l.name AS locatie_name, o.type_id, ot.name AS type_name, o.name FROM ODF AS o, ODF_type AS ot, location AS l WHERE o.name = '" + search + "'";
            if (id > 0) query += " AND o.id!='"+id+"'";
            query += " AND o.type_id=ot.id AND o.location_id=l.id LIMIT " + Max_row; //query

            if (ListQueries(query).Count() == 0) return false;
            return true;
        }

        public Odf Get(int id)
        {
            if (id == 0) return null;

            string query = "SELECT o.id, o.location_id, l.name AS locatie_name, o.type_id, ot.name AS type_name, o.name FROM ODF AS o, ODF_type AS ot, location AS l WHERE o.id='" + id + "' AND o.type_id=ot.id AND o.location_id=l.id LIMIT 1"; //query

            return ListQueries(query)[0];
        }

        public int Add(Odf odf)
        {
            if (odf != null && odf.Name != null)
            {
                string query = "INSERT INTO ODF (location_id, type_id, name) VALUES ('" + odf.Location.Id + "','" + odf.OdfType.Id + "','" + odf.Name + "')"; //query
                this.ShortQuery(query);
                return GetLastInsertedId(); //return new id
            }
            return 0;
        }

        public void Edit(Odf odf)
        {
            if (odf != null || odf.Id != 0)
            {
                string query = "UPDATE ODF SET name='" + odf.Name + "', location_id='" + odf.Location.Id + "', type_id='" + odf.OdfType.Id + "' WHERE id='" + odf.Id + "' LIMIT 1"; //query
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
                                    Location = new Locatie{ Id = Convert.ToInt32(sdr["location_id"]), LocatieNaam = sdr["locatie_name"].ToString() },
                                    OdfType = new OdfType { Id = Convert.ToInt32(sdr["type_id"]), Name = sdr["type_name"].ToString() },
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