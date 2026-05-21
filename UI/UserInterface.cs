using System;

namespace GarageBuilder.UI
{
    internal class UserInterface : IUserInterface
    {
        public (string type, string id, string colour, string weight, string[] additionalAttributes) AddVehicleMenu()
        {
            string[] menuOptions = ["Airplane", "Boat", "Bus", "Car", "Motorcycle"];
            string vehicleType = MultipleChoiceMenu(menuOptions, "What kind of vehicle do you want to add?");
            string id = InputMenu("What id does the vehicle have?", "Input (on the form \"ABC123\")");
            string colour = InputMenu("What coulour is the vehicle?");
            string weight = InputMenu("What is its weight?");
            switch (vehicleType)
            {
                case "Airplane":
                    string numberOfEngines = MultipleChoiceMenu(["1", "2", "3", "4"], "How many engines does the airplane have?");
                    string passengerCapacity= InputMenu("What is the passenger capacity?");
                    return (vehicleType, id, colour, weight, [numberOfEngines, passengerCapacity]);
                case "Boat":
                    menuOptions = ["outboarder", "inboarder", "sailboat"];
                    string boatType = MultipleChoiceMenu(menuOptions, "What kind of boat is it?");
                    return (vehicleType, id, colour, weight, [boatType]);
                case "Bus":
                    bool electric = MultipleChoiceMenu(["Yes", "No"], "Does the bus run on electricity?") == "Yes";
                    passengerCapacity = InputMenu("What is the passenger capacity?");
                    return (vehicleType, id, colour, weight, [electric.ToString(), passengerCapacity]);
                case "Car":
                    bool fourWheelDrive = MultipleChoiceMenu(["Yes", "No"], "Is it a four wheel drive?") == "Yes";
                    electric = MultipleChoiceMenu(["Yes", "No"], "Does the car run on electricity?") == "Yes";
                    return (vehicleType, id, colour, weight, [fourWheelDrive.ToString(), electric.ToString()]);
                case "Motorcycle":
                    menuOptions = ["lightweight", "mediumweight", "heavyweight"];
                    string weightclass = MultipleChoiceMenu(menuOptions, "What weightclass is it?");
                    string cylinderVolume = InputMenu("What is the cylinder volume of the bike?");
                    return (vehicleType, id, colour, weight, [weightclass, cylinderVolume]);
                default:
                    break;
            }
            return ("", "", "", "", []);
        }
        public string RemoveVehicleMenu(string[] vehicles)
        {
            int indexOfLast = vehicles.Length;
            string[] vehiclesWithOptions = new string[indexOfLast+1];
            Array.Copy(vehicles, vehiclesWithOptions, vehicles.Length);
            vehiclesWithOptions[indexOfLast] = "None";
            string vehicleChoice = MultipleChoiceMenu(vehiclesWithOptions, out int choice, "Which vehicle do yout want to remove?");
            if (choice != indexOfLast)
            {
                string yesNoChoice = MultipleChoiceMenu(["Yes", "No"], $"Do you want do remove this vehicle\n{vehicleChoice}");
                if (yesNoChoice == "Yes")
                {
                    return vehicleChoice;
                }
            }
            return "";
        }
        public string MultipleChoiceMenu(string[] menuOptions, out int chosenIndex, string optionalMessage = "")
        {
            string chosenOption = string.Empty;
            chosenIndex = 0;
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
                    if(i == chosenIndex)
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
                        if (chosenIndex < menuOptions.Length-1)
                        {
                            chosenIndex++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (chosenIndex > 0)
                        {
                            chosenIndex--;
                        }
                        break;
                    case ConsoleKey.Enter:
                        chosenOption = menuOptions[chosenIndex];
                        break;
                    default:
                        break;
                }
                // Console.Clear();

            }
            Console.CursorVisible = true;
            return chosenOption;
        }
        public string MultipleChoiceMenu(string[] menuOptions, string optionalMessage) => this.MultipleChoiceMenu(menuOptions, out int throwAwayIndexVariable, optionalMessage);
        public string InputMenu(string primaryMessage, string optionalMessage = "Input")
        {
            Console.WriteLine($"{primaryMessage}\n");
            Console.Write($"{optionalMessage}: ");
            string? input = Console.ReadLine();
            if(input == null)
            {
                return string.Empty;
            }
            else
            {
                return input;
            }
        }
    }
}