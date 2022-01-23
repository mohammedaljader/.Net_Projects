using DAL_interfaces.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_interfaces.Interfaces
{
    public interface IMessageDAL
    {
        void SendMessage(MessageDto message);
        void DeleteMessage(MessageDto message);
        void EditMessage(MessageDto message);
        List<MessageDto> GetAllMessages(int id);
    }
}
