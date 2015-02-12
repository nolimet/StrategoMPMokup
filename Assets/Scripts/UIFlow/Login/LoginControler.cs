using UnityEngine;
using System.Collections;
using util;

public class LoginControler : MonoBehaviour {

    [SerializeField]
    GameObject LoginWindow = null, RegisterWindow = null, StartWindow = null, ServerWait = null;
    [SerializeField]
    UnityEngine.UI.InputField LoginUser = null, LoginPassword = null, RegisterUser = null, RegisterPassword = null, RegisterConfirmPassword = null;
    [SerializeField]
    UnityEngine.UI.Text ServerwaitText = null;
    [SerializeField]
    UnityEngine.UI.Image IRegisterPass = null, IRegisterPassRetype = null, IRegisterUser = null, ILoginUser = null, ILoginPass = null;
    [SerializeField]
    Sprite PassTrue = null, PassFalse = null;
    bool passoke = false;

    public static bool isloggedin;

    void Start()
    {
        isloggedin = saveValue.getBool("isloggedin");
        OpenStart();
        ServerWait.SetActive(false);

        if(isloggedin)
            StartCoroutine(WaitForServer(openScene,1));
    }

    void OnEnable()
    {
        OpenStart();
    }

    void OnDestroy()
    {
        saveValue.setBool(isloggedin, "isloggedin");

        PlayerPrefs.Save();
    }

    public void Login()
    {
        //check with server if username and password are correct if not then give error
        if (LoginUser.text == "bob" && LoginPassword.text == "taart")
                StartCoroutine(WaitForServer(openScene, 1));

        if (LoginUser.text != "bob")
            ILoginUser.sprite = PassFalse;
        else
            ILoginUser.sprite = PassTrue;
        if (LoginPassword.text != "taart")
            ILoginPass.sprite = PassFalse;
        else
            ILoginPass.sprite = PassTrue;
    }

    public void Register()
    {
        LoginWindow.SetActive(false);
        RegisterWindow.SetActive(true);
        StartWindow.SetActive(false);

        
    }

    public void OpenLogin()
    {
        LoginWindow.SetActive(true);
        RegisterWindow.SetActive(false);
        StartWindow.SetActive(false);
    }

    public void OpenStart()
    {
        LoginWindow.SetActive(false);
        RegisterWindow.SetActive(false);
        StartWindow.SetActive(true);
    }

    bool UsernameOke()
    {
        if (RegisterUser.text != "bob")
        {
            IRegisterUser.sprite = PassTrue;
            return true;
        }
        else
        {
            IRegisterUser.sprite = PassFalse;
            return false;
        }
    }

    public void ConfirmRegister()
    {
        
        if( UsernameOke()&& passoke)
            StartCoroutine(WaitForServer(openScene, 1));
    }

    public void CompairPasswords()
    {
        if (RegisterPassword.text == RegisterConfirmPassword.text)
        {
            IRegisterPass.sprite = PassTrue;
            IRegisterPassRetype.sprite = PassTrue;
            passoke = true;
        }
        else
        {
            IRegisterPass.sprite = PassFalse;
            IRegisterPassRetype.sprite = PassFalse;
            passoke = false;
        }
    }

    string openScene(int lvl)
    {
        MenuManager.instance.openMainMenu();
        return "";
    }

    IEnumerator WaitForServer(System.Func<int,string> method, int scene = -1)
    {
        string msg = ServerwaitText.text;
        ServerWait.SetActive(true);
        int length = Mathf.FloorToInt(Random.Range(30, 300)); //simultion purpouses. Response time wait for server
        for (int i = 0; i < length; i++)
        {//wait for server respone
            if (i % 6 == 0)
                ServerwaitText.text = "." + ServerwaitText.text + ".";
            if (i % 30 == 0)
                ServerwaitText.text = msg; 
            yield return new WaitForEndOfFrame();
        }
        ServerwaitText.text = msg;

        //if decode response
        ServerWait.SetActive(false);
        method(scene);
    }
}
