using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MainApplication.HelpClasses;
using MainApplication.Infrastructure.Commands.Base;
using MainApplication.Models;

namespace MainApplication.ViewModels
{
    internal class RegistrationWindowViewModel : Base.ViewModelBase, INotifyDataErrorInfo
    {
        private readonly char[] numChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private readonly UserType userType;

        #region Commands

        public ICommand CloseWindowCommand { get; }
        private bool CanCloseWindowCommandExecute(object p) => true;

        /// <summary>
        /// Closes window.
        /// </summary>
        /// <param name="p"> Window. </param>
        private void OnCloseWindowCommandExecuted(object p) => (p as Window)?.Close();

        public ICommand RegisterUserCommand { get; }
        private bool CanRegisterUserCommandExecute(object p) => !HasErrors;

        /// <summary>
        /// Crates user.
        /// </summary>
        /// <param name="p"> Window. </param>
        private void OnRegisterUserCommandExecuted(object p)
        {
            if (userType == UserType.Buyer)
            {
                Client.Buyers.Add(new Client(Name, SurName, Patronymic, Email, (Password + Address + TelNumber).GetSHA256(), TelNumber, Address, UserType.Buyer));
            }
            else
            {
                Client.Sellers.Add(new Client(Name, SurName, Patronymic, Email, (Password + Address + TelNumber).GetSHA256(), TelNumber, Address, UserType.Seller));
            }
            CloseWindowCommand.Execute(p);
        }

        #endregion

        #region Properties

        private string email;
        public string Email
        {
            get => email;
            set
            {
                Set(ref email, value);
                ClearErrors(nameof(Email));
                if (Email.Count(x => x == '@') != 1 || Email.Count(x => x == '.') != 1)
                {
                    AddError(nameof(Email), "Incorrect email");
                }
                else if (Client.Buyers.Any(x => x.Email.Equals(Email)) || Client.Sellers.Any(x => x.Email.Equals(Email)))
                {
                    AddError(nameof(Email), "User with same email is already registered");
                }
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                Set(ref password, value);
                ClearErrors(nameof(Password));
                if (Password.Length < 6)
                {
                    AddError(nameof(Password), "Password is too weak");
                }
            }
        }

        private string telNumber;

        public string TelNumber
        {
            get => telNumber;
            set
            {
                Set(ref telNumber, value);
                ClearErrors(nameof(TelNumber));
                if (TelNumber.Any(x => numChars.All(y => y != x)) || TelNumber.Length != 11)
                {
                    AddError(nameof(TelNumber), "Incorrect phone number");
                }
            }
        }

        private string address;
        public string Address
        {
            get => address;
            set => Set(ref address, value);
        }

        private string name;
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        private string surName;
        public string SurName
        {
            get => surName;
            set => Set(ref surName, value);
        }

        private string patronymic;
        public string Patronymic
        {
            get => patronymic;
            set => Set(ref patronymic, value);
        }

        #endregion

        public RegistrationWindowViewModel(UserType userType)
        {
            this.userType = userType;

            Email = "@Email.ru";
            Password = "Password";
            TelNumber = "87771232233";
            Address = "Address";
            Name = "Name";
            SurName = "Surname";
            Patronymic = "Patronymic";

            CloseWindowCommand = new RelayCommand(OnCloseWindowCommandExecuted, CanCloseWindowCommandExecute);
            RegisterUserCommand = new RelayCommand(OnRegisterUserCommandExecuted, CanRegisterUserCommandExecute);
        }

        // INotifyDataErrorInfo Implementation.
        public IEnumerable GetErrors(string propertyName) => propertyErrors.GetValueOrDefault(propertyName, null);

        public bool HasErrors => propertyErrors.Any();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private readonly Dictionary<string, List<string>> propertyErrors = new Dictionary<string, List<string>>();

        public void AddError(string propertyName, string errorMessage)
        {
            if (!propertyErrors.ContainsKey(propertyName))
            {
                propertyErrors.Add(propertyName, new List<string>());
            }

            propertyErrors[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void ClearErrors(string propertyName)
        {
            if (propertyErrors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }
    }
}
