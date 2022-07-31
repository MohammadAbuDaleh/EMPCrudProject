using EmpCRUD_API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpCRUD_API
{
    public interface IJWTAuthenticationManager
    {
        Tokens Authenticate(Users users);
    }
}
