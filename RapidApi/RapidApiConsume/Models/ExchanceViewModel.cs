namespace RapidApiConsume.Models
{
    public class ExchanceViewModel // bu yap next yap yani içi içe yapılar
    {
        // yapıştırırken paste speasial diye seçenek var onu json formatında yapıştır
        //bunu edit - orda var paste spesial
      
            public Exchange_Rates[] exchange_rates { get; set; }
            public string base_currency_date { get; set; }
            public string base_currency { get; set; }
        

        public class Exchange_Rates // amaç bunun içindeki propertylere ulaşmak
        {
            public string currency { get; set; }
            public string exchange_rate_buy { get; set; }
        }

    }
}
