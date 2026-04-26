using HMS.BL;
using HMS.DL;
using HMS.Entities;
using HMS.UI;
using System;

namespace HMS
{
    class Program
    {
        static void Main(string[] args)
        {
            RoomDL.load();
            StudentDL.load();
            EmployeeDL.load();
            RequestDL.load();
            MainUI.MainLoginMenu();
        }
    }
}