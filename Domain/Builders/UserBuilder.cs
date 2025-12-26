using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Builders {
    public class UserBuilder {
        private readonly InnerModel _innerModel = new InnerModel();
        private class InnerModel {
            public int Id { get;  set; }

            
            public string Username { get;  set; }

            [Required]
            public string PasswordHash { get;  set; }

            [Required, MaxLength(20)]
            public string Role { get; set; }
            public Student? Student { get; set; }
        }
        public UserBuilder SetId(int id) {
            _innerModel.Id = id;
            return this;
        }
        public UserBuilder SetUsername(string userName) {
            _innerModel.Username = userName;
            return this;
        }
        public UserBuilder SetPassword(string pass) {
            _innerModel.PasswordHash = pass;
            return this;
        }
        public UserBuilder SetRole(string role) {
            _innerModel.Role = role;
            return this;
        }
        public User Build() {
            return new User(
                _innerModel.Username,
                _innerModel.PasswordHash,
                _innerModel.Role);
        }
        public UserBuilder ApplyTo(User user) {
            user.Update(_innerModel.Username,_innerModel.PasswordHash,_innerModel.Role);
            return this;
        }
    }
}
