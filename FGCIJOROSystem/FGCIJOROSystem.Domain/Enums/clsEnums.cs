using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Enums
{
    public class clsEnums
    {
        public enum CRUDEMode : byte
        {
            Add = 0,
            Edit = 1,
            Delete = 2
        }
        public enum CustomerCategory : byte
        {
            Project = 0,
            OtherDepartment = 1,
            OtherProject = 2,
            Equipment = 3,
            OtherEquipment = 4,
            Employee = 5,
            NonEmployee = 6,
            Section = 7,
            Company = 8,
            PrivateCustomer = 9,
            Product = 10,
            EquipmentNonEmployee = 11,
            Quarry = 12,
            FGDepartment = 13,
            EquipmentPPEType = 14,
            None = 15
        }

        public enum LookupMode
        {
            Employee,
            NonEmployee,
            Department,
            Section,
            Equipment,
            Project,
            All
        }
        public enum EmploymentType : byte
        {
            Employee = 0,
            NonEmployee = 1
        }
        public enum EquipmentType : byte
        {
            Equipment = 0,
            Ste = 1,
            OtherEquipment = 2
        }
        //Contractor
        public enum ContractorType : byte
        {
            [Description("External")]
            External = 0,
            [Description("Internal")]
            Internal = 1,
        }        
        public enum ContractorCategory : byte
        {
            [Description("Person")]
            Person = 0,
            [Description("Company")]
            Company = 1,
            [Description("Department")]
            Department = 2,
            [Description("Section")]
            Section = 3,
        }
        public enum CustomerType : byte
        {
            [Description("External")]
            External = 0,
            [Description("Company")]
            Company = 1,
            [Description("Department")]
            Department = 2,
            [Description("Section")]
            Section = 3,
        }
        public enum TypeOrder : byte
        {
            [Description("Job Order")]
            JO = 0,
            [Description("Rescue Order")]
            RO = 1,
        }
        public enum ActualAdvance : byte
        {
            [Description("Actual")]
            Actual = 0,
            [Description("Advance")]
            Advance = 1,

        }
        public enum JOROStatus : byte
        {
            [Description("Generated")]
            Generated = 0,
            [Description("Partially Approved")]
            PartiallyApproved = 1,
            [Description("Approved")]
            Approved = 2,           
            [Description("Partially Completed")]
            PartiallyCompleted = 3,
            [Description("Completed")]
            Completed = 4,
            [Description("Released")]
            Released = 5,
            [Description("Pending")]
            Pending = 6,
            [Description("Cancelled")]
            Cancelled = 7,
            [Description("Sustained")]
            Sustained = 8,
            [Description("Release Sustained")]
            ReleaseSustained = 9
        }
        public enum MsgBox : byte
        {
            [Description("Success")]
            Success = 0,
            [Description("Warning")]
            Warning = 1,
            [Description("Error")]
            Error = 2,           
        }
        public enum PRSStatus : byte
        {
            [Description("None")]
            None = 0,
            [Description("Received")]
            Received = 1,

            [Description("Pending-Waiting For Order")]
            PendingWaitingForOrder = 2,
            [Description("Pending-For Canvass")]
            PendingForCanvass = 3,
            [Description("Pending-In Stock For Deployment")]
            PendingInStockForDeployment = 4,

            [Description("Closed-Served")]
            ClosedServed = 5,
            [Description("Closed-Cancelled")]
            ClosedCancelled = 6,
            [Description("Closed-Transferred To Stock")]
            ClosedTransferredToStock = 7,

            [Description("Ongoing")]
            Ongoing = 8,
            [Description("Closed-Forced")]
            ClosedForced = 9,

            //[Description("Generated")]
            //Generated = 0,
            //[Description("Approved")]
            //Approved = 1,
            //[Description("Cancelled")]
            //Cancelled = 2,
            //[Description("Served")]
            //Served = 3,
            //[Description("Edited")]
            //Edited = 4,
            //[Description("Disapproved")]
            //Disapproved = 5,
            //[Description("Unserved")]
            //Unserved = 6
        }
        public enum FilterEquipmentUR : byte
        {
            [Description("All")]
            All = 0,
            [Description("Equipment Under Repair With JO / RO")]
            URWithJORO = 1,
            [Description("Equipment Under Repair Unattended")]
            URUnattended = 2,
            [Description("Equipment Non-UR But With JO / RO")]
            NonURButWithJORO = 3,

        }


        public enum OrderType : byte
        {
            [Description("Job Order")]
            JO = 0,
            [Description("Rescue Order")]
            RO = 1
        }
    }
}
