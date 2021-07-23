using System.Windows;
using System.Windows.Input;
using MainApplication.Infrastructure.Commands.Base;
using MainApplication.Models;
using MainApplication.Views.Windows;

namespace MainApplication.ViewModels
{
    internal class StartUpWindowViewModel : Base.ViewModelBase
    {
        public ICommand CloseWindowCommand { get; }
        private bool CanCloseWindowCommandExecute(object p) => true;

        /// <summary>
        /// Closes window.
        /// </summary>
        /// <param name="p"> Window. </param>
        private void OnCloseWindowCommandExecuted(object p) => (p as Window)?.Close();

        public ICommand RegisterBuyerCommand { get; }
        private bool CanRegisterBuyerCommandExecute(object p) => true;

        /// <summary>
        /// Registers new buyer.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        private void OnRegisterBuyerExecuted(object p) => new RegistrationWindow()
        { DataContext = new RegistrationWindowViewModel(UserType.Buyer) }.ShowDialog();

        public ICommand RegisterSellerCommand { get; }
        private bool CanRegisterSellerCommandExecute(object p) => true;

        /// <summary>
        /// Registers new seller.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        private void OnRegisterSellerCommandExecuted(object p) => new RegistrationWindow()
        { DataContext = new RegistrationWindowViewModel(UserType.Seller) }.ShowDialog();


        public ICommand LoginBuyerCommand { get; }
        private bool CanLoginBuyerCommandExecute(object p) => true;

        /// <summary>
        /// Logins buyer.
        /// </summary>
        /// <param name="p"> Window. </param>
        private void OnLoginBuyerCommandExecuted(object p) => new LoginWindow()
            { DataContext = new LoginWindowViewModel(UserType.Buyer, p as Window) }.ShowDialog();

        public ICommand LoginSellerCommand { get; }

        /// <summary>
        /// Logins seller.
        /// </summary>
        /// <param name="p"> Window. </param>
        private void OnLoginSellerCommandExecuted(object p) => new LoginWindow()
            { DataContext = new LoginWindowViewModel(UserType.Seller, p as Window) }.ShowDialog();

        private bool CanLoginSellerCommandExecute(object p) => true;

       
        public StartUpWindowViewModel()
        {
            CloseWindowCommand = new RelayCommand(OnCloseWindowCommandExecuted, CanCloseWindowCommandExecute);
            RegisterBuyerCommand = new RelayCommand(OnRegisterBuyerExecuted, CanRegisterBuyerCommandExecute);
            RegisterSellerCommand = new RelayCommand(OnRegisterSellerCommandExecuted, CanRegisterSellerCommandExecute);
            LoginBuyerCommand = new RelayCommand(OnLoginBuyerCommandExecuted, CanLoginBuyerCommandExecute);
            LoginSellerCommand = new RelayCommand(OnLoginSellerCommandExecuted, CanLoginSellerCommandExecute);

        }
    }
}
