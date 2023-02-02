using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.EquipmentReleasal
{
    public class clsROReleasalTransaction
    {
        public Int64 Id { get; set; }
        public Int64 ERNo { get; set; }
        public Int64 BranchId { get; set; }
        public String BranchName { get; set; }
        public String ERNoStr { get { return "ER" + ERNo.ToString("000"); } }        
        public DateTime DateReleased { get; set; }
        public Int64 UserId { get; set; }
        public String Username { get; set; }
        public Int64 ApproverId { get; set; }
        public String ApproverName { get; set; }
        public List<clsROReleased> ListOfROReleased { get; set; }
        public clsROReleasalTransaction()
        {
            Id = 0;
            BranchId = 0;
            ERNo = 0;
            DateReleased = DateTime.Now;
            UserId = 0;
            ApproverId = 0;
            ListOfROReleased = new List<clsROReleased>();
        }
        public clsROReleasalTransaction(clsROReleasalTransaction obj)
        {
            Id = obj.Id;
            BranchId = obj.BranchId;
            ERNo = obj.ERNo;
            DateReleased = obj.DateReleased;
            UserId = obj.UserId;
            ApproverId = obj.ApproverId;
            ListOfROReleased = obj.ListOfROReleased;
        }
    }
}
