using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models.db
{
    public class Db_Company: Db_General
    {
        public List<Company> List(int Start=0)
        {
            if(Start < 0) Start = 0;

            string query = "SELECT id, name FROM company LIMIT "+Start+","+Max_row; //query

            return ListQueries(query);
        }

        public List<Company> Search(string search)
        {
            if (search == null) return null;
            string query = "SELECT id, name WHERE company LIKE '%" + search + "%' LIMIT " + Max_row; //query

            return ListQueries(query);
        }

        public Company Get(int id)
        {
            if (id == 0) return null;

            string query = "SELECT id, name FROM company WHERE id='" + id + "' LIMIT 1"; //query

            return ListQueries(query)[0];
        }

        public int Add(Company company)
        {
            if (company != null && company.Name != "")
            {
                string query = "INSERT INTO company (name) VALUES ('" + company.Name + "')"; //query
                this.ShortQuery(query);
                return GetLastInsertedId(); //return new id
            }
            return 0;
        }

        public void Edit(Company company)
        {
            if (company != null && company.Id != 0 && company.Name != "")
            {
                string query = "UPDATE company SET name='" + company.Name + "' WHERE id='" + company.Id + "' LIMIT 1"; //query
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
        private List<Company> ListQueries(string qry)
        {
            List<Company> company = new List<Company>();

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
                                company.Add(new Company
                                {
                                    Id = Convert.ToInt32(sdr["id"]),
                                    Name = sdr["name"].ToString()
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

            return company;
        }
    }
}