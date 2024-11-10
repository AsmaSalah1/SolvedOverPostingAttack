using FirsAPI_1.Data;
using FirsAPI_1.Data.Models;
using FirsAPI_1.DTOs.Employees;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirsAPI_1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeesController : ControllerBase
	{
		private readonly ApplicationDBContext dBContext;

		public EmployeesController(ApplicationDBContext dBContext )
		{
			this.dBContext = dBContext;
		}
		[HttpGet]
		public IActionResult getAll()
		{
			var emp=dBContext.Employees.ToList();
			var t=emp.Adapt<IEnumerable <GetEmpDTO>>();//لانه لوب
			return Ok(t);
		}
		[HttpGet("details")]
		public IActionResult getById(int id) {
			var emp = dBContext.Employees.Find(id);
			if(emp == null)
			{
				return NotFound();
			}
			var dtp=emp.Adapt<GetEmpDTO>();
			return Ok(dtp);
		}
		[HttpPost("Create")]
		//الداتا الي بدي اوخذها و اضيفها من نوع برودكت
		public IActionResult Create(CreateEmpDTO empDTO)
		{
			var emp = empDTO.Adapt<Employee>();
			dBContext.Employees.Add(emp);
			dBContext.SaveChanges();
			return Ok();
			//return CreatedAtAction(nameof(getById), emp);
		}
		[HttpPut("update")]
		//الاي دي كنا نبعثها قبل تكون مخفية
		public IActionResult Update(int id,CreateEmpDTO request) { 
		var p=dBContext.Employees.Find(id);
			if(p == null)
				return NotFound();
			p.Name=request.Name;
			p.Description=request.Description;

			dBContext.SaveChanges();
			return NoContent();

		}
		[HttpDelete("Delete")]
		public IActionResult delete(int id)
		{
			var p = dBContext.Employees.Find(id);
			if (p == null)
				return NotFound();
			dBContext.Remove(p);
			dBContext.SaveChanges();
			return NoContent();


		}
	}
}
