https://powcoder.com
代写代考加微信 powcoder
Assignment Project Exam Help
Add WeChat powcoder
﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace HRIS.Teaching
{
    public enum Campus { Hobart, Launceston };
    public enum Category { Academic, Technical, Admin, Casual };
    public enum Availability { Free, Consulting, Teaching };
    public class Staff
    {
        public int Id { get; set; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string Title { get; set; }
        public Campus Campus { get; set; }
        public string Room { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Uri Photo { get; set; }
        public Category Category { get; set; }
        public List<Event> Consultation { get; set; }
        public List<Unit> CordiUnit { get; set; }

        private string Availability_Descript;
        public string Availability_Detail
        {
            get
            {
                return string.Format("Current Availibility : \n {0}{1}", Availability(DateTime.Now), Availability_Descript);
            }
            set
            {
                Availability_Descript = value;
            }
        }
        public Staff()
        {

        }

        public Availability Availability(DateTime when)
        {
            DayOfWeek tDay = when.DayOfWeek;
            DateTime TimeNow = Convert.ToDateTime(when.TimeOfDay.ToString());
            Availability_Detail = "";
            Availability temp = new Availability();

            Console.WriteLine(GivenName + FamilyName);
            //DayOfWeek tDay = DayOfWeek.Thursday;
            //DateTime TimeNow = Convert.ToDateTime("16:30");

            //Consultation
            var selected = (from Event Cons in Consultation
                            where DateTime.Compare(TimeNow, Cons.Start) > 0 && DateTime.Compare(Cons.End, TimeNow) > 0 && tDay == Cons.Day
                            select Cons).Count();
            if (selected > 0)
            {
                Console.WriteLine("Consultation");
                temp = Teaching.Availability.Consulting;
            }
            else
            {
                Console.WriteLine("Free");
                temp = Teaching.Availability.Free;
            }

            Console.WriteLine("To Teaching\n\n");

            //Teaching
            if (temp == Teaching.Availability.Free)
            {
                var test = from Unit in CordiUnit
                           from UCss in Unit.Classes
                           where DateTime.Compare(TimeNow, UCss.Start) > 0 && DateTime.Compare(UCss.End, TimeNow) > 0 && tDay == UCss.Day
                           select new { Unit.Code, UCss.Room };
                if (test.Count() == 1)
                {
                    temp = Teaching.Availability.Teaching;
                    Availability_Detail = string.Format(" : {0} {1}", test.ToList()[0].Code, test.ToList()[0].Room);
                }
            }
            Console.WriteLine("Result: " + temp.ToString());
            return temp;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1} ({2})", FamilyName, GivenName, Title);
        }

    }
}
