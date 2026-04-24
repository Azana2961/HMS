using HMS.DL;
using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.BL
{
    class AdminBL
    {
        public static bool addrooms(int roomnum)
        {
            foreach(Room r in RoomBL.getallrooms())
            {
                if(r.getroomnum() == roomnum)
                {
                    return false;
                }
            }
            RoomDL.addroom(roomnum);
            return true;
        }
        public static bool setnewlimit(int newlimit)
        {
            foreach(Room r in RoomBL.getallrooms())
            {
                if(r.getroommates().Count > newlimit && r.getroommates() != null)
                {
                    return false;
                }
            }
            RoomBL.setrommatelimit(newlimit);
            return true;
        }
        
        
        

    }
}
