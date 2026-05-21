using System;
using System.Text;

namespace GarageBuilder.Vehicles
{
    class Motorcycle : Vehicle
    {
        // fields
        // ====================================================================
        private WeightClass weightclass;
        private int cylinderVolume;
        // properties
        // ====================================================================
        public WeightClass Weightclass
        {
            get {return weightclass;}
            set {weightclass = value;}
        }
        public int CylinderVolume
        {
            get {return cylinderVolume;}
            set {cylinderVolume = value;}
        }

        // text alignment helper properties
        public override int propertyLengthIndividualProp1 => Weightclass.ToString().Length + "Weight class".Length;
        public override int propertyLengthIndividualProp2 => CylinderVolume.ToString().Length + "Cylinder volume".Length;
        // constructors
        // ====================================================================
        public Motorcycle(string id, string colour, string weight, string weightclass, string cylinderVolume) : base(id, colour, weight)
        {
            weightclass = weightclass.ToUpper();
            if(weightclass == WeightClass.lightweight.ToString().ToUpper())
            {
                this.Weightclass = WeightClass.lightweight;
            }
            else if(weightclass == WeightClass.mediumweight.ToString().ToUpper())
            {
                this.Weightclass = WeightClass.mediumweight;
            }
            else if(weightclass == WeightClass.heavyweight.ToString().ToUpper())
            {
                this.Weightclass = WeightClass.heavyweight;
            }
            else
            {
                throw new ArgumentException("Weight class not accepted.");
            }
            bool parseSuccess = int.TryParse(cylinderVolume, out int intCylinderVolume);
            if(parseSuccess)
            {
                this.CylinderVolume = intCylinderVolume;
            }
            else
            {
                throw new ArgumentException("Parsing error.", nameof(cylinderVolume));
            }
        }
        // methods
        // ====================================================================
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append("Weight class: ").Append(Weightclass).Append("    ");
            sb.Append("Cylinder volume: ").Append(CylinderVolume);
            return sb.ToString();
        }

        public override string stringWithPadding(int typeColumn, int IDColumn, int colourColumn, int weightColumn, int optionalColumn1, int optionalColumn2, int optionalColumn3)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.stringWithPadding(typeColumn, IDColumn, colourColumn, weightColumn));
            string s = "".PadRight(optionalColumn1-this.propertyLengthIndividualProp1 + extraPadding);
            sb.Append("Weight class: ").Append(Weightclass).Append(s);
            s = "".PadRight(optionalColumn2-this.propertyLengthIndividualProp2 + extraPadding);
            sb.Append("Cylinder volume: ").Append(CylinderVolume).Append(s);
            return sb.ToString();
        }
        // types
        // ====================================================================
        public enum WeightClass
        {
            lightweight,
            mediumweight,
            heavyweight
        }
    }
}