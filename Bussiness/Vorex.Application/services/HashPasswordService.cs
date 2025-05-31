using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vorex.Application.services.interfaces;
using Vorex.Domain.User;

namespace Vorex.Application.services;

public class HashPasswordService(IPasswordHasher<User> passwordHasher) : IPasswordHashService
{
    public string HashPassword(User user, string plainPassword)
    {
        return passwordHasher.HashPassword(user, plainPassword);
    }

    public bool VerifyPassword(User user,string plainPassword, string hashedPassword)
    {
       var result = passwordHasher.VerifyHashedPassword(user, hashedPassword,plainPassword);
        return result == PasswordVerificationResult.Success;
    }
}
