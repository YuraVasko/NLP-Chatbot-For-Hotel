using Newtonsoft.Json;

namespace Hotel_Management_Bot_Console.Models
{
    class TopIntentDataModel
    {
        [JsonProperty("topScoringIntent.intent")]
        public string IntentName { get; set; }
    }
}
