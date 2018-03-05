using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models.db
{
    public class Db_User: Db_General
    {
        public List<User> List(int Start=0)
        {
            if(Start < 0) Start = 0;

            string query = "SELECT id, first_name,last_name,user_name,email FROM user LIMIT "+Start+","+Max_row; //query

            return ListQueries(query);
        }

        public List<User> Search(string search)
        {
            if (search == null) return null;
            string query = "SELECT id, first_name,last_name,user_name,email FROM user WHERE first_name LIKE '%" + search + "%' OR last_name LIKE '%" + search + "%' OR user_name LIKE '%" + search + "%' LIMIT " + Max_row; //query

            return ListQueries(query);
        }

        public User Get(int id)
        {
            if (id == 0) return null;

            string query = "SELECT id, first_name,last_name,user_name,email FROM user WHERE id='" + id + "' LIMIT 1"; //query

            return ListQueries(query)[0];
        }

        public int Add(User user)
        {
            if (user != null)
            {
                string query = "INSERT INTO user (first_name,last_name,user_name,email) VALUES ('" + user.FirstName + "','" + user.LastName + "','" + user.UserName + "','" + user.Email + "')"; //query
                this.ShortQuery(query);
                return GetLastInsertedId(); //return new id
            }
            return 0;
        }

        public void Edit(User user)
        {
            if (user != null || user.Id != 0)
            {
                string query = "UPDATE user SET first_name='" + user.FirstName + "',  last_name='" + user.LastName + "', user_name='" + user.UserName + "',  email='" + user.Email + "' WHERE id='" + user.Id + "' LIMIT 1"; //query
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
        private List<User> ListQueries(string qry)
        {
            List<User> users = new List<User>();

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
                                users.Add(new User
                                {
                                    Id = Convert.ToInt32(sdr["id"]),
                                    FirstName = sdr["first_name"].ToString(),
                                    LastName = sdr["last_name"].ToString(),
                                    UserName = sdr["user_name"].ToString(),
                                    Email = sdr["email"].ToString(),
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

            return users;
        }
    }
}