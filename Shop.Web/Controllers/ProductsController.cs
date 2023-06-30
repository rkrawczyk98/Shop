//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using Shop.WebUi.Areas.Identity.Data;
//using Shop.WebUi.Models;

//namespace Shop.WebUi.Controllers
//{
//    public class ProductsController : Controller
//    {
//        private readonly AplicationDBContext _context;

//        public ProductsController(AplicationDBContext context)
//        {
//            _context = context;
//        }

//        // GET: Answers
//        public async Task<IActionResult> Index()
//        {
//            var aplicationDBContext = _context.Answer.Include(a => a.Question).Include(a => a.Author);
//            return View(await aplicationDBContext.ToListAsync());
//        }

//        // GET: Answers/Details/5
//        [Authorize(Roles = "Logged,Admin")]
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null || _context.Answer == null)
//            {
//                return NotFound();
//            }

//            var answer = await _context.Answer
//                .Include(a => a.Question).Include(a => a.Author)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (answer == null)
//            {
//                return NotFound();
//            }

//            return View(answer);
//        }

//        // GET: Answers/Create
//        [Authorize(Roles = "Logged,Admin")]
//        [HttpGet("Answers/Create/{QuestionId?}")]
//        public IActionResult Create(int? QuestionId)
//        {
//            ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "Content");
//            if(QuestionId != null)
//            {
//                var question = _context.Question.Find(QuestionId);
//                if(question != null)
//                {
//                    ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "Content",QuestionId);
//                }
//            }
//            return View();
//        }

//        // POST: Answers/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Authorize(Roles = "Logged,Admin")]
//        public async Task<IActionResult> Create([Bind("Content,QuestionId")] Answer answer)
//        {
//            if (ModelState.IsValid)
//            {
//                answer.AuthorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//                _context.Add(answer);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "Content", answer.QuestionId);
//            return View(answer);
//        }

//        // GET: Answers/Edit/5
//        [Authorize(Roles = "Logged,Admin")]
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null || _context.Answer == null)
//            {
//                return NotFound();
//            }

//            var answer = await _context.Answer.FindAsync(id);
//            if (answer == null)
//            {
//                return NotFound();
//            }
//            ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "Content", answer.QuestionId);
//            return View(answer);
//        }

//        // POST: Answers/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Authorize(Roles = "Logged,Admin")]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,QuestionId")] Answer answer)
//        {
//            if (id != answer.Id)
//            {
//                return NotFound();
//            }


//            if (ModelState.IsValid)
//            {
//                answer.AuthorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//                try
//                {
//                    _context.Update(answer);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!AnswerExists(answer.Id))
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
//            ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "Content", answer.QuestionId);
//            return View(answer);
//        }

//        // GET: Answers/Delete/5
//        [Authorize(Roles = "Logged,Admin")]
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null || _context.Answer == null)
//            {
//                return NotFound();
//            }

//            var answer = await _context.Answer
//                .Include(a => a.Question)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (answer == null)
//            {
//                return NotFound();
//            }

//            return View(answer);
//        }

//        // POST: Answers/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        [Authorize(Roles = "Logged,Admin")]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            if (_context.Answer == null)
//            {
//                return Problem("Entity set 'AplicationDBContext.Answer'  is null.");
//            }
//            var answer = await _context.Answer.FindAsync(id);
//            if (answer != null)
//            {
//                _context.Answer.Remove(answer);
//            }
            
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        [Authorize(Roles = "Logged,Admin")]
//        private bool AnswerExists(int id)
//        {
//          return _context.Answer.Any(e => e.Id == id);
//        }
//    }
//}
