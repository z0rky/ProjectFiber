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
        public List<SectieType> List(int Start=0)
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
            SectieType sectieType= ListQueries(query)[0];
            sectieType.Fibers = GetFibers(id);

            return sectieType;
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
                string query = "UPDATE section_type SET name='" + sectieType.Naam + "', description='" + sectieType.Beschrijving + "', virtual=" + sectieType.Virtueel + " WHERE id='" + sectieType.Id + "' LIMIT 1"; //query
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
                catch (Exception e)
                {
                    //throw new System.InvalidOperationException("No connection to database");
                    Console.WriteLine("No connection to database. " + e.Message); //should rethrow and handle it in the user part somewhere
                }
            }

            return kabels;
        }

        public List<Fiber> GetFibers(int SectieTypeId)
        {
            if (SectieTypeId == 0) return null;
            con = new MySqlConnection(constr); //moet opnieuw worden ingesteld als het al is gebruikt
            List<Fiber> fibers = new List<Fiber>();
            //color info, is in type info
            string qry = "SELECT st.fiber_nr,st.order_nr AS order_nr,fc.id AS fiber_color_id,fc.name_en AS fiber_color_name_en,st.module_nr,fm.id AS module_color_id,fm.name_en AS module_color_name_en FROM section_type_info AS st,fiber_color AS fc,fiber_color AS fm WHERE st.type_id='" + SectieTypeId + "' AND st.fiber_color_id=fc.id AND st.module_color_id=fm.id ORDER BY st.order_nr";

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
                                    FiberColor = new Color { Id = Convert.ToInt32(sdr["fiber_color_id"]), NameEn = sdr["fiber_color_name_en"].ToString() },
                                    ModuleNr = Convert.ToInt32(sdr["module_nr"]),
                                    ModuleColor = new Color { Id = Convert.ToInt32(sdr["module_color_id"]), NameEn = sdr["module_color_name_en"].ToString() }
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