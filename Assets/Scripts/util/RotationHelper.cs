using UnityEngine;
using System.Collections;

public class RotationHelper : MonoBehaviour {

    void Start()
    {
        if (Application.isPlaying)
        {
            DestroyImmediate(this);
        }
    }
    public void rotate(Vector3 degrees)
    {
        Vector3 newRot = transform.rotation.eulerAngles + degrees;
        
        if (newRot.x >= -360)
            newRot.x -= 360;

        if (newRot.y >= -360)
            newRot.y -= 360;

        if (newRot.z >= -360)
            newRot.z -= 360;

        if (newRot.x <= -360)
            newRot.x += 360;

        if (newRot.y <= -360)
            newRot.y += 360;

        if (newRot.z <= -360)
            newRot.z += 360;
        
        transform.rotation = Quaternion.Euler(newRot);
    }
}
