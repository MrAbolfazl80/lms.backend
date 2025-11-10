using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence {
    public static class DataSeeder {
        public static async Task SeedDataAsync(LmsDbContext context) {
            if (!await context.Users.AnyAsync(u => u.Username == "admin")) {
                var admin = new User(
                    username: "admin",
                    passwordHash: BCrypt.Net.BCrypt.HashPassword("Admin@"),
                    role: "Admin"
                );
                await context.Users.AddAsync(admin);
                await context.SaveChangesAsync();

                var student = new Student(
                    fullName: $"Admin",
                    userId: admin.Id
                );
                await context.Students.AddAsync(student);
            }

            for (int i = 1; i <= 10; i++) {
                var username = $"student{i}";
                if (!await context.Users.AnyAsync(u => u.Username == username)) {
                    var studentUser = new User(
                        username: username,
                        passwordHash: BCrypt.Net.BCrypt.HashPassword($"Student{i}@"),
                        role: "Student"
                    );

                    await context.Users.AddAsync(studentUser);
                    await context.SaveChangesAsync();

                    var student = new Student(
                        fullName: $"Student {i}",
                        userId: studentUser.Id
                    );

                    await context.Students.AddAsync(student);
                }
            }
            if (!await context.Courses.AnyAsync()) {
                for (int i = 1; i <= 200; i++) {
                    var course = new Course(
                        title: $"Course {i}",
                        description: $"Description for Course {i}",
                        startDate: DateTime.Now,
                        endDate: DateTime.Now.AddMonths(1),
                        capacity: 30,
                        fee: 2500 + i * 100,
                        teacherName: $"Teacher {i}"
                    );

                    await context.Courses.AddAsync(course);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
