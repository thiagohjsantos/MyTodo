using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyTodo.Data;
using MyTodo.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace MyTodo.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TodoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Todo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Todos
            .AsNoTracking()
            .Where(x=>x.User == User.Identity.Name)
            .ToListAsync());
        }

        // GET: Todo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = await _context.Todos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            if (todo.User != User.Identity.Name)
            {
                return NotFound();
            }

            return View(todo);
        }

        // GET: Todo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Todo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Message,Image,Done,CreatedAt,LastUpdateAt,User")] Todo todo, IFormFile fileImage)
        {
            if (ModelState.IsValid)
            {
                todo.User = User.Identity.Name;
                await UploadImage(fileImage, todo);
                
                if(fileImage == null)
                {
                    todo.Image = "Default.png";
                }

                _context.Add(todo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }

        // GET: Todo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            if (todo.User != User.Identity.Name)
            {
                return NotFound();
            }
            
            return View(todo);
        }

        // POST: Todo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Message,Image,Done")] Todo todo, IFormFile fileImage, bool imageDel)
        {
            if (id != todo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    todo.User = User.Identity.Name;

                    var entry = _context.Entry(todo);
                    entry.State = EntityState.Modified;

                    await UploadImage(fileImage, todo);

                    var entry2 = _context.Entry(todo);
                    entry2.State = EntityState.Modified;

                    entry2.Property("Image").IsModified = true;

                    _context.Update(todo);

                    if(fileImage == null && imageDel == false)
                    {
                        entry2.Property("Image").IsModified = false;
                    }
                    else if(fileImage == null && imageDel == true)
                    {
                        todo.Image = "Default.png";
                    }

                    entry.Property("CreatedAt").IsModified = false;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoExists(todo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }

        // GET: Todo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = await _context.Todos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            if (todo.User != User.Identity.Name)
            {
                return NotFound();
            }

            return View(todo);
        }

        // POST: Todo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoExists(int id)
        {
            return _context.Todos.Any(e => e.Id == id);
        }

        private async Task UploadImage(IFormFile fileImage, Todo todo)
        {
            try
            {
                var fileName = Path.GetFileName(fileImage.FileName);

                var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                        
                var fileExtension = Path.GetExtension(fileName);
                
                var newFileName = String.Concat(myUniqueFileName, fileExtension);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", newFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await fileImage.CopyToAsync(stream);
                }

                todo.Image = newFileName;
            }
            catch (System.NullReferenceException)
            {
                return;
            }

        }

    }
}

