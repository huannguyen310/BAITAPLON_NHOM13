using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThietBiDienTu.Data;
using ThietBiDienTu.Models;
using ThietBiDienTu.Models.Process;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
using OfficeOpenXml;

namespace ThietBiDienTu.Controllers
{
    public class NhaCungCapController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelPro = new ExcelProcess();
        public NhaCungCapController(ApplicationDbContext context)
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
            var model = _context.NhaCungCap.ToList().ToPagedList(page ?? 1, pagesize);
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
                                    var nhaCC = new NhaCungCap();
                                    nhaCC.MaNCC = dt.Rows[i][0].ToString();
                                    nhaCC.TenNCC = dt.Rows[i][1].ToString();
                                    nhaCC.DiaChi = dt.Rows[i][2].ToString();
                                    nhaCC.SDT = dt.Rows[i][3].ToString();
                                    nhaCC.Email = dt.Rows[i][4].ToString();
                                    _context.Add(nhaCC);
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
            var fileName = "Danhsachnhacungcap" + ".xlsx";
            using(ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                worksheet.Cells["A1"].Value = "Mã NCC";
                worksheet.Cells["B1"].Value = "Tên NCC";
                worksheet.Cells["C1"].Value = "Địa chỉ";
                worksheet.Cells["D1"].Value = "SDT";
                worksheet.Cells["E1"].Value = "Email";
                var nhaCC = _context.NhaCungCap.ToList();
                worksheet.Cells["A2"].LoadFromCollection(nhaCC);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
        private bool NhaCungCapExists(string id)
        {
            return (_context.NhaCungCap?.Any(e => e.MaNCC == id)).GetValueOrDefault();
        }
    }
}