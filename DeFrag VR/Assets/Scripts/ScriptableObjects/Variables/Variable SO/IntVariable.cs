using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "New Int", menuName = "Variables/Simple/Int", order = 0)]
    public class IntVariable : ScriptableObject
    {
    #if UNITY_EDITOR
        [Multiline]
        public string Description = "";
    #endif

        public int Value;

        public void SetValue(int value)
        {
            Value = value;
        }

        public void SetValue(IntVariable value)
        {
            Value = value.Value;
        }

        /// <summary>
        /// Increment the IntVariable by a set amount.
        /// </summary>
        /// <param name="value">The value to change the IntVariable by.</param>
        public void ChangeValue(int value)
        {
            Value += value;
        }

        /// <summary>
        /// Increment the IntVariable by a set amount.
        /// </summary>
        /// <param name="value">The value to change the IntVariable by.</param>
        public void ChangeValue(IntVariable value)
        {
            Value += value.Value;
        }
    }
}

