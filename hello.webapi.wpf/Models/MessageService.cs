using System;
using System.Linq;
using System.Windows;

namespace hello.webapi.wpf.Models
{
    public class MessageService : IMessageService
    {
        public MessageResult Show(string message, string caption = null, MessageButton button = MessageButton.Ok,
            MessageImage icon = MessageImage.None)
        {
            return ShowMessageBox(message, caption ?? string.Empty, button, icon);
        }

        private static MessageResult ShowMessageBox(string message, string caption, MessageButton button,
            MessageImage icon)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException(nameof(message));

            var messageBoxButton = TranslateMessageButton(button);
            var messageBoxImage = TranslateMessageImage(icon);

            var activeWindow = GetActiveWindow();
            var result = activeWindow != null
                ? MessageBox.Show(activeWindow, message, caption, messageBoxButton, messageBoxImage)
                : MessageBox.Show(message, caption, messageBoxButton, messageBoxImage);

            return TranslateMessageBoxResult(result);
        }

        private static MessageResult TranslateMessageBoxResult(MessageBoxResult result)
        {
            var value = result.ToString();
            return (MessageResult) Enum.Parse(typeof(MessageResult), value, true);
        }

        private static MessageBoxImage TranslateMessageImage(MessageImage image)
        {
            var value = image.ToString();
            return (MessageBoxImage) Enum.Parse(typeof(MessageBoxImage), value, true);
        }

        private static Window GetActiveWindow()
        {
            if (Application.Current == null) return null;

            var active = Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);
            return active ?? Application.Current.MainWindow;
        }

        private static MessageBoxButton TranslateMessageButton(MessageButton button)
        {
            try
            {
                var value = button.ToString();
                return (MessageBoxButton) Enum.Parse(typeof(MessageBoxButton), value, true);
            }
            catch (Exception)
            {
                throw new NotSupportedException(
                    $"Unfortunately, the default Message Box class of does not support '{button}' button.");
            }
        }
    }
}