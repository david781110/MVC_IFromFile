using Microsoft.AspNetCore.Mvc;
using MVC_IFromFile.Models;
using System.Diagnostics;

namespace MVC_IFromFile.Controllers
{
    public class HomeController : Controller
    {
        //(Web伺服器環境組態)
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;   
        }

        public IActionResult Index()
        {
            return View();
        }

        //單一檔案上傳
        public IActionResult Index1()
        {
            return View();
        }

        //複數檔案上傳
        public IActionResult Index2()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        //單一檔案
        public IActionResult UploadFile(IFormFile File1) 
        {
            //單一檔案IFormFile
            //複數檔案IFormFileCollection、List<IFormFile>、IEnumerable<IFormFile>
            //IFormFile.Name 此檔案欄位在表單的名稱
            //IFormFile.FileName 檔案來源名稱(無路徑)
            //IFormFile.Length: 檔案大小

            //檔案儲存路徑
            string strFilePath = _webHostEnvironment.ContentRootPath + @"\wwwroot\images\";

            //大於0代表有檔案
            if(File1.Length > 0)
            {
                //取得上傳檔名
                var FileName = File1.FileName;
                //開啟或覆寫上傳的檔案
                using (var varStream = System.IO.File.Create(strFilePath+ FileName))
                {
                    //儲存上傳檔案至伺服端
                    File1.CopyTo(varStream);
                }
            }
            return Content("上傳單一檔案成功");
        }
        //複數檔案
        public IActionResult UploadFiles(List<IFormFile> Files)
        {
            //單一檔案IFormFile
            //複數檔案IFormFileCollection、List<IFormFile>、IEnumerable<IFormFile>
            //IFormFile.Name 此檔案欄位在表單的名稱
            //IFormFile.FileName 檔案來源名稱(無路徑)
            //IFormFile.Length: 檔案大小

            //檔案儲存路徑
            string strFilePath = _webHostEnvironment.ContentRootPath + @"\wwwroot\images\";

            foreach (var file in Files)
            {
                //取得上傳檔名
                var FileName = file.FileName;
                //開啟或覆寫上傳的檔案
                using (var varStream = System.IO.File.Create(strFilePath + FileName))
                {
                    //儲存上傳檔案至伺服端
                    file.CopyTo(varStream);
                }

            }
            
            return Content("上傳複數檔案成功");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}