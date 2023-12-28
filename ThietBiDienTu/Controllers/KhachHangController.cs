using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThietBiDienTu.Data;
using ThietBiDienTu.Models;
using ThietBiDienTu.Models.Process;
using X.PagedList;
using OfficeOpenXml;

namespace ThietBiDienTu.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelPro = new ExcelProcess();
        public KhachHangController(ApplicationDbContext context)
        {
            _context = context;
        }
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
            var model = _context.KhachHang.ToList().ToPagedList(page ?? 1, pagesize);
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
        public async Task<IActionResult> Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if(file != null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if(fileExtension != ".xls" && fileExtension != ".xlsx")
                {
                    ModelState.AddModelError("", "Please choose excel file to upload!");
                }
                else
                {
                    var fileName = DateTime.Now.ToShortTimeString() + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads/Excels", "File" + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond + fileExtension);
                    var fileLocation = new FileInfo(filePath).ToString();
                    if (file.Length > 0)
                        {
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                //save file to server
                                await file.CopyToAsync(stream);
                                //read data from file and write to database
                                var dt = _excelPro.ExcelToDataTable(fileLocation);
                                for(int i = 0; i < dt.Rows.Count; i++)
                                {
                                    var khachHang = new KhachHang();
                                    khachHang.MaKH = dt.Rows[i][0].ToString();
                                    khachHang.TenKH = dt.Rows[i][1].ToString();
                                    khachHang.DiaChi = dt.Rows[i][2].ToString();
                                    khachHang.SDT = dt.Rows[i][3].ToString();
                                    _context.Add(khachHang);
                                }
                                await _context.SaveChangesAsync();
                                return RedirectToAction(nameof(Index));
                            }
                        }
                }
            }
            return View();
        }
        public IActionResult Download()
        {
            var fileName = "Danhsachkhachhang" + ".xlsx";
            using(ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                worksheet.Cells["A1"].Value = "Mã khách hàng";
                worksheet.Cells["B1"].Value = "Tên khách hàng";
                worksheet.Cells["C1"].Value = "Địa chỉ";
                worksheet.Cells["D1"].Value = "SDT";
                var khachhangList = _context.KhachHang.ToList();
                worksheet.Cells["A2"].LoadFromCollection(khachhangList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
        private bool KhachHangExists(string id)
        {
            return (_context.KhachHang?.Any(e => e.MaKH == id)).GetValueOrDefault();
        }
    }
}