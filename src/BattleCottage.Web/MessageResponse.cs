﻿namespace BattleCottage.Web
{
    public class MessageResponse
    {
        public MessageResponse(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
