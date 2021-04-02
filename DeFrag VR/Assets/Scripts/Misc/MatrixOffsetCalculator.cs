using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixOffsetCalculator : MonoBehaviour
{
    
    [Tooltip("The Transform this gameObject is to be offset against.")]
    public Transform pivot;

    [Tooltip("The counterpart of this gameObject in a reference system (e.g. Player Camera).")]
    public Transform referenceObject;

    [Tooltip("The Transform the counterpart is offset against.")]
    public Transform referencePivot;

    void Update()
    {
        Matrix4x4 m = pivot.transform.localToWorldMatrix * referencePivot.transform.worldToLocalMatrix * referenceObject.transform.localToWorldMatrix;

        transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);
    }
}
