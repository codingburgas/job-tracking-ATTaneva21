using JobTracking.DataAccess.Persistance;
namespace JobTracking.Application.Contracts.Base;

public class DependancyProvider
{
    public DependancyProvider(AppDbContext dbContext)
    {
        Db = dbContext;
    }
    
    public AppDbContext Db { get; set;  }
}