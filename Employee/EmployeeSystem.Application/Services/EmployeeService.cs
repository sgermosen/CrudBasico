using EmployeeSystem.Application.Dtos.Employees;
using EmployeeSystem.Domain.Entities;
using EmployeeSystem.Infraestructure;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSystem.Application.Services
{
    public class EmployeeService
    {
        private readonly EmployeeDataContext _context;

        public EmployeeService(EmployeeDataContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();

        }

        private async Task<Employee> GetDbEmployee(int id)
        {
            return await _context.Employees.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<EmployeeDto> GetEmployee(int id)
        {
            var itemFromDb = await GetDbEmployee(id);
            return new EmployeeDto { Department = itemFromDb.Department, Id = id, Name = itemFromDb.Name, Position = itemFromDb.Position };
        }
        public async Task<Employee> CreateEmployee(CreateEmployee model)
        {
            var newItemDb = new Employee
            {
                Department = model.Department,
                Position = model.Position,
                Name = model.Name,
                SexId = 1
            };

            _context.Employees.Add(newItemDb);
            await _context.SaveChangesAsync();
            return newItemDb;
        }

        public async Task<Employee> EditEmployee(EditEmployee model)
        {
            var itemFromDb = await GetDbEmployee(model.Id);
            if (itemFromDb == null)
            {
                return null;
            }
            itemFromDb.Name = model.Name;
            itemFromDb.Position = model.Position;
            itemFromDb.Department = model.Department;

            _context.Employees.Update(itemFromDb);
            await _context.SaveChangesAsync();
            return itemFromDb;
        }
    }
}
