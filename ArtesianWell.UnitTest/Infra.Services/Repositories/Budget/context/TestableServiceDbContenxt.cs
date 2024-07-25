using Infra.Service.Context;
using Microsoft.EntityFrameworkCore;

namespace ArtesianWell.UnitTest.Infra.Services.Repositories.Budget.context;

public class TestableServiceDBContext : ServiceDBContext
{
    public TestableServiceDBContext() : base(new DbContextOptions<ServiceDBContext>())
    {
    }
}