using System.Collections.Generic;
using GarageBuilder.Vehicles;

namespace GarageBuilder
{
    internal interface IGarage<T> : IEnumerable<T> where T : Vehicle
    {
        public string Name {get;}
        public void AddVehicle(string type, string id, string colour, string weight, string[] additionalAttributes);
        public void RemoveVehicle(int index); // borde vara vehicle:id!?
        public T[] FindVehicle(string type, string id, string coulour, int weight);

    }

}