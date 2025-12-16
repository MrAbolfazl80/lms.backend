using System;
using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Entities {
    public class User {
        public int Id { get; private set; }

        [Required, MaxLength(50)]
        public string Username { get; private set; }

        [Required]
        public string PasswordHash { get; private set; }

        [Required, MaxLength(20)]
        public string Role { get; private set; }
        public Student? Student { get; private set; }
        private User() { }
        public User(string username, string passwordHash, string role) {
            if (string.IsNullOrWhiteSpace(username))
                throw new DomainException("Username is required");


            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new DomainException("PasswordHash is required");

            if (string.IsNullOrWhiteSpace(role))
                throw new DomainException("Role is required");

            if (passwordHash.Length < 6)
                throw new DomainException("Username must be at least 6 characters long");
            if (Roles.All.Contains(role)) {
                throw new DomainException("Role is invalid");
            }
            Username = username;
            PasswordHash = passwordHash;
            Role = role;
        }

        public void UpdatePassword(string newPasswordHash) {
            if (string.IsNullOrWhiteSpace(newPasswordHash))
                throw new DomainException("PasswordHash is required");

            PasswordHash = newPasswordHash;
        }
        public bool IsValidRole(string role) {
            var roles = new string[] { "admin", "student" };
            return roles.Contains(role.Trim().ToLower());
        }
        public void UpdateRole(string newRole) {
            if (string.IsNullOrWhiteSpace(newRole))
                throw new DomainException("Role is required");

            Role = newRole;
        }
    }
}
