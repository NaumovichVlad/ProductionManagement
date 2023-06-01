using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Xml.Linq;

namespace ProductionManagementClient.Models
{
    public class PurchaseModel : ModelBase
    {
        private int _id;
        private string _orderNumber;
        private DateTime _manufactureDate;
        private string _manufactureCountry;
        private int _counteragentId;
        private string _counteragentName;

        public int Id 
        { 
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
        public string OrderNumber 
        {
            get => _orderNumber;
            set
            {
                _orderNumber = value;
                OnPropertyChanged();
            }
        }

        public DateTime ManufactureDate 
        {
            get => _manufactureDate;
            set
            {
                _manufactureDate = value;
                OnPropertyChanged();
            }
        }
        public string ManufactureCountry 
        {
            get => _manufactureCountry;
            set
            {
                _manufactureCountry = value;
                OnPropertyChanged();
            }
        }
        public int CounteragentId 
        {
            get => _counteragentId;
            set
            {
                _counteragentId = value;
                OnPropertyChanged();
            }
        }
        public string CounteragentName
        {
            get => _counteragentName;
            set
            {
                _counteragentName = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return OrderNumber;
        }
    }
}
