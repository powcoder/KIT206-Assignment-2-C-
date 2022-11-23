https://powcoder.com
代写代考加微信 powcoder
Assignment Project Exam Help
Add WeChat powcoder
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Teaching
{
    public class UnitClass : Event
    {
        public string Room { get; set; }
        public Campus Campus { get; set; }
        public enum ClassType { Lecture, Tutorial, Practical, Workshop };
        public ClassType Type { get; set; }
        public Staff Staff { get; set; }
        public string StartNEnd
        {
            get
            {
                return string.Format("{0}-{1}", Start.ToString("HH:mm"), End.ToString("HH:mm"));
            }
        }
        public string InvolvedStaff
        {
            get
            {
                return Staff.GivenName.Substring(0, 1) + ". " + Staff.FamilyName;
            }
        }

        public UnitClass()
        {
        }

        public override string ToString()
        {
            //return base.ToString();
            //return string.Format("{0} {1}-{2} \tin {3}", Day,Start.ToString("HH:mm"),End.ToString("HH:mm"), Campus);
            return string.Format("{0} {1}-{2} \tin {3} \t{4} {5} \tby {6}", Day, Start.ToString("HH:mm"), End.ToString("HH:mm"), Campus, Room, Type, Staff);
        }
    }
}
