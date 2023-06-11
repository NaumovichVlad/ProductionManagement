namespace ProductionManagementClient.Models
{
    public class MaterialPurchaseModel : ModelBase
    {
        private int id;
        private int materialId;
        private int purchaseId;
        private double price;
        private int count;
        private bool isAccepted;
        private string materialName;
        private string purchaseNumber;

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }
        public int MaterialId
        {
            get => materialId;
            set
            {
                materialId = value;
                OnPropertyChanged();
            }
        }
        public int PurchaseId
        {
            get => purchaseId;
            set
            {
                purchaseId = value;
                OnPropertyChanged();
            }
        }
        public double Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged();
            }
        }
        public int Count
        {
            get => count;
            set
            {
                count = value;
                OnPropertyChanged();
            }
        }
        public bool IsAccepted
        {
            get => isAccepted;
            set
            {
                isAccepted = value;
                OnPropertyChanged();
            }
        }
        public string MaterialName
        {
            get => materialName;
            set
            {
                materialName = value;
                OnPropertyChanged();
            }
        }
        public string PurchaseNumber
        {
            get => purchaseNumber;
            set
            {
                purchaseNumber = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return $"{PurchaseNumber}: {MaterialName}";
        }
    }
}
