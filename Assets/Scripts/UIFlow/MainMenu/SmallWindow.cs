using UnityEngine;
using System.Collections;

public class SmallWindow : MonoBehaviour
{
   public static SmallWindow singleton;

   // RectTransform _rect;
    void Start()
    {
        singleton = this;
    }
    public void close(GameObject value)
    {
        MenuManager.callOnCloseMiniMenu();
        StartCoroutine(closeI(value.GetComponent<RectTransform>()));
    }

    public void open(GameObject value)
    {
        MenuManager.callOnOpenMiniMenu();
        StartCoroutine(openI(value.GetComponent<RectTransform>()));
    }

    IEnumerator openI(RectTransform _rect)
    {
        _rect.localPosition = Vector3.zero;

        int length = 20;
        float a = 1f / length;
        _rect.gameObject.SetActive(true);
        for (int i = 0; i < length; i++)
        {
            _rect.localScale = new Vector3(a * i, a * i, a * i);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator closeI(RectTransform _rect)
    {
            
        int length = 20;
        float a = 1f / length;

        for (int i = length; i > 0; i--)
        {
            _rect.localScale = new Vector3(a * i, a * i, a * i);
            yield return new WaitForEndOfFrame();
        }

        _rect.gameObject.SetActive(false);
    }
}
