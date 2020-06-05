using Notion.Comman.Dtos;
using RestSharp;
namespace Notion.Api.Helpers
{
    public  class KerasPrediction
    {
        private const string SimpraService = "http://ec2-54-208-76-120.compute-1.amazonaws.com:8080/predict";
        public static PredictionResponse GetPredictionResponse(RequestToPredict requestBody)
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
    public class RequestToPredict
    {
        public string method { get; set; }
        public string query { get; set; }
        public string path { get; set; }
        public string statusCode { get; set; }
        public string requestPayload { get; set; }
    }
}