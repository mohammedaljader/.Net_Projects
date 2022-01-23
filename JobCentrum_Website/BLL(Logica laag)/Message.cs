using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL_interfaces;

namespace BLL_Logica_laag_
{
    public class Message : IMessage
    {
        #region Properties
        public int Message_Id { get; }
        public string Message_subject { get; }
        public string Message_Text { get; }
        #endregion

        #region Constractors
        public Message(int messageId, string messageSubject, string message)
        {
            Message_Id = messageId;
            Message_subject = messageSubject;
            this.Message_Text = message;
        }
        #endregion

        #region Methodes
        public void RemoveMessage()
        {
            throw new NotImplementedException();
        }

        public void SendMessage()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
