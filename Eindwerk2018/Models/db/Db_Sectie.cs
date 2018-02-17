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
        public List<Sectie> List(int Start=0)
        {
            if (Start < 0) Start = 0;

            string query = "SELECT s.id,s.section_nr,s.kabel_id,k.name AS kabel_name,s.odf_start,os.name AS odf_start_name,s.odf_end,oe.name AS odf_end_name,s.type_id,st.name AS type_name,s.length,s.active FROM sections AS s, kabel AS k, ODF as os, ODF as oe,section_type AS st WHERE s.kabel_id=k.id AND s.odf_start=os.id AND s.odf_end=oe.id AND s.type_id=st.id AND s.type_id=st.id ORDER BY s.kabel_id,s.section_nr LIMIT " + Start + ","+Max_row; //query

            return ListQueries(query);
        }

        public List<Sectie> Search(int kabelId)
        {  //enkel op sectie id, ook op kabel?
            if (kabelId == 0) return null;
            string query = "SELECT s.id,s.section_nr,s.kabel_id,k.name AS kabel_name,s.odf_start,os.name AS odf_start_name,s.odf_end,oe.name AS odf_end_name,s.type_id,st.name AS type_name,s.length,s.active FROM sections AS s, kabel AS k, ODF as os, ODF as oe,section_type AS st WHERE kabel_id ='" + kabelId + "' AND s.kabel_id=k.id AND s.odf_start=os.id AND s.odf_end=oe.id AND s.type_id=st.id ORDER BY s.kabel_id,s.section_nr LIMIT " + Max_row; //query

            return ListQueries(query);
        }

        public Sectie Get(int id)
        {
            if (id == 0) return null;

            string query = "SELECT s.id,s.section_nr,s.kabel_id,k.name AS kabel_name,s.odf_start,os.name AS odf_start_name,s.odf_end,oe.name AS odf_end_name,s.type_id,st.name AS type_name,s.length,s.active FROM sections AS s, kabel AS k, ODF as os, ODF as oe,section_type AS st WHERE s.id='" + id+ "' AND s.kabel_id=k.id AND s.odf_start=os.id AND s.odf_end=oe.id AND s.type_id=st.id LIMIT 1"; //query

            Sectie sectie = ListQueries(query)[0]; //should be only 1

            sectie.Fibers=GetFibers(id);
            return sectie; 
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
            List<Sectie> secties = new List<Sectie>();

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
                                secties.Add(new Sectie
                                {
                                    Id = Convert.ToInt32(sdr["id"]),
                                    SectieNr = Convert.ToInt32(sdr["section_nr"]),
                                    KabelId = Convert.ToInt32(sdr["kabel_id"]),
                                    KabelName = sdr["kabel_name"].ToString(),
                                    OdfStartId = Convert.ToInt32(sdr["odf_start"]),
                                    OdfStartName = sdr["odf_start_name"].ToString(),
                                    OdfEndId = Convert.ToInt32(sdr["odf_end"]),
                                    OdfEndName = sdr["odf_end_name"].ToString(),
                                    SectionTypeId = Convert.ToInt32(sdr["type_id"]),
                                    SectionTypeName = sdr["type_name"].ToString(),
                                    Lengte = Convert.ToInt32(sdr["length"]),
                                    Active = Convert.ToBoolean(sdr["active"])
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

            return secties;
        }

        public List<Fiber> GetFibers(int SectieId)
        {
            if (SectieId == 0) return null;
            con = new MySqlConnection(constr); //moet opnieuw worden ingesteld als het al is gebruikt
            List<Fiber> fibers = new List<Fiber>();
            //color info, is in type info
            string qry = "SELECT f.fiber_nr,st.order_nr AS order_nr,fc.id AS fiber_color_id,fc.name_en AS fiber_color_name_en,st.module_nr,fm.id AS module_color_id,fm.name_en AS module_color_name_en,f.quality FROM fibers AS f,sections AS s,section_type_info AS st,fiber_color AS fc,fiber_color AS fm WHERE f.section_id='" + SectieId +"' AND f.fiber_nr=st.fiber_nr AND f.section_id=s.id AND s.type_id=st.type_id AND st.fiber_color_id=fc.id AND st.module_color_id=fm.id ORDER BY st.order_nr";

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
                                fibers.Add(new Fiber
                                {
                                    OrderNr = Convert.ToInt32(sdr["order_nr"]),
                                    FiberNr = Convert.ToInt32(sdr["fiber_nr"]),
                                    FiberColor = new Color { Id= Convert.ToInt32(sdr["fiber_color_id"]), NameEn = sdr["fiber_color_name_en"].ToString() },
                                    ModuleNr = Convert.ToInt32(sdr["module_nr"]),
                                    ModuleColor = new Color { Id = Convert.ToInt32(sdr["module_color_id"]), NameEn = sdr["module_color_name_en"].ToString() },
                                    Quality = sdr["quality"].ToString()
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

            return fibers;
        }
    }
}