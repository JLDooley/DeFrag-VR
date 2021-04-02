using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "New Float", menuName = "Variables/Simple/Float", order = 1)]
    public class FloatVariable : ScriptableObject
    {
    #if UNITY_EDITOR
        [Multiline]
        public string Description = "";
    #endif

        public float Value;

        public void SetValue(float value)
        {
            Value = value;
        }

        public void SetValue(FloatVariable value)
        {
            Value = value.Value;
        }

        public void ChangeValue(float value)
        {
            Value += value;
        }

        public void ChangeValue(FloatVariable value)
        {
            Value += value.Value;
        }
    }
}

