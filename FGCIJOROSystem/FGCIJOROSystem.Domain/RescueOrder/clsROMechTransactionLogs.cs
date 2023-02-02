using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.RescueOrder
{
    public class clsROMechTransactionLogs
    {
         public Int64 Id { get; set; }
        public clsEnums.EmploymentType EmploymentType { get; set; }
        public Int64 ROMechanicId { get; set; }
        public Int64 ROId { get; set; }
        public Int64 ROTransLogId { get; set; }
        public Int64 EmployeeId { get; set; }
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public String Lastname { get; set; }
        public String NameExtension { get; set; }
        public int Tag { get; set; }
        public Boolean IsActive { get; set; }
        public String FullName { 
            get 
            {
                return FirstName + " " + MiddleName + " " + Lastname + " " + NameExtension;
            } 
        }
        public clsROMechTransactionLogs()
        {
            Id = 0;
            EmploymentType = clsEnums.EmploymentType.Employee;
            ROMechanicId = 0;
            ROId = 0;
            ROTransLogId = 0;
            EmployeeId = 0;
            FirstName = "";
            MiddleName = "";
            Lastname = "";
            NameExtension = "";
            Tag = 0;
            IsActive = true;
        }
        public clsROMechTransactionLogs(clsROMechTransactionLogs obj) 
        {
            Id = obj.Id;
            EmploymentType = obj.EmploymentType;
            ROId = obj.ROId;
            EmployeeId = obj.EmployeeId;
            ROTransLogId = obj.ROTransLogId;
            FirstName = obj.FirstName;
            MiddleName = obj.MiddleName;
            Lastname = obj.Lastname;
            NameExtension = obj.NameExtension;
            Tag = obj.Tag;
            IsActive = obj.IsActive;
        }
    }
}
