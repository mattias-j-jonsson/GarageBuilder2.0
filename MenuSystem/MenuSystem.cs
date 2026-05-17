using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Xml.Serialization;
using GarageBuilder.Vehicles;

namespace GarageBuilder
{
    internal static class MenuSystem
    {
        public static int PrelimenaryMenu()
        {
            Console.WriteLine("Welcome to Garagebuilder 2026!\n");
            
            int size = 0;
            bool parseSuccess = false;
            while (!parseSuccess)
            {
                string result = DrawInputMenu("What size should your garage have?", "Garage size");
                parseSuccess = int.TryParse(result, out size);
                if (!parseSuccess)
                {
                    Console.WriteLine("Invalid input. Please input an integer");
                }
                else if (size <= 0)
                {
                    Console.WriteLine("Invalid input. Size has to be 1 or greater");
                    parseSuccess = false;
                }
            }
            return size;
        }
        public static bool LoadFileMenu()
        {
            string result = DrawChoiceMenu(["Yes", "No"], "Would you like to load data from file?");
            if (result.CompareTo("Yes") == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string MainMenu(string[] menuOptions) // implement with the out int index variable?
        {
            return DrawChoiceMenu(menuOptions, "What do you want do do?");
        }

        public static Vehicle GatherVehicleDetails()
        {
            string[] menuOptions = ["Airplane", "Boat", "Bus", "Car", "Motorcycle"];
            string vehicleTyoe = DrawChoiceMenu(menuOptions, "What kind of vehicle do you want to add?");
            string id = DrawInputMenu("What id does the vehicle have?", "Input (on the form \"ABC123\")");
            string colour = DrawInputMenu("What coulour is the vehicle?");
            bool parseSuccess = int.TryParse(DrawInputMenu("What is its weight?"), out int weight);
            switch (vehicleTyoe)
            {
                case "Airplane":
                    int numberOfEngines = int.Parse(DrawChoiceMenu(["1", "2", "3", "4"], "How many engines does the airplane have?"));
                    parseSuccess = int.TryParse(DrawInputMenu("What is the passenger capacity?"), out int passengerCapacity);
                    return new Airplane(id, colour, weight, numberOfEngines, passengerCapacity);
                case "Boat":
                    menuOptions = ["outboarder", "inboarder", "sailboat"];
                    int indexOfEnum;
                    string boatType = DrawChoiceMenu(menuOptions, out indexOfEnum, "What kind of boat is it?");
                    return new Boat(id, colour, weight, (Boat.Type) indexOfEnum);
                case "Bus":
                    bool electric = DrawChoiceMenu(["Yes", "No"], "Does the bus run on electricity?") == "Yes";
                    parseSuccess = int.TryParse(DrawInputMenu("What is the passenger capacity?"), out passengerCapacity);
                    return new Bus(id, colour, weight, electric, passengerCapacity);
                case "Car":
                    bool fourWheelDrive = DrawChoiceMenu(["Yes", "No"], "Is it a four wheel drive?") == "Yes";
                    electric = DrawChoiceMenu(["Yes", "No"], "Does the car run on electricity?") == "Yes";
                    return new Car(id, colour, weight, fourWheelDrive, electric);
                case "Motorcycle":
                    menuOptions = ["lightweight", "mediumweight", "heavyweight"];
                    string weightclass = DrawChoiceMenu(menuOptions, out indexOfEnum, "What weightclass is it?");
                    parseSuccess = int.TryParse(DrawInputMenu("What is the cylinder volume of the bike?"), out int cylinderVolume);
                    return new Motorcycle(id, colour, weight, (Motorcycle.WeightClass) indexOfEnum, cylinderVolume);
                default:
                    break;
            }
            return null;
        }

        public static int RemoveVehicleMenu(string[] vehicles)
        {
            int indexOfLast = vehicles.Length;
            string[] vehiclesWithOptions = new string[indexOfLast+1];
            Array.Copy(vehicles, vehiclesWithOptions, vehicles.Length);
            vehiclesWithOptions[indexOfLast] = "None";
            string strChoice = DrawChoiceMenu(vehiclesWithOptions, out int choice, "Which vehicle do yout want to remove?");
            if (choice != indexOfLast)
            {
                strChoice = DrawChoiceMenu(["Yes", "No"], $"Do you want do remove this vehicle\n{strChoice}");
                if (strChoice == "Yes")
                {
                    return choice;
                }
            }
            return indexOfLast;
        }
        public static int CreateGarageMenu(int currentInventory)
        {
            string input = DrawInputMenu("What size of garage do you want?");
            bool parseSuccess = int.TryParse(input, out int result);
            if(parseSuccess)
            {
                return result;
            }
            else
            {
                return -1;
            }
        }

        public static void FindVehicleMenu(out string type, out string id, out string colour, out int weight)
        {
            // string choice = DrawChoiceMenu(["Yes", "No"], "Do you want to find a specific kind of vehicle?");
            type = DrawChoiceMenu(["Airplane", "Boat", "Bus", "Car", "Motorcycle", "Any kind"], "What kind of vehicle?");
            if(type == "Any kind")
            {
                type = string.Empty;
            }
            
            
            // choice = DrawChoiceMenu(["Yes", "No"], "Do you want to find a specific vehicle ID?");
            
            id = DrawInputMenu("Please type in the ID you want to find. (press ENTER to skip)", "ID");
            

            // choice = DrawChoiceMenu(["Yes", "No"], "Do you want to find a vehicle with specific vehicle ID?");
            colour = DrawInputMenu("Please type colour of the vehicle you want to find. (press ENTER to skip)", "Colour");
            
            bool parseSuccess = int.TryParse(DrawInputMenu("Please type weight of the vehicle you want to find. (press ENTER to skip)", "Weight"), out weight);

        }

        public static string PrintTypeMenu()
        {
            return DrawChoiceMenu(["Airplane", "Boat", "Bus", "Car", "Motorcycle"], "Which vehicles do you want to print?");
        }

        // For some use cases it is more helpful to use the index of chosen menu option, rather
        // than the string of said option. Therefore there are two versions of DrawChoiceMenu,
        // one which catches that index as an "out" int variable, and one that just returns 
        // the chosen string.
        public static string DrawChoiceMenu(string[] menuOptions, string optionalMessage = "")
        {
            return DrawChoiceMenu(menuOptions, out int throwAwayIndexVariable, optionalMessage);
        }
        public static string DrawChoiceMenu(string[] menuOptions, out int menuIndex, string optionalMessage = "")
        {
            string chosenOption = string.Empty;
            menuIndex = 0;
            // Console.Clear();
            Console.CursorVisible = false;

            while (chosenOption == string.Empty)
            {
                Console.Clear();
                if(optionalMessage != string.Empty)
                {
                    Console.WriteLine(optionalMessage);
                }
                for(int i = 0; i < menuOptions.Length; i++)
                {
                    if(i == menuIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine(menuOptions[i]);
                    Console.ResetColor();
                }

                ConsoleKeyInfo pressedKey = Console.ReadKey();

                switch (pressedKey.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (menuIndex < menuOptions.Length-1)
                        {
                            menuIndex++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (menuIndex > 0)
                        {
                            menuIndex--;
                        }
                        break;
                    case ConsoleKey.Enter:
                        chosenOption = menuOptions[menuIndex];
                        break;
                    default:
                        break;
                }
                // Console.Clear();

            }
            Console.CursorVisible = true;
            return chosenOption;
        }

        public static string DrawInputMenu(string message, string optionalMessage = "Input")
        {
            Console.WriteLine($"{message}\n");
            Console.Write($"{optionalMessage}: ");
            string input = Console.ReadLine();
            return input;
        }
        public static List<Vehicle> LoadVehiclesFromFile(string filePath)
        {
            string[] fileText = File.ReadAllLines(filePath);
            List<Vehicle> vehicleList = new List<Vehicle>(fileText.Length);

            foreach (string item in fileText)
            {
                string[] temp = item.Split(";");
                string id = temp[1].Substring(temp[1].IndexOf(":")+1).ToUpper();
                string colour = temp[2].Substring(temp[2].IndexOf(":")+1).ToUpper();
                bool parseSuccess = int.TryParse(temp[3].Substring(temp[3].IndexOf(":")+1), out int weight);
                switch (temp[0])
                {
                    case "Airplane":
                        parseSuccess = int.TryParse(temp[4].Substring(temp[4].IndexOf(":")+1), out int numberOfEngines);
                        parseSuccess = int.TryParse(temp[5].Substring(temp[5].IndexOf(":")+1), out int passengerCapacity);
                        vehicleList.Add(new Airplane(id, colour, weight, numberOfEngines, passengerCapacity));
                        break;
                    case "Boat":
                        parseSuccess = int.TryParse(temp[4].Substring(temp[4].IndexOf(":")+1), out int type);
                        vehicleList.Add(new Boat(id, colour, weight, (Boat.Type) type));
                        break;
                    case "Bus":
                        parseSuccess = bool.TryParse(temp[4].Substring(temp[4].IndexOf(":")+1), out bool electric);
                        parseSuccess = int.TryParse(temp[5].Substring(temp[5].IndexOf(":")+1), out passengerCapacity);
                        vehicleList.Add(new Bus(id, colour, weight, electric, passengerCapacity));
                        break;
                    case "Car":
                        parseSuccess = bool.TryParse(temp[4].Substring(temp[4].IndexOf(":")+1), out bool fourWheelDrive);
                        parseSuccess = bool.TryParse(temp[5].Substring(temp[5].IndexOf(":")+1), out electric);
                        vehicleList.Add(new Car(id, colour, weight, fourWheelDrive, electric));
                        break;
                    case "Motorcycle":
                        parseSuccess = int.TryParse(temp[4].Substring(temp[4].IndexOf(":")+1), out int weightclass);
                        parseSuccess = int.TryParse(temp[5].Substring(temp[5].IndexOf(":")+1), out int cylinderVolume);
                        vehicleList.Add(new Motorcycle(id, colour, weight, (Motorcycle.WeightClass) weightclass, cylinderVolume));
                        break;
                    default:
                        break;
                }
            }
            return vehicleList;
        }
    }
}