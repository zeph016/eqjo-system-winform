using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.WorkAssignment
{
    public class clsWorkAssignments
    {
        public Int64 Id { get; set; }
        public DateTime DateEncoded { get; set; }
        public Boolean IsActive { get; set; }
        //JO Number
        public Int64 JORONo { get; set; }
        public clsEnums.TypeOrder ReferenceType { get; set; }
        public Int64 ReferenceNo { get; set; }
        public String ReferenceNoStr { get { return (ReferenceType == clsEnums.TypeOrder.JO ? "JO" : "RO") + RefYear.Substring(RefYear.Length - 2) + ReferenceNo.ToString("0000"); } }
        public String RefYear { get; set; }
        public clsEnums.EquipmentType EquipmentType { get; set; }
        public Int64 EquipmentId { get; set; }
        public String EquipmentCode { get; set; }
        public String EquipmentName { get; set; }
        public String Contractor { get; set; }
        public String ContractorSectionHead { get; set; }
        //JO Detail
        public Int64 JODetailId { get; set; }
        public String Section { get; set; }
        public String JobCategory { get; set; }
        public String JobType { get; set; }
        public String WorkDescription { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime TargetTime { get; set; }
        //Mechanic Detail
        public Int64 EmployeeId { get; set; }
        public String MechanicName { get; set; }
        public String Position { get; set; }
        
        public clsWorkAssignments()
        {
            Id = 0;
            JORONo = 0;
            JODetailId = 0;
            EmployeeId = 0;
            EquipmentId = 0;
            DateEncoded = DateTime.Now;
            IsActive = false;
        }
        public clsWorkAssignments(clsWorkAssignments obj)
        {
            Id = obj.Id;
            JORONo = obj.JORONo;
            JODetailId = obj.JODetailId;
            EmployeeId = obj.EmployeeId;
            EquipmentId = obj.EquipmentId;
            DateEncoded = obj.DateEncoded;
            IsActive = obj.IsActive;
        }
    }
}
