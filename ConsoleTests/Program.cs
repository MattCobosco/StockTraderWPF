// See https://aka.ms/new-console-template for more information
using StockTrader.Domain.Models;
using StockTrader.Domain.Services;
using StockTrader.EntityFramework;
using StockTrader.EntityFramework.Services;

IDataService<User> userService = new GenericDataService<User>(new StockTraderDbContextFactory());
Console.WriteLine(userService.Delete(3).Result);




