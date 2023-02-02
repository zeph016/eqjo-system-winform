using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Monitoring
{
    public class clsMonitoring
    {
        public String EmployeeName { get; set; }
        public String Section { get; set; }
        public String Position { get; set; }
        public DateTime ? DateOfUpdate { get; set; }
        public Int64 ReferenceNo { get; set; }
        public String ReferenceNoStr { get { return (ReferenceNo != 0 ? ((ReferenceType == clsEnums.TypeOrder.JO ? "JO" : "RO") + RefYear.Substring(RefYear.Length - 2) + ReferenceNo.ToString("0000")) : ""); } }
        public String RefYear { get; set; }
        public clsEnums.TypeOrder ReferenceType { get; set; }
        public String EquipmentName { get; set; }
        public Int64 JODetailId { get; set; }
        public DateTime ? EffectiveDate { get; set; }
        public DateTime ? TargetDate { get; set; }
        public Int64 WorkTimeSpan { get; set; }
        public String Unit { get; set; }
        public String WorkDescription { get; set; }
        public Decimal WorkPercentage { get; set; }
        public String Status { get; set; }
        public List<clsMonitoring> ListOfMonitoring { get; set; }
        public clsMonitoring()
        {
            EmployeeName = "";
            Section = "";
            Position = "";
            DateOfUpdate = null;
            EquipmentName = "";
            ReferenceNo = 0;
            ReferenceType = clsEnums.TypeOrder.JO;
            JODetailId = 0;
            EffectiveDate = null;
            TargetDate = null;
            WorkTimeSpan = 0;
            Unit = "";
            WorkDescription = "";
            WorkPercentage = 0;
            Status = "";
            ListOfMonitoring = new List<clsMonitoring>();
        }
        public clsMonitoring(clsMonitoring obj)
        {
            EmployeeName = obj.EmployeeName;
            Section = obj.Section;
            Position = obj.Position;
            DateOfUpdate = obj.DateOfUpdate;
            EquipmentName = obj.EquipmentName;
            ReferenceNo = obj.ReferenceNo;
            ReferenceType = obj.ReferenceType;
            JODetailId = obj.JODetailId;
            EffectiveDate = obj.EffectiveDate;
            TargetDate = obj.TargetDate;
            WorkTimeSpan = obj.WorkTimeSpan;
            Unit = obj.Unit;
            WorkDescription = obj.WorkDescription;
            WorkPercentage = obj.WorkPercentage;
            Status = obj.Status;
            ListOfMonitoring = obj.ListOfMonitoring;
        }
    }
}
