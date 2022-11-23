https://powcoder.com
代写代考加微信 powcoder
Assignment Project Exam Help
Add WeChat powcoder
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRIS.Teaching;
using HRIS.Database;
using HRIS.ViewItems;
namespace HRIS.Control
{
    class MapController
    {
        List<UnitClass> AllConsults = new List<UnitClass>();
        List<UnitClass> AllClasses = new List<UnitClass>();
        List<Event> AllConsultsRUnit = new List<Event>();
        List<UnitClass> AllClassesRUnit = new List<UnitClass>();
        StaffAdapter StaffAdp = new StaffAdapter();
        UnitAdapter UnitAdp = new UnitAdapter();
        EventTable MapTable = new EventTable();
        private string Colour = "#FFFF2C2C";
        private static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }
        public MapController()
        {
            AllConsults = StaffAdp.FetchDetail();
            AllClasses = UnitAdp.GetUnits();
        }
        public List<MapRow> ShowClashFrom(string campus)
        {
            List<UnitClass> UnitsClasses = new List<UnitClass>();
            if (campus == "All")
            {
                UnitsClasses = AllClassesRUnit;
            }
            else
            {
                UnitsClasses = (from UClass in AllClassesRUnit
                                where UClass.Campus == ParseEnum<Campus>(campus)
                                select UClass).ToList();
            }
            EventTable ClashTable = new EventTable();
            //Fill in Table with Consultation of staff who involved unit
            foreach (Event Consult in AllConsultsRUnit)
            {
                for (int row = Convert.ToInt32(Consult.Start.ToString("HH")); row < Convert.ToInt32(Consult.End.ToString("HH")); row++)
                {
                    ClashTable.AddFrequency(row, Consult.Day);
                }
            }
            //Fill in Table with Classes relevant to unit
            foreach (UnitClass UClass in UnitsClasses)
            {
                for (int row = Convert.ToInt32(UClass.Start.ToString("HH")); row < Convert.ToInt32(UClass.End.ToString("HH")); row++)
                {
                    ClashTable.AddFrequency(row, UClass.Day);
                }
            }
            
            //Generate Clash Map
            List<MapRow> ClashMap = new List<MapRow>();
            for (int i = 9; i < 17; i++) ClashMap.Add(new MapRow { Time = InTime(i) });
            //Set Consultations Colour to Map
            for (int i = 9; i < 17; i++)
            {
                //Draw Blank
                if (ClashTable.FrequencyAt(i, DayOfWeek.Monday) == 0) ClashMap[i - 9].Monday = GenerateCKey(1, 1, "#FFFFFFFF");
                if (ClashTable.FrequencyAt(i, DayOfWeek.Tuesday) == 0) ClashMap[i - 9].Tuesday = GenerateCKey(1, 1, "#FFFFFFFF");
                if (ClashTable.FrequencyAt(i, DayOfWeek.Wednesday) == 0) ClashMap[i - 9].Wednesday = GenerateCKey(1, 1, "#FFFFFFFF");
                if (ClashTable.FrequencyAt(i, DayOfWeek.Thursday) == 0) ClashMap[i - 9].Thursday = GenerateCKey(1, 1, "#FFFFFFFF");
                if (ClashTable.FrequencyAt(i, DayOfWeek.Friday) == 0) ClashMap[i - 9].Friday = GenerateCKey(1, 1, "#FFFFFFFF");
                //Draw Occupied
                if (ClashTable.FrequencyAt(i, DayOfWeek.Monday) == 1) ClashMap[i - 9].Monday = GenerateCKey(1, 1, "#FF77FF2E");
                if (ClashTable.FrequencyAt(i, DayOfWeek.Tuesday) == 1) ClashMap[i - 9].Tuesday = GenerateCKey(1, 1, "#FF77FF2E");
                if (ClashTable.FrequencyAt(i, DayOfWeek.Wednesday) == 1) ClashMap[i - 9].Wednesday = GenerateCKey(1, 1, "#FF77FF2E");
                if (ClashTable.FrequencyAt(i, DayOfWeek.Thursday) == 1) ClashMap[i - 9].Thursday = GenerateCKey(1, 1, "#FF77FF2E");
                if (ClashTable.FrequencyAt(i, DayOfWeek.Friday) == 1) ClashMap[i - 9].Friday = GenerateCKey(1, 1, "#FF77FF2E");
                //Draw Clash
                if (ClashTable.FrequencyAt(i, DayOfWeek.Monday) > 1) ClashMap[i - 9].Monday = GenerateCKey(1, 1, "#FFFA3737");
                if (ClashTable.FrequencyAt(i, DayOfWeek.Tuesday) > 1) ClashMap[i - 9].Tuesday = GenerateCKey(1, 1, "#FFFA3737");
                if (ClashTable.FrequencyAt(i, DayOfWeek.Wednesday) > 1) ClashMap[i - 9].Wednesday = GenerateCKey(1, 1, "#FFFA3737");
                if (ClashTable.FrequencyAt(i, DayOfWeek.Thursday) > 1) ClashMap[i - 9].Thursday = GenerateCKey(1, 1, "#FFFA3737");
                if (ClashTable.FrequencyAt(i, DayOfWeek.Friday) > 1) ClashMap[i - 9].Friday = GenerateCKey(1, 1, "#FFFA3737");
                string[] The_DayNum = new string[5];
                if (ClashTable.FrequencyAt(i, DayOfWeek.Monday) > 1) The_DayNum[0] = "clash";
                if (ClashTable.FrequencyAt(i, DayOfWeek.Tuesday) > 1) The_DayNum[1] = "clash";
                if (ClashTable.FrequencyAt(i, DayOfWeek.Wednesday) > 1) The_DayNum[2] = "clash";
                if (ClashTable.FrequencyAt(i, DayOfWeek.Thursday) > 1) The_DayNum[3] = "clash";
                if (ClashTable.FrequencyAt(i, DayOfWeek.Friday) > 1) The_DayNum[4] = "clash";
                ClashMap[i - 9].NumOfDay = The_DayNum;
            }
            return ClashMap;
        }
        public List<MapRow> ShowClash(Unit unit)
        {
            AllConsultsRUnit = StaffAdp.FetchConsutsOfStaffInUnit(unit);
            AllClassesRUnit = UnitAdp.FetchClassesFor(unit);
            return ShowClashFrom("All");
        }
        public List<MapRow> ShowStaffAct(Staff staff)
        {
            //Generate Consults' Table
            EventTable ConsultsTable = new EventTable();
            foreach (Event Consult in staff.Consultation)
            {
                for (int row = Convert.ToInt32(Consult.Start.ToString("HH")); row < Convert.ToInt32(Consult.End.ToString("HH")); row++)
                {
                    ConsultsTable.AddFrequency(row, Consult.Day);
                }
            }
            //Generate Classes' Table
            EventTable ClassesTable = new EventTable();
            StaffAdapter StaffAdp = new StaffAdapter();
            List<UnitClass> SinClass = StaffAdp.FetchClassesFor(staff);
            foreach (UnitClass Class in SinClass)
            {
                for (int row = Convert.ToInt32(Class.Start.ToString("HH")); row < Convert.ToInt32(Class.End.ToString("HH")); row++)
                {
                    ClassesTable.AddFrequency(row, Class.Day);
                }
            }
            //Generate ActMap
            List<MapRow> StaffActMap = new List<MapRow>();
            for (int i = 9; i < 17; i++) StaffActMap.Add(new MapRow { Time = InTime(i) });
            //Set Classes Colour to Map
            for (int i = 9; i < 17; i++)
            {
                if (ClassesTable.FrequencyAt(i, DayOfWeek.Monday) > 0) StaffActMap[i - 9].Monday = GenerateCKey(1, 1, "#FFF28500");
                else if (StaffActMap[i - 9].Monday == "") StaffActMap[i - 9].Monday = GenerateCKey(1, 1, "#FFFFFFFF");
                if (ClassesTable.FrequencyAt(i, DayOfWeek.Tuesday) > 0) StaffActMap[i - 9].Tuesday = GenerateCKey(1, 1, "#FFF28500");
                else if (StaffActMap[i - 9].Tuesday == "") StaffActMap[i - 9].Tuesday = GenerateCKey(1, 1, "#FFFFFFFF");
                if (ClassesTable.FrequencyAt(i, DayOfWeek.Wednesday) > 0) StaffActMap[i - 9].Wednesday = GenerateCKey(1, 1, "#FFF28500");
                else if (StaffActMap[i - 9].Wednesday == "") StaffActMap[i - 9].Wednesday = GenerateCKey(1, 1, "#FFFFFFFF");
                if (ClassesTable.FrequencyAt(i, DayOfWeek.Thursday) > 0) StaffActMap[i - 9].Thursday = GenerateCKey(1, 1, "#FFF28500");
                else if (StaffActMap[i - 9].Thursday == "") StaffActMap[i - 9].Thursday = GenerateCKey(1, 1, "#FFFFFFFF");
                if (ClassesTable.FrequencyAt(i, DayOfWeek.Friday) > 0) StaffActMap[i - 9].Friday = GenerateCKey(1, 1, "#FFF28500");
                else if (StaffActMap[i - 9].Friday == "") StaffActMap[i - 9].Friday = GenerateCKey(1, 1, "#FFFFFFFF");
            }
            //Set Consultations Colour to Map
            for (int i = 9; i < 17; i++)
            {
                if (ConsultsTable.FrequencyAt(i, DayOfWeek.Monday) > 0) StaffActMap[i - 9].Monday = GenerateCKey(1, 1, "#FF7fffd4");
                else if (StaffActMap[i - 9].Monday == "") StaffActMap[i - 9].Monday = GenerateCKey(1, 1, "#FFFFFFFF");
                if (ConsultsTable.FrequencyAt(i, DayOfWeek.Tuesday) > 0) StaffActMap[i - 9].Tuesday = GenerateCKey(1, 1, "#FF7fffd4");
                else if (StaffActMap[i - 9].Tuesday == "") StaffActMap[i - 9].Tuesday = GenerateCKey(1, 1, "#FFFFFFFF");
                if (ConsultsTable.FrequencyAt(i, DayOfWeek.Wednesday) > 0) StaffActMap[i - 9].Wednesday = GenerateCKey(1, 1, "#FF7fffd4");
                else if (StaffActMap[i - 9].Wednesday == "") StaffActMap[i - 9].Wednesday = GenerateCKey(1, 1, "#FFFFFFFF");
                if (ConsultsTable.FrequencyAt(i, DayOfWeek.Thursday) > 0) StaffActMap[i - 9].Thursday = GenerateCKey(1, 1, "#FF7fffd4");
                else if (StaffActMap[i - 9].Thursday == "") StaffActMap[i - 9].Thursday = GenerateCKey(1, 1, "#FFFFFFFF");
                if (ConsultsTable.FrequencyAt(i, DayOfWeek.Friday) > 0) StaffActMap[i - 9].Friday = GenerateCKey(1, 1, "#FF7fffd4");
                else if (StaffActMap[i - 9].Friday == "") StaffActMap[i - 9].Friday = GenerateCKey(1, 1, "#FFFFFFFF");
            }
            return StaffActMap;
        }
        public List<MapRow> GetConsultsFrom(Campus campus)
        {
            EventTable tempHeatMap = new EventTable();
            //Set a new List from All Consultations
            List<UnitClass> SelectedCon = (from Consults in AllConsults
                                           where Consults.Campus == campus
                                           select Consults).ToList();
            //Fill it in MapTable
            foreach (UnitClass Consult in SelectedCon)
            {
                for (int row = Convert.ToInt32(Consult.Start.ToString("HH")); row < Convert.ToInt32(Consult.End.ToString("HH")); row++)
                {
                    tempHeatMap.AddFrequency(row, Consult.Day);
                }
            }
            MapTable = tempHeatMap;
            //MapTable.ShowTable();
            return RefreshHeatMap();
        }
        public List<MapRow> GetClassesFrom(Campus campus)
        {
            EventTable tempHeatMap = new EventTable();
            //Set a new List from All Classes
            List<UnitClass> SelClass = (from Class in AllClasses
                                        where Class.Campus == campus
                                        select Class).ToList();
            //Fill it in MapTable
            foreach (UnitClass Class in SelClass)
            {
                for (int row = Convert.ToInt32(Class.Start.ToString("HH")); row < Convert.ToInt32(Class.End.ToString("HH")); row++)
                {
                    tempHeatMap.AddFrequency(row, Class.Day);
                }
            }
            MapTable = tempHeatMap;
            //MapTable.ShowTable();
            return RefreshHeatMap();
        }
        public List<MapRow> RefreshHeatMap()
        {
            List<MapRow> MapRows = new List<MapRow>();
            //Transform MapTable to HeatMap Rows
            for (int i = 9; i < 17; i++)
            {
                //Set Colour Code for Each Day in Row
                MapRow HMRow = new MapRow
                {
                    Time = InTime(i),
                    Monday = GenerateCKey(MapTable.FrequencyAt(i, DayOfWeek.Monday), MapTable.maxFrequency, Colour),
                    Tuesday = GenerateCKey(MapTable.FrequencyAt(i, DayOfWeek.Tuesday), MapTable.maxFrequency, Colour),
                    Wednesday = GenerateCKey(MapTable.FrequencyAt(i, DayOfWeek.Wednesday), MapTable.maxFrequency, Colour),
                    Thursday = GenerateCKey(MapTable.FrequencyAt(i, DayOfWeek.Thursday), MapTable.maxFrequency, Colour),
                    Friday = GenerateCKey(MapTable.FrequencyAt(i, DayOfWeek.Friday), MapTable.maxFrequency, Colour)
                };
                string[] The_DayNum = new string[5];
                if (MapTable.FrequencyAt(i, DayOfWeek.Monday) != 0) The_DayNum[0] = MapTable.FrequencyAt(i, DayOfWeek.Monday).ToString();
                if (MapTable.FrequencyAt(i, DayOfWeek.Tuesday) != 0) The_DayNum[1] = MapTable.FrequencyAt(i, DayOfWeek.Tuesday).ToString();
                if (MapTable.FrequencyAt(i, DayOfWeek.Wednesday) != 0) The_DayNum[2] = MapTable.FrequencyAt(i, DayOfWeek.Wednesday).ToString();
                if (MapTable.FrequencyAt(i, DayOfWeek.Thursday) != 0) The_DayNum[3] = MapTable.FrequencyAt(i, DayOfWeek.Thursday).ToString();
                if (MapTable.FrequencyAt(i, DayOfWeek.Friday) != 0) The_DayNum[4] = MapTable.FrequencyAt(i, DayOfWeek.Friday).ToString();
                HMRow.NumOfDay = The_DayNum;
                MapRows.Add(HMRow);
            }
            //MapTable.ShowTable();
            return MapRows;
        }
        private string GenerateCKey(int current, int max, string SColour)
        {
            double Dcurrent = Convert.ToDouble(current);
            double Dmax = Convert.ToDouble(max);
            double DRatio = Dcurrent / Dmax;
            string FrontString = "";
            string RearString = "";
            if (DRatio == 0) FrontString = "#00";
            else if (DRatio > 0 && DRatio <= 0.25) FrontString = "#44";
            else if (DRatio > 0.25 && DRatio <= 0.50) FrontString = "#88";
            else if (DRatio > 0.50 && DRatio <= 0.75) FrontString = "#CC";
            else if (DRatio > 0.75 && DRatio <= 1.00) FrontString = "#FF";
            else FrontString = "#FF";
            RearString = SColour.Substring(3, 6);
            return FrontString + RearString;
        }
        private string InTime(int i)
        {
            string ChgTime = "";
            switch (i)
            {
                case 9:
                    ChgTime = " 9 AM - 10 AM";
                    break;
                case 10:
                    ChgTime = "10 AM - 11 AM";
                    break;
                case 11:
                    ChgTime = "11 AM - 12 PM";
                    break;
                case 12:
                    ChgTime = "12 PM - 1 PM";
                    break;
                case 13:
                    ChgTime = " 1 PM - 2 PM";
                    break;
                case 14:
                    ChgTime = " 2 PM - 3 PM";
                    break;
                case 15:
                    ChgTime = " 3 PM - 4 PM";
                    break;
                case 16:
                    ChgTime = " 4 PM - 5 PM";
                    break;
                default:
                    break;
            }
            return ChgTime;
        }
        public void SetColour(string colour)
        {
            Colour = colour;
        }
    }
}