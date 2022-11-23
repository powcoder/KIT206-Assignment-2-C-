https://powcoder.com
代写代考加微信 powcoder
Assignment Project Exam Help
Add WeChat powcoder
﻿using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HRIS.Teaching;

namespace HRIS.Database
{
    class UnitAdapter
    {
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";
        private const string server = "alacritas.cis.utas.edu.au";
        private static MySqlConnection conn = null;

        private static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        private static MySqlConnection GetConnection()
        {
            if (conn == null)
            {
                string connectionString = String.Format("Database={0};Data Source ={1}; User Id = {2}; Password ={3}; SslMode = none", db, server, user, pass);
                conn = new MySqlConnection(connectionString);
            }
            return conn;
        }

        internal List<UnitClass> GetUnits()
        {
            throw new NotImplementedException();
        }

        public List<Unit> GetUnits(Staff staff)
        {
            List<Unit> CorUnit = new List<Unit>();
            MySqlConnection conn = GetConnection();
            MySqlDataReader toread = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select code, title " + "from unit " + "where coordinator=?id " + "order by code", conn);
                cmd.Parameters.AddWithValue("id", staff.Id);
                toread = cmd.ExecuteReader();
                while (toread.Read())
                {
                    CorUnit.Add(GenerateUnit(toread.GetString(0), toread.GetString(1)));
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (toread != null)
                {
                    toread.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return CorUnit;
        }

        private Unit GenerateUnit(string scode, string stitle)
        {
            return new Unit
            {
                Code = scode,
                Title = stitle
            };
        }

        internal List<UnitClass> FetchClassesFor(Unit unit)
        {
            throw new NotImplementedException();
        }
    }
}
