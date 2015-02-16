using UnityEngine;
using System.Collections;

public class User : MonoBehaviour
{

    public static User Instance;
    public UserToken token;
    [SerializeField]
    Texture2D DefautIcon;
    public bool loggedin = false;

    // Use this for initialization
    void Start()
    {
        if (Instance)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        Object.DontDestroyOnLoad(this);

        if (Serialization.Load())
        {
            loggedin = token.loggedin;
        }

    }

    public void setToken(string _name, int _level, string[] _staticsics, Texture2D _icon = null)
    {
        if(_icon==null)
            _icon = DefautIcon;
        token = new UserToken(_name, _level, _staticsics, _icon);
    }

    public void OnApplicationQuit()
    {
        Serialization.Save(token);
    }
}
