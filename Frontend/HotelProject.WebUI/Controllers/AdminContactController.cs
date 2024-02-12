﻿using HotelProject.WebUI.Dtos.ContactDto;
using HotelProject.WebUI.Dtos.SendMessageDto;
using HotelProject.WebUI.Models.Staff;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace HotelProject.WebUI.Controllers
{
    public class AdminContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Inbox()
        {
            //api nin consume adilmesi
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:12795/api/Contact");//listeleme , bu adrese istek
            if (responseMessage.IsSuccessStatusCode) 
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();//gelen veriyei jsonData ya taşıdık json türünde
                var values = JsonConvert.DeserializeObject<List<InboxContactDto>>(jsonData); // bu json veriyi deserilaze ederek normal tabloda gösterilecek şekle getirdik.
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> SendBox() // GÜNDERİLEN
        {
            //api nin consume adilmesi
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:12795/api/SendMessage");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultSendboxDto>>(jsonData); 
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddSendMessage() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddSendMessage(CreateSendMessage createSendMessage)
        {
            createSendMessage.SenderMail = "admin@gmail.com";
            createSendMessage.SenderName = "admin";
            createSendMessage.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
            var client = _httpClientFactory.CreateClient(); 
            var jsonData = JsonConvert.SerializeObject(createSendMessage);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("http://localhost:12795/api/SendMessage", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("SendBox"); 
            }
            return View(); 
        }


        public PartialViewResult SideBarAdminContactPartial()
        {
            return PartialView();
        }
        public PartialViewResult SideBarAdminContactCategoryPartial()
        {
            return PartialView();
        }

        public async Task<IActionResult> MessageDetailsBySendbox(int id) // gönderilen detaylarını göre
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:12795/api/SendMessage/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetMessageByIDDto>(jsonData);
                return View(values);
            }
            return View();
        }
        public async Task<IActionResult> MessageDetailsByInbox(int id) //bize gönderilen yani uı katmanından gönderilen mesajların detaylarını görecez
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:12795/api/Contact/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<InboxContactDto>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
