using System.Windows;

namespace MainApplication.Views.Windows
{
    /// <summary>
    /// Interaction logic for GetTitleWindow.xaml
    /// </summary>
    public partial class GetTitleWindow : Window
    {
        // Here I forgot that it is MVVM =)
        // So i did one not truly MVVM window.
        public GetTitleWindow()
        {
            InitializeComponent();
        }

        private bool onceExecuted = false;

        /// <summary>
        /// Sends dialog result.
        /// </summary>
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        /// <summary>
        /// Clears text box.
        /// </summary>
        private void TitleBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (!onceExecuted)
            {
                titleBox.Text = string.Empty;
                onceExecuted = true;
            }
        }
    }
    
}
