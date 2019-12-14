using Newtonsoft.Json;


namespace Hotel_Management_Bot_Console.Models
{
    class IntroductionModel
    {
        [JsonProperty("topScoringIntent.intent")]
        public string TopIntentName { get; set; }
        
        [JsonProperty("PersonName")]
        public string PersonName { get; set; }
    }
}
