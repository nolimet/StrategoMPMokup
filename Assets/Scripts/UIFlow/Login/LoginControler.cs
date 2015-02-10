using UnityEngine;
using System.Collections;

public class LoginControler : MonoBehaviour {

    [SerializeField]
    GameObject LoginWindow, RegisterWindow, ServerWait;
    [SerializeField]
    UnityEngine.UI.InputField LoginUser, LoginPassword, RegisterUser, RegisterPassword, RegisterConfirmPassword;
    [SerializeField]
    UnityEngine.UI.Text ServerwaitText;
    [SerializeField]
    Sprite PassTrue, PassFalse;
    bool passoke = false, registerOpen = false, isloggedin;

    void Start()
    {
        isloggedin = getBool("isloggedin");
    }

    void OnDestroy()
    {
        setBool(isloggedin, "isloggedin");

        PlayerPrefs.Save();
    }

    public void Login()
    {
        if(LoginUser.text == "bob")
            if(LoginPassword.text == "taart")
        StartCoroutine(WaitForServer(openScene, ServerwaitText, 1));
    }

    public void Register()
    {
        LoginWindow.SetActive(false);
        RegisterWindow.SetActive(true);
    }

    public void CancelRegister()
    {
        LoginWindow.SetActive(true);
        RegisterWindow.SetActive(false);
    }

    public void ConfirmRegister()
    {
        StartCoroutine(WaitForServer(openScene, ServerwaitText, 1));
    }

    public void CompairPasswords()
    {
        if (RegisterPassword.text == RegisterConfirmPassword.text)
            passoke = true;
        else
            passoke = false;
    }

    string openScene(int lvl)
    {
        Application.LoadLevel(lvl);
        return "";
    }

    void setBool(bool value,string name)
    {
        if (value)
            PlayerPrefs.SetInt(name, 1);
        else
            PlayerPrefs.SetInt(name, 0);
    }

    bool getBool(string name)
    {
        int tmp = PlayerPrefs.GetInt(name, 0);

        if (tmp == 0)
            return false;
        else
            return true;
    }

    IEnumerator WaitForServer(System.Func<int,string> method, UnityEngine.UI.Text textField, int scene)
    {
        string msg = textField.text;
        int length = Mathf.FloorToInt(Random.Range(30, 300));
        for (int i = 0; i < length; i++)
        {
            if (i % 3 == 0)
                textField.text = "." + textField.text + ".";
            if (i % 15 == 0)
                textField.text = msg;
            yield return new WaitForEndOfFrame();
        }
        textField.text = msg;

        if (scene != -1)
        method(scene);
    }
}
