using System;

namespace Game.Data
{
    [Serializable]
    public class IntReference
    {
        public bool UseConstant = true;
        public int ConstantValue;
        public IntVariable Variable;

        public IntReference()
        {

        }
    
        public IntReference(int value)
        {
            UseConstant = true;
            ConstantValue = value;
        }

        //  If UseConstant = true, FloatReference.Value = ConstantValue, else FloatReference.Value = Variable.Value (FloatVariable.Value)
        public int Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }

        public static implicit operator int(IntReference reference) //Allows IntReference to be used in place of an Int
        {
            return reference.Value;
        }
    }
}

