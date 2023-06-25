﻿//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
////using Microsoft.EntityFrameworkCore;
//using Shop.WebUi.Areas.Identity.Data;
////using Shop.WebUi.Models;

//namespace Shop.WebUi.Controllers
//{
//    public class CategoriesController : Controller
//    {
//        private readonly AplicationDBContext _context;

//        public CategoriesController(AplicationDBContext context)
//        {
//            _context = context;
//        }

//        // GET: Categories
//        [Authorize(Roles = "Admin")]
//        public async Task<IActionResult> Index()
//        {
//              return View(await _context.Category.ToListAsync());
//        }

//        // GET: Categories/Details/5
//        [Authorize(Roles = "Admin")]
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null || _context.Category == null)
//            {
//                return NotFound();
//            }

//            var category = await _context.Category
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (category == null)
//            {
//                return NotFound();
//            }

//            return View(category);
//        }

//        // GET: Categories/Create
//        [Authorize(Roles = "Admin")]
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: Categories/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Authorize(Roles = "Admin")]
//        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Category category)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(category);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(category);
//        }

//        // GET: Categories/Edit/5
//        [Authorize(Roles = "Admin")]
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null || _context.Category == null)
//            {
//                return NotFound();
//            }

//            var category = await _context.Category.FindAsync(id);
//            if (category == null)
//            {
//                return NotFound();
//            }
//            return View(category);
//        }

//        // POST: Categories/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Authorize(Roles = "Admin")]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Category category)
//        {
//            if (id != category.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(category);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!CategoryExists(category.Id))
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
//            return View(category);
//        }

//        // GET: Categories/Delete/5
//        [Authorize(Roles = "Admin")]
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null || _context.Category == null)
//            {
//                return NotFound();
//            }

//            var category = await _context.Category
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (category == null)
//            {
//                return NotFound();
//            }

//            return View(category);
//        }

//        // POST: Categories/Delete/5
//        [Authorize(Roles = "Admin")]
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            if (_context.Category == null)
//            {
//                return Problem("Entity set 'AplicationDBContext.Category'  is null.");
//            }
//            var category = await _context.Category.FindAsync(id);
//            if (category != null)
//            {
//                _context.Category.Remove(category);
//            }

//            //await _context.Question.FindAsync(x => x.CategoryId == id)
            
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        [Authorize(Roles = "Admin")]
//        private bool CategoryExists(int id)
//        {
//          return _context.Category.Any(e => e.Id == id);
//        }
//    }
//}
