using System.Collections.Generic;
using GarageBuilder.Vehicles;

namespace GarageBuilder
{
    internal interface IGarage<T> : IGarage, IEnumerable<T> where T : Vehicle
    {
        public string Name {get;}
        public void AddVehicle(T vehicle);
        public void RemoveVehicle(int index); // borde vara vehicle:id!?
        public T[] FindVehicle(string type, string id, string coulour, int weight);

    }

    internal interface IGarage
    {
        
    }
}