using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskDay8.Models;

namespace TaskDay8.Controllers
{
    public class EmployeeController : Controller
    {
        CompanyModel db;
        //ctor injection
        public EmployeeController(CompanyModel _db)
        {
            db = _db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var emps = db.Employees.Include(ww => ww.Department);
            return View(emps);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.depts = new SelectList(db.Departments, "DeptId", "DeptName", 1);
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            db.Employees.Add(emp);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var emp = db.Employees.Find(id);
            ViewBag.depts = new SelectList(db.Departments, "DeptId", "DeptName", emp.DeptId);
            return View(emp);
        }

        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            db.Update(emp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await db.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await db.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Draft/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await db.Employees.FindAsync(id);
            db.Employees.Remove(employee);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
