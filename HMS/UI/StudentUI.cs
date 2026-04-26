using HMS.BL;
using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.UI
{
    class StudentUI 
    {
        public static void ShowStudentMenu(Student currentStudent)
        {
            bool logout = false;

            while (!logout)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("================================================================================");
                Console.WriteLine($"  HMS v1.0 | Student Portal                  ");
                Console.WriteLine("================================================================================\n");
                Console.ResetColor();

                Console.WriteLine("  [1] View My Profile & Room Details");
                Console.WriteLine("  [2] View My Roommates");
                Console.WriteLine("  [3] Request Room Change / Checkout");
                Console.WriteLine("  ------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  [0] Logout\n");
                Console.ResetColor();

                Console.Write("  Select an operation (0-5): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        logout = true;
                        break;
                    case "1":
                        ViewProfile(currentStudent);
                        break;
                    case "2":
                        ViewRoommates(currentStudent);
                        break;
                    case "3":
                        RequestRoomChange(currentStudent);
                        Console.ReadKey();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n  [ERROR] Invalid choice. Please select an option between 0 and 5.");
                        Console.ResetColor();
                        Console.WriteLine("  Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        public static void ViewProfile(Student s)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | My Profile");
            Console.WriteLine("================================================================================\n");
            Console.ResetColor();

            Console.WriteLine($"  Name:     {s.getname()}");
            Console.WriteLine($"  Reg No:   {s.getid()}");
            Console.WriteLine($"  Semester: {s.getsem()}");

            Room r = s.getroom();
            if (r != null)
            {
                Console.WriteLine($"  Room No:  {r.getroomnum()}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  Room No:  Not Assigned Yet");
                Console.ResetColor();
            }

            Console.WriteLine("\n  Press any key to return to the menu...");
            Console.ReadKey();
        }
        public static void ViewRoommates(Student s)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | My Roommates");
            Console.WriteLine("================================================================================\n");
            Console.ResetColor();

            Room r = s.getroom();
            if (r == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"  [STATUS] You are not currently assigned to any room, {s.getname()}.");
                Console.ResetColor();
            }
            else
            {
                List<Student> roommates = r.getroommates();

                if (roommates != null && roommates.Count > 1)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"  --- Roommates in Room {r.getroomnum()} ---");
                    Console.ResetColor();

                    foreach (Student stu in roommates)
                    {
                        if (stu.getid() != s.getid())
                        {
                            Console.WriteLine($"  Name:     {stu.getname()}");
                            Console.WriteLine($"  Reg No:   {stu.getid()}");
                            Console.WriteLine($"  Semester: {stu.getsem()}");
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("  ---------------------------------");
                            Console.ResetColor();
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("  [STATUS] You currently have no other roommates.");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("\n  Press any key to return to the menu...");
            Console.ReadKey();
        }
        public static void RequestRoomChange(Student s)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | Request Room Change / Vacate");
            Console.WriteLine("================================================================================\n");
            Console.ResetColor();

            Room r = s.getroom();

            if (r == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  [STATUS] You are not currently assigned to any room.");
                Console.ResetColor();
                Console.WriteLine("\n  Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"  Current Room: {r.getroomnum()}\n");

            Console.WriteLine("  [1] Request Room Change");
            Console.WriteLine("  [2] Request to Vacate / Checkout");
            Console.WriteLine("  [3] General Complaint");
            Console.WriteLine("  ------------------------------------------------------------------");
            Console.Write("  Select an option (1-3): ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n  --- Available Rooms ---");
                Console.ResetColor();

                List<Room> avialablerooms = RoomBL.getavailablerooms();
                bool otherRoomsExist = false;

                if (avialablerooms == null || avialablerooms.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  [STATUS] No rooms are currently available for transfer.");
                    Console.ResetColor();
                }
                else
                {
                    foreach (Room aroom in avialablerooms)
                    {
                        if (aroom.getroomnum() != r.getroomnum())
                        {
                            otherRoomsExist = true;
                            Console.WriteLine($"  Room No: {aroom.getroomnum()} | Current Occupants: {aroom.getroommates().Count}/{RoomBL.getroommatelimit()}");
                        }
                    }

                    if (!otherRoomsExist)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("  [STATUS] No other rooms are currently available for transfer.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("\n  Enter the desired Room Number: ");
                        if (!int.TryParse(Console.ReadLine(), out int targetRoom))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n  [ERROR] Invalid input. Please enter numbers only.");
                            Console.ResetColor();
                        }
                        else if (targetRoom == r.getroomnum())
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n  [ERROR] You are already in this room!");
                            Console.ResetColor();
                        }
                        else
                        {
                            if (StudentBL.roomchangepossible(targetRoom))
                            {
                                string reason = "";
                                while (string.IsNullOrWhiteSpace(reason))
                                {
                                    Console.Write("  Enter a brief reason for the change: ");
                                    reason = Console.ReadLine();
                                    if (string.IsNullOrWhiteSpace(reason))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("  [ERROR] Reason cannot be empty.\n");
                                        Console.ResetColor();
                                    }
                                }

                                RequestBL.requestroomchange(s, reason, targetRoom);

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\n  [SUCCESS] Room change request submitted to administration!");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\n  [ERROR] The selected room does not exist or is at full capacity.");
                                Console.ResetColor();
                            }
                        }
                    }
                }
            }
            else if (choice == "2")
            {
                string reason = "";
                while (string.IsNullOrWhiteSpace(reason))
                {
                    Console.Write("\n  Enter a brief reason for vacating: ");
                    reason = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(reason))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("  [ERROR] Reason cannot be empty.");
                        Console.ResetColor();
                    }
                }

                RequestBL.addvacaterequest(s, reason);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n  [SUCCESS] Vacate request submitted to administration!");
                Console.ResetColor();
            }
            else if (choice == "3")
            {
                string reason = "";
                while (string.IsNullOrWhiteSpace(reason))
                {
                    Console.Write("\n  Enter the Complaint: ");
                    reason = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(reason))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("  [ERROR] Complaint cannot be empty.");
                        Console.ResetColor();
                    }
                }

                RequestBL.genralcomplain(s, reason);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n  [SUCCESS] General complaint submitted to administration!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  [ERROR] Invalid selection.");
                Console.ResetColor();
            }

            Console.WriteLine("\n  Press any key to return to the menu...");
            Console.ReadKey();
        }

    }

}
