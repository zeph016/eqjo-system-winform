using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.JobOrder
{
    public class clsJOAuditTrail
    {
        public Int64 Id { get; set; }
        public Int64 JODetailId { get; set; }
        public String WorkDescription { get; set; }
        public String JONo { get; set; }
        public Int64 StatusId { get; set; }
        public String StatusName { get; set; }
        public String Remarks { get; set; }
        public DateTime StatusDateTime { get; set; }
        public Int64 UserId { get; set; }
        public String Username { get; set; }
        public clsJOAuditTrail()
        {
            Id = 0;
            JODetailId = 0;
            StatusId = 0;
            Remarks = "";
            StatusDateTime = DateTime.Now;
            UserId = 0;
            Username = "";
        }
        public clsJOAuditTrail(clsJOAuditTrail obj)
        {
            Id = obj.Id;
            JODetailId = obj.JODetailId;
            StatusId = obj.StatusId;
            Remarks = obj.Remarks;
            StatusDateTime = obj.StatusDateTime;
            UserId = obj.UserId;
            Username = obj.Username;
        }
    }
}
