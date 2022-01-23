

using Models.DataModels;

namespace Interfaces.Logic
{
    /// <summary>
    /// Defines functionality for a user logic class.
    /// </summary>
    public interface IUserLogic
    {
        User GetUserById(int id);
        User Login(string email, string password);
        void Register(User user);
        void EditUser(User user);
    }
}