//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using Shop.WebUi.Areas.Identity.Data;
//using Shop.WebUi.Models;

//namespace Shop.WebUi.Controllers
//{
//    public class OrdersController : Controller
//    {
//        private readonly AplicationDBContext _context;

//        public OrdersController(AplicationDBContext context)
//        {
//            _context = context;
//        }

//        // GET: Questions
//        public async Task<IActionResult> Index()
//        {
//            var aplicationDBContext = _context.Question.Include(q => q.Category);
//            return View(await aplicationDBContext.ToListAsync());
//        }

//        // GET: Questions/Details/5
//        [Authorize(Roles = "Logged,Admin")]
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null || _context.Question == null)
//            {
//                return NotFound();
//            }

//            var question = await _context.Question
//                .Include(q => q.Category)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (question == null)
//            {
//                return NotFound();
//            }

//            return View(question);
//        }



//        // GET: Questions/Create
//        [Authorize(Roles = "Logged,Admin")]
//        public IActionResult Create()
//        {
//            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
//            return View();
//        }

//        // POST: Questions/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        //[HttpPost]
//        //[ValidateAntiForgeryToken]
//        //[Authorize(Roles = "Logged,Admin")]
//        //public async Task<IActionResult> Create([Bind("Id,Content,CategoryId")] Question question)
//        //{
//        //    if (ModelState.IsValid)
//        //    {
//        //        _context.Add(question);
//        //        await _context.SaveChangesAsync();
//        //        return RedirectToAction(nameof(Index));
//        //    }
//        //    ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", question.CategoryId);
//        //    return View(question);
//        //}

//        //// GET: Questions/Edit/5
//        //[Authorize(Roles = "Logged,Admin")]
//        //public async Task<IActionResult> Edit(int? id)
//        //{
//        //    if (id == null || _context.Question == null)
//        //    {
//        //        return NotFound();
//        //    }

//        //    var question = await _context.Question.FindAsync(id);
//        //    if (question == null)
//        //    {
//        //        return NotFound();
//        //    }
//        //    ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", question.CategoryId);
//        //    return View(question);
//        //}

//        // POST: Questions/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Authorize(Roles = "Logged,Admin")]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,CategoryId")] Question question)
//        {
//            if (id != question.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(question);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!QuestionExists(question.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", question.CategoryId);
//            return View(question);
//        }

//        // GET: Questions/Delete/5
//        [Authorize(Roles = "Logged,Admin")]
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null || _context.Question == null)
//            {
//                return NotFound();
//            }

//            var question = await _context.Question
//                .Include(q => q.Category)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (question == null)
//            {
//                return NotFound();
//            }

//            return View(question);
//        }

//        // POST: Questions/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        [Authorize(Roles = "Logged,Admin")]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            if (_context.Question == null)
//            {
//                return Problem("Entity set 'AplicationDBContext.Question'  is null.");
//            }
//            var question = await _context.Question.FindAsync(id);
//            if (question != null)
//            {
//                _context.Question.Remove(question);
//            }
            
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        [Authorize(Roles = "Logged,Admin")]
//        private bool QuestionExists(int id)
//        {
//          return _context.Question.Any(e => e.Id == id);
//        }
//    }
//}
