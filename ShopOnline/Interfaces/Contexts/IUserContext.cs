using Models.DataModels;

namespace Interfaces.Contexts
{
    /// <summary>
    /// Defines functionality for a user context.
    /// </summary>
    public interface IUserContext
    {
        User GetUserById(int id);
        User Login(string email, string password);
        void Register(User user);
        void EditUser(User user);
    }
}