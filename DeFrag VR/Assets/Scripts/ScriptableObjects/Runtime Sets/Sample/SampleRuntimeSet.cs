using UnityEngine;

namespace Game.Data
{
    //Create Extensions of RuntimeSet S/O in this format, along with a reference class ('Sample') to handle objects in the Set
    //Reference this extension in any code affecting the Set
    [CreateAssetMenu(fileName = "New Sample Set", menuName = "Runtime Sets/Sample")]
    public class SampleRuntimeSet : RuntimeSet<Sample>
    {
    
    }
}