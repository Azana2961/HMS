using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Entities;
using HMS.BL;
using HMS.DL;
namespace HMS.BL
{
    class RequestBL
    {
        public static void requestroomchange(Student s, string r, int troom)
        {
            Request newrequest = new Request(s, r, troom, "Roomchange");
            RequestDL.AddRequests(newrequest);
        }
        public static void addvacaterequest(Student s, string r)
        {
            Request newrequest = new Request(s, r, "Vacante");
            RequestDL.AddRequests(newrequest);
        }
        public static void genralcomplain(Student s, string r)
        {
            Request newrequest = new Request(s, r, "General");
            RequestDL.AddRequests(newrequest);
        }
        public static List<Request> getallrequests()
        {
            return RequestDL.getRequests();
        }
        public static Request Requestexist(int id)
        {
            foreach(Request r in RequestDL.getRequests())
            {
                if(r.getrequestid()== id)
                {
                    return r;
                }
            }
            return null;
        }
        public static void updatestatus(Request r, String s)
        {
            r.setstatus(s);
        }
        public static void removerequestbystudent(Student s)
        {
            List<Request> allrequests = RequestDL.getRequests();
            for (int i = allrequests.Count - 1; i >= 0; i--)
            {
                Request r = allrequests[i];
                Student stu = r.getrequestor();

                if (stu.getid() == s.getid())
                {
                 RequestDL.removerequest(r);
                }
            }
        }
    }
}
