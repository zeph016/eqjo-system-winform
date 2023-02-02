using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Configurations.Users
{
    public class clsUserLevel
    {
        public Int64 Id { get; set; }
        public String UserLevelName { get; set; }
        public bool Active { get; set; }
    }
}
