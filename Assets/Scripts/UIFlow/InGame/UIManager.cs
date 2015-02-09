using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    #region Variables
    [SerializeField]
    GameObject ChatWindow = null, PauseWindow = null;
    [SerializeField]
    UnityEngine.UI.Text PauseText;
    bool PauseReceivedCallback;
    bool pauseopen = false;
    #endregion


    #region Events
    public delegate void PauseRequest();
    public static event PauseRequest OnPauseRequest;

    public delegate void RecievePauseRequest();
    public static event RecievePauseRequest OnRecievedPauseRequest;

    public delegate void AcceptPauseRequest();
    public static event AcceptPauseRequest OnAcceptPauseRequest;
    public static void callOnAcceptPauseRequest()
    {
        OnAcceptPauseRequest();
    }

    public delegate void ContinueGameRequest();
    public static event ContinueGameRequest onContinueGameRequest;
    public static void callOnContinueGameRequest()
    {
        onContinueGameRequest();
    }

    public delegate void DSendSurrender();
    public static event DSendSurrender OnSurrenderSend;

    public delegate void GotSurrender();
    public static event GotSurrender OnGotSurrender;

    public delegate void GotTieRequest();
    public static event GotTieRequest OnTieRequest;

    public delegate void DRequestTie();
    public static event DRequestTie OnRequestTie;
    #endregion

    #region UI driven Functions
    public void RequestPause()
    {
        if (!pauseopen)
        {
            OnPauseRequest();
            StartCoroutine(WaitPause());
        }
    }

    public void ToggleChat()
    {
        StartCoroutine(ShowChat());
    }

    public void SendSurrender()
    {
        OnSurrenderSend();
    }

    public void RequestTie()
    {
        OnRequestTie();
    }

    public void ToggleGraveYard()
    {
        throw new System.NotImplementedException();
    }

    public void Continue()
    {
        onContinueGameRequest();
        StopCoroutine(WaitPause());
    }
    #endregion

    void Start()
    {
        OnTieRequest += UIManager_OnTieRequest;
        OnGotSurrender += UIManager_OnGotSurrender;
        OnAcceptPauseRequest += UIManager_OnAcceptPauseRequest;
        OnRecievedPauseRequest += UIManager_OnRecievedPauseRequest;
        OnPauseRequest += UIManager_OnPauseRequest;
        onContinueGameRequest += UIManager_onContinueGameRequest;
    }

    void OnDestroy()
    {
        OnTieRequest -= UIManager_OnTieRequest;
        OnGotSurrender -= UIManager_OnGotSurrender;
        OnAcceptPauseRequest -= UIManager_OnAcceptPauseRequest;
        OnRecievedPauseRequest -= UIManager_OnRecievedPauseRequest;
        OnPauseRequest -= UIManager_OnPauseRequest;
        onContinueGameRequest -= UIManager_onContinueGameRequest;
    }

    void UIManager_onContinueGameRequest()
    {
        StartCoroutine(ClosePause());
    }

    #region EventDriven Functions
    void UIManager_OnGotSurrender()
    {
        throw new System.NotImplementedException();
    }

    void UIManager_OnTieRequest()
    {
        throw new System.NotImplementedException();
    }

    void UIManager_OnPauseRequest()
    {
        //throw new System.NotImplementedException();
        //StartCoroutine(OpenPause());
    }

    void UIManager_OnRecievedPauseRequest()
    {
        throw new System.NotImplementedException();
    }

    void UIManager_OnAcceptPauseRequest()
    {
        //throw new System.NotImplementedException();
        PauseReceivedCallback = true;
    }
    #endregion

    #region IEumerators
    IEnumerator ShowChat()
    {
        bool isActive = ChatWindow.activeSelf;
        int length = 40;
        float a = 15f / 20;
        if (!isActive)
        {
            ChatWindow.SetActive(true);
            for (int i = length; i > 0; i--)
            {
                ChatWindow.transform.localPosition = new Vector3(ChatWindow.transform.localPosition.x, 0f - a * i, ChatWindow.transform.localPosition.z);
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            for (int i = 0; i < length; i++)
            {
                ChatWindow.transform.localPosition = new Vector3(ChatWindow.transform.localPosition.x, 0f - a * i, ChatWindow.transform.localPosition.z);
                yield return new WaitForEndOfFrame();
            }
            ChatWindow.SetActive(false);
        }
    }

    IEnumerator WaitPause()
    {
        if (!pauseopen)
        {
            pauseopen = true;
            StartCoroutine(OpenPause());

            PauseText.text = "Waiting for Response";
            int length = 300;
            for (int i = 0; i < length; i++)
            {
                if (PauseReceivedCallback)
                    break;

                if (i % 12 == 0)
                    PauseText.text = "." + PauseText.text + ".";
                if (i % 48 == 0)
                    PauseText.text = "Waiting for Response";
                yield return new WaitForEndOfFrame();
            }
            if (!PauseReceivedCallback)
            {
                PauseText.text = "No response";
                yield return new WaitForSeconds(0.9f);
                StartCoroutine("ClosePause");
                CustomDebug.Log("Did not recieve");
            }
            else
            {
                PauseText.text = "Paused";
                CustomDebug.Log("Paused");
            }
            PauseReceivedCallback = false;
           // pauseopen = false;
        }
        yield return new WaitForEndOfFrame();
    }

    IEnumerator OpenPause()
    {
        pauseopen = true;

        int length = 40;
        float a = 15f / 20;

        PauseWindow.SetActive(true);
        for (int i = length; i > 0; i--)
        {
            PauseWindow.transform.localPosition = new Vector3(PauseWindow.transform.localPosition.x, 0f - a * i, PauseWindow.transform.localPosition.z);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator ClosePause()
    {
        pauseopen = false;

        int length = 40;
        float a = 15f / 20;

        for (int i = 0; i < length; i++)
        {
            PauseWindow.transform.localPosition = new Vector3(PauseWindow.transform.localPosition.x, 0f - a * i, PauseWindow.transform.localPosition.z);
            yield return new WaitForEndOfFrame();
        }
        PauseWindow.SetActive(false);
       
    }
    #endregion
}
