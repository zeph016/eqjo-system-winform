using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Reports
{
    public class clsJOROReports

    {
        public Boolean CheckEquipment { get; set; }
        //JO
        public Int64 Id { get; set; }
        public Int64 JONo { get; set; }
        public String JONoStr { get { return (Type == clsEnums.TypeOrder.JO ? "JO" : "RO") + RefYear.Substring(RefYear.Length - 2) + JONo.ToString("0000"); } }
        public String RefYear { get; set; }
        public DateTime JODate { get; set; }
        public Boolean EquipmentOnBranch { get; set; }
        public Boolean PartsRequest { get; set; }
        public clsEnums.TypeOrder Type { get; set; }
        //RO
        //public Int64 RONo { get; set; }
        ////public String RONoStr { get { return (Type == clsEnums.TypeOrder.JO ? "JO" : "RO") + RONo.ToString("0000"); } }
        //public DateTime RODate { get; set; }

        //Customer
        public clsEnums.CustomerType CustomerType { get; set; }
        public Int64 CustomerId { get; set; }
        public String CustomerName { get; set; }
        public String Location { get; set; }
        public String CustomerRemarks { get; set; }

        //Equipment
        public clsEnums.EquipmentType ItemType { get; set; }
        public Int64 EquipmentId { get; set; }
        public String EquipmentCode { get; set; }
        public String EquipmentName { get; set; }
        public String EquipmentLocation { get; set; }
        public String EquipmentRemarks { get; set; }

        //Contractor
        public Int64 ContractorId { get; set; }
        public clsEnums.ContractorCategory ContractorCategory { get; set; }
        public clsEnums.ContractorType ContractorType { get; set; }
        public String ContractorName { get; set; }
        public String ContractorSectionHead { get; set; }

        //Drivers
        public Int64 DriverId { get; set; }
        public String DriverName { get; set; }

        //Status
        public clsEnums.JOROStatus Status { get; set; }
        //public String StatusName { get; set; }

        //List of JOROReports
        public List<clsJOROReports> ListOfJOROReports { get; set; }

        public Int64 UserId { get; set; }
        public String EncoderName { get; set; }
        public String EncoderPosition { get; set; }
        
        //JOB DESCRIPTION
        public String WorkDescription { get; set; }
        public Decimal Amount { get; set; }
        public Decimal WorkTimeSpan { get; set; }

        //Section
        public String SectionName { get; set; }

        //Mechanic
        public Boolean CheckPersonnel { get; set; }
        public Int64 EmployeeId { get; set; }
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public String LastName { get; set; }
        public String NameExtension { get; set; }
        public String FullName { get { return FirstName + " " + MiddleName + " " + LastName + " " + NameExtension; } }
       // public String Fullname { get; set; }
        public Int64 SectionId { get; set; }
        public Int64 PositionId { get; set; }
        public String PositionName { get; set; }
        //Checklist No
        public String ChecklistNo { get; set; }
        //Last update and work percentage

        public DateTime? DateOfUpdate { get; set; }
        public Decimal WorkPercentage { get; set; }
        public clsJOROReports()
        {
            CheckPersonnel = false;
            CheckEquipment = false;
            Id = 0;
            JONo = 0;
            Type = clsEnums.TypeOrder.JO;
            JODate = DateTime.Now;
            EquipmentOnBranch = true;
            PartsRequest = false;
            CustomerType = clsEnums.CustomerType.External;
            CustomerId = 0;
            ItemType = clsEnums.EquipmentType.Equipment;
            EquipmentId = 0;
            ContractorId = 0;
            DriverId = 0;
            UserId = 0;
            WorkDescription = "";
            WorkTimeSpan = 0;
            Amount = 0;
            SectionName = "";
            EmployeeId = 0;
            PositionId = 0;
            ChecklistNo = "";
            ListOfJOROReports = new List<clsJOROReports>();
        }
        public clsJOROReports(clsJOROReports obj)
        {
            CheckEquipment = obj.CheckEquipment;
            Id = obj.Id;
            JONo = obj.JONo;
            Type = obj.Type;
            JODate = obj.JODate;
            EquipmentOnBranch = obj.EquipmentOnBranch;
            PartsRequest = obj.PartsRequest;
            CustomerType = obj.CustomerType;
            CustomerId = obj.CustomerId;
            ItemType = obj.ItemType;
            EquipmentId = obj.EquipmentId;
            ContractorId = obj.ContractorId;
            DriverId = obj.DriverId;
            UserId = obj.UserId;
            WorkDescription = obj.WorkDescription;
            WorkTimeSpan = obj.WorkTimeSpan;
            Amount = obj.Amount;
            SectionName = obj.SectionName;

            EmployeeId = obj.EmployeeId;
            PositionId = obj.PositionId;

            ChecklistNo = obj.ChecklistNo;
            ListOfJOROReports = obj.ListOfJOROReports;
        }
    }
}
