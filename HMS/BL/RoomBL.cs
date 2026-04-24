using HMS.DL;
using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HMS.BL
{
    class RoomBL
    {
        public static void roomnumberalloter()
        {
            foreach (Room r in RoomDL.getrooms())
            {
                foreach (Student s in r.getroommates())
                {
                    s.setroom(r);
                }
            }
        }
        public static Room avialibiltycheck(int num)
        {
            foreach (Room r in RoomDL.getrooms())
            {
                if (r.getroomnum() == num)
                {
                    return r;
                }
            }
            return null;
        }
        public static List<Room> getavailablerooms()
        {
            List<Room> roomsavailabel = new List<Room>();
            foreach (Room r in RoomDL.getrooms())
            {
                if (RoomDL.getroommateslimit() > r.getroommates().Count)
                {
                    roomsavailabel.Add(r);
                }
            }
            return roomsavailabel;
        }
        public static int getroommatelimit()
        {
            return RoomDL.getroommateslimit();
        }
        public static void setrommatelimit(int num)
        {
            RoomDL.setroommateslimit(num);
        }
        public static List<Room> getallrooms()
        {
            return RoomDL.getrooms();
        }
        public static int roomcount()
        {
            return RoomDL.getrooms().Count;
        }
        public static void addroom(int roomnum)
        {
            RoomDL.addroom(roomnum);
        }
        public static Room getroombyid(int id)
        {
            foreach(Room r in getallrooms())
            {
                if(r.getroomnum() == id)
                {
                    return r;
                }
            }
            return null;
        }
        public static void changeroom(Student s, int newRoomId)
        {
            Room currentRoom = s.getroom();
            if (currentRoom != null)
            {
                currentRoom.removerommate(s);
            }

            Room newRoom = getroombyid(newRoomId);
            if (newRoom != null)
            {
                s.setroom(newRoom);
                newRoom.addroommate(s);
            }
        }
    }
}
