using Employeees.Domain.Entities;
using Employeees.Web.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeDataContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetEmployees")]
        public IEnumerable<Employee> Get()
        {
            var employeesFromDb = _context.Employees.ToList();
            return employeesFromDb;
        }

        [HttpGet("{id}", Name = "GetEmployee")]
        public ActionResult<Employee> Get(int id)
        {
            var employeeFromDb = _context.Employees.FirstOrDefault(p => p.Id == id);
            if (employeeFromDb == null)
            {
                return NotFound("Employee not found");
            }
            return Ok(employeeFromDb);
        }

        [HttpPost(Name = "CreateEmployee")]
        public async Task<IActionResult> Create([FromBody] Employee model)
        {
            if (model == null)
            {
                return BadRequest("Employee data is null");
            }

            bool emailExists = await _context.Employees
                .AnyAsync(c => c.Email == model.Email);
            if (emailExists)
            {
                return BadRequest("This email is already in use");
            }

            if (ModelState.IsValid)
            {
                var employeeDb = new Employee
                {
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Name = model.Name
                };

                _context.Employees.Add(employeeDb);
                await _context.SaveChangesAsync();
                return CreatedAtRoute("GetEmployee", new { id = employeeDb.Id }, employeeDb);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}", Name = "UpdateEmployee")]
        public async Task<IActionResult> Update(int id, [FromBody] Employee model)
        {
            if (model == null || id != model.Id)
            {
                return BadRequest("Employee data is invalid");
            }

            var employeeFromDb = await _context.Employees.FindAsync(id);
            if (employeeFromDb == null)
            {
                return NotFound("Employee not found");
            }

            if (await _context.Employees.AnyAsync(c => c.Email == model.Email && c.Id != id))
            {
                return BadRequest("This email is already in use by another employee");
            }

            if (ModelState.IsValid)
            {
                employeeFromDb.Email = model.Email;
                employeeFromDb.PhoneNumber = model.PhoneNumber;
                employeeFromDb.Name = model.Name;

                _context.Employees.Update(employeeFromDb);
                await _context.SaveChangesAsync();
                return Ok(employeeFromDb);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}", Name = "DeleteEmployee")]
        public async Task<IActionResult> Delete(int id)
        {
            var employeeFromDb = await _context.Employees.FindAsync(id);
            if (employeeFromDb == null)
            {
                return NotFound("Employee not found");
            }

            _context.Employees.Remove(employeeFromDb);
            await _context.SaveChangesAsync();
            return Ok("Employee Deleted");
        }
    }
}
