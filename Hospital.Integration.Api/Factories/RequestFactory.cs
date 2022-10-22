using Hospital.Integration.Application.Models;
using System.Text;
using System.Text.Json;

namespace Hospital.Integration.Api.Factories;

public static class RequestFactory
{
    public static Request From(string request)
    {
        var data = Convert.FromBase64String(request);
        var decodeString = ASCIIEncoding.UTF8.GetString(data);
        var requestModel = JsonSerializer.Deserialize<Request>(decodeString);

        return
            new()
            {
                Id = Guid.NewGuid().ToString(),
                Type = requestModel?.Type,
                Sort = requestModel?.Sort,
                Skip = requestModel?.Skip,
                Take = requestModel?.Take,
                DateNow = DateTimeOffset.Now,
                ModelBase64 = requestModel?.ModelBase64 ?? null,
            };
    }
}
