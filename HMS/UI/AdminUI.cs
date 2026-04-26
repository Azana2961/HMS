using HMS.BL;
using HMS.DL;
using HMS.Entities; 
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace HMS.UI
{
    class AdminUI
    {
        public static void ShowAdminMenu()
        {
            bool logout = false;

            while (!logout)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("================================================================================");
                Console.WriteLine("  HMS v1.0 | Admin (Warden) Dashboard");
                Console.WriteLine("================================================================================\n");
                Console.ResetColor();

                Console.WriteLine("  [1] Add New Rooms to Hostel Infrastructure");
                Console.WriteLine("  [2] Set Global Roommate Limits");
                Console.WriteLine("  [3] Batch Student Registration");
                Console.WriteLine("  [4] Process Batch Room Allocation");
                Console.WriteLine("  [5] Process Batch Student Vacate (End of Semester)");
                Console.WriteLine("  [6] Manage Employee Accounts");
                Console.WriteLine("  ------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  [0] Logout\n");
                Console.ResetColor();

                Console.Write("  Select an operation (0-6): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        logout = true;
                        break;
                    case "1":
                        AddRooms();
                        Console.ReadKey();
                        break;
                    case "2":
                        setnewlimit();
                        Console.ReadKey();
                        break;
                    case "3":
                     addstudents();
                        break;
                    case "4":
                        allocationbatch();
                        break;
                    case "5":
                        checkoutbatch();
                        break;
                    case "6":
                        ManageEmployees();
                        Console.ReadKey();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n  [ERROR] Invalid choice. Please select an option between 0 and 6.");
                        Console.ResetColor();
                        Console.WriteLine("  Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public static void AddRooms()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | Add Hostel Infrastructure (Rooms)");
            Console.WriteLine("================================================================================\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  --- Current Rooms in System ---");
            Console.ResetColor();

            List<Room> currentRooms = RoomBL.getallrooms();

            if (currentRooms == null || currentRooms.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  [No rooms currently exist. The hostel infrastructure is empty.]\n");
                Console.ResetColor();
            }
            else
            {
                Console.Write("  ");
                for (int i = 0; i < currentRooms.Count; i++)
                {
                    Console.Write($"[{currentRooms[i].getroomnum()}]  ");

                    if ((i + 1) % 10 == 0)
                    {
                        Console.WriteLine();
                        Console.Write("  ");
                    }
                }
                Console.WriteLine("\n");
            }

            Console.WriteLine("  [1] Add a Single Room");
            Console.WriteLine("  [2] Add a Range of Rooms (Batch Add)");
            Console.WriteLine("  ------------------------------------------------------------------");
            Console.Write("  Select an option (1-2): ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                bool addMore = true;

                while (addMore)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n  --- Enter New Room Details ---");
                    Console.ResetColor();

                    int roomNum = 0;
                    bool validRoom = false;

                    while (!validRoom)
                    {
                        Console.Write("  Enter Room Number (e.g., 101): ");
                        string input = Console.ReadLine();

                        if (int.TryParse(input, out roomNum) && roomNum > 0)
                        {
                            validRoom = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("  [ERROR] Invalid input. Please enter a valid positive number.\n");
                            Console.ResetColor();
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("  Processing room creation...");
                    Console.ResetColor();

                    bool isAdded = AdminBL.addrooms(roomNum);

                    if (isAdded)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"  [SUCCESS] Room {roomNum} has been successfully added to the hostel!");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"  [ERROR] Room {roomNum} already exists in the system.");
                        Console.ResetColor();
                    }

                    Console.Write("\n  Would you like to add another room? (Y/N): ");
                    string cont = Console.ReadLine().Trim().ToUpper();

                    if (cont != "Y")
                    {
                        addMore = false;
                    }
                }
            }
            else if (choice == "2")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n  --- Enter Room Range ---");
                Console.ResetColor();

                int startRoom = 0;
                int endRoom = 0;

                Console.Write("  Enter Starting Room Number (e.g., 101): ");
                int.TryParse(Console.ReadLine(), out startRoom);

                Console.Write("  Enter Ending Room Number (e.g., 150): ");
                int.TryParse(Console.ReadLine(), out endRoom);

                if (startRoom > 0 && endRoom >= startRoom)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("\n  Processing batch creation...");
                    Console.ResetColor();

                    int addedCount = 0;
                    int skippedCount = 0;

                    for (int i = startRoom; i <= endRoom; i++)
                    {
                        bool isAdded = AdminBL.addrooms(i);
                        if (isAdded)
                        {
                            addedCount++;
                        }
                        else
                        {
                            skippedCount++;
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"  [SUCCESS] {addedCount} rooms successfully generated and added.");
                    Console.ResetColor();

                    if (skippedCount > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"  [INFO] {skippedCount} rooms were skipped because they already exist.");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  [ERROR] Invalid range. Ensure numbers are positive and the ending number is greater than the starting number.");
                    Console.ResetColor();
                }
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
        public static void setnewlimit()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | Set Global Roommate Limit");
            Console.WriteLine("================================================================================\n");
            Console.ResetColor();

            int currentLimit = RoomBL.getroommatelimit();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"  [ Current Global Roommate Limit: {currentLimit} Students per Room ]\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  --- Update Room Capacity ---");
            Console.ResetColor();

            int newLimit = 0;
            bool validLimit = false;

            while (!validLimit)
            {
                Console.Write("  Enter the new maximum students per room: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out newLimit) && newLimit > 0)
                {
                    validLimit = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  [ERROR] Invalid input. Please enter a valid positive number.\n");
                    Console.ResetColor();
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n  Validating current room occupancies against the new limit...");
            Console.ResetColor();

            bool isSuccessful = AdminBL.setnewlimit(newLimit);

            if (isSuccessful)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n  [SUCCESS] Global roommate limit successfully updated to {newLimit}!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  [ERROR] Cannot lower the limit to {newLimit}.");
                Console.WriteLine("  One or more rooms currently have more occupants than this new limit.");
                Console.WriteLine("  You must vacate students from overcrowded rooms before lowering the capacity.");
                Console.ResetColor();
            }

            Console.WriteLine("\n  Press any key to return to the menu...");
            Console.ReadKey();
        }
        public static void allocationbatch()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | Batch Room Allocation");
            Console.WriteLine("================================================================================\n");
            Console.ResetColor();

            if (RoomBL.roomcount() == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  [ERROR] The Admin has not entered any Rooms!");
                Console.ResetColor();
                Console.WriteLine("\n  Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            if (StudentBL.studentcount() == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  [ERROR] There are no students in the system.");
                Console.ResetColor();
                Console.WriteLine("\n  Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            int unassignedCount = 0;
            foreach (Student s in StudentBL.viewallstudents())
            {
                if (s.getroom() == null)
                {
                    unassignedCount++;
                }
            }

            if (unassignedCount == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  [STATUS] All registered students already have rooms allocated.");
                Console.ResetColor();
                Console.WriteLine("\n  Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            int availableBeds = 0;
            int currentLimit = RoomBL.getroommatelimit();

            foreach (Room r in RoomBL.getallrooms())
            {
                int occupants = 0;
                if (r.getroommates() != null)
                {
                    occupants = r.getroommates().Count;
                }

                if (currentLimit > occupants)
                {
                    availableBeds += (currentLimit - occupants);
                }
            }

            if (availableBeds == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  [ERROR] The hostel is currently at maximum capacity. No beds available.");
                Console.ResetColor();
                Console.WriteLine("\n  Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  --- Allocation Overview ---");
            Console.ResetColor();
            Console.WriteLine($"  Students waiting for rooms : {unassignedCount}");
            Console.WriteLine($"  Total available beds       : {availableBeds}");

            if (unassignedCount > availableBeds)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  [WARNING] Rooms are less than students. {unassignedCount - availableBeds} student(s) will not get a room.");
                Console.ResetColor();
            }

            Console.Write("\n  Press Enter to process batch allocation...");
            Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  Processing allocation...");
            Console.ResetColor();

            StudentBL.addstudntsbatch(currentLimit, unassignedCount);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n  [SUCCESS] Batch allocation process completed successfully!");
            Console.ResetColor();

            Console.WriteLine("\n  Press any key to return to the menu...");
            Console.ReadKey();
        }
        public static void addstudents()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | Batch Student Registration");
            Console.WriteLine("================================================================================\n");
            Console.ResetColor();

            int studentCount = 0;
            bool validCount = false;
            while (!validCount)
            {
                Console.Write("  How many students would you like to register? ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out studentCount) && studentCount > 0)
                {
                    validCount = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  [ERROR] Please enter a valid number greater than 0.\n");
                    Console.ResetColor();
                }
            }

            for (int i = 1; i <= studentCount; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n  --- Entering details for Student {i} of {studentCount} ---");
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
                        Console.WriteLine($"  [ERROR] Student Already exists with registration '{regNum}'!\n");
                        Console.ResetColor();
                    }
                    else
                    {
                        validReg = true;
                    }
                }

                string name = "";
                bool validName = false;

                while (!validName)
                {
                    Console.Write("  Full Name: ");
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
                string pass = "";
                bool validPass = false;

                while (!validPass)
                {
                    Console.Write("  Assign a password (min 8 chars, no spaces): ");
                    pass = Console.ReadLine();

                    if (ValidationBL.IsValidPassword(pass))
                    {
                        validPass = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("  [ERROR] Invalid password. Must be at least 8 characters with no spaces.\n");
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

                StudentBL.addsinglestudent(regNum, name, semester, pass);
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n  Processing registration batch...");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  [SUCCESS] {studentCount} students have been securely routed to the system!");
            Console.ResetColor();

            Console.WriteLine("\n  Press any key to return to the menu...");
            Console.ReadKey();
        }
        public static void checkoutbatch()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | Batch Student Check-Out (By Semester)");
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
            int sem = 0;
            bool validSem = false;

            while (!validSem)
            {
                Console.Write("  Enter the semester to vacate (1-8): ");
                if (int.TryParse(Console.ReadLine(), out sem) && sem >= 1 && sem <= 8)
                {
                    validSem = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  [ERROR] Invalid input. Please enter a number between 1 and 8.\n");
                    Console.ResetColor();
                }
            }

            List<Student> s = StudentBL.getstudentsofsem(sem);

            if (s == null || s.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n  [STATUS] No students currently found in semester {sem}.");
                Console.ResetColor();
                Console.WriteLine("\n  Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n  --- Available students of semester {sem} ---");
            Console.ResetColor();

            foreach (Student student in s)
            {
                Console.WriteLine($"  Name: {student.getname(),-15} | Reg No: {student.getid(),-15} | Sem: {student.getsem()}");
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"\n  WARNING: Are you sure you want to completely vacate all {s.Count} students? (Y/N): ");
            Console.ResetColor();

            string confirmation = Console.ReadLine();

            if (confirmation.Trim().ToUpper() == "Y")
            {
                StudentBL.remstudentsofsem(sem);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n  [SUCCESS] All students from semester {sem} have been vacated and removed!");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("\n  [STATUS] Batch check-out cancelled.");
            }

            Console.WriteLine("\n  Press any key to return to the menu...");
            Console.ReadKey();
        }
        public static void ManageEmployees()
        {
            bool back = false;

            while (!back)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("================================================================================");
                Console.WriteLine("  HMS v1.0 | Manage Employee Accounts");
                Console.WriteLine("================================================================================\n");
                Console.ResetColor();

                Console.WriteLine("  [1] View All Active Employees");
                Console.WriteLine("  [2] Register New Employee Account");
                Console.WriteLine("  [3] Remove/Revoke Employee Access");
                Console.WriteLine("  [4] Update Employee Details");
                Console.WriteLine("  ------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  [0] Back to Admin Dashboard\n");
                Console.ResetColor();

                Console.Write("  Select an operation (0-3): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        back = true;
                        break;
                    case "1":
                        ViewEmployees();
                        Console.ReadKey();
                        break;
                    case "2":
                        AddNewEmployee();
                        Console.ReadKey();
                        break;
                    case "3":
                        RemoveEmployee();
                        Console.ReadKey();
                        break;
                    case "4":
                        UpdateEmployee();
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
        public static void ViewEmployees()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | Active Employee Roster");
            Console.WriteLine("================================================================================\n");
            Console.ResetColor();

            DisplayEmployeeTable();

            Console.WriteLine("\n  Press any key to return to the menu...");
            Console.ReadKey();
        }
        private static bool DisplayEmployeeTable()
        {
            List<Employee> employees = EmployeeBL.getemployees();

            if (employees == null || employees.Count <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  [STATUS] There are currently no active employees.");
                Console.ResetColor();
                return false; 
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  ----------------------------------------");
            Console.WriteLine(String.Format("  | {0,-20} | {1,-10} |", "Employee Name", "ID"));
            Console.WriteLine("  ----------------------------------------");
            Console.ResetColor();

            foreach (Employee e in employees)
            {
                if (e.getstatus() == true && e.getrole()== "Front Desk")
                Console.WriteLine(String.Format("  | {0,-20} | {1,-10} |", e.getname(), e.getemployeeid()));
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  ----------------------------------------\n");
            Console.ResetColor();

            return true; 
        }
        public static void AddNewEmployee()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | Register New Employee");
            Console.WriteLine("================================================================================\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  --- Enter Employee Details ---");
            Console.ResetColor();

            bool namevalid = false;
            string name = "";

            while (!namevalid)
            {
                Console.Write("  Enter the name: ");
                name = Console.ReadLine();

                if (!ValidationBL.IsValidName(name))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  [ERROR] Invalid name. Please use only letters and spaces.\n");
                    Console.ResetColor();
                }
                else
                {
                    namevalid = true;
                }
            }

            bool passvalid = false;
            string pass = "";

            while (!passvalid)
            {
                Console.Write("  Assign the password (min 8 chars, no spaces): ");
                pass = Console.ReadLine();

                if (!ValidationBL.IsValidPassword(pass))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  [ERROR] Invalid password. Must be at least 8 characters with no spaces.\n");
                    Console.ResetColor();
                }
                else
                {
                    passvalid = true;
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n  Processing registration...");
            Console.ResetColor();

            EmployeeBL.addemployee(name, pass);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  [SUCCESS] Employee '{name}' has been successfully registered!");
            Console.ResetColor();

            Console.WriteLine("\n  Press any key to return to the menu...");
            Console.ReadKey();
        }
        public static void RemoveEmployee()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | Revoke Employee Access");
            Console.WriteLine("================================================================================\n");
            Console.ResetColor();

            bool hasData = DisplayEmployeeTable();

            if (!hasData)
            {
                Console.WriteLine("  Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            int idToRemove = 0;
            bool validId = false;

            while (!validId)
            {
                Console.Write("  Enter the ID of the employee to remove: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out idToRemove) && idToRemove > 0)
                {
                    validId = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  [ERROR] Invalid ID. Please enter a valid number from the table.\n");
                    Console.ResetColor();
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n  Processing removal request...");
            Console.ResetColor();

            bool isRemoved = EmployeeBL.RemoveEmployeeById(idToRemove);

            if (isRemoved)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n  [SUCCESS] Employee with ID {idToRemove} has been successfully removed.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  [ERROR] Employee with ID {idToRemove} was not found.");
                Console.ResetColor();
            }

            Console.WriteLine("\n  Press any key to return to the menu...");
            Console.ReadKey();
        }
        public static void UpdateEmployee()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine("  HMS v1.0 | Update Employee Details");
            Console.WriteLine("================================================================================\n");
            Console.ResetColor();

            bool hasData = DisplayEmployeeTable();

            if (!hasData)
            {
                Console.WriteLine("  Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  --- Select Employee ---");
            Console.ResetColor();

            int eid = 0;
            bool validId = false;

            while (!validId)
            {
                Console.Write("  Enter Employee ID to update: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out eid) && eid > 0)
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

            if (EmployeeBL.employeecheck(eid))
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

                string pass = "";
                bool validPass = false;

                while (!validPass)
                {
                    Console.Write("  Enter the new password (min 8 chars, no spaces): ");
                    pass = Console.ReadLine();

                    if (ValidationBL.IsValidPassword(pass))
                    {
                        validPass = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("  [ERROR] Invalid password. Must be at least 8 characters with no spaces.\n");
                        Console.ResetColor();
                    }
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\n  Processing update request...");
                Console.ResetColor();

                EmployeeBL.updateemployee(eid, name, pass);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n  [SUCCESS] Employee ID {eid} has been successfully updated!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  [ERROR] Employee ID {eid} does not exist in the system.");
                Console.ResetColor();
            }

            Console.WriteLine("\n  Press any key to return to the menu...");
            Console.ReadKey();
        }

    }
}