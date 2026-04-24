using HMS.BL;
using HMS.Entities;
using HMS.UI;
using System;

namespace HMS
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("================================================================================");
                Console.WriteLine("  HOSTEL MANAGEMENT SYSTEM (HMS) v1.0 | TEST MODE (NO AUTH)");
                Console.WriteLine("================================================================================\n");
                Console.ResetColor();

                Console.WriteLine("  [1] Enter Admin Dashboard");
                Console.WriteLine("  [2] Enter Employee Dashboard");
                Console.WriteLine("  [3] Enter Student Dashboard");
                Console.WriteLine("  ------------------------------------------------------------------");

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  [0] Exit Application\n");
                Console.ResetColor();

                Console.Write("  Select your dashboard (0-3): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AdminUI.ShowAdminMenu();
                        break;
                    case "2":
                        EmployeeUI.ShowOperationalMenu();
                        break;
                    case "3":
                        TestStudentLogin();
                        break;
                    case "0":
                        exit = true;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n  [SYSTEM] Shutting down HMS. Goodbye!");
                        Console.ResetColor();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n  [ERROR] Invalid choice. Please select an option between 0 and 3.");
                        Console.ResetColor();
                        Console.WriteLine("  Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public static void TestStudentLogin()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  --- Select Student for Testing ---");
            Console.ResetColor();

            Console.Write("  Enter Registration Number of existing student: ");
            string reg = Console.ReadLine();

            Student s = StudentBL.studentexist(reg);

            if (s != null)
            {
                StudentUI.ShowStudentMenu(s);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  [ERROR] Student not found. Register a student via the Employee menu first!");
                Console.ResetColor();
                Console.WriteLine("  Press any key to return...");
                Console.ReadKey();
            }
        }
    }
}