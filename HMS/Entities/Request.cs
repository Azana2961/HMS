using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
    class Request
    {
        private int requestid;
        private string reason;
        private Student requestor;
        private string status;
        private int targetroom;
        private string reqtype;

        public Request(Student s, string r, int troom, string reqtype)
        {
            requestor = s;
            reason = r;
            targetroom = troom;
            status = "Pending";
            this.reqtype = reqtype;
        }

        public Request(Student s, string r, string reqt)
        {
            requestor = s;
            reason = r;
            status = "Pending";
            reqtype = reqt;
        }

        public Request(int id, Student s, string r, int troom, string reqStatus, string reqtype)
        {
            requestid = id;
            requestor = s;
            reason = r;
            targetroom = troom;
            status = reqStatus;
            this.reqtype = reqtype;
        }
        public int getrequestid()
        { return requestid; 
        }
        public Student getrequestor() 
        { return requestor; 
        }
        public string getreason() 
        { return reason; 
        }
        public int gettargetroom() 
        { return targetroom;
        }
        public string getstatus() 
        { return status;
        }

        public void setstatus(string newStatus)
        {
            status = newStatus;
        }
        public string gettype()
        {
            return reqtype;
        }
    }
}
