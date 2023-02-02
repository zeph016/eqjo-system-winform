using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Global
{
    public class clsDepartment
    {
        public Int64 Id { get; set; }
        public String Department { get; set; }
        public Int64 CompaniesId { get; set; }
        public String DepartmentShortcutName { get; set; }
        public Int64 DivisionId { get; set; }
    }
}
