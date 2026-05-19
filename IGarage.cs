using System.Collections.Generic;
using GarageBuilder.Vehicles;

namespace GarageBuilder
{
    internal interface IGarage<T> : IEnumerable<T> where T : Vehicle
    {
        public void AddVehicle(T vehicle);
        public void RemoveVehicle(int index); // borde vara vehicle:id!?
        public T[] FindVehicle(string type, string id, string coulour, int weight);

    }
}