using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

    public void loadLevel(string lvl)
    {
        //Application.LoadLevel(lvl);
        CustomDebug.Log("Loaded: " + lvl);
    }

    public void loadScene(int lvl)
    {
        if (lvl != -1)
        {
            Application.LoadLevel(lvl);
            return;
        }
        CustomDebug.Log("Loaded: " + lvl);
    }
}
