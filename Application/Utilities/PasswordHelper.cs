using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utilities
{
    public static class PasswordHelper {
        public static string HashPassword(string password)
            => BCrypt.Net.BCrypt.HashPassword(password);

        public static bool VerifyPassword(string password, string hash)
            => BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
