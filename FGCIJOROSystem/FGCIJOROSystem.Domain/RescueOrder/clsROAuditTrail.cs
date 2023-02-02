using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.RescueOrder
{
    public class clsROAuditTrail
    {
        public Int64 Id { get; set; }
        public Int64 RODetailId { get; set; }
        public String WorkDescription { get; set; }
        public String RONo { get; set; }
        public Int64 StatusId { get; set; }
        public String StatusName { get; set; }
        public String Remarks { get; set; }
        public DateTime StatusDateTime { get; set; }
        public Int64 UserId { get; set; }
        public String Username { get; set; }
        public clsROAuditTrail()
        {
            Id = 0;
            RODetailId = 0;
            StatusId = 0;
            Remarks = "";
            StatusDateTime = DateTime.Now;
            UserId = 0;
            Username = "";
        }
        public clsROAuditTrail(clsROAuditTrail obj)
        {
            Id = obj.Id;
            RODetailId = obj.RODetailId;
            StatusId = obj.StatusId;
            Remarks = obj.Remarks;
            StatusDateTime = obj.StatusDateTime;
            UserId = obj.UserId;
            Username = obj.Username;
        }
    }
}
