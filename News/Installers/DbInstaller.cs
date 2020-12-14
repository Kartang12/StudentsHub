﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using News.Data;
using News.Domain;
using News.Services;

namespace News.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("Connection2")));
            services.AddDefaultIdentity<MyUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DataContext>();

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IFormService, FormService>();
            services.AddScoped<IExerciseService, ExerciseService>();
            services.AddScoped<ISubjectService, SubjectService>();
        }
    }
}