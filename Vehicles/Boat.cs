using System;
using System.Text;

namespace GarageBuilder.Vehicles
{
    class Boat : Vehicle
    {
        // fields
        // ====================================================================
        private Type boatType;
        // properties
        // ====================================================================
        public Type BoatType
        {
            get {return boatType;}
            set {boatType = value;}
        }

        // text alignment helper properties
        public override int propertyLengthIndividualProp1 => BoatType.ToString().Length + "Boat type".Length;
        // constructors
        // ====================================================================
        public Boat(string id, string colour, string weight, string boatType) : base(id, colour, weight)
        {
            boatType = boatType.ToUpper();
            if(boatType == "OUTBOARDER")
            {
                this.BoatType = Type.outboarder;
            }
            else if(boatType == "INBOARDER")
            {
                this.BoatType = Type.inboarder;
            }
            else if(boatType == "SAILBOAT")
            {
                this.BoatType = Type.sailboat;
            }
            else
            {
                throw new ArgumentException("Boat type not accepted.");
            }
        }
        // methods
        // ====================================================================
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append("Boat type: ").Append(BoatType);
            return sb.ToString();
        }

        public override string stringWithPadding(int typeColumn, int IDColumn, int colourColumn, int weightColumn, int optionalColumn1, int optionalColumn2, int optionalColumn3)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.stringWithPadding(typeColumn, IDColumn, colourColumn, weightColumn));
            string s = "".PadRight(optionalColumn1-this.propertyLengthIndividualProp1 + extraPadding);
            sb.Append("Boat type: ").Append(BoatType).Append(s);
            return sb.ToString();
        }
        // Types
        // ====================================================================
        public enum Type
        {
            outboarder,
            inboarder,
            sailboat
        }
    }
}