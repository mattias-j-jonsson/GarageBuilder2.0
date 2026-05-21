using System;
using System.Collections.Generic;
using System.IO;
using GarageBuilder.UI;
using GarageBuilder.Vehicles;

namespace GarageBuilder
{
    internal static class Manager
    {
        private static List<IGarage<Vehicle>> garageList = new List<IGarage<Vehicle>>();
        
        public static void runApp(IUserInterface userIf)
        {
            
            garageList.Add(new Garage<Vehicle>("Default Garage", 15));
            IGarage<Vehicle> currentGarage;
            currentGarage = garageList[0];
            foreach (var item in LoadVehiclesFromFile())
            {
                (string type, string id, string colour, string weight, string[] additionalAttributes) = item;
                currentGarage.AddVehicle(type, id, colour, weight, additionalAttributes);
            }
        }
        public static IEnumerable<(string type, string id, string colour, string weight, string[] additionalAttributes)> LoadVehiclesFromFile()
        {
            string pathToFile = Environment.CurrentDirectory;
            pathToFile = pathToFile.Substring(pathToFile.IndexOf("GarageBuilder2.0"));
            pathToFile = Path.GetRelativePath(pathToFile, "GarageBuilder2.0/Data/vehicles.txt");
            string[] fileText = File.ReadAllLines(pathToFile);
            // List<Vehicle> vehicleList = new List<Vehicle>(fileText.Length);

            foreach (string item in fileText)
            {
                string[] temp = item.Split(";");
                string type = temp[0];
                string id = temp[1].Substring(temp[1].IndexOf(":")+1).ToUpper();
                string colour = temp[2].Substring(temp[2].IndexOf(":")+1);
                string weight = temp[3].Substring(temp[3].IndexOf(":")+1);
                string[] typeSpecific = new string[temp.Length-4]; // will always have room for all subtype specific attributes
                for (int i = 0; i < typeSpecific.Length; i++)
                {
                    typeSpecific[i] = temp[i+4];
                }
                yield return (type, id, colour, weight, typeSpecific);
                // switch (temp[0])
                // {
                //     case "Airplane":
                //         parseSuccess = int.TryParse(temp[4].Substring(temp[4].IndexOf(":")+1), out int numberOfEngines);
                //         parseSuccess = int.TryParse(temp[5].Substring(temp[5].IndexOf(":")+1), out int passengerCapacity);
                //         yield return new Airplane(id, colour, weight, numberOfEngines, passengerCapacity);
                //         break;
                //     case "Boat":
                //         parseSuccess = int.TryParse(temp[4].Substring(temp[4].IndexOf(":")+1), out int type);
                //         yield return new Boat(id, colour, weight, (Boat.Type) type);
                //         break;
                //     case "Bus":
                //         parseSuccess = bool.TryParse(temp[4].Substring(temp[4].IndexOf(":")+1), out bool electric);
                //         parseSuccess = int.TryParse(temp[5].Substring(temp[5].IndexOf(":")+1), out passengerCapacity);
                //         yield return new Bus(id, colour, weight, electric, passengerCapacity);
                //         break;
                //     case "Car":
                //         parseSuccess = bool.TryParse(temp[4].Substring(temp[4].IndexOf(":")+1), out bool fourWheelDrive);
                //         parseSuccess = bool.TryParse(temp[5].Substring(temp[5].IndexOf(":")+1), out electric);
                //         yield return new Car(id, colour, weight, fourWheelDrive, electric);
                //         break;
                //     case "Motorcycle":
                //         parseSuccess = int.TryParse(temp[4].Substring(temp[4].IndexOf(":")+1), out int weightclass);
                //         parseSuccess = int.TryParse(temp[5].Substring(temp[5].IndexOf(":")+1), out int cylinderVolume);
                //         yield return new Motorcycle(id, colour, weight, (Motorcycle.WeightClass) weightclass, cylinderVolume);
                //         break;
                //     default:
                //         break;
                // }
            }
            // yield break;
        }
    }

}