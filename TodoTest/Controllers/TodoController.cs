using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoTest.Infastructure;
using TodoWeek7.Models;

namespace TodoTest.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
        }

        //GET
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IQueryable<Todo> items = from i in _context.Todo orderby i.Id select i;
            List<Todo> todo = await items.ToListAsync();
            return View(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string SearchText = "")


        {
            List<Todo> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.Todo
                  .Where(t => t.Name.Contains(SearchText)).ToList();
                if (items.Count > 0)

                    TempData["Success"] = "The Item was successfully found";

            }


            else
            {
                items = await _context.Todo.ToListAsync();

            }
            if (items.Count == 0)
                TempData["Error"] = "The Item was not found";


            return View(items);

        }


        // GET/todo/create
        public IActionResult Create() => View();

        //POST /TODO/CREATE 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Todo item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                TempData["Success"] = "The item has been successfully added";
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Todo item = await _context.Todo.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }



        //POST /TODO/Edit 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Todo item)
        {
            if (ModelState.IsValid)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                TempData["Success"] = "The item has been successfully Updated";
                return RedirectToAction("Index");
            }
            return View(item);
        }


        public async Task<IActionResult> Delete(int id)
        {
            //Todo item = _context.Todo.FirstOrDefault(s => s.Id == id);
            Todo item = await _context.Todo.FindAsync(id);
            if (item == null)
            {
                TempData["Error"] = "The Item does not exist";
            }
            else
            {
                _context.Remove(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
                TempData["Success"] = "The Item has been successfully deleted";
            }


            return View();
        }


       /* [HttpPost]
        public async Task<IActionResult> DeleteAll(IEnumerable<int> items)
        {
            _context.Todo.Where(x => items.Contains(x.Id)).ToList().ForEach(_context.Todo.Remove(items));
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }*/
       
    }
}
