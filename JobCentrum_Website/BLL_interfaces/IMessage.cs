using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_interfaces
{
    public interface IMessage
    {
        public int Message_Id { get; }

        public string Message_subject { get; }

        public string Message_Text { get; }

        void SendMessage();
        void RemoveMessage();
    }
}
