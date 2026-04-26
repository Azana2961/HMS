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
        private static List<Request> Requests = new List<Request>();
        public static List<Request> getRequests()
        {
            return Requests;
        }
        public static void AddRequests(Request r)
        {
            Requests.Add(r);
        }
        public static void removerequest(Request r)
        {
            Requests.Remove(r);
        }
    }

}
