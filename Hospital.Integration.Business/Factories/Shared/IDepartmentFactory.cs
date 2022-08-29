using Hospital.Integration.Business.Entities;
using Hospital.Integration.Business.Models;

namespace Hospital.Integration.Business.Factories;

public interface IDepartmentFactory
{
    Department? From(Request request);

    Department FromCreate(Request request, string userId);

    Department FromUpdate(Request request, string userId);
}
