using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using GarageBuilder.UI;
using GarageBuilder.Vehicles;

namespace GarageBuilder
{
    internal class Manager
    {
        private List<IGarage> allGarages = new List<Garage<T>>();
        
        public void runApp(IUserInterface userIf)
        {
            userIf = new UserInterface();
            
        }
    }
}