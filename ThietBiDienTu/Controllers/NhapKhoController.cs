using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThietBiDienTu.Data;
using ThietBiDienTu.Models;

namespace ThietBiDienTu.Controllers
{
    public class NhapKhoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NhapKhoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NhapKho
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.NhapKho.Include(n => n.HangHoa).Include(n => n.NhaCungCap);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: NhapKho/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.NhapKho == null)
            {
                return NotFound();
            }

            var nhapKho = await _context.NhapKho
                .Include(n => n.HangHoa)
                .Include(n => n.NhaCungCap)
                .FirstOrDefaultAsync(m => m.MaNhapKho == id);
            if (nhapKho == null)
            {
                return NotFound();
            }

            return View(nhapKho);
        }

        // GET: NhapKho/Create
        public IActionResult Create()
        {
            ViewData["MaHH"] = new SelectList(_context.HangHoa, "MaHH", "MaHH");
            ViewData["MaNCC"] = new SelectList(_context.NhaCungCap, "MaNCC", "MaNCC");
            return View();
        }

        // POST: NhapKho/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNhapKho,MaHH,SoLuong,NgayNhapKho,MaNCC")] NhapKho nhapKho)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhapKho);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaHH"] = new SelectList(_context.HangHoa, "MaHH", "MaHH", nhapKho.MaHH);
            ViewData["MaNCC"] = new SelectList(_context.NhaCungCap, "MaNCC", "MaNCC", nhapKho.MaNCC);
            return View(nhapKho);
        }

        // GET: NhapKho/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.NhapKho == null)
            {
                return NotFound();
            }

            var nhapKho = await _context.NhapKho.FindAsync(id);
            if (nhapKho == null)
            {
                return NotFound();
            }
            ViewData["MaHH"] = new SelectList(_context.HangHoa, "MaHH", "MaHH", nhapKho.MaHH);
            ViewData["MaNCC"] = new SelectList(_context.NhaCungCap, "MaNCC", "MaNCC", nhapKho.MaNCC);
            return View(nhapKho);
        }

        // POST: NhapKho/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaNhapKho,MaHH,SoLuong,NgayNhapKho,MaNCC")] NhapKho nhapKho)
        {
            if (id != nhapKho.MaNhapKho)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhapKho);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhapKhoExists(nhapKho.MaNhapKho))
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
            ViewData["MaHH"] = new SelectList(_context.HangHoa, "MaHH", "MaHH", nhapKho.MaHH);
            ViewData["MaNCC"] = new SelectList(_context.NhaCungCap, "MaNCC", "MaNCC", nhapKho.MaNCC);
            return View(nhapKho);
        }

        // GET: NhapKho/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.NhapKho == null)
            {
                return NotFound();
            }

            var nhapKho = await _context.NhapKho
                .Include(n => n.HangHoa)
                .Include(n => n.NhaCungCap)
                .FirstOrDefaultAsync(m => m.MaNhapKho == id);
            if (nhapKho == null)
            {
                return NotFound();
            }

            return View(nhapKho);
        }

        // POST: NhapKho/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.NhapKho == null)
            {
                return Problem("Entity set 'ApplicationDbContext.NhapKho'  is null.");
            }
            var nhapKho = await _context.NhapKho.FindAsync(id);
            if (nhapKho != null)
            {
                _context.NhapKho.Remove(nhapKho);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhapKhoExists(string id)
        {
          return (_context.NhapKho?.Any(e => e.MaNhapKho == id)).GetValueOrDefault();
        }
    }
}
