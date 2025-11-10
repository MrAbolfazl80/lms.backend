using Application.Services;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class ServiceExtensions {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            return services;
        }
    }
}
