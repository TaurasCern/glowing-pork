using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPES.Domain.Services.IServices
{
    public interface IJWTService
    {
        string CreateToken(Guid userId, string[] roles);
    }
}
