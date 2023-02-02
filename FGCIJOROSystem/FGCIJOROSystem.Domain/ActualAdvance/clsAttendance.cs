using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.ActualAdvance
{
    public class clsAttendance
    {
        public Int64 EmployeeId { get; set; }
        public String EmployeeName { get; set; }
        public String FirstName { get; set; }
        public string LastName { get; set; }
        public String NameExtension { get; set; }
        public Int64 SectionId { get; set; }
        public String Section { get; set; }
        public String AM { get; set; }
        public String PM { get; set; }
        public String Location { get; set; }
        public Int64 EquipmentId { get; set; }
        public String EquipmentCode { get; set; }
        public String Activity { get; set; }
        public String EstimatedTime { get; set; }
        public String ActualTimeWorked { get; set; }
        public String MinMaj { get; set; }
        public String Status { get; set; }
        public String Advance { get; set; }
        public DateTime DateOfUpdate { get; set; }
        public int URDays { get; set; }
        public Int64 EquipmentTypeId { get; set; }
        public String EquipmentTypeName { get; set; }
        public List<clsAttendance> ListOfAttendance { get; set; }
        public decimal TotalPresent
        {
            get
            {
                decimal _count = 0;
                if (AM == "P" && PM == "P")
                {
                    _count = 1; // dhaniele
                }
                else if (AM == "A" && PM == "A")
                {
                    _count = 0;
                }
                else if (AM == "AL" && PM == "AL")
                {
                    _count = 0;
                }
                else if (AM == "AL" && PM == "A")
                {
                    _count = 0;
                }
                else if (AM == "A" && PM == "AL")
                {
                    _count = 0;
                }
                else if (((AM == "A" || PM == "A") || (AM == "AL" || PM == "AL") || (AM == "" || PM == "")) && (AM == "P" || PM == "P")) //dhaniele
                {
                    _count = (decimal)0.5;
                }
                return _count;
            }
        }

        public decimal TotalAbsent
        {
            get
            {
                decimal _count = 0;
                if (AM == "A" && PM == "A" )
                {
                    _count = 1;
                }
                else if (AM == "AL" && PM == "AL")
                {
                    _count = 1;
                }
                else if (AM == "AL" && PM == "A")
                {
                    _count = 1;
                }
                else if (AM == "A" && PM == "AL")
                {
                    _count = 1;
                }
                else if (AM == "P" && PM == "P")
                {
                    _count = 0;
                }
                else if (AM == "P" || PM == "P")
                {
                    _count = (decimal)0.5;
                }
                return _count;
            }
        }
        public String NoOfDays { get; set; }
        public clsAttendance()
        {
            EmployeeId = 0;
            EmployeeName = "";
            SectionId = 0;
            Section = "";
            AM = "";
            PM = "";
            Location = "";
            EquipmentId = 0;
            EquipmentCode = "";
            Activity = "";
            EstimatedTime = "";
            MinMaj = "";
            ActualTimeWorked = "";
            MinMaj = "";
            Status = "";
            Advance = "";
            DateOfUpdate = System.DateTime.Now;
            URDays = 0;
            EquipmentTypeId = 0;
            EquipmentTypeName = "";
            ListOfAttendance = new List<clsAttendance>();
        }
        public clsAttendance(clsAttendance obj)
        {
            EmployeeId = obj.EmployeeId;
            EmployeeName = obj.EmployeeName;
            SectionId = obj.SectionId;
            Section = obj.Section;
            AM = obj.AM;
            PM = obj.PM;
            Location = obj.Location;
            EquipmentId = obj.EquipmentId;
            EquipmentCode = obj.EquipmentCode;
            Activity = obj.Activity;
            EstimatedTime = obj.EstimatedTime;
            MinMaj = obj.MinMaj;
            Status = obj.Status;
            Advance = obj.Advance;
            DateOfUpdate = obj.DateOfUpdate;
            URDays = obj.URDays;
            EquipmentTypeId = obj.EquipmentTypeId;
            EquipmentTypeName = obj.EquipmentTypeName;
            ListOfAttendance = obj.ListOfAttendance;
        }
    }
}
