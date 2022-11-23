https://powcoder.com
代写代考加微信 powcoder
Assignment Project Exam Help
Add WeChat powcoder
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS .Teaching
{
    public class EventTable
    {
        private int[,] ETable = new int[8, 5];

        public int maxFrequency
        {
            get
            {
                int themax = 0;
                for (int hour = 0; hour < 8; hour++)
                {
                    for (int day = 0; day < 5; day++)
                    {
                        if (ETable[hour, day] > themax)
                        {
                            themax = ETable[hour, day];
                        }
                    }
                }
                return themax;
            }
        }

        public EventTable()
        {

        }

        public void AddFrequency(int hour, DayOfWeek day)
        {
            int onday;
            switch (day)
            {
                case DayOfWeek.Monday:
                    onday = 0;
                    break;
                case DayOfWeek.Tuesday:
                    onday = 1;
                    break;
                case DayOfWeek.Wednesday:
                    onday = 2;
                    break;
                case DayOfWeek.Thursday:
                    onday = 3;
                    break;
                case DayOfWeek.Friday:
                    onday = 4;
                    break;
                default:
                    onday = -1;
                    break;
            }
            if (onday != -1)
            {
                ETable[hour - 9, onday] += 1;
            }
        }
        public int FrequencyAt(int hour, DayOfWeek day)
        {
            int onday;
            switch (day)
            {
                case DayOfWeek.Monday:
                    onday = 0;
                    break;
                case DayOfWeek.Tuesday:
                    onday = 1;
                    break;
                case DayOfWeek.Wednesday:
                    onday = 2;
                    break;
                case DayOfWeek.Thursday:
                    onday = 3;
                    break;
                case DayOfWeek.Friday:
                    onday = 4;
                    break;
                default:
                    return -1;
            }

            return ETable[hour - 9, onday];
        }


        public void ShowTable()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int y =0; y < 5; y++)
                {
                    Console.Write(ETable[i, y] + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}
