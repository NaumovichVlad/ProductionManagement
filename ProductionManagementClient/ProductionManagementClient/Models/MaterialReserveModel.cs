﻿namespace ProductionManagementClient.Models
{
    public class MaterialReserveModel : ModelBase
    {
        private int _id;
        private int _storagePlaceId;
        private int _materialOrderId;
        private int _count;
        private string _storagePlaceName;
        private string _materialOrderNumber;
        private string _materialName;

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
        public int StoragePlaceId
        {
            get
            {
                return _storagePlaceId;
            }
            set
            {
                _storagePlaceId = value;
                OnPropertyChanged();
            }
        }
        public int MaterialPurchaseId
        {
            get
            {
                return _materialOrderId;
            }
            set
            {
                _materialOrderId = value;
                OnPropertyChanged();
            }
        }
        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
                OnPropertyChanged();
            }
        }
        public string StoragePlaceName
        {
            get
            {
                return _storagePlaceName;
            }
            set
            {
                _storagePlaceName = value;
                OnPropertyChanged();
            }
        }
        public string MaterialOrderNumber
        {
            get
            {
                return _materialOrderNumber;
            }
            set
            {
                _materialOrderNumber = value;
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
    }
}
