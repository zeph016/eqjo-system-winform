using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Configurations.JobCategories
{
    public class clsJobCategory
    {
        public Int64 Id { get; set; }
        public String JobCategoryName { get; set; }
        public String Description { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean CheckJobCategoryName { get; set; }
        public Int64 JobCategoryId { get; set; }
    }
}
