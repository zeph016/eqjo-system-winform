using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Configurations.JobType
{
    public class clsJobType
    { 
        public Int64 Id { get; set; }
        public String JobTypeName { get; set; }
        public String JobTypeDescription { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean CheckJobTypeName { get; set; }
        //public UInt64 JobTypeId { get; set; }
       
    }

}
