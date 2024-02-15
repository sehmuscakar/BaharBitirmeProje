using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;


namespace HotelProject.WebUI.Controllers
{
    public class AdminImageFileController : Controller //images klsörüne yukluyor resimi
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();  
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {//frontend tarafında consume ediliyor 
            var stream=new MemoryStream();
            await file.CopyToAsync(stream);
            var bytes=stream.ToArray();

            ByteArrayContent byteArrayContent = new ByteArrayContent(bytes);
            byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
            multipartFormDataContent.Add(byteArrayContent, "file", file.FileName);
            var httpclient = new HttpClient();
            var responseMessage = await httpclient.PostAsync("http://localhost:12795/api/FileImage", multipartFormDataContent);


            return View();
        }
    }
}
