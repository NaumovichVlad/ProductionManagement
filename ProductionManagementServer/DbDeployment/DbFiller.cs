using DataAccess.Config;
using DataAccess.Entities;

namespace DbDeployment
{
    public class DbFiller
    {
        private readonly ProductionManagementDbContext _context;
        private Random _random;
        private int _firstCount;
        private int _secondCount;
        private int _thirdCount;

        public DbFiller(ProductionManagementDbContext context, int firstCount, int secondCount, int thirdCount)
        {
            _context = context;
            _firstCount = firstCount;
            _secondCount = secondCount;
            _thirdCount = thirdCount;
            _random = new Random();
        }

        public void FillAllTables()
        {
            AddAdmin();

            /*FillStoragePlaces();
            _context.SaveChanges();

            FillEmployees();
            _context.SaveChanges();

            FillUsers();
            _context.SaveChanges();
            
            FillSalaries();
            _context.SaveChanges();

            FillCounteragents();
            _context.SaveChanges();

            FillMaterials();
            _context.SaveChanges();

            FillPurchases();
            _context.SaveChanges();

            FillProducts();
            _context.SaveChanges();

            FillProductOrders();
            _context.SaveChanges();

            FillFinishedProducts();
            _context.SaveChanges();

            FillMaterialsForProducts();
            _context.SaveChanges();*/
        }

        private void AddAdmin()
        {
            FillRoles();
            _context.SaveChanges();

            var employees = _context.Set<Employee>();

            employees.Add(new Employee()
            {
                Surname = "Наумович",
                Name = "Владислав",
                MiddleName = "Олегович",
                PassportNumber = "KB23132432",
                PhoneNumber = "+375445463423",
                Address = "г. Бобруйск, ул. Горелика"
            });

            _context.SaveChanges();

            var users = _context.Set<User>();

            users.Add(new User()
            {
                EmployeeId = 1,
                Login = "admin",
                Password = "admin",
                RoleId = 1
            });

            _context.SaveChanges();
        }
        private void FillRoles()
        {
            var data = _context.Set<Role>();

            data.Add(new Role { Name = "admin" });
            data.Add(new Role { Name = "logistics" });
            data.Add(new Role { Name = "purchases" });
            data.Add(new Role { Name = "sales" });
            data.Add(new Role { Name = "hr" });
        }

        private void FillStoragePlaces()
        {
            var data = _context.Set<StoragePlace>();
            var entities = new List<StoragePlace>();

            for (var i = 0; i < _firstCount; i++)
            {
                var entity = new StoragePlace();

                entity.Id = i + 1;
                entity.Name = $"Склад_{i + 1}";

                entities.Add(entity);
            }

            data.AddRange(entities);
        }

        private void FillEmployees()
        {
            var data = _context.Set<Employee>();
            var entities = new List<Employee>();

            for (var i = 0; i < _secondCount; i++)
            {
                var entity = new Employee();

                entity.Id = i + 1;
                entity.Surname = $"Фамилия_{i + 1}";
                entity.Name = $"Имя_{i + 1}";
                entity.MiddleName = $"Отчество_{i + 1}";
                entity.PassportNumber = $"KB{_random.Next(100000, 999999)}";
                entity.Address = $"Адрес_{i + 1}";
                entity.PhoneNumber = $"+37529{_random.Next(1000000, 9999999)}";

                entities.Add(entity);
            }

            data.AddRange(entities);
        }

        private void FillUsers()
        {
            var data = _context.Set<User>();
            var entities = new List<User>();

            entities.Add(new User { Id = 1, RoleId = 1, EmployeeId = 1, Login = "admin", Password = "admin" });

            for (var i = 1; i < _secondCount; i++)
            {
                var entity = new User();

                entity.Id = i + 1;
                entity.RoleId = _random.Next(2, 5);
                entity.Login = $"login_{i + 1}";
                entity.Password = $"password_{i + 1}";
                entity.EmployeeId = i + 1;

                entities.Add(entity);
            }

            data.AddRange(entities);
        }

        private void FillSalaries()
        {
            var data = _context.Set<Salary>();
            var entities = new List<Salary>();

            for (var i = 0; i < _thirdCount; i++)
            {
                var entity = new Salary();

                entity.Id = i + 1;
                entity.PaymentDate = GetRandomDate(2017, 1, 1);

                var paid = _random.Next(700, 2000);

                entity.Accrued = paid;
                entity.Paid = paid;
                entity.ToBePaid = paid;
                entity.EmployeeId = _random.Next(1, _secondCount - 1);

                entities.Add(entity);
            }

            data.AddRange(entities);
        }

        private void FillCounteragents()
        {
            var data = _context.Set<Counteragent>();
            var entities = new List<Counteragent>();

            for (var i = 0; i < _secondCount; i++)
            {
                var entity = new Counteragent();

                entity.Id = i + 1;
                entity.Name = $"Название_{i + 1}";
                entity.INN = _random.Next(1000000000, int.MaxValue);
                entity.AccountNumber = _random.Next(1000000000, int.MaxValue);
                entity.Address = $"Адрес_{i + 1}";
                entity.PhoneNumber = $"+37529{_random.Next(1000000, 9999999)}";
                entity.PostalCode = _random.Next(10000, 99999);
                entity.RegistrationCountry = $"Страна_{_random.Next(1, _firstCount)}";

                entities.Add(entity);
            }

            data.AddRange(entities);
        }

        private void FillMaterials()
        {
            var data = _context.Set<Material>();
            var entities = new List<Material>();

            for (var i = 0; i < _secondCount; i++)
            {
                var entity = new Material();

                entity.Id = i + 1;
                entity.Name = $"Материал_{i + 1}";

                entities.Add(entity);
            }

            data.AddRange(entities);
        }

        private void FillPurchases()
        {
            var data = _context.Set<Purchase>();
            var entities = new List<Purchase>();

            for (var i = 0; i < _secondCount; i++)
            {
                var entity = new Purchase();

                entity.Id = i + 1;
                entity.OrderNumber = _random.Next(10000000, 99999999).ToString();
                entity.ManufactureDate = GetRandomDate(2010, 1, 1);
                entity.ManufactureCountry = $"Страна_{_random.Next(1, _firstCount)}";
                entity.CounteragentId = _random.Next(1, _secondCount - 1);

                entities.Add(entity);
            }

            data.AddRange(entities);

            FillMaterialsPurchases(entities);
        }

        private void FillMaterialsPurchases(List<Purchase> purchases)
        {
            var data = _context.Set<MaterialPurchase>();
            var entities = new List<MaterialPurchase>();
            var id = 1;
            foreach (var purchase in purchases)
            {
                for (var i = 0; i < _firstCount; i++)
                {
                    var entity = new MaterialPurchase();

                    entity.Id = id;
                    entity.MaterialId = _random.Next(1, _secondCount - 1);
                    entity.PurchaseId = purchase.Id;
                    entity.Price = _random.Next(1000, 10000);
                    entity.Count = _random.Next(10, 1000);
                    entity.IsAccepted = true;

                    entities.Add(entity);

                    id++;
                }
            }

            data.AddRange(entities);

            FillMaterialReserves(entities);
        }

        private void FillMaterialReserves(List<MaterialPurchase> materialsPurchases)
        {
            var data = _context.Set<MaterialReserve>();
            var entities = new List<MaterialReserve>();
            var i = 1;

            foreach (var purchase in materialsPurchases)
            {
                var entity = new MaterialReserve();

                entity.Id = i;
                entity.StoragePlaceId = _random.Next(1, _firstCount - 1);
                entity.MaterialPurchaseId = purchase.Id;
                entity.Count = purchase.Count;

                entities.Add(entity);
                data.Append(entity);
                i++;
            }

            data.AddRange(entities);

        }

        private void FillProducts()
        {
            var data = _context.Set<Product>();
            var entities = new List<Product>();

            for (var i = 0; i < _secondCount; i++)
            {
                var entity = new Product();

                entity.Id = i + 1;
                entity.Name = $"Изделие_{i + 1}";

                entities.Add(entity);
            }

            data.AddRange(entities);
        }

        private void FillProductOrders()
        {
            var data = _context.Set<Sale>();
            var entities = new List<Sale>();

            for (var i = 0; i < _secondCount; i++)
            {
                var entity = new Sale();

                entity.Id = i + 1;
                entity.OrderNumber = _random.Next(10000, 30000);
                entity.OrderDate = GetRandomDate(2019, 1, 1);
                entity.CounteragentId = _random.Next(1, _secondCount - 1);

                entities.Add(entity);
            }

            data.AddRange(entities);
        }


        private void FillFinishedProducts()
        {
            var data = _context.Set<FinishedProduct>();
            var entities = new List<FinishedProduct>();

            for (var i = 0; i < _thirdCount; i++)
            {
                var entity = new FinishedProduct();

                entity.Id = i + 1;
                entity.ManufactureDate = GetRandomDate(2020, 1, 1);
                entity.ProductId = _random.Next(1, _secondCount - 1);
                entity.Count = _random.Next(1, 100);
                entity.IsApproved = true;

                entities.Add(entity);
            }

            data.AddRange(entities);
            FillProductReserves(entities);
        }

        private void FillProductReserves(List<FinishedProduct> finishedProducts)
        {
            var data = _context.Set<ProductsReserve>();
            var entities = new List<ProductsReserve>();

            for (var i = 0; i < _thirdCount; i++)
            {
                var entity = new ProductsReserve();

                entity.Id = i + 1;
                entity.StoragePlaceId = _random.Next(1, _firstCount - 1);
                entity.FinishedProductId = finishedProducts[i].Id;
                entity.Count = finishedProducts[i].Count;

                entities.Add(entity);
            }

            data.AddRange(entities);
            FillFinishedProductsForOrders(finishedProducts);
        }

        private void FillFinishedProductsForOrders(List<FinishedProduct> finishedProducts)
        {
            var data = _context.Set<FinishedProductSale>();
            var entities = new List<FinishedProductSale>();

            for (var i = 0; i < _thirdCount; i++)
            {
                var entity = new FinishedProductSale();

                entity.Id = i + 1;
                entity.SaleId = _random.Next(1, _secondCount - 1);
                entity.ProductsReserveId = finishedProducts[i].Id;
                entity.Count = finishedProducts[i].Count;

                entities.Add(entity);
            }

            data.AddRange(entities);
        }

        private void FillMaterialsForProducts()
        {
            var data = _context.Set<MaterialsForProducts>();
            var entities = new List<MaterialsForProducts>();

            for (var i = 0; i < _thirdCount; i++)
            {
                var entity = new MaterialsForProducts();

                entity.Id = i + 1;
                entity.MaterialId = (i + 1 + (i % _secondCount)) % (_secondCount) == 0 ? 1 : (i + 1 + (i % _secondCount)) % (_secondCount);
                entity.ProductId = (i + 1) % (_secondCount) == 0 ? 1 : (i + 1) % (_secondCount);

                entities.Add(entity);
            }

            data.AddRange(entities);
        }

        private DateTime GetRandomDate(int startYear, int startMonth, int startDay)
        {
            var start = new DateTime(startYear, startMonth, startDay);
            var range = (DateTime.Today - start).Days;

            return start.AddDays(_random.Next(range));
        }
    }
}
