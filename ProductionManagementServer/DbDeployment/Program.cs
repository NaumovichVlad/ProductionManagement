// See https://aka.ms/new-console-template for more information
using DataAccess.Config;
using DbDeployment;

var filler = new DbFiller(new ProductionManagementDbContext(), 10, 100, 1000);
filler.FillAllTables();

Console.WriteLine("Db filled");
