using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Global
{
    public class clsEmployee
    {
        public Int64 Id { get; set; }
        public clsEnums.EmploymentType EmploymentType { get; set; }
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public String LastName { get; set; }
        public String NameExtension { get; set; }
        public String FullName
        {
            get
            {
                return FirstName + " " + MiddleName + " " + LastName + " " + NameExtension;
            }
        }
        public DateTime DateHired { get; set; }
        public Int64 EmployeeStatusId { get; set; }
        public String EmployeeStatus { get; set; }
        public byte[] EmpPicture { get; set; }

        public Int64 PositionId { get; set; }
        public String Position { get; set; }

        public Int64 SectionId { get; set; }
        public String Section { get; set; }
        public Int64 DepartmentId { get; set; }
        public String Department { get; set; }
        public clsEmployee()
        {
            Id = 0;
            EmploymentType = clsEnums.EmploymentType.Employee;
            FirstName = "";
            MiddleName = "";
            LastName = "";
            NameExtension = "";
            DateHired = DateTime.Now;
            EmployeeStatusId = 0;
            EmployeeStatus = "";
            PositionId = 0;
            SectionId = 0;
            DepartmentId = 0;
        }
        public clsEmployee(clsEmployee obj)
        {
            Id = obj.Id;
            EmploymentType = obj.EmploymentType;
            FirstName = obj.FirstName;
            MiddleName = obj.MiddleName;
            LastName = obj.LastName;
            NameExtension = obj.NameExtension;
            DateHired = obj.DateHired;
            EmployeeStatusId = obj.EmployeeStatusId;
            EmployeeStatus = obj.EmployeeStatus;
            PositionId = obj.PositionId;
            SectionId = obj.SectionId;
            DepartmentId = obj.DepartmentId;
        }
    }
}
