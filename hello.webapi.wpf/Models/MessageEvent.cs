namespace hello.webapi.wpf.Models
{
    public class MessageEvent
    {
        public MessageEvent()
        {
            Caption = "Information";
            Button = MessageButton.Ok;
            Icon = MessageImage.Information;
        }

        public string Message { get; set; }
        public string Caption { get; set; }
        public MessageButton Button { get; set; }
        public MessageImage Icon { get; set; }

        public MessageResult Result { get; set; }

        public static MessageEvent Info(string message)
        {
            return new MessageEvent {Message = message};
        }

        public static MessageEvent Warning(string message)
        {
            return new MessageEvent
            {
                Message = message,
                Caption = "Warning",
                Icon = MessageImage.Warning
            };
        }

        public static MessageEvent Error(string message)
        {
            return new MessageEvent
            {
                Message = message,
                Caption = "Error",
                Icon = MessageImage.Error
            };
        }

        public static MessageEvent Question(string message)
        {
            return new MessageEvent
            {
                Message = message,
                Caption = "Question",
                Button = MessageButton.YesNoCancel,
                Icon = MessageImage.Question
            };
        }
    }
}