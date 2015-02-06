using UnityEngine;
using System.Collections;

public class EnableMaskOnPlay : MonoBehaviour
{

    [SerializeField]
    UnityEngine.UI.Mask Mask;
    [SerializeField]
    UnityEngine.UI.Image ImageMask;
	void Start () {
        Mask.enabled = true;
        ImageMask.enabled = true;
        Destroy(this);
	}
}
