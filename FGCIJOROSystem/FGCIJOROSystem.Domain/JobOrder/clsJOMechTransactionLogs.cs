using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.JobOrder
{
    public class clsJOMechTransactionLogs
    {
        public Int64 Id { get; set; }
        public clsEnums.EmploymentType EmploymentType { get; set; }
        public Int64 JOMechanicId { get; set; }
        public Int64 JOId { get; set; }
        public Int64 JOTransLogId { get; set; }
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
        public clsJOMechTransactionLogs()
        {
            Id = 0;
            EmploymentType = clsEnums.EmploymentType.Employee;
            JOMechanicId = 0;
            JOId = 0;
            JOTransLogId = 0;
            EmployeeId = 0;
            FirstName = "";
            MiddleName = "";
            Lastname = "";
            NameExtension = "";
            Tag = 0;
            IsActive = true;
        }
        public clsJOMechTransactionLogs(clsJOMechTransactionLogs obj) 
        {
            Id = obj.Id;
            EmploymentType = obj.EmploymentType;
            JOId = obj.JOId;
            EmployeeId = obj.EmployeeId;
            JOTransLogId = obj.JOTransLogId;
            FirstName = obj.FirstName;
            MiddleName = obj.MiddleName;
            Lastname = obj.Lastname;
            NameExtension = obj.NameExtension;
            Tag = obj.Tag;
            IsActive = obj.IsActive;
        }
    }
}
