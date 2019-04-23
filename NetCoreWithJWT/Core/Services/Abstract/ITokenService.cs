using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Abstract
{
    public interface ITokenService
    {
        string GetToken(string userName, string password);
    }
}
