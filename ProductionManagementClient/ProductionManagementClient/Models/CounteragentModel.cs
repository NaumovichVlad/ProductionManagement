using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionManagementClient.Models
{
    public class CounteragentModel : ModelBase
    {
        private int _id;
        private string _name;
        private int _inn;
        private int _accountNumber;
        private string _address;
        private int _postalCode;
        private string _phoneNumber;
        private string _registrationCountry;

        public int Id 
        { 
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Name 
        { 
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public int Inn
        {
            get => _inn;
            set
            {
                _inn = value;
                OnPropertyChanged();
            }
        }

        public int AccountNumber
        {
            get => _accountNumber;
            set
            {
                _accountNumber = value;
                OnPropertyChanged();
            }
        }

        public string Address 
        { 
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }

        public int PostalCode
        {
            get => _postalCode;
            set
            {
                _postalCode = value;
                OnPropertyChanged();
            }
        }

        public string PhoneNumber 
        { 
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        public string RegistrationCountry 
        { 
            get => _registrationCountry;
            set
            {
                _registrationCountry = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
