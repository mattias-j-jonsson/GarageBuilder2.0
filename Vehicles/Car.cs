using System;
using System.Text;

namespace GarageBuilder.Vehicles
{
    class Car : Vehicle
    {
        // fields
        // ====================================================================
        private bool fourWheelDrive;
        private bool electric;
        // properties
        // ====================================================================
        public bool FourWheelDrive
        {
            get {return fourWheelDrive;}
            private set {fourWheelDrive = value;}
        }

        public bool Electric
        {
            get {return electric;}
            set {electric = value;}
        }

        // text alignment helper properties
        public override int propertyLengthIndividualProp1 => FourWheelDrive.ToString().Length + "Four wheel drive".Length;
        public override int propertyLengthIndividualProp2 => Electric.ToString().Length + "Electric".Length;
        // constructors
        // ====================================================================
        public Car(string id, string colour, string weight, string fourWheelDrive, string electric) : base(id, colour, weight)
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
            parseSuccess = bool.TryParse(fourWheelDrive, out bool boolFourWheelDrive);
            if (parseSuccess)
            {
                this.FourWheelDrive = boolFourWheelDrive;
            }
            else
            {
                throw new ArgumentException("Parsing error.", nameof(fourWheelDrive));
            }
        }
        // methods
        // ====================================================================
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append("Four wheel drive: ").Append(FourWheelDrive).Append("    ");
            sb.Append("Electric: ").Append(Electric);
            return sb.ToString();
        }

        public override string stringWithPadding(int typeColumn, int IDColumn, int colourColumn, int weightColumn, int optionalColumn1, int optionalColumn2, int optionalColumn3)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.stringWithPadding(typeColumn, IDColumn, colourColumn, weightColumn));
            string s = "".PadRight(optionalColumn1-this.propertyLengthIndividualProp1 + extraPadding);
            sb.Append("Four wheel drive: ").Append(FourWheelDrive).Append(s);
            s = "".PadRight(optionalColumn2-this.propertyLengthIndividualProp2 + extraPadding);
            sb.Append("Electric: ").Append(Electric).Append(s);
            return sb.ToString();
        }
    }
}