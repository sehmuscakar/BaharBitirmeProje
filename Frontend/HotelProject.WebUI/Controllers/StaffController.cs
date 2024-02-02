﻿using HotelProject.WebUI.Models.Staff;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace HotelProject.WebUI.Controllers
{
    public class StaffController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public StaffController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index1()
        {
            //api nin consume adilmesi
            var client = _httpClientFactory.CreateClient();//bir istemci oluştur.
            var responseMessage = await client.GetAsync("http://localhost:12795/api/Staff");//listeleme , bu adrese istek
            if (responseMessage.IsSuccessStatusCode) //başarılı ise
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();//gelen veriyei jsonData ya taşıdık json türünde
                var values = JsonConvert.DeserializeObject<List<StaffViewModel>>(jsonData); // bu json veriyi deserilaze ederek normal tabloda gösterilecek şekle getirdik.
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddStaff()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddStaff(AddStaffViewModel model)
        {
            var client= _httpClientFactory.CreateClient(); //HttpClientFactory kullanılarak bir HTTP istemcisini (client) oluşturuluyor.
            var jsonData = JsonConvert.SerializeObject(model);//model adlı bir nesnenin JSON formatına dönüştürülmesi için Newtonsoft.Json kütüphanesi (JsonConvert) kullanılıyor.
            StringContent stringContent=new StringContent(jsonData,Encoding.UTF8,"application/json");//Dönüştürülen JSON verisi, StringContent sınıfı aracılığıyla bir HTTP içeriği oluşturmak için kullanılıyor. Bu içerik, UTF-8 karakter kodlaması ve "application/json" medya türü ile ayarlanmıştır.
            var responseMessage = await client.PostAsync("http://localhost:12795/api/Staff",stringContent);//Oluşturulan içerikle birlikte client, belirtilen URL'ye ("http://localhost:12795/api/Staff") HTTP POST isteği gönderiyor.
            if (responseMessage.IsSuccessStatusCode)//Yanıt, responseMessage değişkeninde saklanıyor.
            {
                return RedirectToAction("Index1"); //Eğer yanıtın durumu başarılı (IsSuccessStatusCode), Index adlı başka bir eyleme yönlendirme (RedirectToAction) yapılıyor.
            }
            return View(); //Eğer yanıt başarısızsa, mevcut görünümü tekrar göstermek için View döndürülüyor.
        }
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var client = _httpClientFactory.CreateClient();  
            var responseMessage = await client.DeleteAsync($"http://localhost:12795/api/Staff/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index1"); 
            }
            return View(); 
        }
        [HttpGet]
        public async Task<IActionResult> UpdateStaff(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:12795/api/Staff/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateStaffViewModel>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStaff(UpdateStaffViewModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:12795/api/Staff", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index1"); 
            }
            return View(); 
        }
    }
}
