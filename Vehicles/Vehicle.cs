
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GarageBuilder.Vehicles
{
    internal abstract class Vehicle
    {
        // fields
        // ====================================================================
        private string id;
        private string colour;
        private int weight;
        private const int maxWeight = 800000;
        protected const int extraPadding = 5;
        // int size; // optional functionality
        

        // properties
        // ====================================================================
        public string Id
        {
            get {return id;}
            private set
            {
                if(IsValidVehicleId(value))
                {
                    id = value;
                }
                else
                {
                    throw new ArgumentException("ID must be on form \"ABC123\"", nameof(id));
                }
                
            }
        }

        public string Colour
        {
            get {return colour;}
            private set
            {
                colour = value;
            }
        }
        public int Weight
        {
            get {return weight;}
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Weight must be a positive integer", nameof(weight));
                }
                else if (value > maxWeight)
                {
                    throw new ArgumentOutOfRangeException($"Weight cannot be greater than {maxWeight} kg.", nameof(weight));
                }
                weight = value;
            }
        }

        // some simple properties used for formatting output consisting of Vehicles properties
        public int propertyLengthType {get {return this.GetType().Name.Length;}}
        public int propertyLengthID {get {return this.Id.Length;}}
        public int propertyLengthColour {get {return this.Colour.Length;}}
        public int propertyLengthWeight {get {return this.Weight.ToString().Length;}}
        public virtual int propertyLengthIndividualProp1 {get {return 0;}}
        public virtual int propertyLengthIndividualProp2 {get {return 0;}}
        public virtual int propertyLengthIndividualProp3 {get {return 0;}}

        // constructors
        // ====================================================================
        public Vehicle(string id, string colour, string weight)
        {
            this.Id = id.ToUpper();
            this.Colour = colour.ToUpper();
            
            bool parseSuccess = int.TryParse(weight, out int intWeight);
            if(parseSuccess)
            {
                this.Weight = intWeight;
            }
            else
            {
                throw new ArgumentException("Parsing error.", nameof(weight));
            }
        }
        // methods
        // ====================================================================
        private bool IsValidVehicleId(string id)
        {
            bool correctLength = id.Length == 6;
            if (correctLength == false)
            {
                return false;
            }
            bool parseFirstHalfId = char.IsLetter(id[0]) && char.IsLetter(id[1]) && char.IsLetter(id[2]);
            bool parseSecondHalfId = int.TryParse(id.Substring(3, 3), out int throwAwayNumber);
            return parseFirstHalfId && parseSecondHalfId;
        }
        public override string ToString()
        {
            string typeAsString = this.GetType().ToString().Split(".")[2];
            StringBuilder sb = new StringBuilder();
            sb.Append("Type: ").Append(typeAsString).Append("    ");
            sb.Append("ID: ").Append(Id).Append("    ");
            sb.Append("Colour: ").Append(Colour).Append("    ");
            sb.Append("Weight: ").Append(Weight).Append("    ");
            return sb.ToString();
        }

        public virtual string stringWithPadding(int typeColumn, int IDColumn, int colourColumn, int weightColumn, int optionalColumn1=0, int optionalColumn2=0, int optionalColumn3=0)
        {
            StringBuilder sb = new StringBuilder();
            string typeAsString = this.GetType().Name;
            string s = "".PadRight(typeColumn-typeAsString.Length + extraPadding);
            sb.Append("Type: ").Append(typeAsString).Append(s);
            s = "".PadRight(IDColumn-Id.Length + extraPadding);
            sb.Append("ID: ").Append(Id).Append(s);
            s = "".PadRight(colourColumn-Colour.Length + extraPadding);
            sb.Append("Colour: ").Append(Colour).Append(s);
            s = "".PadRight(weightColumn-Weight.ToString().Length + extraPadding);
            sb.Append("Weight: ").Append(Weight).Append(s);
            return sb.ToString();
        }
    }
}