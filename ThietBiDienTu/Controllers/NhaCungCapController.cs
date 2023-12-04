using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThietBiDienTu.Data;
using ThietBiDienTu.Models;

namespace ThietBiDienTu.Controllers
{
    public class NhaCungCapController : Controller
    {
        private readonly ApplicationDbContext _context;
        public NhaCungCapController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _context.NhaCungCap.ToListAsync();
            return View(model);
        }
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.NhaCungCap == null)
            {
                return NotFound();
            }

            var nhaCC = await _context.NhaCungCap.FirstOrDefaultAsync(m => m.MaNCC == id);
            if (nhaCC == null)
            {
                return NotFound();
            }

            return View(nhaCC);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNCC, TenNCC, DiaChi, SDT, Email")] NhaCungCap nhaCC)
        {
            if(ModelState.IsValid)
            {
                _context.Add(nhaCC);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhaCC);
        }
        public async Task<IActionResult> Edit(string id)
        {
            if(id == null || _context.NhaCungCap == null)
            {
                return NotFound();
            }
            var nhaCC = await _context.NhaCungCap.FindAsync(id);
            if(nhaCC == null)
            {
                return NotFound();
            }
            return View(nhaCC);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaNCC, TenNCC, DiaChi, SDT, Email")] NhaCungCap nhaCC)
        {
            if(id != nhaCC.MaNCC)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhaCC);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!NhaCungCapExists(nhaCC.MaNCC))
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
            return View(nhaCC);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if(id == null || _context.NhaCungCap == null)
            {
                return NotFound();
            }
            var nhaCC = await _context.NhaCungCap.FirstOrDefaultAsync(m => m.MaNCC == id);
            if(nhaCC == null)
            {
                return NotFound();
            }
            return View(nhaCC);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if(_context.NhaCungCap == null)
            {
                return Problem("Entity set 'ApplicationDbContext.NhaCungCap' is null.");
            }
            var nhaCC =  await _context.NhaCungCap.FindAsync(id);
            if(nhaCC != null)
            {
                _context.NhaCungCap.Remove(nhaCC);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool NhaCungCapExists(string id)
        {
            return (_context.NhaCungCap?.Any(e => e.MaNCC == id)).GetValueOrDefault();
        }
    }
}