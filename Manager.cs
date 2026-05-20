using System.Collections.Generic;
using System.IO;
using GarageBuilder.UI;
using GarageBuilder.Vehicles;

namespace GarageBuilder
{
    internal static class Manager
    {
        private static List<IGarage> garageList = new List<IGarage>();
        
        public static void runApp(IUserInterface userIf)
        {
            garageList.Add(new Garage<Vehicle>("Default Garage", 15));
            var currentGarage = garageList[0];
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