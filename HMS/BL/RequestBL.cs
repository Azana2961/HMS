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
            RequestDL.Requests.Add(newrequest);
        }
        public static void addvacaterequest(Student s, string r)
        {
            Request newrequest = new Request(s, r, "Vacante");
            RequestDL.Requests.Add(newrequest);
        }
        public static void genralcomplain(Student s, string r)
        {
            Request newrequest = new Request(s, r, "General");
            RequestDL.Requests.Add(newrequest);
        }
        public static List<Request> getallrequests()
        {
            return RequestDL.Requests;
        }
        public static Request Requestexist(int id)
        {
            foreach(Request r in RequestDL.Requests)
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
            foreach(Request r in RequestDL.Requests)
            {
                Student stu = r.getrequestor();
                if(stu.getid() == s.getid())
                {
                    RequestDL.removerequest(r);
                }
            }
        }
    }
}
