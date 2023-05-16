using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Xml.Linq;

namespace ProductionManagementClient.Models
{
    public class MaterialOrderModel : ModelBase
    {
        private int _id;
        private string _orderNumber;
        private int _materialId;
        private double _price;
        private int _count;
        private DateTime _manufactureDate;
        private string _manufactureCountry;
        private int _counteragentId;
        private string _counteragentName;
        private string _materialName;

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
        public int MaterialId 
        {
            get => _materialId;
            set
            {
                _materialId = value;
                OnPropertyChanged();
            }
        }
        public double Price 
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }
        public int Count 
        {
            get => _count;
            set
            {
                _count = value;
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
        public string MaterialName
        {
            get => _materialName;
            set
            {
                _materialName = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return OrderNumber;
        }
    }
}
