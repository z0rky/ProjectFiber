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
            string query = "SELECT s.id,s.section_nr,s.kabel_id,k.name AS kabel_name,s.odf_start,os.name AS odf_start_name,s.odf_end,oe.name AS odf_end_name,s.type_id,st.name AS type_name,s.length,s.active FROM sections AS s, kabel AS k, ODF as os, ODF as oe,section_type AS st WHERE s.kabel_id ='" + kabelId + "' AND s.kabel_id=k.id AND s.odf_start=os.id AND s.odf_end=oe.id AND s.type_id=st.id ORDER BY s.kabel_id,s.section_nr LIMIT " + Max_row; //query

            return ListQueries(query);
        }

        public List<Sectie> SearchOdf(int odfId)
        { //might have to expand it to location of odfId
            if (odfId == 0) return null;
            string query = "SELECT s.id,s.section_nr,s.kabel_id,k.name AS kabel_name,s.odf_start,os.name AS odf_start_name,s.odf_end,oe.name AS odf_end_name,s.type_id,st.name AS type_name,s.length,s.active FROM sections AS s, kabel AS k, ODF as os, ODF as oe,section_type AS st WHERE (s.odf_start ='" + odfId + "' OR s.odf_end ='" + odfId + "') AND s.kabel_id=k.id AND s.odf_start=os.id AND s.odf_end=oe.id AND s.type_id=st.id ORDER BY s.kabel_id,s.section_nr LIMIT " + Max_row; //query

            return ListQueries(query);
        }

        public Sectie Get(int id)
        {
            if (id == 0) return null;

            string query = "SELECT s.id,s.section_nr,s.kabel_id,k.name AS kabel_name,s.odf_start,os.name AS odf_start_name,s.odf_end,oe.name AS odf_end_name,s.type_id,st.name AS type_name,s.length,s.active FROM sections AS s, kabel AS k, ODF as os, ODF as oe,section_type AS st WHERE s.id='" + id+ "' AND s.kabel_id=k.id AND s.odf_start=os.id AND s.odf_end=oe.id AND s.type_id=st.id LIMIT 1"; //query

            Sectie sectie = new Sectie();
            try
            {
                sectie = ListQueries(query)[0]; //should be only 1
                sectie.Fibers = GetFibers(id);
            }
            catch (Exception e) { ; }

            return sectie; 
        }

        public Sectie GetLastSection(int kabelId)
        {
            if (kabelId != 0)
            {
                string query = "SELECT s.id,s.section_nr,s.kabel_id,k.name AS kabel_name,s.odf_start,os.name AS odf_start_name,s.odf_end,oe.name AS odf_end_name,s.type_id,st.name AS type_name,s.length,s.active FROM sections AS s, kabel AS k, ODF as os, ODF as oe,section_type AS st WHERE kabel_id='" + kabelId + "' AND s.kabel_id=k.id AND s.odf_start=os.id AND s.odf_end=oe.id AND s.type_id=st.id AND section_nr=( SELECT MAX(section_nr) FROM sections WHERE kabel_id='" + kabelId + "')"; //query
                try { return ListQueries(query)[0]; }
                catch (Exception e) { }
            }
            return null;
        }

        public int Add(Sectie sectie)
        {
            if (sectie != null)
            {
                string query = "INSERT INTO sections (section_nr,kabel_id,odf_start,odf_end,type_id,length,active) VALUES ('" + sectie.SectieNr + "','" + sectie.KabelId + "','" + sectie.OdfStartId + "','" + sectie.OdfEndId+ "','" + sectie.SectionTypeId + "','" + sectie.Lengte + "','" + sectie.Active+ "')"; //query
                this.ShortQuery(query);
                int newId = GetLastInsertedId(); //return new id
                query = "INSERT INTO fibers (section_id,fiber_nr) SELECT '"+ newId + "',fiber_nr FROM section_type_info WHERE type_id='"+sectie.SectionTypeId+"'"; //others are default fields,
                this.ShortQuery(query);
                return newId;
            }
            return 0;
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
            con = new MySqlConnection(constr); //moet opnieuw worden ingesteld als het al is gebruikt
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
                                    Id = MyConvertInt(sdr["id"].ToString()),
                                    SectieNr = MyConvertInt(sdr["section_nr"].ToString()),
                                    KabelId = MyConvertInt(sdr["kabel_id"].ToString()),
                                    KabelName = sdr["kabel_name"].ToString(),
                                    OdfStartId = MyConvertInt(sdr["odf_start"].ToString()),
                                    OdfStartName = sdr["odf_start_name"].ToString(),
                                    OdfEndId = MyConvertInt(sdr["odf_end"].ToString()),
                                    OdfEndName = sdr["odf_end_name"].ToString(),
                                    SectionTypeId = MyConvertInt(sdr["type_id"].ToString()),
                                    SectionTypeName = sdr["type_name"].ToString(),
                                    Lengte = MyConvertInt(sdr["length"].ToString()),
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
            string qry = "SELECT f.Foid,fo.name AS foid_name,f.fiber_nr,st.order_nr AS order_nr,fc.id AS fiber_color_id,fc.name_en AS fiber_color_name_en,st.module_nr,fm.id AS module_color_id,fm.name_en AS module_color_name_en,f.quality FROM fibers AS f LEFT JOIN FOID AS fo ON f.FOID=fo.id,sections AS s,section_type_info AS st,fiber_color AS fc,fiber_color AS fm WHERE f.section_id='" + SectieId +"' AND f.fiber_nr=st.fiber_nr AND f.section_id=s.id AND s.type_id=st.type_id AND st.fiber_color_id=fc.id AND st.module_color_id=fm.id ORDER BY st.order_nr";

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
                                    Foid = MyConvertInt(sdr["Foid"].ToString()),
                                    FoidName = sdr["foid_name"].ToString(),
                                    OrderNr = MyConvertInt(sdr["order_nr"].ToString()),
                                    FiberNr = MyConvertInt(sdr["fiber_nr"].ToString()),
                                    FiberColor = new Color { Id= MyConvertInt(sdr["fiber_color_id"].ToString()), NameEn = sdr["fiber_color_name_en"].ToString() },
                                    ModuleNr = MyConvertInt(sdr["module_nr"].ToString()),
                                    ModuleColor = new Color { Id = MyConvertInt(sdr["module_color_id"].ToString()), NameEn = sdr["module_color_name_en"].ToString() },
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