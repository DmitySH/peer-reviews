using MainApplication.ViewModels.Base;

namespace MainApplication.ViewModels
{
    /// <summary>
    /// MV of GetTitleWindow.
    /// </summary>
    internal class GetTitleWindowViewModel : ViewModelBase
    {
        // Sends to MainWindow as a result of dialog.

        public static string toSendTitle = "None";
        private string title = "Your_title";
        public string Title
        {
            get => title;
            set
            {
                Set(ref title, value);
                toSendTitle = title;
            }
        }

        public static uint toSendCode = 0;
        private uint code = 0;
        public uint Code
        {
            get => code;
            set
            {
                Set(ref code, value);
                toSendCode = code;
            }
        }

        public GetTitleWindowViewModel()
        {
            toSendTitle = "None";
            toSendCode = 0;
        }
    }
}
