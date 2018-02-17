using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
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

            return ListQueries(query)[0];
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
                                foids.Add(new Foid
                                {
                                    Id = Convert.ToInt32(sdr["id"]),
                                    Name = sdr["name"].ToString(),
                                    CreatieDatum = Convert.ToDateTime(sdr["date_creation"]),
                                    Status = Convert.ToInt32(sdr["status"]),
                                    LastStatusDate= Convert.ToDateTime(sdr["date_last_status"]),
                                    RequestorId = Convert.ToInt32(sdr["requestor_id"]),
                                    Comments = sdr["comments"].ToString(),
                                    LengthCalculated = Convert.ToInt32(sdr["length_calculated"]),
                                    LengthOtdr = Convert.ToInt32(sdr["length_otdr"]),
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
    }
}