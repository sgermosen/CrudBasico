using EmployeeSystem.Application.Dtos.Employees;
using EmployeeSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Contactes.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeService _service;

        public EmployeesController(EmployeeService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()//Accion
        {
            return View(await _service.GetEmployees());
        }

        //  [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmployee model)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateEmployee(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dbItem = await _service.GetEmployee(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var vm = new EditEmployee();
            vm.Position = dbItem.Position;
            vm.Name = dbItem.Name;
            vm.Department = dbItem.Department;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, EditEmployee model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _service.EditEmployee(model);

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

    }
}
