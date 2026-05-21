using System;
using System.Text;

namespace GarageBuilder.Vehicles
{
    class Bus : Vehicle
    {
        // fields
        // ====================================================================
        private bool electric;
        private int passengerCapacity;
        // properties
        // ====================================================================
        public bool Electric
        {
            get {return electric;}
            set {electric = value;}
        }
        public int PassengerCapacity
        {
            get {return passengerCapacity;}
            set {passengerCapacity = value;}
        }

        // text alignment helper properties
        public override int propertyLengthIndividualProp1 => Electric.ToString().Length + "Electric".Length;
        public override int propertyLengthIndividualProp2 => PassengerCapacity.ToString().Length + "Passenger capacity".Length;
        // constructors
        // ====================================================================
        public Bus(string id, string colour, string weight, string electric, string passengerCapacity) : base(id, colour, weight)
        {
            bool parseSuccess = bool.TryParse(electric, out bool boolElectric);
            if (parseSuccess)
            {
                this.Electric = boolElectric;
            }
            else
            {
                throw new ArgumentException("Parsing error.", nameof(electric));
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
            sb.Append("Electric: ").Append(Electric).Append("    ");
            sb.Append("Passenger capacity: ").Append(PassengerCapacity);
            return sb.ToString();
        }

        public override string stringWithPadding(int typeColumn, int IDColumn, int colourColumn, int weightColumn, int optionalColumn1, int optionalColumn2, int optionalColumn3)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.stringWithPadding(typeColumn, IDColumn, colourColumn, weightColumn));
            string s = "".PadRight(optionalColumn1-this.propertyLengthIndividualProp1 + extraPadding);
            sb.Append("Electric: ").Append(Electric).Append(s);
            s = "".PadRight(optionalColumn2-propertyLengthIndividualProp2 + extraPadding);
            sb.Append("Passenger capacity: ").Append(PassengerCapacity).Append(s);
            return sb.ToString();
        }
    }
}