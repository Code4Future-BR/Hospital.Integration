using Hospital.Integration.Business.Models;

namespace Hospital.Integration.Business.Factories;

public interface IRequestFactory
{
    Request From(string request);
}
