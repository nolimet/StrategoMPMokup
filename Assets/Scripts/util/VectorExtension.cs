using UnityEngine;
using System.Collections;

public class VectorExtension
{

    public static Vector2 angleToVector(float angle)
    {
        angle = ConvertToRadians(angle);
        float X = (float)Mathf.Cos(angle);
        float Y = (float)Mathf.Sin(angle); ;

        return new Vector2(X, Y);
    }

    public static float ConvertToRadians(float angle)
    {
        return (Mathf.PI / 180) * angle;
    }
}
