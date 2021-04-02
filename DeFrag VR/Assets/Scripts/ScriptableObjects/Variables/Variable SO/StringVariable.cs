using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "New String", menuName = "Variables/Simple/String", order = 4)]
    public class StringVariable : ScriptableObject
    {
    #if UNITY_EDITOR
        [Multiline]
        public string Description = "";
    #endif
        // 'value' can be set in the inspector, and will update 'Value' for use by functions
        [SerializeField]
        private string value = "";
        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }
    }
}
