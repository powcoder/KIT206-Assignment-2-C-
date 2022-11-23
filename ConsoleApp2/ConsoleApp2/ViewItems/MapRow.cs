https://powcoder.com
代写代考加微信 powcoder
Assignment Project Exam Help
Add WeChat powcoder
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.ViewItems
{
    class MapRow
    {
        public string[] NumOfDay { get; set; }
        public string Time { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }

        public MapRow()
        {
            NumOfDay = new string[5];
        }

        public override string ToString()
        {
            return string.Format("\t{1}\t{2}\t{3}\t{4}\t{5}",Time,Monday,Tuesday,Wednesday,Thursday,Friday);
        }
    }
}
