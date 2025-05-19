using JobTracking.Application.Contracts;
using JobTracking.Application.Contracts.Base;
using JobTracking.DataAccess.Data;

namespace JobTracking.Application.Implementation;

public class UserService : IUserService
{
    protected DependancyProvider Provider { get; set; }

    public UserService(DependancyProvider provider)
    {
        Provider = provider;
    }

    public Task<IQueryable<User>> GetAllUsers(int page = 1, int pageCount)
    {
        var query = Provider.Db.Users
            .SKip(page - 1 * pageCount);
            .Take(pageCount);
            .ToListAsync();

        /* query = query.Where(s => s.Name == "preslava");

         var result = query.ToList();
         foreach (var VARIABLE in query.ToList()
         {

         }*/    
    }

    public Task<UserResponseDTO> GetUser(int UserId) // ti imash samo id opravi go
    {
        return Provider.Db.Users
            .Where(x => x.Id == UserId)
            .Select(x => new UserResponseDTO()
                {
                    //po bazata neshtata
                }
            .FirstAsync();
    }

    public Task<IQueryable<User>> GetUser(int singerId)
    {
        throw new NotImplementedException();
    }
}
