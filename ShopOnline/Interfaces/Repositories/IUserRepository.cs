using Models.DataModels;

namespace Interfaces.Repositories
{
    /// <summary>
    /// Defines functionality for a user repository.
    /// </summary>
    public interface IUserRepository
    {
        User GetUserById(int id);
        User Login(string email, string password);
        void Register(User user);
        void EditUser(User user);
    }
}