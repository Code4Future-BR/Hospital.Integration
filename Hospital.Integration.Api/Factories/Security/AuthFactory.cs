using Hospital.Integration.Application.QueriesHandlers.Security;
using System.Text.Json;

namespace Hospital.Integration.Api.Factories.Security;

public static class AuthFactory
{
    public static UserValidateCredentialQuery? FromPost(string request)
    {
        var requestModel = RequestFactory.From(request);

        if (string.IsNullOrEmpty(requestModel.ModelBase64) || requestModel.ModelBase64 == "{}")
        {
            return null;
        }

        return JsonSerializer.Deserialize<UserValidateCredentialQuery>(requestModel.ModelBase64);
    }
}
