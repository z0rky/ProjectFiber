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

            //info from table section_yype
            string query = "SELECT id, name,description,virtual FROM section_type WHERE id='" + id + "' LIMIT 1"; //query
            SectieType sectieType = ListQueries(query)[0];
            //list of fibers from table section_type_info
            query = "SELECT s.order_nr,s.fiber_nr,s.fiber_color_id,c.name_en AS fiber_name_en,c.name_nl AS fiber_name_nl,c.name_fr AS fiber_name_fr,s.module_nr,s.module_color_id,cb.name_en AS module_name_en,cb.name_nl AS module_name_nl,cb.name_fr AS module_name_fr FROM section_type_info AS s,fiber_color AS c,fiber_color AS cb WHERE s.type_id='" + id + "' AND s.fiber_color_id=c.id AND  s.module_color_id=cb.id ORDER BY s.order_nr"; //query

            sectieType.Fibers = new List<Fiber>();

            con = new MySqlConnection(constr); //moeten nieuwe maken, omdat de vorige het wegsmijt
            using (con) //con in Db_general
            {
                try
                {
                    using (MySqlCommand cmd2 = new MySqlCommand(query))
                    {
                         cmd2.Connection = con;
                         con.Open();  //disposed? Why can't we re-use it ?
                         using (MySqlDataReader sdr2 = cmd2.ExecuteReader())
                         {
                            while (sdr2.Read())
                            {
                                sectieType.Fibers.Add(new Fiber
                                {
                                    OrderNr = Convert.ToInt32(sdr2["order_nr"]),
                                    FiberNr = Convert.ToInt32(sdr2["fiber_nr"]),
                                    FiberColor = new Color { Id= Convert.ToInt32(sdr2["fiber_color_id"]), NameEn= sdr2["fiber_name_en"].ToString(), NameNl = sdr2["fiber_name_nl"].ToString(), NameFr = sdr2["fiber_name_fr"].ToString() },
                                    ModuleNr = Convert.ToInt32(sdr2["module_nr"]),
                                    ModuleColor = new Color { Id = Convert.ToInt32(sdr2["module_color_id"]), NameEn = sdr2["module_name_en"].ToString(), NameNl = sdr2["module_name_nl"].ToString(), NameFr = sdr2["module_name_fr"].ToString() }
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

            using (con) //con in Db_general
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