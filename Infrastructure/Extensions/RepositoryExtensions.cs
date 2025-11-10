using Application.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class RepositoryExtensions {
        public static IServiceCollection AddRepositories(this IServiceCollection services) {
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

            return services;
        }
    }
}
