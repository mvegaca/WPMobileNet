using System.Threading.Tasks;
using System.Windows;

namespace WPMobileNet.Service
{
    public class MessageBoxService
    {
        internal enum MessageButtonType
        {
            Ok,
            OkCancel,
            YesNo
        }

        internal MessageBoxResult Show(string message) { return this.Show(string.Empty, message, MessageButtonType.Ok); }
        internal MessageBoxResult Show(string title, string message, MessageButtonType messageButtonType)
        {
            switch (messageButtonType)
            {
                case MessageButtonType.Ok:
                    return MessageBox.Show(message, title, MessageBoxButton.OK);                    
                case MessageButtonType.OkCancel:
                    return MessageBox.Show(message, title, MessageBoxButton.OKCancel);                    
                case MessageButtonType.YesNo:
                    return MessageBox.Show(message, title, MessageBoxButton.OKCancel);                    
                default:
                    return MessageBox.Show(message, title, MessageBoxButton.OK);                    
            }
        }

        internal bool Ask(string title, string message, MessageButtonType messageButtonType)
        {
            MessageBoxResult result;
            switch (messageButtonType)
            {
                case MessageButtonType.Ok:
                    result = MessageBox.Show(message, title, MessageBoxButton.OK);
                    break;
                case MessageButtonType.OkCancel:
                    result = MessageBox.Show(message, title, MessageBoxButton.OKCancel);
                    break;
                case MessageButtonType.YesNo:
                    result = MessageBox.Show(message, title, MessageBoxButton.OKCancel);
                    break;
                default:
                    result = MessageBox.Show(message, title, MessageBoxButton.OKCancel);
                    break;
            }
            if (result.Equals(MessageBoxResult.OK) || result.Equals(MessageBoxResult.Yes)) return true;
            else return false;
        }
        public MessageBoxService() { }
    }
}
