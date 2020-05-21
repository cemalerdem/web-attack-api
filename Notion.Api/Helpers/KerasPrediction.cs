using Notion.Comman.Dtos;
using RestSharp;
namespace Notion.Api.Helpers
{
    public static class KerasPrediction
    {
        private const string SimpraService = "http://localhost:5001/predict";
        public static PredictionResponse GetPredictionResponse(RequestDto requestBody)
        {
            var client = new RestClient(SimpraService);
            var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
            request.AddJsonBody(requestBody);
            return client.Execute<PredictionResponse>(request).Data;
        }
    }


    public class PredictionResponse
    {
        public string Result { get; set; }
    }
}