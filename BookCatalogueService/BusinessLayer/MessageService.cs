using BookCatalogue.Constants;
using BookCatalogue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Web;

namespace BookCatalogue.BusinessLayer
{
    public class MessageService: IMessageService
    {
        /// <summary>
        /// Adding the message to the queue when there is add, update or delete event on books
        /// </summary>
        /// <param name="messageToQueue"></param>
        /// <returns></returns>
        public bool AddToQueue(string messageToQueue)
        {
            bool isSuccess = false;
            MessageQueue msgQ = null;
            try
            {
                if(!MessageQueue.Exists(AppConstants.QUEUEPATH))
                {
                    MessageQueue.Create(AppConstants.QUEUEPATH);
                }
                msgQ = new MessageQueue(AppConstants.QUEUEPATH);
                msgQ.Path = AppConstants.QUEUEPATH;
                Message msg = new Message();
                msg.Body = messageToQueue;
                msgQ.Send(msg);
                isSuccess = true;
            }
            catch //(MessageQueueException ex)
            {
                //write exception to log
            }
            finally
            {
                msgQ.Dispose();
            }
            return isSuccess;
        }
    }
}