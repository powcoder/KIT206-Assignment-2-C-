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
    public class StaffAdapter
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

        internal List<UnitClass> FetchDetail()
        {
            throw new NotImplementedException();
        }

        public List<Staff> FetchDetails()
        {
            List<Staff> staff = new List<Staff>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select id, given_name, family_name, title, category from staff order by family_name, given_name", conn);

                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    staff.Add(GenerateStaffList(Int32.Parse(rdr.GetString(0)), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), "Hobart", "", "", "", null, rdr.GetString(4)));
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return staff;
        }

        public Staff CompleteDetails(Staff s)
        {
            Staff tempStaff = new Staff();
            List<Event> Consultation = new List<Event>();
            MySqlConnection conn = GetConnection();
            MySqlDataReader toread = null;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select id, given_name, family_name, title, campus, phone, room, email, photo, category " + "from staff " + "where id=?id", conn);
                cmd.Parameters.AddWithValue("id", s.Id);
                toread = cmd.ExecuteReader();
                toread.Read();
                tempStaff = GenerateStaffList(Int32.Parse(toread.GetString(0)), toread.GetString(1), toread.GetString(2), toread.GetString(3), toread.GetString(4), toread.GetString(5), toread.GetString(6), toread.GetString(7), new Uri(toread.GetString(8)), toread.GetString(9));
                toread.Close();
                cmd = new MySqlCommand("select day, start, end " + "from consultation " + "where staff_id=?id", conn);
                cmd.Parameters.AddWithValue("id", s.Id);
                toread = cmd.ExecuteReader();
                while (toread.Read())
                {
                    Consultation.Add(new Event()
                    {
                        Day = ParseEnum<DayOfWeek>(toread.GetString(0)),
                        Start = Convert.ToDateTime(toread.GetString(1)),
                        End = Convert.ToDateTime(toread.GetString(2))
                    });
                }
                tempStaff.Consultation = Consultation;
                UnitAdapter UADP = new UnitAdapter();
                tempStaff.CordiUnit = UADP.GetUnits(s);
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
            return tempStaff;
        }

        private Staff GenerateStaffList(int sid, string sgvname, string sfname, string stitle, string scampus, string sphone, string sroom, string semail, Uri sphoto, string scategory)
        {
            return new Staff
            {
                Id = sid,
                GivenName = sgvname,
                FamilyName = sfname,
                Title = stitle,
                Campus = ParseEnum<Campus>(scampus),
                Room = sroom,
                Email = semail,
                Phone = sphone,
                Photo = sphoto,
                Category = ParseEnum<Category>(scategory)

            };
        }

        internal List<UnitClass> FetchClassesFor(Staff staff)
        {
            throw new NotImplementedException();
        }

        internal List<Event> FetchConsutsOfStaffInUnit(Unit unit)
        {
            throw new NotImplementedException();
        }
    }
}