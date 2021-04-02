using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "New Bool", menuName = "Variables/Simple/Boolean", order = 3)]
    public class BoolVariable : ScriptableObject
    {
    #if UNITY_EDITOR
        [Multiline]
        public string Description = "";
    #endif

        public bool Value;

        public void SetValue(bool value)
        {
            Value = value;
        }

        public void SetValue(BoolVariable value)
        {
            Value = value.Value;
        }

        public void ToggleValue()
        {
            Value = !Value;
        }
    }
}

