using System.Text;
using System.Text.Json;
using Authentication.Shared.Common;

namespace Shared.Utils;

public static class HttpContentUtil
{
    public static HttpContent GetHttpContent<T>(T request, ContentType contentType = ContentType.Json, JsonSerializerOptions? jsonSerializerOptions = null)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        switch (contentType)
        {
            case ContentType.Json:
                var jsonRequest = JsonSerializer.Serialize(request, jsonSerializerOptions);
                return new StringContent(jsonRequest, Encoding.UTF8, CommonConsts.AcceptValue);
            
            case ContentType.FormData:
                var formDataContent = new MultipartFormDataContent();
                
                foreach (var property in typeof(T).GetProperties())
                {
                    formDataContent.Add(new StringContent(property.GetValue(request)?.ToString() ?? ""), property.Name);
                }
                return formDataContent;
            case ContentType.FormUrlEncoded:
                var urlEncodedContent = new FormUrlEncodedContent(
                    from prop in typeof(T).GetProperties()
                    select new KeyValuePair<string, string>(prop.Name, prop.GetValue(request)?.ToString() ?? "")
                );

                return urlEncodedContent;
            default:
                throw new ArgumentException("Invalid content type.", nameof(contentType));
        }
    }
}