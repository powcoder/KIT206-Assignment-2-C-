https://powcoder.com
代写代考加微信 powcoder
Assignment Project Exam Help
Add WeChat powcoder
﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace HRIS.Teaching
{
    public class Unit
    {
        public List<UnitClass> Classes { get; set; }
        public string Code;
        public string Title;

        public Unit()
        {

        }
        public override string ToString()
        {
            return string.Format("{0} {1}", Code, Title);
        }
    }
}
