using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {

    [SerializeField]
    string[] scenes = new string[0];

    void Start()
    {
        foreach (string s in scenes)
        {
            Application.LoadLevelAdditive(s);
        }

        Destroy(this);
    }
}
