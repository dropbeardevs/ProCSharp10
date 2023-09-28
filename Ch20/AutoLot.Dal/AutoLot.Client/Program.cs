﻿using AutoLot.Dal.Models;
using AutoLot.Dal.DataOperations;
using AutoLot.Dal.BulkImport;

var dal = new InventoryDal();
List<CarViewModel> list = dal.GetAllInventory();

Console.WriteLine(" ************** All Cars ************** ");
Console.WriteLine("Id\tMake\tColor\tPet Name");

foreach (var itm in list)
{
    Console.WriteLine($"{itm.Id}\t{itm.Make}\t{itm.Color}\t{itm.PetName}");
}

Console.WriteLine();

CarViewModel car = dal.GetCar(list.OrderBy(x=>x.Color).Select(x => x.Id).First());

Console.WriteLine(" ************** First Car By Color ************** ");
Console.WriteLine("CarId\tMake\tColor\tPet Name");
Console.WriteLine($"{car.Id}\t{car.Make}\t{car.Color}\t{car.PetName}");

try
{
    //This will fail because of related data in the Orders table
    dal.DeleteCar(5);
    Console.WriteLine("Car deleted.");
}
catch (Exception ex)
{
    Console.WriteLine($"An exception occurred: {ex.Message}");
}

dal.InsertAuto(new Car { Color = "Blue", MakeId = 5, PetName = "TowMonster" });

list = dal.GetAllInventory();

var newCar = list.First(x => x.PetName == "TowMonster");

Console.WriteLine(" ************** New Car ************** ");
Console.WriteLine("CarId\tMake\tColor\tPet Name");
Console.WriteLine($"{newCar.Id}\t{newCar.Make}\t{newCar.Color}\t{newCar.PetName}");

dal.DeleteCar(newCar.Id);

var petName = dal.LookUpPetName(car.Id);

Console.WriteLine(" ************** New Car ************** ");
Console.WriteLine($"Car pet name: {petName}");
Console.Write("Press enter to continue...");
Console.ReadLine();
