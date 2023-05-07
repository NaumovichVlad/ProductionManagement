using ProductionManagementClient.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionManagementClient.Services
{
    public class DataContainer : ModelBase
    {
        private DataTable _table;
        private DataTypes _dataType;
        private int pageNumber;
        private int pageSize;
        private string sortParam;
        private string sortDirection;

        public DataTable Table
        {
            get => _table;
            set
            {
                _table = value;
                OnPropertyChanged();
            }
        }
        public DataTypes DataType 
        { 
            get => _dataType;
            set
            {
                _dataType = value;
                OnPropertyChanged();
            }
        }

        public int PageNumber { get => pageNumber; set => pageNumber = value; }
        public int PageSize { get => pageSize; set => pageSize = value; }
        public string SortParam { get => sortParam; set => sortParam = value; }
        public string SortDirection { get => sortDirection; set => sortDirection = value; }
    }
}
