using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vorex.Domain.User;

namespace Vorex.Application.services.interfaces;

public interface IPasswordHashService
{
    string HashPassword(User user, string plainPassword);

    bool VerifyPassword(User user, string plainPassword,string hashedPassword);
}
