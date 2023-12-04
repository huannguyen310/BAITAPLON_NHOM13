using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThietBiDienTu.Data;
using ThietBiDienTu.Models;

namespace ThietBiDienTu.Controllers
{
    public class NhanVienController : Controller
    {
        private readonly ApplicationDbContext _context;
        public NhanVienController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _context.NhanVien.ToListAsync();
            return View(model);
        }
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.NhanVien == null)
            {
                return NotFound();
            }

            var NV = await _context.NhanVien.FirstOrDefaultAsync(m => m.MaNV == id);
            if (NV == null)
            {
                return NotFound();
            }

            return View(NV);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNV, TenNV, ChucVu, DiaChi, SDT, TKNH")] NhanVien NV)
        {
            if(ModelState.IsValid)
            {
                _context.Add(NV);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(NV);
        }
        public async Task<IActionResult> Edit(string id)
        {
            if(id == null || _context.NhanVien == null)
            {
                return NotFound();
            }
            var NV = await _context.NhanVien.FindAsync(id);
            if(NV == null)
            {
                return NotFound();
            }
            return View(NV);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaNV, TenNV, ChucVu, DiaChi, SDT, TKNH")] NhanVien NV)
        {
            if(id != NV.MaNV)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(NV);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!NhanVienExists(NV.MaNV))
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
            return View(NV);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if(id == null || _context.NhanVien == null)
            {
                return NotFound();
            }
            var NV = await _context.NhanVien.FirstOrDefaultAsync(m => m.MaNV == id);
            if(NV == null)
            {
                return NotFound();
            }
            return View(NV);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if(_context.NhanVien == null)
            {
                return Problem("Entity set 'ApplicationDbContext.NhanVien' is null.");
            }
            var NV =  await _context.NhanVien.FindAsync(id);
            if(NV != null)
            {
                _context.NhanVien.Remove(NV);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool NhanVienExists(string id)
        {
            return (_context.NhanVien?.Any(e => e.MaNV == id)).GetValueOrDefault();
        }
    }
}