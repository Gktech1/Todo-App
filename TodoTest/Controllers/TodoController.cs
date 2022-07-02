using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoTest.Infastructure;
using TodoTest.ViewModel;
using TodoTest.Models;


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
            List<Todo> todos = await items.ToListAsync();

            var list = new List<TodoViewModel>();

            //Map the todomdel to todoviewmodel
            if (todos != null)
            {
                foreach (var todo in todos)
                {
                    list.Add(
                        new TodoViewModel()
                        {
                            Id = todo.Id,
                            Description = todo.Description,
                            Name = todo.Name,
                            Schedule = todo.Schedule
                        }
                    );
                }
            }
            return View(list);

        }

        [HttpPost]
        public async Task<IActionResult> Index(string SearchText = "")


        {
            var todos = new List<Todo>();
            var list = new List<TodoViewModel>();

           
           if (SearchText != "" && SearchText != null)
            {
                var items = await _context.Todo
                  .Where(t => t.Name.Contains(SearchText)).ToListAsync();
                todos = items.ToList();

                TempData["Success"] = "The Item was successfully found";
            }
          

            //Map the todomdel to todoviewmodel
            if (todos != null)
            {
                foreach (var todo in todos)
                {
                    list.Add(
                        new TodoViewModel()
                        {
                            Id = todo.Id,
                            Description = todo.Description,
                            Name = todo.Name,
                            Schedule = todo.Schedule
                        }
                    );
                }
            }

            if (todos.Count == 0)
            {
                TempData["Error"] = "The Item was not found";
            }

            return View(list);
        }

        // GET/todo/create
        [HttpGet]
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

        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> DeleteSelected(List<TodoViewModel> items)
        {

             var todoList = new List<Todo>();

            foreach (var item in items)
            {
                if (item.TodoList.Selected)
                {
                    var singleTodo = await _context.Todo.FindAsync(item.Id);
                    todoList.Add(singleTodo);
                }
            }
                
            _context.RemoveRange(todoList);
            await _context.SaveChangesAsync();

            TempData["success"] = "The Item has been successfully deleted";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Todo item = await _context.Todo.FindAsync(id);
            if (item == null)
            {
                TempData["Error"] = "The Item does not exist";
            }
            else
            {
                _context.Remove(item);
                await _context.SaveChangesAsync();
                TempData["Success"] = "The Items has been successfully deleted";
            }
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> DeleteAll()
        {
              var deleteAll = _context.Todo.ToList();
                _context.RemoveRange(deleteAll);
               await _context.SaveChangesAsync();
                TempData["Success"] = "The Item has been successfully deleted";

            return RedirectToAction("Index");
        }
           
    }
    

}

/* [HttpPost]
        public async Task<IActionResult> DeleteAll(IEnumerable<int> items)
        {
            items = _context.Todo
                  .Where(t => t.Name.Contains(SearchText)).ToList();

            _context.Todo.Where(x => items.Contains(x.Id)).ToList().ForEach(_context.Todo.Remove(items));
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }*/