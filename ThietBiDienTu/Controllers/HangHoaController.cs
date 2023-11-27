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
        public async Task<IActionResult> Create([Bind("MaHH, TenHH, ThongTinHH")] HangHoa hangHoa)
        {
            if(ModelState.IsValid)
            {
                _context.Add(hangHoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hangHoa);
        }
        public async Task<IActionResult> Edit(string hh)
        {
            if(hh == null || _context.HangHoa == null)
            {
                return NotFound();
            }
            var hanghoa = await _context.HangHoa.FindAsync(hh);
            if(hanghoa == null)
            {
                return NotFound();
            }
            return View(hanghoa);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string hh, [Bind("MaHH, TenHH, ThongTinHH")] HangHoa hanghoa)
        {
            if(hh != hanghoa.MaHH)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(hanghoa);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!HangHoaExists(hanghoa.MaHH))
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
            return View(hanghoa);
        }
        public async Task<IActionResult> Delete(string hh)
        {
            if(hh == null || _context.HangHoa == null)
            {
                return NotFound();
            }
            var hanghoa = await _context.HangHoa.FirstOrDefaultAsync(m => m.MaHH == hh);
            if(hanghoa == null)
            {
                return NotFound();
            }
            return View(hanghoa);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string hh)
        {
            if(_context.HangHoa == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HangHoa' is null.");
            }
            var hanghoa = await _context.HangHoa.FindAsync(hh);
            if(hanghoa != null)
            {
                _context.HangHoa.Remove(hanghoa);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool HangHoaExists(string hh)
        {
            return (_context.HangHoa?.Any(e => e.MaHH == hh)).GetValueOrDefault();
        }
    }
}