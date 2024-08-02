using EmployeeSystem.Application.Dtos.Employees;
using EmployeeSystem.Application.Services;
using EmployeeSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _service;

        public EmployeesController(EmployeeService service)
        {
            _service = service;
        }

        [HttpGet(Name = "GetEmployees")]
        public async Task<IEnumerable<Employee>> Get()
        {
            var employeesFromDb = await _service.GetEmployees();
            return employeesFromDb;
        }

        [HttpGet("{id}", Name = "GetEmployee")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            var employeeFromDb = await _service.GetEmployee(id);
            if (employeeFromDb == null)
            {
                return NotFound("Employee not found");
            }
            return Ok(employeeFromDb);
        }

        [HttpPost(Name = "CreateEmployee")]
        public async Task<IActionResult> Create([FromBody] CreateEmployee model)
        {
            if (model == null)
            {
                return BadRequest("Employee data is null");
            }


            if (ModelState.IsValid)
            {
                var result = await _service.CreateEmployee(model);
                return CreatedAtRoute("GetEmployee", new { id = result.Id }, result);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}", Name = "UpdateEmployee")]
        public async Task<IActionResult> Update(int id, [FromBody] EditEmployee model)
        {
            if (model == null || id != model.Id)
            {
                return BadRequest("Employee data is invalid");
            }
            if (ModelState.IsValid)
            {

                var result = await _service.EditEmployee(model);
                if (result == null)
                {
                    return NotFound("Employee not found");
                }
                 
                return Ok(result);
            }

            return BadRequest(ModelState);
        }

        //[HttpDelete("{id}", Name = "DeleteEmployee")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var employeeFromDb = await _context.Employees.FindAsync(id);
        //    if (employeeFromDb == null)
        //    {
        //        return NotFound("Employee not found");
        //    }

        //    _context.Employees.Remove(employeeFromDb);
        //    await _context.SaveChangesAsync();
        //    return Ok("Employee Deleted");
        //}
    }
}
