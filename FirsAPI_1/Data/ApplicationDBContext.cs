using FirsAPI_1.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FirsAPI_1.Data
{
	public class ApplicationDBContext:DbContext 
	{
		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options ):
			base(options){ }

		public DbSet<Employee> Employees { get; set; }
		public DbSet<Department> Departments { get; set; }
	}
}
