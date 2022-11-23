https://powcoder.com
代写代考加微信 powcoder
Assignment Project Exam Help
Add WeChat powcoder
﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HRIS.Teaching;
using HRIS.Database;
namespace HRIS.Control
{
    public class StaffController
    {
        private string thisNameFilter;
        private Category thisCategoryFilter;
        public List<Staff> allStaff;
        public List<Staff> filtedStaff = new List<Staff>();
        public enum NameCate { GivenName, FamilyName };
        public NameCate NameCatee;
        public int toFilter = 0; //0 none set,1 By Name,2 By Cate, 3 by Name & Cate

        public StaffController()
        {
            LoadStaff();
        }

        public void LoadStaff()
        {
            SchoolDBAdapter SDBA = new SchoolDBAdapter();
            allStaff = SDBA.FetchDetails();

        }

        public void FilterByCate(Category category)
        {
            Console.WriteLine("Change Category to {0}\n", category);
            thisCategoryFilter = category;
            ApplyFilters();
        }

        public void FilterByName(string name, NameCate NCateOption)
        {
            Console.WriteLine("Change FilterNameCate to {1} Values {0}\n", name, NCateOption);
            thisNameFilter = name;
            NameCatee = NCateOption;
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            List<Staff> newStaff = new List<Staff>();
            List<Staff> newByName = new List<Staff>();
            List<Staff> newByCate = new List<Staff>();
            if (toFilter == 1 || toFilter == 3)
            {
                if (NameCatee == NameCate.GivenName)
                {
                    newByName = (allStaff.Where(s => s.GivenName.ToUpper().Contains(thisNameFilter.ToUpper()))).ToList();
                }
                else
                {
                    newByName = (allStaff.Where(s => s.FamilyName.ToUpper().Contains(thisNameFilter.ToUpper()))).ToList();
                }
            }
            if (toFilter == 2 || toFilter == 3)
            {
                newByCate = (allStaff.Where(s => s.Category.Equals(thisCategoryFilter))).ToList();
            }
            switch (toFilter)
            {
                case 1:
                    newStaff = newByName;
                    break;
                case 2:
                    newStaff = newByCate;
                    break;
                case 3:
                    newStaff = (from tCate in newByCate
                                from tName in newByName
                                where tCate.Id == tName.Id
                                select tCate).ToList();
                    break;
                default:
                    newStaff = allStaff;
                    break;
            }
            filtedStaff.Clear();
            newStaff.ForEach(filtedStaff.Add);
        }

        public Staff ShowStaffDetails(Staff s)
        {
            SchoolDBAdapter SDBA = new SchoolDBAdapter();
            return SDBA.CompleteDetails(s);
        }
    }
}