using ERPXTpl.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;

namespace ERPXTpl.Service
{
    internal static class ResponseService
    {
        public static Result TakeResult(HttpResponseMessage response, string responseBody, object data = null)
        {
            Result result = new Result();
            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
            {
                result.Data = data;
                result.StatusCode = response.StatusCode.ToString();
                result.Message = response.ReasonPhrase;
            }
            else
            {
                var errorMessage = JsonConvert.DeserializeObject<Result.Error>(responseBody);
                result.Data = null;
                result.StatusCode = response.StatusCode.ToString();
                result.Message = errorMessage.Message;
            }
            return result;
        }
    }
}
