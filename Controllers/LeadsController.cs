using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    public class LeadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Leads
        [Authorize(Roles = "Admin,sales")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.SalesLeads.ToListAsync());
        }

        // GET: Leads/Details/5
        [Authorize(Roles = "Admin,sales")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesLeadEntity = await _context.SalesLeads
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesLeadEntity == null)
            {
                return NotFound();
            }

            return View(salesLeadEntity);
        }

        // GET: Leads/Create
        [Authorize(Roles = "Admin,sales")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin,sales")]
        // POST: Leads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Mobile,Email,Source")] SalesLeadEntity salesLeadEntity)
        {

            if (ModelState.IsValid)
            {
                _context.Add(salesLeadEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salesLeadEntity);
        }

        // GET: Leads/Edit/5
        [Authorize(Roles = "Admin,sales")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesLeadEntity = await _context.SalesLeads.FindAsync(id);
            if (salesLeadEntity == null)
            {
                return NotFound();
            }
            return View(salesLeadEntity);
        }


        [Authorize(Roles = "Admin,sales")]
        // POST: Leads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,LastName,Mobile,Email,Source")] SalesLeadEntity salesLeadEntity)
        {
            if (id != salesLeadEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesLeadEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesLeadEntityExists(salesLeadEntity.Id))
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
            return View(salesLeadEntity);
        }

        // GET: Leads/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesLeadEntity = await _context.SalesLeads
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesLeadEntity == null)
            {
                return NotFound();
            }

            return View(salesLeadEntity);
        }

        [Authorize(Roles = "Admin")]
        // POST: Leads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var salesLeadEntity = await _context.SalesLeads.FindAsync(id);
            if (salesLeadEntity != null)
            {
                _context.SalesLeads.Remove(salesLeadEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesLeadEntityExists(string id)
        {
            return _context.SalesLeads.Any(e => e.Id == id);
        }
    }
}
