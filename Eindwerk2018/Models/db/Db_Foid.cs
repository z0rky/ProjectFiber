using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models.db
{
    public class Db_Foid : Db_General
    {
        public List<Foid> List(int Start=0)
        {
            if(Start < 0) Start = 0;

            string query = "SELECT id,name,date_creation,status,date_last_status,requestor_id,comments,length_calculated,length_otdr,start_odf,end_odf FROM FOID LIMIT " + Start+","+Max_row; //query

            return ListQueries(query);
        }

        public List<Foid> Search(string search)
        {
            if (search == null) return null;
            string query = "SELECT id,name,date_creation,status,date_last_status,requestor_id,comments,length_calculated,length_otdr,start_odf,end_odf FROM FOID WHERE name LIKE '%" + search + "%' LIMIT " + Max_row; //query

            return ListQueries(query);
        }

        public Foid Get(int id)
        {
            if (id == 0) return null;

            string query = "SELECT id,name,date_creation,status,date_last_status,requestor_id,comments,length_calculated,length_otdr,start_odf,end_odf FROM FOID WHERE id='" + id + "' LIMIT 1"; //query

            Foid foid = ListQueries(query)[0];

            foid.Fibers = ListFibers(id);

            return foid;
        }

        public List<Sectie> GetSectieList(int id)
        {
            if (id == 0) return null;

            string query = "SELECT f.FOID_serial_nr,f.FOID_fibre_nr,k.name,s.section_nr,oa.name,ob.name,s.type_id,s.length,s.length_otdr,date_in_service,infrabel_terein FROM fibers AS f,sections AS s,kabel AS k,ODF AS oa,ODF as ob WHERE f.FOID='"+id+"' AND f.section_id=s.id AND s.kabel_id=k.id AND s.odf_start=oa.id AND s.odf_end=ob.id ORDER BY s.section_nr,f.FOID_fibre_nr;"; //query

            return null; // ListQueries(query)[0]; //new type of sectie, viewSectie ?
        }

        public void Add(Foid foid)
        {
            if (foid != null)
            {
                string query = "INSERT INTO FOID (name,date_creation,status,date_last_status,requestor_id,comments,length_calculated,length_otdr,start_odf,end_odf) VALUES ('" + foid.Name + "','" + foid.CreatieDatum + "','" + foid.Status+ "','" + foid.LastStatusDate + "','" + foid.RequestorId + "','" + foid.Comments + "','" + foid.LengthCalculated + "','" + foid.LengthOtdr + "','" + foid.StartOdfId + "','" + foid.EndOdfId + "')"; //query
                this.ShortQuery(query);
            }
        }

        public void Edit(Foid foid)
        {
            if (foid != null || foid.Id != 0)
            {
                string query = "UPDATE FOID SET name='" + foid.Name + "', date_creation='" + foid.CreatieDatum+ "', status='" + foid.Status + "', date_last_status='" + foid.LastStatusDate + "', requestor_id='" + foid.RequestorId + "', comments='" + foid.Comments + "', length_calculated='" + foid.LengthOtdr + "', start_odf='" + foid.StartOdfId + "', end_odf='" + foid.EndOdfId + "' WHERE id='" + foid.Id + "' LIMIT 1"; //query
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
        private List<Foid> ListQueries(string qry)
        {
            List<Foid> foids = new List<Foid>();

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
                                int lengthOtdr = 0;
                                if (sdr["length_otdr"].Equals("null")) lengthOtdr = 0;
                                //else lengthOtdr = Convert.ToInt32(sdr["length_otdr"]);

                                foids.Add(new Foid
                                {
                                    Id = Convert.ToInt32(sdr["id"]),
                                    Name = sdr["name"].ToString(),
                                    CreatieDatum = DateTime.Parse(sdr["date_creation"].ToString()),
                                    //CreatieDatum = Convert.ToDateTime(sdr["date_last_status"]),
                                    //CreatieDatum = DateTime.ParseExact(sdr["date_creation"].ToString(), "d/MM/yyyy H:mm:ss", null), //Why always the current date ??
                                    //CreatieDatum = (DateTime) sdr["date_creation"],
                                    //CreatieDatum = Convert.ToDateTime(sdr["date_creation"], new CultureInfo("nl-BE")),
                                    // DateTime.TryParse(sdr["date_creation"].ToString(), CreatieDatum),
                                    //CreatieDatum = sdr["date_creation"].,
                                    Comments = sdr["date_creation"].ToString(),
                                    Status = Convert.ToInt32(sdr["status"]),
                                    //LastStatusDate= Convert.ToDateTime(sdr["date_last_status"]),
                                    RequestorId = Convert.ToInt32(sdr["requestor_id"]),
                                    //Comments = sdr["comments"].ToString(),
                                    LengthCalculated = Convert.ToInt32(sdr["length_calculated"]),
                                    //LengthOtdr = Convert.ToInt32(sdr["length_otdr"]),
                                    LengthOtdr = lengthOtdr,
                                    StartOdfId = Convert.ToInt32(sdr["start_odf"]),
                                    EndOdfId = Convert.ToInt32(sdr["end_odf"])
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

            return foids;
        }

        /*Get the fibers on the route*/
        private List<FiberFoid> ListFibers(int foid)
        {  //not fibers nor section, somthing new, with all the info ODFs ...
            if (foid <= 0) return null;
            con = new MySqlConnection(constr); //moet opnieuw worden ingesteld als het al is gebruikt

            List<FiberFoid> fibers = new List<FiberFoid>();
            string query = "SELECT k.id AS cable_id,k.name AS cable_name,s.section_nr,s.length,f.FOID_serial_nr,f.FOID_fibre_nr,os.id AS odf_start_id,os.name AS odf_start_name,oe.id AS odf_end_id, oe.name AS odf_end_name FROM fibers AS f, sections AS s, kabel AS k, ODF AS os, ODF AS oe WHERE f.FOID='" + foid + "' AND f.section_id=s.id AND s.kabel_id=k.id AND s.odf_start=os.id AND s.odf_end=oe.id ORDER BY FOID_serial_nr, FOID_fibre_nr";

            using (con) //con in Db_general
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(query))
                    {
                        cmd.Connection = con;
                        con.Open();
                        using (MySqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                fibers.Add(new FiberFoid
                                {
                                    KabelId = Convert.ToInt32(sdr["cable_id"]),
                                    KabelName = sdr["cable_name"].ToString(),
                                    SectieNr = Convert.ToInt32(sdr["section_nr"]),
                                    SectieLength = Convert.ToInt32(sdr["length"]),
                                    FoidSerialNr = Convert.ToInt32(sdr["FOID_serial_nr"]),
                                    FoidFibreNr = Convert.ToInt32(sdr["FOID_serial_nr"]),
                                    OdfStartId = Convert.ToInt32(sdr["odf_start_id"]),
                                    OdfStartName = sdr["odf_start_name"].ToString(),
                                    OdfEndId = Convert.ToInt32(sdr["odf_end_id"]),
                                    OdfEndName = sdr["odf_end_name"].ToString()
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