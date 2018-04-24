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
        public List<Foid> List(int Start = 0)
        {
            if (Start < 0) Start = 0;

            string query = "SELECT f.id,f.name,f.date_creation,f.status,f.date_last_status,f.requestor_id,'user_name',f.comments,f.length_calculated,f.length_otdr,f.start_odf,f.end_odf,'OdfStartName','OdfEndName' FROM FOID AS f ORDER BY f.id DESC LIMIT " + Start + "," + Max_row; //query

            return ListQueries(query);
        }

        public List<Foid> Search(string search)
        {
            if (search == null) return null;
            string query = "SELECT f.id,f.name,f.date_creation,f.status,f.date_last_status,f.requestor_id,'user_name',f.comments,f.length_calculated,f.length_otdr,f.start_odf,f.end_odf,'OdfStartName','OdfEndName' FROM FOID AS f WHERE name LIKE '%" + search + "%' LIMIT " + Max_row; //query

            return ListQueries(query);
        }

        public List<Foid> SearchOnId(string search)
        {
            if (search == null) return null;
            string query = "SELECT f.id,f.name,f.date_creation,f.status,f.date_last_status,f.requestor_id,'user_name',f.comments,f.length_calculated,f.length_otdr,f.start_odf,f.end_odf,'OdfStartName','OdfEndName' FROM FOID AS f WHERE f.id LIKE '%" + search + "%' LIMIT " + Max_row; //query

            return ListQueries(query);
        }

        public Foid Get(int id)
        {
            if (id == 0) return null;

            string query = "SELECT f.id,f.name,f.date_creation,f.status,f.date_last_status,f.requestor_id,u.user_name AS user_name,f.comments,f.length_calculated,f.length_otdr,f.start_odf,oa.name AS OdfStartName,f.end_odf,oe.name AS OdfEndName FROM FOID AS f, user AS u, ODF AS oa, ODF AS oe WHERE  f.id='" + id + "' AND f.start_odf=oa.id AND f.end_odf=oe.id AND f.requestor_id=u.id LIMIT 1"; //query

            Foid foid = ListQueries(query)[0];

            //foid.Fibers = ListFibers(id);
            foid.Secties = ListSections(id);

            return foid;
        }

        public List<Sectie> GetSectieList(int id)
        {
            if (id == 0) return null;

            string query = "SELECT f.FOID_serial_nr,f.FOID_fibre_nr,k.name,s.section_nr,oa.name,ob.name,s.type_id,s.length,s.length_otdr,date_in_service,infrabel_terein FROM fibers AS f,sections AS s,kabel AS k,ODF AS oa,ODF as ob WHERE f.FOID='" + id + "' AND f.section_id=s.id AND s.kabel_id=k.id AND s.odf_start=oa.id AND s.odf_end=ob.id ORDER BY s.section_nr,f.FOID_fibre_nr;"; //query

            return null; // ListQueries(query)[0]; //new type of sectie, viewSectie ?
        }

        public int Add(Foid foid)
        {
            if (foid != null)
            {
                string query = "INSERT INTO FOID (name,date_creation,status,date_last_status,requestor_id,comments,length_calculated,length_otdr,start_odf,end_odf) VALUES ('" + foid.Name + "','" + MySqlDate(foid.CreatieDatum) + "','" + foid.Status + "','" + MySqlDate(foid.LastStatusDate) + "','" + foid.RequestorId + "','" + foid.Comments + "','" + foid.LengthCalculated + "','" + foid.LengthOtdr + "','" + foid.StartOdfId + "','" + foid.EndOdfId + "')"; //query
                this.ShortQuery(query);
                return GetLastInsertedId(); //return new id
            }
            return 0;
        }

        public int AddSecties(FiberFoid fiberFoid)
        {
            if (fiberFoid != null)
            {
                string query = "UPDATE fibers SET FOID='" + fiberFoid.Foid + "', FOID_serial_nr='" + fiberFoid.FoidSerialNr + "', FOID_fibre_nr='" + fiberFoid.FoidFibreNr + "' WHERE section_id='" + fiberFoid.SectieNr + "' AND fiber_nr='" + fiberFoid.FiberNr + "'"; //query
                this.ShortQuery(query);
                return 1;
            }
            return 0;
        }

        public void Edit(Foid foid)
        {
            if (foid != null || foid.Id != 0)
            {
                //length_calculated='" + foid.LengthOtdr + "', should be updated by another process, not by edit
                //fibers/sections is also another funtions
                string query = "UPDATE FOID SET name='" + foid.Name + "', status='" + foid.Status + "', date_last_status='" + MySqlDate(foid.LastStatusDate) + "', requestor_id='" + foid.RequestorId + "', comments='" + foid.Comments + "', start_odf='" + foid.StartOdfId + "', end_odf='" + foid.EndOdfId + "' WHERE id='" + foid.Id + "' LIMIT 1"; //query
                this.ShortQuery(query);
            }
        }

        public void UpdateFibers(Foid foid)
        {
            //First clear
            string query = "UPDATE fibers SET FOID='0',FOID_serial_nr='0',FOID_fibre_nr='0' WHERE FOID='"+ foid.Id +"'"; //query
            this.ShortQuery(query);
            //then add for each fiber in each section
            int foid_serial = 100;
            foreach(Sectie sectie in foid.Secties)
            {
                int foid_fiber_serial = 1;
                foreach (Fiber fiber in sectie.Fibers)
                {
                    query = "UPDATE fibers SET FOID='"+ foid.Id +"',FOID_serial_nr='"+ foid_serial + "', FOID_fibre_nr='"+ foid_fiber_serial + "' WHERE section_id='"+ sectie.Id +"' AND fiber_nr='"+fiber.FiberNr+"'";
                    this.ShortQuery(query);
                    foid_fiber_serial++;
                }
                foid_serial += 100;
            }
        }

        public void Delete(int id)
        {
            if (id != 0)
            {
                //delete, set bit ?
            }
        }

        public void DeleteFibers(int id)
        {
            if (id != 0)
            {
                string query = "UPDATE fibers SET FOID='0', FOID_serial_nr='0',FOID_fibre_nr='0' WHERE FOID='"+id+"'"; //query
                this.ShortQuery(query);
            }
        }

        //for return queries
        public List<Foid> ListQueries(string qry)
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
                            DateTime statusDate = new DateTime(2000,1,1); //just a default date
                            while (sdr.Read())
                            {
                                try { if (!sdr["date_last_status"].Equals(null)) statusDate = Convert.ToDateTime(sdr["date_last_status"].ToString());}
                                catch (Exception e) { Console.WriteLine("What is this " + e.Message); }

                                try
                                {
                                    foids.Add(new Foid
                                    {
                                        Id = Convert.ToInt32(sdr["id"]),
                                        Name = sdr["name"].ToString(),
                                        CreatieDatum = DateTime.Parse(sdr["date_creation"].ToString()),
                                        Status = MyConvertInt(sdr["status"].ToString()),
                                        LastStatusDate= statusDate, //somtimes null, connectionstring adapted (Convert Zero Datetime=True)
                                        //Lastdate Sometimes has a problem
                                        RequestorId = MyConvertInt(sdr["requestor_id"].ToString()),
                                        Requestor = new User { Id = Convert.ToInt32(sdr["requestor_id"]), UserName = sdr["user_name"].ToString() },
                                        Comments = sdr["comments"].ToString(),
                                        LengthCalculated = MyConvertInt(sdr["length_calculated"].ToString()),
                                        //LengthOtdr = Convert.ToInt32(sdr["length_otdr"]), //when  null, blockes, so created own funtion
                                        LengthOtdr = MyConvertInt(sdr["length_otdr"].ToString()),
                                        StartOdfId = MyConvertInt(sdr["start_odf"].ToString()),
                                        StartOdfName = sdr["OdfStartName"].ToString(),
                                        EndOdfId = MyConvertInt(sdr["end_odf"].ToString()),
                                        EndOdfName = sdr["OdfEndName"].ToString()
                                    });
                                }
                                catch (Exception e) { Console.WriteLine("Parsing went bad" + e.Message); }
                            }
                        }
                        con.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("No connection to database. " + e.Message); //should rethrow and handle it in the user part somewhere
                }
            }

            return foids;
        }

        /*Get the fibers on the route*/
        private List<FiberFoid> ListFibers(int foid)
        {  //not fibers nor section, somthing new, with all the info ODFs ...
            //actually sections might be better -> ListSections(int foid)
            if (foid <= 0) return null;
            con = new MySqlConnection(constr); //moet opnieuw worden ingesteld als het al is gebruikt

            List<FiberFoid> fibers = new List<FiberFoid>();
            string query = "SELECT k.id AS cable_id,k.name AS cable_name,s.section_nr,f.fiber_nr,s.length,f.FOID_serial_nr,f.FOID_fibre_nr,st.virtual,os.id AS odf_start_id,os.name AS odf_start_name,oe.id AS odf_end_id, oe.name AS odf_end_name FROM fibers AS f, sections AS s, section_type AS st, kabel AS k, ODF AS os, ODF AS oe WHERE f.FOID='" + foid + "' AND f.section_id=s.id AND s.type_id=st.id AND s.kabel_id=k.id AND s.odf_start=os.id AND s.odf_end=oe.id ORDER BY FOID_serial_nr, FOID_fibre_nr";

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
                                    KabelId = MyConvertInt(sdr["cable_id"].ToString()),
                                    KabelName = sdr["cable_name"].ToString(),
                                    SectieNr = MyConvertInt(sdr["section_nr"].ToString()),
                                    FiberNr = MyConvertInt(sdr["fiber_nr"].ToString()),
                                    SectieLength = MyConvertInt(sdr["length"].ToString()),
                                    FoidSerialNr = MyConvertInt(sdr["FOID_serial_nr"].ToString()),
                                    FoidFibreNr = MyConvertInt(sdr["FOID_fibre_nr"].ToString()),
                                    SectieVirtual = Convert.ToBoolean(sdr["virtual"]),
                                    OdfStartId = MyConvertInt(sdr["odf_start_id"].ToString()),
                                    OdfStartName = sdr["odf_start_name"].ToString(),
                                    OdfEndId = MyConvertInt(sdr["odf_end_id"].ToString()),
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

        /*Get the sections on the route, could replace ListFibers(int foid)*/
        public List<Sectie> ListSections(int foid)
        {
            if (foid <= 0) return null;
            con = new MySqlConnection(constr); //moet opnieuw worden ingesteld als het al is gebruikt

            List<Sectie> sectiefibers = new List<Sectie>();
            string query = "SELECT s.id AS sectie_id,k.id AS cable_id,k.name AS cable_name,s.section_nr,f.fiber_nr,s.length,f.FOID_serial_nr,f.FOID_fibre_nr,st.virtual,os.id AS odf_start_id,os.name AS odf_start_name,oe.id AS odf_end_id, oe.name AS odf_end_name FROM fibers AS f, sections AS s, section_type AS st, kabel AS k, ODF AS os, ODF AS oe WHERE f.FOID='" + foid + "' AND f.section_id=s.id AND s.type_id=st.id AND s.kabel_id=k.id AND s.odf_start=os.id AND s.odf_end=oe.id ORDER BY FOID_serial_nr, FOID_fibre_nr";

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
                                sectiefibers.Add(new Sectie
                                {
                                    Id = MyConvertInt(sdr["sectie_id"].ToString()),
                                    KabelId = MyConvertInt(sdr["cable_id"].ToString()),
                                    KabelName = sdr["cable_name"].ToString(),
                                    SectieNr = MyConvertInt(sdr["section_nr"].ToString()),
                                    Lengte = MyConvertInt(sdr["length"].ToString()),
                                    Fibers = new List<Fiber>
                                    {
                                        new Fiber
                                        {
                                            FiberNr = MyConvertInt(sdr["fiber_nr"].ToString()),
                                            FoidSerialNr = MyConvertInt(sdr["FOID_serial_nr"].ToString()),
                                            FoidFibreNr = MyConvertInt(sdr["FOID_fibre_nr"].ToString())
                                        }
                                    },
                                    SectieVirtual = Convert.ToBoolean(sdr["virtual"]),
                                    OdfStartId = MyConvertInt(sdr["odf_start_id"].ToString()),
                                    OdfStartName = sdr["odf_start_name"].ToString(),
                                    OdfEndId = MyConvertInt(sdr["odf_end_id"].ToString()),
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

            //combine
            int sectieId = 0;
            Sectie tmpSectie = null;
            List<Sectie> listSecties = new List<Sectie>();
            foreach (Sectie sectie in sectiefibers)
            {
                if (sectie.Id != sectieId)
                {
                    sectieId = sectie.Id;
                    if (tmpSectie != null) listSecties.Add(tmpSectie);//only write, when change
                    tmpSectie = sectie;
                }
                else tmpSectie.Fibers.Add(sectie.Fibers.First());
            }
            listSecties.Add(tmpSectie); //write one last time

            return listSecties;
        }

        /*Get a list of free fibers*/
        public List<Fiber> ListFreeFibers(int sectieId)
        {
            if (sectieId <= 0) return null;
            con = new MySqlConnection(constr); //moet opnieuw worden ingesteld als het al is gebruikt

            List<Fiber> fibers = new List<Fiber>();
            string query = "SELECT fiber_nr,quality FROM fibers WHERE section_id='"+sectieId+"' AND FOID='0' ORDER BY fiber_nr";

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
                                fibers.Add(new Fiber
                                {
                                    FiberNr = MyConvertInt(sdr["fiber_nr"].ToString()),
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