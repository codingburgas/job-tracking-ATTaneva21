namespace JobTracking.Application.Contracts;

public interface IUserService
{
    public Task<List<User>> GetAllUsers(int page, int pageCount);
    public Task<User> GetUser(int UserId);
}