using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour {

    public delegate void openedMiniMenu();
    public static event openedMiniMenu OnOpenMiniMenu;
    public static void callOnOpenMiniMenu()
    {
        OnOpenMiniMenu();
    }
    public delegate void closedMiniMenu();
    public static event closedMiniMenu OnClosedMiniMenu;
    public static void callOnCloseMiniMenu()
    {
        OnClosedMiniMenu();
    }

    [SerializeField]
    RectTransform[] menus = null;
    [SerializeField]
    GameObject Main = null, Login = null, Menu = null;
    [SerializeField]
    int current = 0;
    public static MenuManager instance;

    void Start()
    {
        int length = menus.Length;
        for (int i = 1; i < length; i++)
        {
            menus[i].localPosition = Vector3.zero;
            menus[i].gameObject.SetActive(false);
        }

        OnOpenMiniMenu += MenuManager_OnOpenMiniMenu;
        OnClosedMiniMenu += MenuManager_OnClosedMiniMenu;

        instance = this;
        if (!User.Instance.loggedin)
            openLogin();
        else
            openMainMenu();
    }

    public void openMainMenu()
    {
        Login.SetActive(false);
        Menu.SetActive(true);
        open(0);
       
    }

    public void openLogin()
    {
        Login.SetActive(true);
        Menu.SetActive(false);
        
    }

    public void Logout()
    {

        User.Instance.token.loggedin = false;
        User.Instance.loggedin = false;
        openLogin();
    }

    void OnDestroy()
    {
        OnOpenMiniMenu -= MenuManager_OnOpenMiniMenu;
        OnClosedMiniMenu -= MenuManager_OnClosedMiniMenu;
    }

    void MenuManager_OnClosedMiniMenu()
    {
        UnityEngine.UI.Button[] buttons = menus[current].gameObject.GetComponentsInChildren<UnityEngine.UI.Button>();
        foreach (UnityEngine.UI.Button b in buttons)
            b.enabled = true;
    }

    void MenuManager_OnOpenMiniMenu()
    {
        UnityEngine.UI.Button[] buttons = menus[current].gameObject.GetComponentsInChildren<UnityEngine.UI.Button>();
        foreach (UnityEngine.UI.Button b in buttons)
            b.enabled = false;
    }



    public void open(int arg)
    {
        if (current == arg)
        {
            CustomDebug.Log("Menu Tried to open self", CustomDebug.Level.Warn);
            return;
        }
        else
        {
            CustomDebug.Log("Opened menu no: " + arg);

            if (arg != 0 && current != 0)
                StartCoroutine(openAMenu(arg));
            else if (current == 0)
                StartCoroutine(openFromMain(arg));
            else if (arg == 0)
                StartCoroutine(openMain());
            else
                CustomDebug.Log("something weard happend", CustomDebug.Level.Warn);
        }
    }

    IEnumerator openAMenu(int newMenu)
    {
        int length = 30;
        RectTransform temp = menus[newMenu];
        float a = 1f / length;
        for (int i = length; i >0; i--)
        {
            temp.localScale = new Vector2(a * i, a * i);
            yield return new WaitForEndOfFrame();
        }
        temp.gameObject.SetActive(false);
        temp = menus[current];
        temp.gameObject.SetActive(true);
        for (int i = 0; i < length; i++)
        {
            temp.localScale = new Vector2(a * i, a * i);
            yield return new WaitForEndOfFrame();
        }

        current = newMenu;
    }

    IEnumerator openMain()
    {
        int length = 30;
        RectTransform temp = menus[current];
        float a = 1f / length;
        for (int i = length; i > 0; i--)
        {
            temp.localScale = new Vector2(a * i, a * i);
            yield return new WaitForEndOfFrame();
        }
        temp.gameObject.SetActive(false);
        UnityEngine.UI.Button[] buttons = Main.GetComponentsInChildren<UnityEngine.UI.Button>();
        foreach (UnityEngine.UI.Button b in buttons)
        {
            b.enabled = true;
        }
        current = 0;
    }

    IEnumerator openFromMain(int newMenu)
    {
        int length = 30;
        RectTransform temp;
        
        UnityEngine.UI.Button[] buttons = Main.GetComponentsInChildren<UnityEngine.UI.Button>();
        foreach (UnityEngine.UI.Button b in buttons)
        {
            b.enabled = false;
        }
        
        temp = menus[newMenu];
        temp.gameObject.SetActive(true);
        float a = 1f / length;
        for (int i = 0; i < length; i++)
        {
            temp.localScale = new Vector2(a * i, a * i);
            yield return new WaitForEndOfFrame();
        }

        current = newMenu;
    }
}
