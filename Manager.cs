using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using GarageBuilder.UI;
using GarageBuilder.Vehicles;

namespace GarageBuilder
{
    internal static class Manager
    {
        private static List<IGarage> garageList = new List<IGarage>();
        
        public static void runApp(IUserInterface userIf)
        {
            userIf = new UserInterface();
        }
    }
}