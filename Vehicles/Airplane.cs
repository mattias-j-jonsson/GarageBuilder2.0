using System;
using System.Text;

namespace GarageBuilder.Vehicles
{
    class Airplane : Vehicle
    {
        // fields
        // ====================================================================
        private int numberOfEngines;
        private int passengerCapacity;
        // properties
        // ====================================================================
        public int NumberOfEngines
        {
            get {return numberOfEngines;}
            set {numberOfEngines = value;}
        }

        public int PassengerCapacity
        {
            get {return passengerCapacity;}
            set {passengerCapacity = value;}
        }

        // text alignment helper properties
        public override int propertyLengthIndividualProp1 => NumberOfEngines.ToString().Length + "Number of engines".Length;
        public override int propertyLengthIndividualProp2 => PassengerCapacity.ToString().Length + "Passenger capacity".Length;

        // constructors
        // ====================================================================
        public Airplane(string id, string colour, string weight, string engines, string passengerCapacity) : base(id, colour, weight)
        {
            bool parseSuccess = int.TryParse(engines, out int intEngine);
            if (parseSuccess)
            {
                this.NumberOfEngines = intEngine;
            }
            else
            {
                throw new ArgumentException("Parsing error.", nameof(engines));
            }
            parseSuccess = int.TryParse(passengerCapacity, out int intPassengerCapacity);
            if (parseSuccess)
            {
                this.PassengerCapacity = intPassengerCapacity;
            }
            else
            {
                throw new ArgumentException("Parsing error.", nameof(passengerCapacity));
            }
        }
        // methods
        // ====================================================================
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append("Number of engines: ").Append(NumberOfEngines).Append("    ");
            sb.Append("Passenger capacity: ").Append(PassengerCapacity);
            return sb.ToString();
        }

        public override string stringWithPadding(int typeColumn, int IDColumn, int colourColumn, int weightColumn, int optionalColumn1, int optionalColumn2, int optionalColumn3)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.stringWithPadding(typeColumn, IDColumn, colourColumn, weightColumn));
            string s = "".PadRight(optionalColumn1-this.propertyLengthIndividualProp1 + extraPadding);
            sb.Append("Number of engines: ").Append(NumberOfEngines).Append(s);
            s = "".PadRight(optionalColumn2-this.propertyLengthIndividualProp2 + extraPadding);
            sb.Append("Passenger capacity: ").Append(PassengerCapacity).Append(s);
            return sb.ToString();
        }
    }
}