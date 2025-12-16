using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common {
    public static class Roles {
        public const string Admin = "Admin";
        public const string Student = "Student";
        public static string[] All => new string[] { Admin , Student };
    }
}
