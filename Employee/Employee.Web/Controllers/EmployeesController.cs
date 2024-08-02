using EmployeeSystem.Domain.Entities;
using EmployeeSystem.Infraestructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contactes.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeDataContext _context;

        public EmployeesController(EmployeeDataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()//Accion
        {
            return View(await _context.Employees.ToListAsync());
        }

        //  [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee model)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Employees.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Employee model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                //    var contactFromDb = await _context.Contacts
                //.FirstOrDefaultAsync(c => c.Email == model.Email);


                //    contactFromDb.Email = model.Email;
                //    contactFromDb.PhoneNumber = model.PhoneNumber;
                //    contactFromDb.Name = model.Name;


                _context.Employees.Update(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

    }
}
