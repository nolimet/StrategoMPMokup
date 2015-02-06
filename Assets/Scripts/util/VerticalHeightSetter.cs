using UnityEngine;
using System.Collections;

public class VerticalHeightSetter : MonoBehaviour {

    RectTransform thisRect = null;
    [SerializeField]
    float height;

	void Start () {
        thisRect = GetComponent<RectTransform>();
	}

    void calcHeight()
    {
        if(thisRect==null)
            thisRect = GetComponent<RectTransform>();
            int length = GetComponentsInChildren<RectTransform>().Length - 1;
            thisRect.sizeDelta = new Vector2(thisRect.sizeDelta.x, length * height);
    }
}
