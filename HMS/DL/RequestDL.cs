using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DL
{
    class RequestDL
    {
        public static List<Request> Requests = new List<Request>();
        public static void removerequest(Request r)
        {
            Requests.Remove(r);
        }
    }

}
