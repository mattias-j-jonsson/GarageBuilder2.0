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
        private static IGarage<Vehicle> currentGarage;
        public static void InitializeApp()
        {
            
            garageList.Add(new Garage<Vehicle>("Default Garage", 15));
            currentGarage = garageList[0];
            foreach (var item in LoadVehiclesFromFile())
            {
                (string type, string id, string colour, string weight, string[] additionalAttributes) = item;
                currentGarage.AddVehicle(type, id, colour, weight, additionalAttributes);
            }
        }

        public static void RunApp(IUserInterface userIf)
        {
            Console.WriteLine(currentGarage.GetStatusString());
            Console.WriteLine(currentGarage.ToString());
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
                    typeSpecific[i] = temp[i+4].Substring(temp[i+4].IndexOf(":")+1);
                }
                yield return (type, id, colour, weight, typeSpecific);
            }
        }
    }

}