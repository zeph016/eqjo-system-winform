using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.EquipmentReleasal
{
    public class clsJOReleased
    {
        public Int64 Id { get; set; }
        public Int64 JOId { get; set; }
        public Int64 JONo { get; set; }
        public String JONoStr { get { return  "JO" + RefYear.Substring(RefYear.Length - 2) + JONo.ToString("0000"); } }
        public String RefYear { get; set; }

        public clsEnums.EquipmentType ItemType { get; set; }
        public Int64 EquipmentId { get; set; }
        public String EquipmentName { get; set; }
        public String EquipmentCode { get; set; }
        public Int64 JOTransReleasalId { get; set; }
        public Boolean IsActive { get; set; }
        public clsEnums.JOROStatus Status { get; set; }
        public clsJOReleased()
        {
            Id = 0;
            JOId = 0;
            JONo = 0;
            JOTransReleasalId = 0;
            ItemType = clsEnums.EquipmentType.Equipment;
            EquipmentId = 0;
            Status = clsEnums.JOROStatus.Completed;
        }
        public clsJOReleased(clsJOReleased obj)
        {
            Id = obj.Id;
            JOId = obj.JOId;
            JONo = obj.JONo;
            JOTransReleasalId = obj.JOTransReleasalId;
            ItemType = obj.ItemType;
            EquipmentId = obj.EquipmentId;
            Status = obj.Status;
        }
    }
}