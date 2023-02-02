using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Global
{
    public class clsNonEmployee
    {
        public Int64 Id { get; set; }
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public String LastName { get; set; }
        public String NameExtension { get; set; }
        public String FullName
        {
            get
            {
                return LastName + ", " + FirstName + " " + MiddleName + " " + NameExtension;
            }
        }
        public String Designation { get; set; }
        public Int64 NonEmpDesignationId { get; set; }
        public String Position { get; set; }
        public DateTime DateEntered { get; set; }
        public DateTime DateEnded { get; set; }
        public Boolean Gender { get; set; }
        public Boolean IsActive { get; set; }
    }
    public enum Gender : byte
    {
        [Description("Male")]
        Male = 0,
        [Description("Female")]
        Female = 1
    }
}
