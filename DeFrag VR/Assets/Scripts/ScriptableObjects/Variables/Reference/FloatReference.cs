using System;

namespace Game.Data
{
    [Serializable]
    public class FloatReference
    {
        public bool UseConstant = true;
        public float ConstantValue;
        public FloatVariable Variable;


        public FloatReference() //UseConstant becomes false by default unless this is here (WHY?)
        {
        }

        public FloatReference(float value)
        {
            UseConstant = true;
            ConstantValue = value;
        }

        //  If UseConstant = true, FloatReference.Value = ConstantValue, else FloatReference.Value = Variable.Value (FloatVariable.Value)
        public float Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }

        //https://www.codeproject.com/Articles/15191/Understanding-Implicit-Operator-Overloading-in-C

        //  Implicit Operator accepts 'FloatReference' objects and converts the 'Value' property into a float

        public static implicit operator float(FloatReference reference)
        {
            return reference.Value;
        }
    }
}