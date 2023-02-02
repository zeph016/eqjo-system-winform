using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.ActualAdvance
{
    public class clsActualAdvanceDetails
    {
        public Int64 Id { get; set; }
        public Int64 EmployeeId { get; set; }
        public String Position { get; set; }
        public String Section { get; set; }
        public String EmployeeName { get; set; }
        public Int64 EncoderId { get; set; }
        public String EncoderName { get; set; }
        public DateTime? DateOfUpdate { get; set; }
        public Int64 BranchId { get; set; }
        public String BranchName { get; set; }
        public DateTime DateEncoded { get; set; }
        public Int64 AMStatus { get; set; }
        public String AMStatusStr { get; set; }
        public Int64 PMStatus { get; set; }
        public String PMStatusStr { get; set; }
        public String AttendanceRemarks { get; set; }

        public clsEnums.ActualAdvance Type { get; set; }

        public List<clsActualAdvanceReference> ListOfActualReference{get;set;}
        public List<clsActualAdvanceReference> ListOfAdvanceReference { get; set; }
        public clsActualAdvanceDetails()
        {
            Id = 0;
            EmployeeId = 0;
            EncoderId = 0;
            DateOfUpdate = null;
            BranchId = 0;
            DateEncoded = DateTime.Now;
            Type = clsEnums.ActualAdvance.Actual;
            AMStatus = 0;
            PMStatus = 0;
            AttendanceRemarks = "";
            ListOfActualReference = new List<clsActualAdvanceReference>();
            ListOfAdvanceReference = new List<clsActualAdvanceReference>();
        }
        public clsActualAdvanceDetails(clsActualAdvanceDetails obj)
        {
            Id = obj.Id;
            EmployeeId = obj.EmployeeId;
            EncoderId = obj.EncoderId;
            DateOfUpdate = obj.DateOfUpdate;
            BranchId = 0;
            DateEncoded = obj.DateEncoded;
            Type = obj.Type;
            AMStatus = 0;
            PMStatus = 0;
            AttendanceRemarks = "";
            ListOfActualReference = obj.ListOfActualReference;
            ListOfAdvanceReference = obj.ListOfAdvanceReference;
        }
    }
}
