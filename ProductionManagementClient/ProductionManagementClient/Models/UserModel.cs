using System;

namespace ProductionManagementClient.Models
{
    [Serializable]
    public class UserModel : ModelBase
    {
        private int _id;
        private string _login;
        private string _password;
        private int _roleId;
        private string _role;
        private int _employeeId;
        private string _employeeSurname;
        private string _employeeName;
        private string _employeeMiddleName;


        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string RoleName
        {
            get
            {
                return _role;
            }
            set
            {
                _role = value;
                OnPropertyChanged(nameof(RoleName));
            }
        }

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string EmployeeSurname
        {
            get
            {
                return _employeeSurname;
            }
            set
            {
                _employeeSurname = value;
                OnPropertyChanged(nameof(EmployeeSurname));
            }
        }

        public string EmployeeName
        {
            get
            {
                return _employeeName;
            }
            set
            {
                _employeeName = value;
                OnPropertyChanged(nameof(EmployeeName));
            }
        }

        public string EmployeeMiddleName
        {
            get
            {
                return _employeeMiddleName;
            }
            set
            {
                _employeeMiddleName = value;
                OnPropertyChanged(nameof(EmployeeMiddleName));
            }
        }

        public int EmployeeId
        {
            get
            {
                return _employeeId;
            }
            set
            {
                _employeeId = value;
                OnPropertyChanged(nameof(EmployeeId));
            }
        }

        public int RoleId
        {
            get
            {
                return _roleId;
            }
            set
            {
                _roleId = value;
                OnPropertyChanged(nameof(RoleId));
            }
        }
    }
}
