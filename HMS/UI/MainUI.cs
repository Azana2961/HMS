using System;
using HMS.BL;
using HMS.Entities;

namespace HMS.UI
{
    class MainUI
    {
        public static void MainLoginMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("================================================================================");
                Console.WriteLine("  HMS v1.0 | System Login Portal");
                Console.WriteLine("================================================================================\n");
                Console.ResetColor();

                Console.WriteLine("  [1] Login as Admin (Warden)");
                Console.WriteLine("  [2] Login as Front Desk Employee");
                Console.WriteLine("  [3] Login as Student");
                Console.WriteLine("  ------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  [0] Exit Application\n");
                Console.ResetColor();

                Console.Write("  Select your portal (0-3): ");
                string choice = Console.ReadLine();

                if (choice == "0")
                {
                    exit = true;
                }
                else if (choice == "1")
                {
                    AdminLogin();
                }
                else if (choice == "2")
                {
                    EmployeeLogin();
                }
                else if (choice == "3")
                {
                    StudentLogin();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  [ERROR] Invalid selection.");
                    Console.ResetColor();
                    Console.WriteLine("  Press any key to try again...");
                    Console.ReadKey();
                }
            }
        }

        private static void AdminLogin()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | Admin Login");
            Console.WriteLine("================================================================================\n");
            Console.ResetColor();

            Console.Write("  Username: ");
            string username = Console.ReadLine();
            Console.Write("  Password: ");
            string password = Console.ReadLine();

            Employee admin = EmployeeBL.VerifyEmployeeLogin(username, password, "Admin");

            if (admin != null)
            {
                AdminUI.ShowAdminMenu();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  [ERROR] Invalid Admin credentials or account is deactivated.");
                Console.ResetColor();
                Console.WriteLine("  Press any key to return...");
                Console.ReadKey();
            }
        }

        private static void EmployeeLogin()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | Front Desk Login");
            Console.WriteLine("================================================================================\n");
            Console.ResetColor();

            Console.Write("  Username: ");
            string username = Console.ReadLine();
            Console.Write("  Password: ");
            string password = Console.ReadLine();

            Employee emp = EmployeeBL.VerifyEmployeeLogin(username, password, "Front Desk");

            if (emp != null)
            {
                EmployeeUI.ShowOperationalMenu();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  [ERROR] Invalid Employee credentials or account is deactivated.");
                Console.ResetColor();
                Console.WriteLine("  Press any key to return...");
                Console.ReadKey();
            }
        }

        private static void StudentLogin()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | Student Login");
            Console.WriteLine("================================================================================\n");
            Console.ResetColor();

            Console.Write("  Registration Number: ");
            string regNo = Console.ReadLine();
            Console.Write("  Password: ");
            string password = Console.ReadLine();

            Student student = StudentBL.VerifyStudentLogin(regNo, password);

            if (student != null)
            {
                StudentUI.ShowStudentMenu(student);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  [ERROR] Invalid Student credentials.");
                Console.ResetColor();
                Console.WriteLine("  Press any key to return...");
                Console.ReadKey();
            }
        }
    }
}