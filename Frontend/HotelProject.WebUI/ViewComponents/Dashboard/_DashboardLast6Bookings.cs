using HotelProject.WebUI.Dtos.BookingDto;
using HotelProject.WebUI.Dtos.StaffDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelProject.WebUI.ViewComponents.Dashboard
{
    public class _DashboardLast6Bookings:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DashboardLast6Bookings(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //api nin consume adilmesi
            var client = _httpClientFactory.CreateClient();//bir istemci oluştur.
            var responseMessage = await client.GetAsync("http://localhost:12795/api/Booking/Last6Booking");//listeleme , bu adrese istek
            if (responseMessage.IsSuccessStatusCode) //başarılı ise
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBookingDto>>(jsonData); 
                return View(values);
            }
            return View();
        }
    }
}
