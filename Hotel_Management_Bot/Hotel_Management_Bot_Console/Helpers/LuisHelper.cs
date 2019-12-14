using Hotel_Management_Bot_Console.Settings;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using System;
using System.Threading.Tasks;

namespace Hotel_Management_Bot_Console.Helpers
{
    class LuisHelper
    {
        static LUISRuntimeClient CreateClient()
        {
            var credentials = new ApiKeyServiceClientCredentials(AppSettingsLuis.PredictionKey);
            var luisClient = new LUISRuntimeClient(credentials, new System.Net.Http.DelegatingHandler[] { })
            {
                Endpoint = AppSettingsLuis.PredictionEndpoint
            };
            return luisClient;
        }

        public static async Task<PredictionResponse> GetPredictionAsync(string query)
        {

            // Get client 
            using (var luisClient = CreateClient())
            {

                var requestOptions = new PredictionRequestOptions
                {
                    DatetimeReference = DateTime.Parse("2019-01-01"),
                    PreferExternalEntities = true
                };

                var predictionRequest = new PredictionRequest
                {
                    Query = query,
                    Options = requestOptions
                };

                // get prediction
                return await luisClient.Prediction.GetSlotPredictionAsync(
                    Guid.Parse(AppSettingsLuis.AppId),
                    slotName: "staging",
                    predictionRequest,
                    verbose: true,
                    showAllIntents: false,
                    log: true);
            }
        }
    }
}
