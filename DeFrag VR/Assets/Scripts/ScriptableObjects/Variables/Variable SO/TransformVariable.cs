using UnityEngine;

namespace Game.Data
{
    //Stores the position and rotation data of a transform which can be refered to by other functions (e.g. for teleportation)
    [CreateAssetMenu(fileName = "New Transform", menuName = "Variables/Object/Transform", order = 0)]
    public class TransformVariable : ScriptableObject
    {
    
    #if UNITY_EDITOR
        [Multiline]
        public string Description = "";
    #endif

        public Vector3 posValue;
        public Quaternion rotValue;

        public void SetTransformValue(Transform value)
        {
            posValue = value.position;
            rotValue = value.rotation;
        }

        public void SetTransformValue(TransformVariable value)
        {
            posValue = value.posValue;
            rotValue = value.rotValue;
        }

        public void SetTransformValue(Vector3 position, Quaternion rotation)
        {
            posValue = position;
            rotValue = rotation;
        }

        /// <summary>
        /// Updates a provided transform with the value of the Transform Variable
        /// </summary>
        public void GetTransformValue(Transform transformToUpdate)
        {
            transformToUpdate.SetPositionAndRotation(posValue, rotValue);
        }
    }
}
