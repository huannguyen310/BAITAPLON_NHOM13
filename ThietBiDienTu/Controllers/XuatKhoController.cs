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
    public class XuatKhoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public XuatKhoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: XuatKho
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.XuatKho.Include(x => x.HangHoa).Include(x => x.KhachHang).Include(x => x.NhaCungCap);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: XuatKho/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.XuatKho == null)
            {
                return NotFound();
            }

            var xuatKho = await _context.XuatKho
                .Include(x => x.HangHoa)
                .Include(x => x.KhachHang)
                .Include(x => x.NhaCungCap)
                .FirstOrDefaultAsync(m => m.MaHH == id);
            if (xuatKho == null)
            {
                return NotFound();
            }

            return View(xuatKho);
        }

        // GET: XuatKho/Create
        public IActionResult Create()
        {
            ViewData["MaHH"] = new SelectList(_context.HangHoa, "MaHH", "MaHH");
            ViewData["MaKH"] = new SelectList(_context.KhachHang, "MaKH", "MaKH");
            ViewData["MaNCC"] = new SelectList(_context.NhaCungCap, "MaNCC", "MaNCC");
            return View();
        }

        // POST: XuatKho/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHH,TenHH,MaNCC,TenNCC,MaXuatKho,NgayXuatKho,TrangThai,MaKH")] XuatKho xuatKho)
        {
            if (ModelState.IsValid)
            {
                _context.Add(xuatKho);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaHH"] = new SelectList(_context.HangHoa, "MaHH", "MaHH", xuatKho.MaHH);
            ViewData["MaKH"] = new SelectList(_context.KhachHang, "MaKH", "MaKH", xuatKho.MaKH);
            ViewData["MaNCC"] = new SelectList(_context.NhaCungCap, "MaNCC", "MaNCC", xuatKho.MaNCC);
            return View(xuatKho);
        }

        // GET: XuatKho/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.XuatKho == null)
            {
                return NotFound();
            }

            var xuatKho = await _context.XuatKho.FindAsync(id);
            if (xuatKho == null)
            {
                return NotFound();
            }
            ViewData["MaHH"] = new SelectList(_context.HangHoa, "MaHH", "MaHH", xuatKho.MaHH);
            ViewData["MaKH"] = new SelectList(_context.KhachHang, "MaKH", "MaKH", xuatKho.MaKH);
            ViewData["MaNCC"] = new SelectList(_context.NhaCungCap, "MaNCC", "MaNCC", xuatKho.MaNCC);
            return View(xuatKho);
        }

        // POST: XuatKho/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaHH,TenHH,MaNCC,TenNCC,MaXuatKho,NgayXuatKho,TrangThai,MaKH")] XuatKho xuatKho)
        {
            if (id != xuatKho.MaHH)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(xuatKho);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!XuatKhoExists(xuatKho.MaHH))
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
            ViewData["MaHH"] = new SelectList(_context.HangHoa, "MaHH", "MaHH", xuatKho.MaHH);
            ViewData["MaKH"] = new SelectList(_context.KhachHang, "MaKH", "MaKH", xuatKho.MaKH);
            ViewData["MaNCC"] = new SelectList(_context.NhaCungCap, "MaNCC", "MaNCC", xuatKho.MaNCC);
            return View(xuatKho);
        }

        // GET: XuatKho/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.XuatKho == null)
            {
                return NotFound();
            }

            var xuatKho = await _context.XuatKho
                .Include(x => x.HangHoa)
                .Include(x => x.KhachHang)
                .Include(x => x.NhaCungCap)
                .FirstOrDefaultAsync(m => m.MaHH == id);
            if (xuatKho == null)
            {
                return NotFound();
            }

            return View(xuatKho);
        }

        // POST: XuatKho/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.XuatKho == null)
            {
                return Problem("Entity set 'ApplicationDbContext.XuatKho'  is null.");
            }
            var xuatKho = await _context.XuatKho.FindAsync(id);
            if (xuatKho != null)
            {
                _context.XuatKho.Remove(xuatKho);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool XuatKhoExists(string id)
        {
          return (_context.XuatKho?.Any(e => e.MaHH == id)).GetValueOrDefault();
        }
    }
}
