using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixVectorTranslation : MonoBehaviour
{
    public Transform referencePoint;

    [Space]

    public Vector3 translationOne;
    public Vector3 rotationOne;

    [Space]

    public Vector3 translationTwo;
    public Vector3 rotationTwo;

    private Vector3 scale = Vector3.one;


    
    void Update()
    {
        Matrix4x4 m = referencePoint.transform.localToWorldMatrix * Matrix4x4.TRS(translationOne, SetQuaternion(rotationOne), scale) * Matrix4x4.TRS(translationTwo, SetQuaternion(rotationTwo), scale);

        transform.SetPositionAndRotation(m.GetColumn(3), m.GetRotation());
    }

    private Quaternion SetQuaternion(Vector3 eulerRotation)
    {
        Quaternion rotation = Quaternion.Euler(eulerRotation);

        return rotation;
    }

    
}
