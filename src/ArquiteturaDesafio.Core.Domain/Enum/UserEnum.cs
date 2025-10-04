using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArquiteturaDesafio.Core.Domain.Enum
{
    public enum UserStatus { None = 0, Active = 1, Inactive = 2, Suspended = 3 }
    public enum UserRole { None = 0, Customer = 1, Manager = 2, Admin = 3 }
}
