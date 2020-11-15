namespace FastServices.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Common;
    using FastServices.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedRoleAsync(roleManager, GlobalConstants.AdministratorRoleName);

            await SeedRoleAsync(roleManager, GlobalConstants.EmployeeRoleName);

            // Assign user by email address to admin role
            await AssignUserToRolesAsync(roleManager, userManager, GlobalConstants.AdministratorRoleName, "admin@abv.bg");

            // Adds employee and assings employee role to his roles
            await AddEmployee(roleManager, userManager, dbContext, "employee@abv.bg");
            await AddEmployee(roleManager, userManager, dbContext, "testuser@abv.bg");
            await AddEmployee(roleManager, userManager, dbContext, "noviqtest@abv.bg");
        }

        private static async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }

        private static async Task AssignUserToRolesAsync(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, string roleName, string email)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            var user = await userManager.FindByEmailAsync(email);

            if (role != null && user != null &&
                !user.Roles.Any(x => x.RoleId == role.Id))
            {
                await userManager.AddToRoleAsync(user, roleName);
            }
        }

        private static async Task AddEmployee(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, string email)
        {
            await AssignUserToRolesAsync(roleManager, userManager, GlobalConstants.EmployeeRoleName, email);
            var user = await userManager.FindByEmailAsync(email);

            var employee = new Employee
            {
                ApplicationUserId = user.Id,
                DepartmentId = 2,
                FirstName = "Rabotnika",
                LastName = "Ivanov",
                IsAvailable = true,
                Salary = 640M,
                CreatedOn = DateTime.UtcNow,
            };

            if (!dbContext.Employees.Any(x => x.ApplicationUserId == employee.ApplicationUserId))
            {
                await dbContext.Employees.AddAsync(employee);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
