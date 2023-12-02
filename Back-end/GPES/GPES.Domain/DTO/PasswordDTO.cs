using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPES.Domain.DTO
{
    public class PasswordDTO
    {
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
    }
}
