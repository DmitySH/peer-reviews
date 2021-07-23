using System.Linq;
using System.Windows;
using System.Windows.Input;
using MainApplication.HelpClasses;
using MainApplication.Infrastructure.Commands.Base;
using MainApplication.Models;
using MainApplication.Views.Windows;

namespace MainApplication.ViewModels
{
    internal class LoginWindowViewModel : Base.ViewModelBase
    {
        #region Commands
        public ICommand CloseWindowCommand { get; }
        private bool CanCloseWindowCommandExecute(object p) => true;

        /// <summary>
        /// Closes window.
        /// </summary>
        /// <param name="p"> Window. </param>
        private void OnCloseWindowCommandExecuted(object p) => (p as Window)?.Close();

        public ICommand LoginCommand { get; }
        private bool CanLoginCommandExecute(object p) => true;

        /// <summary>
        /// Logins.
        /// </summary>
        /// <param name="p"> Window. </param>
        private void OnLoginUserCommandExecuted(object p)
        {
            if (userType == UserType.Seller)
            {
                Client currentUser;
                if ((currentUser = Client.Sellers.Find(x => x.Email.Equals(Email) && x.Password.Equals((Password + x.Address + x.TelNumber).GetSHA256()))) != null)
                {
                    new SellerWindow() { DataContext = new SellerWindowViewModel(currentUser) }.Show();
                    CloseWindowCommand.Execute(p);
                    prevWindow.Close();
                }
                else if (!Client.Sellers.Any(x => x.Email.Equals(Email)))
                {
                    MessageBox.Show("No seller with such Email", "Authentication error");
                }
                else
                {
                    MessageBox.Show("Incorrect password", "Authentication error");
                }
            }
            else
            {
                Client currentUser;
                if ((currentUser = Client.Buyers.Find(x => x.Email.Equals(Email) && x.Password.Equals((Password + x.Address + x.TelNumber).GetSHA256()))) != null)
                {
                    new BuyerWindow() { DataContext = new BuyerWindowViewModel(currentUser) }.Show();
                    CloseWindowCommand.Execute(p);
                    prevWindow.Close();
                }
                else if (!Client.Buyers.Any(x => x.Email.Equals(Email)))
                {
                    MessageBox.Show("No buyer with such Email", "Authentication error");
                }
                else
                {
                    MessageBox.Show("Incorrect password", "Authentication error");
                }
            }
        }

        #endregion

        #region Properties

        private string email;
        public string Email
        {
            get => email;
            set => Set(ref email, value);
        }

        private string password;
        public string Password
        {
            get => password;
            set => Set(ref password, value);
        }

        #endregion

        private readonly UserType userType;
        private readonly Window prevWindow;

        public LoginWindowViewModel(UserType userType, Window prevWindow)
        {
            this.prevWindow = prevWindow;
            this.userType = userType;

            Password = "Password";
            Email = "@Email.ru";

            CloseWindowCommand = new RelayCommand(OnCloseWindowCommandExecuted, CanCloseWindowCommandExecute);
            LoginCommand = new RelayCommand(OnLoginUserCommandExecuted, CanLoginCommandExecute);
        }
    }
}
