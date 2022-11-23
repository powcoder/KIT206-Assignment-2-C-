https://powcoder.com
代写代考加微信 powcoder
Assignment Project Exam Help
Add WeChat powcoder
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRIS.Control;
using HRIS.Teaching;

namespace ConsoleApp2
{
    class Program
    {
        //Controller
        private static StaffController StaffControl = new StaffController();

        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("\n==== Options ====\n");
                Console.WriteLine("1. Test StaffList");
                Console.WriteLine("2. Test StaffDetails");
                Console.WriteLine("3. Test Filter");
                Console.WriteLine("\n===================");
                Console.Write("\nSelect Action: ");
                int act = Int32.Parse(Console.ReadLine());
                Console.Clear();
                switch (act)
                {
                    case 1:
                        Console.WriteLine("\n===== Staff List =====");
                        testStaffList();
                        break;
                    case 2:
                        Console.WriteLine("\n===== Staff Details =====");
                        testDetails();
                        break;
                    case 3:
                        Console.WriteLine("\n===== Filtered =====");
                        testFilter();
                        break;
                }
                Console.WriteLine("\nPress to Continue...");
                Console.ReadKey();
            } while (true);
        }

        private static void testStaffList()
        {
            List<Staff> slist = StaffControl.allStaff;
            foreach (Staff staff in slist)
            {
                Console.WriteLine(staff.ToString());
            }
        }

        private static void testFilter()
        {
            StaffControl.toFilter = 1;
            Console.Write("Enter Partly GivenName: ");
            string giveName = Console.ReadLine();
            StaffControl.FilterByName(giveName, StaffController.NameCate.GivenName);
            List<Staff> slist = StaffControl.filtedStaff;
            foreach (Staff staff in slist)
            {
                Console.WriteLine(staff.ToString());
            }
        }

        private static void testDetails()
        {
            List<Staff> slist = StaffControl.allStaff;
            Staff staff = StaffControl.ShowStaffDetails(slist[0]);
            Console.WriteLine("ID: " + staff.Id);
            Console.WriteLine("FamilyName: " + staff.FamilyName);
            Console.WriteLine("GivenName: " + staff.GivenName);
            Console.WriteLine("Title: " + staff.Title);
            Console.WriteLine("Campus: " + staff.Campus);
            Console.WriteLine("Email: " + staff.Email);
            Console.WriteLine("\nConsultations:");
            foreach (Event Cons in staff.Consultation)
            {
                Console.WriteLine(" > " + Cons.ToString());
            }
            Console.WriteLine("\nInvolved Unites:");
            foreach (Unit unit in staff.CordiUnit)
            {
                Console.WriteLine(" > " + unit.ToString());
            }
        }
    }
}