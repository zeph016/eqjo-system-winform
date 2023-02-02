using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.EquipmentReleasal
{
    public class clsROReleased
    {
        public Int64 Id { get; set; }
        public Int64 ROId { get; set; }
        public Int64 RONo { get; set; }
        public String RONoStr { get { return "RO" + RefYear.Substring(RefYear.Length - 2) + RONo.ToString("0000"); } }
        public String RefYear { get; set; }
        public clsEnums.EquipmentType ItemType { get; set; }
        public Int64 EquipmentId { get; set; }
        public String EquipmentName { get; set; }
        public String EquipmentCode { get; set; }
        public Int64 ROTransReleasalId { get; set; }
        public Boolean Active { get; set; }
        public clsEnums.JOROStatus Status { get; set; }
        public clsROReleased()
        {
            Id = 0;
            ROId = 0;
            RONo = 0;
            RefYear = DateTime.Now.Year.ToString();
            ROTransReleasalId = 0;
            ItemType = clsEnums.EquipmentType.Equipment;
            EquipmentId = 0;
            Status = clsEnums.JOROStatus.Completed;
            Active = true;
        }
        public clsROReleased(clsROReleased obj)
        {
            Id = obj.Id;
            ROId = obj.ROId;
            RONo = obj.RONo;
            RefYear = obj.RefYear;
            ROTransReleasalId = obj.ROTransReleasalId;
            ItemType = obj.ItemType;
            EquipmentId = obj.EquipmentId;
            Status = obj.Status;
            Active = obj.Active;
        }
    }
}
