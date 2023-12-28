using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using ThietBiDienTu.Data;
using ThietBiDienTu.Models;
using ThietBiDienTu.Models.Process;
using X.PagedList;

namespace ThietBiDienTu.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelPro = new ExcelProcess();
        public HangHoaController(ApplicationDbContext context)
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
            var model = _context.HangHoa.ToList().ToPagedList(page ?? 1, pagesize);
            return View(model);
        }
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.HangHoa == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoa.FirstOrDefaultAsync(m => m.MaHH == id);
            if (hangHoa == null)
            {
                return NotFound();
            }

            return View(hangHoa);
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
                                    var hanghoa = new HangHoa();
                                    hanghoa.MaHH = dt.Rows[i][0].ToString();
                                    hanghoa.TenHH = dt.Rows[i][1].ToString();
                                    hanghoa.HangSX = dt.Rows[i][2].ToString();
                                    hanghoa.XuatXu = dt.Rows[i][3].ToString();
                                    hanghoa.DonGia = Convert.ToInt32(dt.Rows[i][4]);
                                    _context.Add(hanghoa);
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
            var fileName = "Danhsachhanghoa" + ".xlsx";
            using(ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                worksheet.Cells["A1"].Value = "Mã hàng";
                worksheet.Cells["B1"].Value = "Tên hàng";
                worksheet.Cells["C1"].Value = "Hãng SX";
                worksheet.Cells["D1"].Value = "Xuất xứ";
                worksheet.Cells["E1"].Value = "Đơn giá";
                var hanghoaList = _context.HangHoa.ToList();
                worksheet.Cells["A2"].LoadFromCollection(hanghoaList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
        private bool HangHoaExists(string id)
        {
            return (_context.HangHoa?.Any(e => e.MaHH == id)).GetValueOrDefault();
        }
    }
}