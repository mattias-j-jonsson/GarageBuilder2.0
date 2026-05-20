using System.Collections.Generic;
using GarageBuilder.UI;

namespace GarageBuilder
{
    internal class Manager
    {
        private List<IGarage<T>> allGarages;
        
        public void runApp(IUserInterface userIf)
        {
            userIf = new UserInterface();
            
        }
    }
}