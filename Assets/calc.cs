using UnityEngine;
using System.Collections;

public class calc : MonoBehaviour {

    ulong output;

    void Start()
    {
        output = (uint)Mathf.Pow((2147483647),2)  * 256 * 4;

        print(output);
    }
}
