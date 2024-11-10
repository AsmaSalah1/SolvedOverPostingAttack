using FirsAPI_1.Data.Models;
using FirsAPI_1.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FirsAPI_1.DTOs.Department;

namespace FirsAPI_1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmentsController : ControllerBase
	{
		private readonly ApplicationDBContext dBContext;

		public DepartmentsController(ApplicationDBContext dBContext)
		{
			this.dBContext = dBContext;
		}
		[HttpGet("ShowAll")]
		public IActionResult getAll()
		{
			var dep = dBContext.Departments.Select(
				x => new GetDepTOD
				{
					Id = x.Id,
					Name = x.Name,

				});

			return Ok(dep);
		}
		[HttpGet("details")]
		public IActionResult getById(int id)
		{
			var dep = dBContext.Departments.Find(id);
			if (dep == null)
			{
				return NotFound();
			}
			GetDepTOD dDOT = new GetDepTOD()
			{
				Id = dep.Id,
				Name = dep.Name,
			};
			return Ok(dDOT);
		}
		[HttpPost("CreateDep")]
		//الداتا الي بدي اوخذها و اضيفها من نوع برودكت
		public IActionResult Create(CreateDepDTOs depDot)
		{
			Department department = new Department()
			{
				Name = depDot.Name,
			};
			dBContext.Departments.Add(department);
			dBContext.SaveChanges();
			return Ok(department);
		}
		[HttpPut("update")]
		//الاي دي كنا نبعثها قبل تكون مخفية
		public IActionResult Update(int id, CreateDepDTOs depDOT)
		{
			var department = dBContext.Departments.Find(id);
			if (department == null)
				return NotFound();
			/*Department dep = new Department()
			{
				Name = depDOT.Name,
			};*/
			department.Name = depDOT.Name;
			dBContext.SaveChanges();
			return NoContent();

		}
		[HttpDelete("Delete")]
		public IActionResult delete(int id)
		{
			var p = dBContext.Departments.Find(id);
			if (p == null)
				return NotFound();
			dBContext.Remove(p);
			dBContext.SaveChanges();
			return NoContent();


		}
	}
}
