using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models.db
{
    public class Db_Sectie : Db_General
    {
        public List<Sectie> List() { return List(0); }

        public List<Sectie> List(int Start)
        {
            if (Start < 0) Start = 0;

            string query = "SELECT id,section_nr,kabel_id,odf_start,odf_end,type_id,length,active FROM sections LIMIT " + Start + ","+Max_row; //query

            return ListQueries(query);
        }

        public List<Sectie> Search(string search)
        {  //enkel op sectie id, ook op kabel?
            if (search == null) return null;
            string query = "SELECT id,section_nr,kabel_id,odf_start,odf_end,type_id,length,active FROM sections WHERE section_nr ='" + search + "' LIMIT " + Max_row; //query

            return ListQueries(query);
        }

        public Sectie Get(int id)
        {
            if (id == 0) return null;

            string query = "SELECT id,section_nr,kabel_id,odf_start,odf_end,type_id,length,active FROM sections WHERE id='" + id+"' LIMIT 1"; //query

            return ListQueries(query)[0]; //sjould be only 1
        }

        public void Add(Sectie sectie)
        {
            if (sectie != null)
            {
                string query = "INSERT INTO sections (section_nr,kabel_id,odf_start,odf_end,type_id,length,active) VALUES ('" + sectie.SectieNr + "','" + sectie.KabelId + "','" + sectie.OdfStartId + "','" + sectie.OdfEndId+ "','" + sectie.SectionTypeId + "','" + sectie.Lengte + "','" + sectie.Active+ "')"; //query
                this.ShortQuery(query);
            }
        }

        public void Edit(Sectie sectie)
        {
            if (sectie != null || sectie.Id != 0)
            {
                string query = "UPDATE sections SET section_nr='" + sectie.SectieNr + "', kabel_id='" + sectie.KabelId+ "', odf_start='" + sectie.OdfStartId  + "', odf_end='" + sectie.OdfEndId + "', type_id='" + sectie.SectionTypeId + "', length='" + sectie.Lengte + "', active='" + sectie.Active + "' WHERE id='" + sectie.Id + "' LIMIT 1"; //query
                this.ShortQuery(query);
            }
        }

        public void Deactivate(int id)
        {
            if (id != 0)
            {
                string query = "UPDATE sections SET active='false' WHERE id='" + id + "' LIMIT 1"; //query
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
        private List<Sectie> ListQueries(string qry)
        {
            List<Sectie> OdfTypes = new List<Sectie>();

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
                            OdfTypes.Add(new Sectie
                            {
                                Id = Convert.ToInt32(sdr["id"]),
                                SectieNr = Convert.ToInt32(sdr["section_nr"]),
                                KabelId = Convert.ToInt32(sdr["kabel_id"]),
                                OdfStartId = Convert.ToInt32(sdr["odf_start"]),
                                OdfEndId = Convert.ToInt32(sdr["odf_end"]),
                                SectionTypeId = Convert.ToInt32(sdr["type_id"]),
                                Lengte = Convert.ToInt32(sdr["length"]),
                                Active = Convert.ToBoolean(sdr["active"])
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