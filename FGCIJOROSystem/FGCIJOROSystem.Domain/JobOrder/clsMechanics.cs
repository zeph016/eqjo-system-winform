using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.JobOrder
{
    public class clsMechanics
    {
        public Int64 Id { get; set; }
        public clsEnums.EmploymentType EmploymentType { get; set; }
        public Int64 JOId { get; set; }
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
        public String Position { get; set; }
        public string SectionName { get; set; }
        public clsMechanics()
        {
            Id = 0;
            EmploymentType = clsEnums.EmploymentType.Employee;
            JOId = 0;
            EmployeeId = 0;
            FirstName = "";
            MiddleName = "";
            Lastname = "";
            NameExtension = "";
            Tag = 0;
            IsActive = true;
            Position = "";
            SectionName = "";
        }
        public clsMechanics(clsMechanics obj) 
        {
            Id = obj.Id;
            EmploymentType = obj.EmploymentType;
            JOId = obj.JOId;
            EmployeeId = obj.EmployeeId;
            FirstName = obj.FirstName;
            MiddleName = obj.MiddleName;
            Lastname = obj.Lastname;
            NameExtension = obj.NameExtension;
            Tag = obj.Tag;
            IsActive = obj.IsActive;
            Position = obj.Position;
            SectionName = obj.SectionName;
        }
    }
}
