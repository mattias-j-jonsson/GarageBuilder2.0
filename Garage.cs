using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using GarageBuilder.Vehicles;

namespace GarageBuilder
{
    internal class Garage<T> : IGarage<T> where T : Vehicle
    {
        // fields
        // ====================================================================
        private T[] storageSpace = Array.Empty<T>();
        // private bool isFull;
        private int capacity;

        private int airplaneCount = 0;
        private int boatCount = 0;
        private int busCount = 0;
        private int carCount = 0;
        private int motorcycleCount = 0;

        // properties
        // ====================================================================
        public string Name 
        {
            get;
            private set => field = value.Length <= 50 && value.Length > 0 ? value : throw new ArgumentException("Name of garage cannot be longer than 50 characters");
        }
        private T[] StorageSpace
        {
            get {return storageSpace;}
            set
            {
                storageSpace = value;
            }
        }
        public int Capacity
        {
            get {return capacity;}
            private set {capacity = value;}
        }

        public int VehicleCount
        {
            get {return airplaneCount + boatCount + busCount + carCount + motorcycleCount;}
        }

        public bool IsFull
        {
            get {return Capacity-VehicleCount <= 0;}
        }
        // constructors
        // ====================================================================
        public Garage(string name, int capacity)
        {
            StorageSpace = new T[capacity];
            this.Capacity = capacity;
            this.Name = name;
        }
        // methods
        // ====================================================================
        // public void AddVehicle(T v)
        // {
        //     // Console.WriteLine($"capacity: {capacity} VehicleCount: {VehicleCount}");
        //     if (VehicleCount >= Capacity)
        //     {
        //         Console.WriteLine("The garage has reached its limit. Please remove vehicles to free up space");
        //         Console.Read();
        //     } else if (IdExists(v.Id) == true)
        //     {
        //         Console.WriteLine($"ID {v.Id} already exists. Duplicate ID's are NOT allowed.");
        //         Console.Read();
        //     }
        //     else
        //     {
        //         int index = 0;
        //         while (StorageSpace[index] != null)
        //         {
        //             index++;
        //         }
        //         storageSpace[index] = v; // should copy rather than put input reference?
        //         modifyVehicleCounter(v, 1);
        //     }
        // }
        public void AddVehicle(string type, string id, string colour, string weight, string[] additionalAttributes)
        {
            // Console.WriteLine($"capacity: {capacity} VehicleCount: {VehicleCount}");
            if (VehicleCount >= Capacity)
            {
                Console.WriteLine("The garage has reached its limit. Please remove vehicles to free up space");
                Console.Read();
            } else if (IdExists(id) == true)
            {
                Console.WriteLine($"ID {id} already exists. Duplicate ID's are NOT allowed.");
                Console.Read();
            }
            else
            {
                int index = 0;
                while (StorageSpace[index] != null)
                {
                    index++;
                }
                Vehicle v;
                switch (type.ToUpper())
                {
                    case "AIRPLANE":
                        v = new Airplane(id, colour, weight, additionalAttributes[0], additionalAttributes[1]);
                        break;
                    case "BOAT":
                        v = new Boat(id, colour, weight, additionalAttributes[0]);
                        break;
                    case "BUS":
                        v = new Bus(id, colour, weight, additionalAttributes[0], additionalAttributes[1]);
                        break;
                    case "CAR":
                        v = new Car(id, colour, weight, additionalAttributes[0], additionalAttributes[1]);
                        break;
                    case "MOTORCYCLE":
                        v = new Motorcycle(id, colour, weight, additionalAttributes[0], additionalAttributes[1]);
                        break;
                    default:
                        throw new Exception("Vehicle was not valid. Operation aborted");
                }
                storageSpace[index] = (T) v; // should copy rather than put input reference?
                modifyVehicleCounter((T)v, 1);
            }
        }

        private bool IdExists(string id)
        {
            foreach (T current in storageSpace)
            {
                try
                {
                    if(id == current.Id)
                    {
                        return true;
                    }
                }
                catch {}
            }
            return false;
        }

        public void RemoveVehicle(int index)
        {
            if (index >= VehicleCount || index < 0)
            {
                throw new ArgumentException("Error: Invalid index. Vehicle removal aborted.");
            }
            else
            {
                int occupiedSlots = 0;
                int i = 0;
                while (occupiedSlots < index || StorageSpace[i] == null)
                {
                    if(StorageSpace[i] != null)
                    {
                        occupiedSlots++;
                    }
                    i++;
                }
                modifyVehicleCounter(StorageSpace[i], -1);
                StorageSpace[i] = null;

            }
            return;
        }

        private void modifyVehicleCounter(T v, int modifier)
        {
            if(modifier != -1 && modifier != 1)
            {
                throw new ArgumentException("Error: Illegal modifier to garage inventory");
            }

            if (v is Airplane)
            {
                airplaneCount += modifier;
            }
            else if (v is Boat)
            {
                boatCount += modifier;
            }
            else if (v is Bus)
            {
                busCount += modifier;
            }
            else if (v is Car)
            {
                carCount += modifier;
            }
            else if (v is Motorcycle)
            {
                motorcycleCount += modifier;
            }
        }


        public T[] FindVehicle(string type = "", string id = "", string colour = "", int weight = 0)
        {
            if(type == "" && id == "" && colour == "" && weight == 0)
            {
                return [];
            }
            T[] foundVehicles = new T[VehicleCount];
            int counter = 0;
            foreach (T current in StorageSpace)
            {
                if(current == null)
                {
                    continue;
                }
                else if(type != string.Empty && current.GetType().Name.ToUpper() != type.ToUpper())
                {
                    continue;
                }
                else if (id != string.Empty && current.Id.ToUpper() != id.ToUpper())
                {
                    continue;
                }
                else if (colour != string.Empty && current.Colour.ToUpper() != colour.ToUpper())
                {
                    continue;
                }
                else if (weight != 0 && current.Weight != weight)
                {
                    continue;
                }
                else
                {
                    foundVehicles[counter] = current;
                    counter++;
                }
            }

            
            T[] returnArray = new T[counter];
            Array.Copy(foundVehicles, returnArray, counter);
            return returnArray;            
        }

        public void PrintType(string type)
        {
            int printedVehicles = 0;
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < VehicleCount; i++)
            {
                if (StorageSpace[i] != null && StorageSpace[i].GetType().Name.ToUpper() == type.ToUpper())
                {
                    sb.Append(StorageSpace[i]);
                    printedVehicles++;
                }
            }
            Console.WriteLine($"{printedVehicles} vehicles was found.");
            Console.WriteLine(sb.ToString());
        }

        public override string ToString()
        {
            if(VehicleCount > 0)
            {
                int[] columnLength = new int[7];

                for (int i = 0; i < StorageSpace.Length; i++)
                {
                    if (StorageSpace[i] != null)
                    {
                        columnLength[0] = int.Max(columnLength[0], StorageSpace[i].propertyLengthType);
                        columnLength[1] = int.Max(columnLength[1], StorageSpace[i].propertyLengthID);
                        columnLength[2] = int.Max(columnLength[2], StorageSpace[i].propertyLengthColour);
                        columnLength[3] = int.Max(columnLength[3], StorageSpace[i].propertyLengthWeight);
                        columnLength[4] = int.Max(columnLength[4], StorageSpace[i].propertyLengthIndividualProp1);
                        columnLength[5] = int.Max(columnLength[5], StorageSpace[i].propertyLengthIndividualProp2);
                        columnLength[6] = int.Max(columnLength[6], StorageSpace[i].propertyLengthIndividualProp3);
                    }
                }

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < StorageSpace.Length; i++)
                {
                    if (StorageSpace[i] != null)
                    {
                        sb.Append(StorageSpace[i].stringWithPadding(columnLength[0], columnLength[1], columnLength[2], columnLength[3], columnLength[4], columnLength[5], columnLength[6]));
                        sb.Append('\n');
                    }
                }
                sb.Remove(sb.Length-1, 1);
                return sb.ToString();
            }
            else
            {
                return "Garage is empty";
            }
        }

        public string GetStatusString()
        {
            return $"{Name} {VehicleCount}/{Capacity}";
        }

        public IEnumerator<T> GetEnumerator()
        {
            return StorageSpace.OfType<T>().GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }


}