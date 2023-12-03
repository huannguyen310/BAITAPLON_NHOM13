using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThietBiDienTu.Data;
using ThietBiDienTu.Models;

namespace ThietBiDienTu.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HangHoaController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _context.HangHoa.ToListAsync();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHH, TenHH, HangSX, XuatXu, DonGia")] HangHoa hangHoa)
        {
            if(ModelState.IsValid)
            {
                _context.Add(hangHoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hangHoa);
        }
        public async Task<IActionResult> Edit(string id)
        {
            if(id == null || _context.HangHoa == null)
            {
                return NotFound();
            }
            var hangHoa = await _context.HangHoa.FindAsync(id);
            if(hangHoa == null)
            {
                return NotFound();
            }
            return View(hangHoa);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaHH, TenHH, HangSX, XuatXu, DonGia")] HangHoa hangHoa)
        {
            if(id != hangHoa.MaHH)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(hangHoa);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!HangHoaExists(hangHoa.MaHH))
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
            return View(hangHoa);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if(id == null || _context.HangHoa == null)
            {
                return NotFound();
            }
            var hangHoa = await _context.HangHoa.FirstOrDefaultAsync(m => m.MaHH == id);
            if(hangHoa == null)
            {
                return NotFound();
            }
            return View(hangHoa);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if(_context.HangHoa == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HangHoa' is null.");
            }
            var hangHoa =  await _context.HangHoa.FindAsync(id);
            if(hangHoa != null)
            {
                _context.HangHoa.Remove(hangHoa);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool HangHoaExists(string id)
        {
            return (_context.HangHoa?.Any(e => e.MaHH == id)).GetValueOrDefault();
        }
    }
}