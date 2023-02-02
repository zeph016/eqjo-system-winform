using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.ActualAdvance
{
    public class clsActualAdvanceReference
    {
        public Int64 Id { get; set; }
        public Int32 DummyId { get; set; }
        public Int64 EmployeeId { get; set; }
        public Int64 ActualAdvanceId  { get; set; }
        public clsEnums.TypeOrder ReferenceType { get; set; }
        public Int64 ReferenceNo { get; set; }
        public String ReferenceNoStr { get { return (ReferenceNo != 0 ? ((ReferenceType == clsEnums.TypeOrder.JO ? "JO" : "RO") + RefYear.Substring(RefYear.Length - 2) + ReferenceNo.ToString("0000")) : ""); } } 
        public String RefYear { get; set; }
        public clsEnums.EquipmentType EquipmentType { get; set; }
        public Int64 EquipmentId { get; set; }
        public String EquipmentName { get; set; }
        public String Activity { get; set; }
        public Decimal WorkPercentage { get; set; }
        public String Remarks { get; set; }
        public Int64 EncoderId { get; set; }
        public String EncoderName { get; set; }
        public DateTime DateOfUpdate { get; set; }
        public DateTime TimeStarted { get; set; }
        public DateTime TimeEnded { get; set; }
        public Int64 BranchId { get; set; }
        public String BranchName { get; set; }
        public DateTime DateEncoded { get; set; }
        public clsEnums.ActualAdvance Type { get; set; }
        public Int64 JODetailId { get; set; }
        public clsActualAdvanceReference()
        {
            Id = 0;
            EmployeeId = 0;
            ReferenceType = clsEnums.TypeOrder.JO;
            ReferenceNo = 0;
            ActualAdvanceId = 0;
            EquipmentType = clsEnums.EquipmentType.Equipment;
            EquipmentId = 0;
            Activity = "";
            Remarks = "";
            WorkPercentage = 0;
            EncoderId = 0;
            DateOfUpdate = DateTime.Now;
            TimeStarted = DateTime.Now;
            TimeEnded = DateTime.Now;
            BranchId = 0;
            DateEncoded = DateTime.Now;
            Type = clsEnums.ActualAdvance.Actual;
            JODetailId = 0;
        }
        public clsActualAdvanceReference(clsActualAdvanceReference obj)
        {
            Id = 0;
            EmployeeId = obj.EmployeeId;
            ReferenceType = obj.ReferenceType;
            ReferenceNo = 0;
            ActualAdvanceId = 0;
            ReferenceType = obj.ReferenceType;
            ReferenceNo = obj.ReferenceNo;
            EquipmentType = obj.EquipmentType;
            EquipmentId = obj.EquipmentId;
            EquipmentName = obj.EquipmentName;
            Activity = obj.Activity;
            Remarks = obj.Remarks;
            WorkPercentage = obj.WorkPercentage;
            EncoderId = obj.EncoderId;
            DateOfUpdate = obj.DateOfUpdate;
            TimeStarted = obj.TimeStarted;
            TimeEnded = obj.TimeEnded;
            BranchId = 0;
            DateEncoded = obj.DateEncoded;
            Type = obj.Type;
            JODetailId = obj.JODetailId;
        }
    }
}
