using System;
using System.Collections.Generic;
using System.IO;
using GarageBuilder.Vehicles;

namespace GarageBuilder
{
    class Program
    {
        static void Main()
        {
            int size = MenuSystem.PrelimenaryMenu();
            var garage = new Garage<Vehicle>("mattias ställe", size);
            bool loadedQuestion = MenuSystem.LoadFileMenu();
            if (loadedQuestion)
            {
                string pathToFile = Environment.CurrentDirectory;
                pathToFile = pathToFile.Substring(pathToFile.IndexOf("GarageBuilder2.0"));
                pathToFile = Path.GetRelativePath(pathToFile, "GarageBuilder2.0/Data/vehicles.txt");
                List<Vehicle> fileInput = MenuSystem.LoadVehiclesFromFile(pathToFile);
                foreach (var item in fileInput)
                {
                    garage.AddVehicle(item);
                }
            }
            
            bool running = true;
            while(running)
            {
                string[] menuOptions = ["Add vehicle", "Remove vehicle", "Find vehicle", "Print all vehicles", "Print type", "Quit"];
                string menuChoice = MenuSystem.MainMenu(menuOptions);
                switch (menuChoice)
                {
                    case "Add vehicle":
                        if(garage.IsFull)
                        {
                            Console.WriteLine("The garage has reached its limit. Please remove vehicles to free up space");
                            Console.WriteLine("Press ENTER to return to menu.");
                            Console.Read();
                        }
                        else
                        {
                            try
                            {
                                Vehicle newVehicle = MenuSystem.GatherVehicleDetails();
                                garage.AddVehicle(newVehicle);
                            }
                            catch (System.Exception e)
                            {
                                Console.WriteLine($"Error: {e.Message}\nVehicle insert aborted. Press ENTER to return to menu");
                                Console.Read();
                            }
                        }
                        break;
                    case "Remove vehicle":
                        if (garage.VehicleCount == 0)
                        {
                            Console.WriteLine("The garage is Empty.");
                            Console.WriteLine("Press ENTER to return to menu.");
                            Console.Read();
                        }
                        else
                        {
                            string[] vehiclesAsMenuOptions = garage.ToString().Split("\n");
                            int indexOfVehicle = MenuSystem.RemoveVehicleMenu(vehiclesAsMenuOptions);
                            if (indexOfVehicle >= garage.VehicleCount)
                            {
                                Console.WriteLine("Vehicle removal aborted.");
                                Console.WriteLine("Press ENTER to return to menu.");
                                Console.Read();
                            }
                            else
                            {
                                garage.RemoveVehicle(indexOfVehicle);
                                Console.Read();
                            }
                            // Console.Read();
                        }
                        break;
                    case "Find vehicle":
                        MenuSystem.FindVehicleMenu(out string type, out string id, out string colour, out int weight);
                        Vehicle[] foundVehicles = garage.FindVehicle(type, id, colour, weight);
                        if (foundVehicles.Length == 0)
                        {
                            Console.WriteLine("No vehicles matched these criterias.");
                        }
                        else
                        {
                            Console.WriteLine($"{foundVehicles.Length} vehicles was found:");
                            foreach (var v in foundVehicles)
                            {
                                Console.WriteLine(v);
                            }                            
                        }
                        Console.Read();
                        break;
                    case "Print all vehicles":
                        Console.WriteLine(garage.ToString());
                        Console.Read();
                        break;
                    case "Print type":
                        garage.PrintType(MenuSystem.PrintTypeMenu());
                        Console.Read();
                        break;
                    case "Quit":
                        running = false;
                        break;
                    default:
                        break;
                }
            }
        }


        // DEBUG VERSION OF MAIN
        // static void Main()
        // {
        //     Garage garage = new Garage(12);
        //     string pathToFile = Environment.CurrentDirectory;
        //     pathToFile = pathToFile.Substring(pathToFile.IndexOf("GarageBuilder"));
        //     pathToFile = Path.GetRelativePath(pathToFile, "GarageBuilder/Data/vehicles.txt");
        //     List<Vehicle> fileInput = MenuSystem.LoadVehiclesFromFile(pathToFile);
        //     foreach (var item in fileInput)
        //     {
        //         garage.AddVehicle(item);
        //     }

        //     Console.WriteLine(garage.ToString());
        //     // Console.Read();
        //     Vehicle[] foundVehicles = garage.FindVehicle("", "", "", 0);
        //     foreach (var v in foundVehicles)
        //     {
        //         Console.WriteLine(v);
        //     }
        //     // Console.Read();
        // }
    }
}