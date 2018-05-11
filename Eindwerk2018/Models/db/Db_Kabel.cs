using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models.db
{
    public class Db_Kabel : Db_General
    {
        public List<Kabel> List(int Start=0)
        {
            if(Start < 0) Start = 0;

            string query = "SELECT id, name,kabel_type,'' AS type_name,owner_id,'' AS owner_name,reference,date_creation FROM kabel LIMIT " + Start+","+Max_row; //query

            return ListQueries(query);
        }

        public List<Kabel> Search(string search)
        {
            if (search == null) return null;
            string query = "SELECT id, name,kabel_type,'' AS type_name,owner_id,'' AS owner_name,reference,date_creation FROM kabel WHERE name LIKE '%" + search + "%' LIMIT " + Max_row; //query

            return ListQueries(query);
        }

        /*check id name exists, except own id (when filled in)*/
        public Boolean CheckName(string search, int id = 0)
        {
            if (search == null) return true;
            string query = "SELECT id, name,kabel_type,'' AS type_name,owner_id,'' AS owner_name,reference,date_creation FROM kabel WHERE name='" + search + "'";
            if (id > 0) query += " AND id!='" + id + "'";
            query += " LIMIT " + Max_row; //query

            if (ListQueries(query).Count() == 0) return false;
            return true;
        }

        public Kabel Get(int id)
        {
            if (id == 0) return null;

            string query = "SELECT k.id,k.name,k.kabel_type,kt.name_nl AS type_name,k.owner_id,c.name AS owner_name,k.reference,k.date_creation FROM kabel AS k,kabel_type AS kt,company AS c WHERE k.id='" + id + "' AND k.kabel_type=kt.id AND k.owner_id=c.id LIMIT 1"; //query

            return ListQueries(query)[0];
        }

        public List<Sectie> GetSectieList(int id)
        {
            if (id == 0) return null;

            string query = "SELECT s.id,s.section_nr,oa.name,ob.name,s.type_id,s.length,s.length_otdr,date_in_service,infrabel_terein FROM sections AS s,ODF AS oa,ODF as ob WHERE s.kabel_id='"+ id +"' AND s.odf_start=oa.id AND s.odf_end=ob.id ORDER BY s.section_nr"; //query

            return null; // ListQueries(query)[0]; //new type of sectie, viewSectie ?
        }

        public int Add(Kabel kabel)
        {
            if (kabel != null)
            {
                string query = "INSERT INTO kabel (name,kabel_type,owner_id,reference,date_creation) VALUES ('" + kabel.Naam + "','"+ kabel.KabelType.Id + "','" + kabel.Owner.Id + "','" + kabel.Reference + "','" + MySqlDate(kabel.CreatieDatum) + "')"; //query
                this.ShortQuery(query);
                return GetLastInsertedId(); //return new id
            }
            return 0;
        }

        public void Edit(Kabel kabel)
        {
            if (kabel != null || kabel.Id != 0)
            {
                string query = "UPDATE kabel SET name='" + kabel.Naam + "',kabel_type='"+ kabel.KabelType.Id + "',owner_id='"+ kabel.Owner.Id + "', reference='" + kabel.Reference+ "' WHERE id='" + kabel.Id + "' LIMIT 1"; //query
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
        private List<Kabel> ListQueries(string qry)
        {
            List<Kabel> kabels = new List<Kabel>();

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
                                kabels.Add(new Kabel
                                {
                                    Id = Convert.ToInt32(sdr["id"]),
                                    Naam = sdr["name"].ToString(),
                                    KabelType = new KabelType { Id= MyConvertInt(sdr["kabel_type"].ToString()),NameNL = sdr["type_name"].ToString() },
                                    Owner = new Company { Id = MyConvertInt(sdr["owner_id"].ToString()), Name = sdr["owner_name"].ToString() },
                                    Reference = sdr["reference"].ToString(),
                                    CreatieDatum = Convert.ToDateTime(sdr["date_creation"])
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
    }
}