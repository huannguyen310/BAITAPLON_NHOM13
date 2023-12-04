using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThietBiDienTu.Data;
using ThietBiDienTu.Models;

namespace ThietBiDienTu.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly ApplicationDbContext _context;
        public KhachHangController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _context.KhachHang.ToListAsync();
            return View(model);
        }
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.KhachHang == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHang.FirstOrDefaultAsync(m => m.MaKH == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKH, TenKH, DiaChi, SDT")] KhachHang khachHang)
        {
            if(ModelState.IsValid)
            {
                _context.Add(khachHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(khachHang);
        }
        public async Task<IActionResult> Edit(string id)
        {
            if(id == null || _context.KhachHang == null)
            {
                return NotFound();
            }
            var khachHang = await _context.KhachHang.FindAsync(id);
            if(khachHang == null)
            {
                return NotFound();
            }
            return View(khachHang);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaKH, TenKH, DiaChi, SDT")] KhachHang khachHang)
        {
            if(id != khachHang.MaKH)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(khachHang);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!KhachHangExists(khachHang.MaKH))
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
            return View(khachHang);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if(id == null || _context.KhachHang == null)
            {
                return NotFound();
            }
            var khachHang = await _context.KhachHang.FirstOrDefaultAsync(m => m.MaKH == id);
            if(khachHang == null)
            {
                return NotFound();
            }
            return View(khachHang);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if(_context.KhachHang == null)
            {
                return Problem("Entity set 'ApplicationDbContext.KhachHang' is null.");
            }
            var khachHang =  await _context.KhachHang.FindAsync(id);
            if(khachHang != null)
            {
                _context.KhachHang.Remove(khachHang);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool KhachHangExists(string id)
        {
            return (_context.KhachHang?.Any(e => e.MaKH == id)).GetValueOrDefault();
        }
    }
}