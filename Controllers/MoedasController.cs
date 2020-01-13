using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoedasCrypt.Models;
using MoedasCrypt.Models.Context;

namespace MoedasCrypt.Controllers
{
    
    public class MoedasController : Controller
    {
        private readonly MoedasContext _context;

        public MoedasController(MoedasContext context)
        {
            _context = context;
        }

        // GET: Moedas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Moedas.ToListAsync());
        }

      
        // GET: Moedas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moedas = await _context.Moedas
                .FirstOrDefaultAsync(m => m.MoedasID == id);
            if (moedas == null)
            {
                return NotFound();
            }

            return View(moedas);
        }

        public async Task<IActionResult> EscolhadeMoedas(List<Moedas> moedas)
        {
            foreach(var item in moedas)
            {
                if(item.CheckBoxMarcado == true)
                {
                    Moedas moeda = await _context.Moedas.FirstAsync(x => x.MoedasID.Equals(item.MoedasID));
                    moeda.Quantidade = moeda.Quantidade + 1;
                    _context.Update(moeda);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public JsonResult DadosGrafico()
        {
            return Json(_context.Moedas.Select(x => new { x.Nome , x.Quantidade }));
        }
        // GET: Moedas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Moedas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MoedasID,Nome,Quantidade")] Moedas moedas)
        {
            if (ModelState.IsValid)
            {
                moedas.Quantidade = 0; 
                _context.Add(moedas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moedas);
        }

        // GET: Moedas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moedas = await _context.Moedas.FindAsync(id);
            if (moedas == null)
            {
                return NotFound();
            }
            return View(moedas);
        }

        // POST: Moedas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MoedasID,Nome,Quantidade")] Moedas moedas)
        {
            if (id != moedas.MoedasID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moedas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoedasExists(moedas.MoedasID))
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
            return View(moedas);
        }

        // GET: Moedas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moedas = await _context.Moedas
                .FirstOrDefaultAsync(m => m.MoedasID == id);
            if (moedas == null)
            {
                return NotFound();
            }

            return View(moedas);
        }

        // POST: Moedas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moedas = await _context.Moedas.FindAsync(id);
            _context.Moedas.Remove(moedas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoedasExists(int id)
        {
            return _context.Moedas.Any(e => e.MoedasID == id);
        }
    }
}
