using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Entities;
namespace HMS.DL
{
    class RoomDL 
    {
        private static int roommateslimit=3;
        private static List<Room> rooms = new List<Room>();
        public static void addroom(int num)
        {
            rooms.Add(new Room(num));
        }
        public static List<Room> getrooms()
        {
            return rooms;
        }
        public static int getroommateslimit()
        {
            return roommateslimit;
        }
        public static void setroommateslimit(int limit)
        {
            roommateslimit = limit;
        }
    }

}
