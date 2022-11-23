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
    public class Event
    {
        public DayOfWeek Day { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Event()
        {
        }

        public override string ToString()
        {
            
            
            return string.Format("{0}-{1} {2}", Start.ToString("HH:mm"), Day);
        }
    }
}
