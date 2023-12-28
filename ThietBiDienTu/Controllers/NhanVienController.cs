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
    public class NhanVienController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelPro = new ExcelProcess();
        public NhanVienController(ApplicationDbContext context)
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
            var model = _context.NhanVien.ToList().ToPagedList(page ?? 1, pagesize);
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
                                    var NV = new NhanVien();
                                    NV.MaNV = dt.Rows[i][0].ToString();
                                    NV.TenNV = dt.Rows[i][1].ToString();
                                    NV.ChucVu = dt.Rows[i][2].ToString();
                                    NV.DiaChi = dt.Rows[i][3].ToString();
                                    NV.SDT = dt.Rows[i][4].ToString();
                                    NV.TKNH = dt.Rows[i][5].ToString();
                                    _context.Add(NV);
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
            var fileName = "Danhsachnhanvien" + ".xlsx";
            using(ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                worksheet.Cells["A1"].Value = "Mã nhân viên";
                worksheet.Cells["B1"].Value = "Tên nhân viên";
                worksheet.Cells["C1"].Value = "Chức vụ";
                worksheet.Cells["D1"].Value = "Địa chỉ";
                worksheet.Cells["E1"].Value = "SDT";
                worksheet.Cells["F1"].Value = "TKNH";
                var NVList = _context.NhanVien.ToList();
                worksheet.Cells["A2"].LoadFromCollection(NVList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
        private bool NhanVienExists(string id)
        {
            return (_context.NhanVien?.Any(e => e.MaNV == id)).GetValueOrDefault();
        }
    }
}