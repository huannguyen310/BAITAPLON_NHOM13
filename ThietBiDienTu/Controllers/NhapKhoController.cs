using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThietBiDienTu.Data;
using ThietBiDienTu.Models;
using ThietBiDienTu.Models.Process;
using OfficeOpenXml;
using X.PagedList;

namespace ThietBiDienTu.Controllers
{
    public class NhapKhoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelPro = new ExcelProcess();

        public NhapKhoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NhapKho
        public async Task<IActionResult> Index(int? page, int? PageSize)
        {
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "3", Text = "3"},
                new SelectListItem() { Value = "5", Text = "5"},
                new SelectListItem() { Value = "10", Text = "10"},
                new SelectListItem() { Value = "15", Text = "15"},
                new SelectListItem() { Value = "20", Text = "20"},
                new SelectListItem() { Value = "25", Text = "25"}
            };
            int pagesize = (PageSize ?? 5);
            ViewBag.psize = pagesize;
            var model = _context.NhapKho.ToList().ToPagedList(page ?? 1, pagesize);
            return View(model);
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
        public IActionResult Download()
        {
            var fileName = "Danhsachhangnhapkho" + ".xlsx";
            using(ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                worksheet.Cells["A1"].Value = "Mã nhập kho";
                worksheet.Cells["B1"].Value = "Mã hàng";
                worksheet.Cells["C1"].Value = "Số lượng";
                worksheet.Cells["D1"].Value = "Ngày nhập kho";
                worksheet.Cells["E1"].Value = "Mã NCC";
                var nhapKhoList = _context.NhapKho.ToList();
                worksheet.Cells["A2"].LoadFromCollection(nhapKhoList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
        private bool NhapKhoExists(string id)
        {
          return (_context.NhapKho?.Any(e => e.MaNhapKho == id)).GetValueOrDefault();
        }
    }
}
