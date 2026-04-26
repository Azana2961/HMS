using HMS.BL;
using HMS.DL;
using HMS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
namespace HMS.UI
{
    class EmployeeUI
    {
        public static void ShowOperationalMenu()
        {
            bool logout = false;

            while (!logout)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("================================================================================");
                Console.WriteLine($"  HMS v1.0 | Employee Operations Dashboard             ");
                Console.WriteLine("================================================================================\n");
                Console.ResetColor();

                Console.WriteLine("  [1] Student Management");
                Console.WriteLine("  [2] Alocate Room to a new Student");
                Console.WriteLine("  [3] Process a Student Check-Out / Vacate");
                Console.WriteLine("  [4] View Rooms Report");
                Console.WriteLine("  [5] View Room Availability Matrix");
                Console.WriteLine("  [6] Manage Requests");
                Console.WriteLine("  ------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  [0] Logout\n");
                Console.ResetColor();

                Console.Write("  Select an operation (0-6): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        {
                            ManageStudents();
                            break;
                        }
                    case "2":
                        {
                            allocatesinglestudent();
                            break;
                        }
                    case "3":
                        {
                            checkoutstudent();
                            break;
                        }
                    case "4":
                        {
                            ViewRoomOccupancy();
                            break;
                        }
                    case "5":
                        {
                            availabilitycheck();
                            break;
                        }
                    case "6":
                        {
                            ManageRequests();
                            break;
                        }
                    case "0":
                        {
                            logout = true;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Enter a valid option!");
                            Console.ReadKey();
                            break;
                        }

                }
            }
        }
        public static void ManageStudents()
        {
            bool back = false;

            while (!back)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("================================================================================");
                Console.WriteLine("  HMS v1.0 | Manage Student Records");
                Console.WriteLine("================================================================================\n");
                Console.ResetColor();

                Console.WriteLine("  [1] Register New Student");
                Console.WriteLine("  [2] View All Registered Students");
                Console.WriteLine("  [3] Update Student Details");
                Console.WriteLine("  [4] Discharge / Remove Student");
                Console.WriteLine("  ------------------------------------------------------------------");

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  [0] Back to Employee Dashboard\n");
                Console.ResetColor();

                Console.Write("  Select an operation (0-4): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        back = true;
                        break;
                    case "1":
                        addsinglestudent();
                        Console.ReadKey();
                        break;
                    case "2":
                        ViewStudents();
                        Console.ReadKey();
                        break;
                    case "3":
                        UpdateStudent();
                        Console.ReadKey();
                        break;
                    case "4":
                        RemoveStudent();
                        Console.ReadKey();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n  [ERROR] Invalid choice. Please select an option between 0 and 4.");
                        Console.ResetColor();
                        Console.WriteLine("  Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        public static void addsinglestudent()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | Single Student Registration");
            Console.WriteLine("================================================================================\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  --- Entering Student Details ---");
            Console.ResetColor();

            string regNum = "";
            bool validReg = false;

            while (!validReg)
            {
                Console.Write("  Registration Number (e.g., 2024-CS-101): ");
                regNum = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(regNum))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  [ERROR] Registration Number cannot be empty.\n");
                    Console.ResetColor();
                }
                else if (StudentBL.studentexist(regNum) != null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"  [ERROR] Student already exists with registration '{regNum}'!\n");
                    Console.ResetColor();
                }
                else
                {
                    validReg = true;
                }
            }

            string name = "";
            while (!ValidationBL.IsValidName(name))
            {
                Console.Write("  Full Name: ");
                name = Console.ReadLine();
                if (!ValidationBL.IsValidName(name))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  [ERROR] Name cannot be empty OR Contain Number or Special character.\n");
                    Console.ResetColor();
                }
            }

            int semester = 0;
            bool validSem = false;

            while (!validSem)
            {
                Console.Write("  Semester (1-8): ");
                string semInput = Console.ReadLine();

                if (int.TryParse(semInput, out semester) && semester >= 1 && semester <= 8)
                {
                    validSem = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  [ERROR] Invalid semester. Please enter a number between 1 and 8.\n");
                    Console.ResetColor();
                }
            }

           StudentBL.addsinglestudent(regNum, name, semester);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n  Processing registration...");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  [SUCCESS] Student {name} has been securely registered to the system!");
            Console.ResetColor();

            Console.WriteLine("\n  Press any key to return to the menu...");
            Console.ReadKey();
        }
        public static void ViewStudents()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | Active Student Roster");
            Console.WriteLine("================================================================================\n");
            Console.ResetColor();

            List<Student> students = StudentBL.viewallstudents();

            if (students == null || students.Count <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  [STATUS] There are currently no students registered in the system.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  ----------------------------------------------------------------------");
                Console.WriteLine(String.Format("  | {0,-20} | {1,-15} | {2,-10} | {3,-12} |", "Student Name", "Reg No", "Semester", "Room No"));
                Console.WriteLine("  ----------------------------------------------------------------------");
                Console.ResetColor();

                foreach (Student s in students)
                {
                    Room r = s.getroom();
                    string roomDisplay = "";

                    if (r != null)
                    {
                        roomDisplay = r.getroomnum().ToString();
                    }
                    else
                    {
                        roomDisplay = "Not Allotted";
                    }

                    Console.WriteLine(String.Format("  | {0,-20} | {1,-15} | {2,-10} | {3,-12} |", s.getname(), s.getid(), s.getsem(), roomDisplay));
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  ----------------------------------------------------------------------");
                Console.ResetColor();
            }

            Console.WriteLine("\n  Press any key to return to the menu...");
            Console.ReadKey();
        }
       public static void UpdateStudent()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | Update Student Details");
            Console.WriteLine("================================================================================\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  --- Select Student ---");
            Console.ResetColor();

            Console.Write("  Enter the registration number: ");
            string reg = Console.ReadLine();

            Student s = StudentBL.studentexist(reg);

            if (s != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n  --- Enter New Details ---");
                Console.ResetColor();

                string name = "";
                bool validName = false;

                while (!validName)
                {
                    Console.Write("  Enter the new name: ");
                    name = Console.ReadLine();

                    if (ValidationBL.IsValidName(name))
                    {
                        validName = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("  [ERROR] Invalid name. Please use only letters and spaces.\n");
                        Console.ResetColor();
                    }
                }

                int sem = 0;
                bool validSem = false;

                while (!validSem)
                {
                    Console.Write("  Enter the new semester (1-8): ");
                    string semInput = Console.ReadLine();

                    if (int.TryParse(semInput, out sem) && sem > 0 && sem <= 8)
                    {
                        validSem = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("  [ERROR] Invalid semester. Please enter a number between 1 and 8.\n");
                        Console.ResetColor();
                    }
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\n  Processing update request...");
                Console.ResetColor();

                StudentBL.updatestudentinfo(s, name, sem);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n  [SUCCESS] Student {reg} has been successfully updated!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  [ERROR] Student with registration '{reg}' does not exist in the system.");
                Console.ResetColor();
            }

            Console.WriteLine("\n  Press any key to return to the menu...");
            Console.ReadKey();
        }
        public static void RemoveStudent()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | Discharge Student");
            Console.WriteLine("================================================================================\n");
            Console.ResetColor();

            Console.Write("  Enter the registration number: ");
            string reg = Console.ReadLine();

            Student s = StudentBL.studentexist(reg);

            if (s != null)
            {
                Room r = s.getroom();

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\n  Processing discharge request...");

                if (r != null)
                {
                    Console.WriteLine($"  [INFO] Unassigning student from Room {r.getroomnum()}...");
                }
                else
                {
                    Console.WriteLine("  [INFO] Student is not assigned to any room.");
                }
                Console.ResetColor();

                StudentBL.removesinglestudent(s, r);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n  [SUCCESS] Student {reg} has been successfully discharged and removed!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  [ERROR] Student with registration '{reg}' does not exist in the system.");
                Console.ResetColor();
            }

            Console.WriteLine("\n  Press any key to return to the menu...");
            Console.ReadKey();
        }
        public static void ManageRequests()
        {
            bool back = false;

            while (!back)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("================================================================================");
                Console.WriteLine("  HMS v1.0 | Manage Student Requests & Complaints");
                Console.WriteLine("================================================================================\n");
                Console.ResetColor();

                Console.WriteLine("  [1] View All Pending Requests");
                Console.WriteLine("  [2] Process a Request (Reject / Resolve)");
                Console.WriteLine("  [3] View Request History ");
                Console.WriteLine("  ------------------------------------------------------------------");

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  [0] Back to Employee Dashboard\n");
                Console.ResetColor();

                Console.Write("  Select an operation (0-3): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        back = true;
                        break;
                    case "1":
                        ViewPendingRequests();
                        Console.ReadKey();
                        break;
                    case "2":
                        ProcessRequest();
                        Console.ReadKey();
                        break;
                    case "3":
                        ViewRequestHistory();
                        Console.ReadKey();
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
        public static void ViewPendingRequests()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | Pending Requests Inbox");
            Console.WriteLine("================================================================================\n");
            Console.ResetColor();

            List<Request> requests = RequestBL.getallrequests();
            bool hasPending = false;

            if (requests != null && requests.Count > 0)
            {
                foreach (Request r in requests)
                {
                    if (r.getstatus() == "Pending")
                    {
                        hasPending = true;
                        break;
                    }
                }
            }

            if (!hasPending)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  [STATUS] There are currently no pending requests in the inbox.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  ----------------------------------------------------------------------");
                Console.WriteLine(String.Format("  | {0,-10} | {1,-53} |", "Req ID", "Reason / Description"));
                Console.WriteLine("  ----------------------------------------------------------------------");
                Console.ResetColor();

                foreach (Request r in requests)
                {
                    if (r.getstatus() == "Pending")
                    {
                        string reason = r.getreason();
                        if (reason.Length > 53)
                        {
                            reason = reason.Substring(0, 50) + "...";
                        }

                        Console.WriteLine(String.Format("  | {0,-10} | {1,-53} |", r.getrequestid(), reason));
                    }
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  ----------------------------------------------------------------------");
                Console.ResetColor();
            }

            Console.WriteLine("\n  Press any key to return to the menu...");
            Console.ReadKey();
        }
        public static void ProcessRequest()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | Process Student Request");
            Console.WriteLine("================================================================================\n");
            Console.ResetColor();

            int id = 0;
            bool validId = false;

            while (!validId)
            {
                Console.Write("  Enter the ticket id: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out id) && id > 0)
                {
                    validId = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  [ERROR] Invalid ID format. Please enter a valid number.\n");
                    Console.ResetColor();
                }
            }

            Request r = RequestBL.Requestexist(id);

            if (r != null)
            {
                if (r.getstatus() == "Pending")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n  --- Request Details ---");
                    Console.ResetColor();

                    Console.WriteLine(String.Format("  | {0,-10} | {1,-53} |", r.getrequestid(), r.getreason()));

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n  --- Options ---");
                    Console.ResetColor();
                    Console.WriteLine("  [1] Mark as Resolved");
                    Console.WriteLine("  [2] Mark as Rejected");
                    Console.WriteLine("  [0] Cancel");

                    Console.Write("\n  Select an option: ");
                    string choice = Console.ReadLine();

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("\n  Processing update...");
                    Console.ResetColor();

                    switch (choice)
                    {
                        case "1":
                            string reqtype = r.gettype();
                            Student reqostor = r.getrequestor();
                            Room room = reqostor.getroom();
                            if(reqtype == "Vacante")
                            {
                                StudentBL.removesinglestudent(reqostor, room);
                            }
                            else if(reqtype == "Roomchange")
                            {
                                RoomBL.changeroom(reqostor, r.gettargetroom());
                            }
                            RequestBL.updatestatus(r, "Resolved");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"  [SUCCESS] Request {id} marked as Resolved.");
                            break;
                        case "2":
                            RequestBL.updatestatus(r, "Rejected");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"  [STATUS] Request {id} marked as Rejected.");
                            break;
                        case "0":
                            Console.WriteLine("  [INFO] Action cancelled. Request remains Pending.");
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("  [ERROR] Invalid action. Request remains Pending.");
                            break;
                    }
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n  [INFO] The ticket is already {r.getstatus()}.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  [ERROR] The ticket does not exist!");
                Console.ResetColor();
            }

            Console.WriteLine("\n  Press any key to return to the menu...");
            Console.ReadKey();
        }
        public static void ViewRequestHistory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | Request History");
            Console.WriteLine("================================================================================\n");
            Console.ResetColor();

            List<Request> requests = RequestBL.getallrequests();
            bool hasHistory = false;

            if (requests != null && requests.Count > 0)
            {
                foreach (Request r in requests)
                {
                    if (r.getstatus() == "Resolved" || r.getstatus() == "Rejected")
                    {
                        hasHistory = true;
                        break;
                    }
                }
            }

            if (hasHistory == false)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  [STATUS] There is currently no request history.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  --------------------------------------------------------------------------------");
                Console.WriteLine(String.Format("  | {0,-10} | {1,-12} | {2,-49} |", "Req ID", "Status", "Reason / Description"));
                Console.WriteLine("  --------------------------------------------------------------------------------");
                Console.ResetColor();

                foreach (Request r in requests)
                {
                    if (r.getstatus() == "Resolved" || r.getstatus() == "Rejected")
                    {
                        string reason = r.getreason();
                        if (reason.Length > 49)
                        {
                            reason = reason.Substring(0, 46) + "...";
                        }

                        Console.WriteLine(String.Format("  | {0,-10} | {1,-12} | {2,-49} |", r.getrequestid(), r.getstatus(), reason));
                    }
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  --------------------------------------------------------------------------------");
                Console.ResetColor();
            }

            Console.WriteLine("\n  Press any key to return to the menu...");
            Console.ReadKey();
        }
        public static void ViewRoomOccupancy()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | Hostel Room Occupancy Report");
            Console.WriteLine("================================================================================\n");
            Console.ResetColor();

            List<Room> rooms = RoomBL.getallrooms();

            if (rooms == null || rooms.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  [STATUS] No rooms are currently registered in the system.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  --------------------------------------------------------------------------");
                Console.WriteLine(String.Format("  | {0,-10} | {1,-15} | {2,-15} | {3,-15} |", "Room No", "Occupants", "Capacity", "Status"));
                Console.WriteLine("  --------------------------------------------------------------------------");
                Console.ResetColor();

                int limit = RoomBL.getroommatelimit();

                foreach (Room r in rooms)
                {
                    int currentCount = 0;
                    if (r.getroommates() != null)
                    {
                        currentCount = r.getroommates().Count;
                    }

                    string status = "";
                    if (currentCount == 0)
                    {
                        status = "Empty";
                    }
                    else if (currentCount >= limit)
                    {
                        status = "Full";
                    }
                    else
                    {
                        status = "Available";
                    }

                    Console.WriteLine(String.Format("  | {0,-10} | {1,-15} | {2,-15} | {3,-15} |", r.getroomnum(), currentCount, limit, status));
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  --------------------------------------------------------------------------");
                Console.ResetColor();
            }

            Console.WriteLine("\n  Press any key to return to the menu...");
            Console.ReadKey();
        }
        public static void availabilitycheck()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | Room Availability Matrix");
            Console.WriteLine("================================================================================\n");
            Console.ResetColor();

            if (RoomDL.getrooms() == null || RoomDL.getrooms().Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  [ERROR] Admin has not entered any rooms!");
                Console.ResetColor();
                Console.WriteLine("\n  Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            bool roomstatus = false;

            foreach (Room r in RoomBL.getallrooms())
            {
                List<Student> roommates = r.getroommates();

                if (roommates == null || roommates.Count < RoomBL.getroommatelimit())
                {
                    roomstatus = true;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n  --- Room Number: {r.getroomnum()} ---");
                    Console.ResetColor();

                    if (roommates == null || roommates.Count == 0)
                    {
                        Console.WriteLine("  Status: Fully Vacant");
                    }
                    else
                    {
                        Console.WriteLine($"  Status: Partially Occupied ({roommates.Count}/{RoomBL.getroommatelimit()})");
                        Console.WriteLine("  Current Occupants:");
                        foreach (Student stu in roommates)
                        {
                            Console.WriteLine($"   - {stu.getname()}");
                        }
                    }
                }
            }

            if (!roomstatus)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  [STATUS] There are no empty or partially available rooms.");
                Console.ResetColor();
            }

            Console.WriteLine("\n  Press any key to return to the menu...");
            Console.ReadKey();
        }
        public static void allocatesinglestudent()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | Manual Single Student Allocation");
            Console.WriteLine("================================================================================\n");
            Console.ResetColor();

            if (RoomBL.roomcount() == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  [ERROR] The Admin has not entered the Rooms!");
                Console.ResetColor();
                Console.WriteLine("\n  Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            if (StudentBL.studentcount() == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  [ERROR] There are no students to Allot.");
                Console.ResetColor();
                Console.WriteLine("\n  Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }
            Console.Write("  Enter the Student Registration: ");
            string reg = Console.ReadLine();

            Student s = StudentBL.studentexist(reg);

            if (s == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  [ERROR] The student does not exist!");
                Console.ResetColor();
                Console.WriteLine("\n  Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }
            if (s.getroom() != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  [ERROR] Student is already allocated to Room {s.getroom().getroomnum()}!");
                Console.WriteLine("  If you wish to move them, please process a Check-Out or Room Change request.");
                Console.ResetColor();
                Console.WriteLine("\n  Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n  --- Fetching Available Rooms ---");
            Console.ResetColor();

            List<Room> avialablerooms = RoomBL.getavailablerooms();
            if (avialablerooms.Count == 0 || avialablerooms == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  [STATUS] No rooms are currently available.");
                Console.ResetColor();
                Console.WriteLine("\n  Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }
            else
            {
                foreach (Room aroom in avialablerooms)
                {
                    Console.WriteLine($"  Room No: {aroom.getroomnum()} | Current Occupants: {aroom.getroommates().Count}");
                }
            }

            Console.Write("\n  Enter the Room number you want to assign: ");

            if (!int.TryParse(Console.ReadLine(), out int room))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  [ERROR] Invalid input. Please enter numbers only.");
                Console.ResetColor();
                Console.WriteLine("\n  Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            Room r = RoomBL.avialibiltycheck(room);

            if (r != null && avialablerooms.Contains(r))
            {
                StudentBL.addinglestudent(s, r);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n  [SUCCESS] Student successfully allotted to Room {r.getroomnum()}!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  [ERROR] That room does not exist or is already at full capacity!");
                Console.ResetColor();
            }

            Console.WriteLine("\n  Press any key to return to the menu...");
            Console.ReadKey();
        }
        public static void checkoutstudent()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | Process Student Check-Out / Vacate");
            Console.WriteLine("================================================================================\n");
            Console.ResetColor();

            if (RoomBL.roomcount() == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  [ERROR] The Admin has not entered the Rooms!");
                Console.ResetColor();
                Console.WriteLine("\n  Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            if (StudentBL.studentcount() == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  [ERROR] There are no students to Allot.");
                Console.ResetColor();
                Console.WriteLine("\n  Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }
            Console.Write("  Enter the Student Registration: ");
            string reg = Console.ReadLine();

            Student s = StudentBL.studentexist(reg);

            if (s == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  [ERROR] The student does not exist in the system!");
                Console.ResetColor();
                Console.WriteLine("\n  Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            Room r = s.getroom();

            if (r == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n  [WARNING] Student {s.getname()} is not currently assigned to any room.");
                Console.ResetColor();
                Console.WriteLine("\n  Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n  --- Student Details ---");
            Console.ResetColor();
            Console.WriteLine($"  Name:     {s.getname()}");
            Console.WriteLine($"  Reg No:   {s.getid()}");
            Console.WriteLine($"  Semester: {s.getsem()}");
            Console.WriteLine($"  Room No:  {r.getroomnum()}");

            Console.Write("\n  Confirm check-out for this student? (Y/N): ");
            string confirmation = Console.ReadLine();

            if (confirmation.Trim().ToUpper() == "Y")
            {
                StudentBL.removesinglestudent(s, r);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n  [SUCCESS] Student successfully vacated from Room {r.getroomnum()}!");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("\n  [STATUS] Check-out cancelled.");
            }

            Console.WriteLine("\n  Press any key to return to the menu...");
            Console.ReadKey();
        }


    }
}

