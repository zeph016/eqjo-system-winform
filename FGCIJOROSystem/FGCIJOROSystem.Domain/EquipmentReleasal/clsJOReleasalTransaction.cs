using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Domain.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.EquipmentReleasal
{
    public class clsJOReleasalTransaction
    {
        public Int64 Id { get; set; }
        public Int64 BranchId { get; set; }
        public String BranchName { get; set; }
        public Int64 ERNo { get; set; }
        public String ERNoStr { get { return "ER" + ERNo.ToString("000"); } }        
        public DateTime DateReleased { get; set; }
        public Int64 UserId { get; set; }
        public String Username { get; set; }
        public Int64 ApproverId { get; set; }
        public String ApproverName { get; set; }
        public clsEnums.JOROStatus Status { get; set; }
        public List<clsJOReleased> ListOfJOReleased { get; set; }
        public List<clsEquipment> ListOfEquipments { get; set; }
        public clsJOReleasalTransaction()
        {
            Id = 0;
            BranchId = 0;
            ERNo = 0;
            DateReleased = DateTime.Now;
            UserId = 0;
            ApproverId = 0;
            Status = clsEnums.JOROStatus.Released;
            ListOfJOReleased = new List<clsJOReleased>();
            ListOfEquipments = new List<clsEquipment>();
        }
        public clsJOReleasalTransaction(clsJOReleasalTransaction obj)
        {
            Id = obj.Id;
            BranchId = obj.BranchId;
            ERNo = obj.ERNo;
            DateReleased = obj.DateReleased;
            UserId = obj.UserId;
            ApproverId = obj.ApproverId;
            Status = obj.Status;
            ListOfJOReleased = obj.ListOfJOReleased;
            ListOfEquipments = obj.ListOfEquipments;
        }
    }
}
