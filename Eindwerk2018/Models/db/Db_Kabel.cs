﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models.db
{
    public class Db_Kabel : Db_General
    {
        public List<Kabel> List() { return List(0); }

        public List<Kabel> List(int Start)
        {
            if(Start < 0) Start = 0;

            string query = "SELECT id, name,kabel_type,owner_id,reference,date_creation FROM kabel LIMIT "+Start+","+Max_row; //query

            return ListQueries(query);
        }

        public List<Kabel> Search(string search)
        {
            if (search == null) return null;
            string query = "SELECT id, name,kabel_type,owner_id,reference,date_creation FROM kabel WHERE name LIKE '%" + search + "%' LIMIT " + Max_row; //query

            return ListQueries(query);
        }

        public Kabel Get(int id)
        {
            if (id == 0) return null;

            string query = "SELECT id, name,kabel_type,owner_id,reference,date_creation FROM kabel WHERE id='" + id + "' LIMIT 1"; //query

            return ListQueries(query)[0];
        }

        public void Add(Kabel kabel)
        {
            if (kabel != null)
            {
                string query = "INSERT INTO kabel (name,kabel_type,owner_id,reference,date_creation) VALUES ('" + kabel.Naam + "',null,null,'" + kabel.Reference + "','" + kabel.CreatieDatum + "')"; //query
                this.ShortQuery(query);
            }
        }

        public void Edit(Kabel kabel)
        {
            if (kabel != null || kabel.Id != 0)
            {
                string query = "UPDATE kabel SET name='" + kabel.Naam + "', reference='" + kabel.Reference+ "', date_creation'" + kabel.CreatieDatum + "' WHERE id='" + kabel.Id + "' LIMIT 1"; //query
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
                                Reference = sdr["reference"].ToString(),
                                CreatieDatum = Convert.ToDateTime(sdr["date_creation"])
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