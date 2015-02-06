using UnityEngine;
using System.Collections;

public class LightDampner : MonoBehaviour
{

    [SerializeField]
    private Light[] ls;

    bool damped;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            StartCoroutine(DampenLights());
    }

    IEnumerator DampenLights()
    {
        int le = 40;
        for (int i = 0; i < le; i++)
        {
            foreach(Light l in ls)
            {
                if (damped)
                    l.intensity += 0.23f / le;
                else
                    l.intensity -= 0.23f / le;
            }
            yield return new WaitForSeconds(2f / le);
        }
        damped = !damped;
    }
}
