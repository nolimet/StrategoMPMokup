﻿using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    #region Variables
    [SerializeField]
    GameObject ChatWindow = null, PauseWindow = null, SurrenderConfirmWindow = null, TieConfirmWindow = null, TieWaitWindow = null;
    [SerializeField]
    GameObject[] MenuItems = new GameObject[0], Canvases = new GameObject[0];
    [SerializeField]
    RectTransform dropDown = null;
    [SerializeField]
    UnityEngine.UI.Text PauseText = null, TieWaitText = null;
    [SerializeField]
    CanvasGroup ChatGroup = null, PauseGroup = null;
    bool PauseReceivedCallback = false, pauseopen = false, recievedTieconfirm = false, dropdownMoving = false, chatmoving = false;
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
    public static void CallOnGotSurrender()
    {
        OnGotSurrender();
    }

    public delegate void GotTieRequest();
    public static event GotTieRequest OnGotTieRequest;
    public static void callOnGotTieRequest()
    {
        OnGotTieRequest();
    }

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
        SurrenderConfirmWindow.SetActive(true);
    }

    public void ConfirmSurrender()
    {
        OnSurrenderSend();
    }

    public void CancelSurrender()
    {
        SurrenderConfirmWindow.SetActive(false);
    }

    public void RequestTie()
    {
        TieConfirmWindow.SetActive(true);
    }

    public void ConfirmTie()
    {
        OnRequestTie();
        StartCoroutine(TieWait());
        TieConfirmWindow.SetActive(false);
        TieWaitWindow.SetActive(true);
    }

    public void CancelTie()
    {
        TieConfirmWindow.SetActive(false);
    }

    public void OpenMenu(int value)
    {
        foreach (GameObject g in MenuItems)
            g.SetActive(false);
        MenuItems[value].SetActive(true);
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

    public void ToggleDropDown()
    {
        StartCoroutine(ToggleDropdown());
    }
    #endregion

    #region monoDrivenEvents
    void Start()
    {
        OnGotSurrender += UIManager_OnGotSurrender;
        OnAcceptPauseRequest += UIManager_OnAcceptPauseRequest;
        OnRecievedPauseRequest += UIManager_OnRecievedPauseRequest;
        OnPauseRequest += UIManager_OnPauseRequest;
        onContinueGameRequest += UIManager_onContinueGameRequest;
        OnGotTieRequest += UIManager_OnGotTieRequest;

        foreach (GameObject g in Canvases)
            g.SetActive(true);

        dropDown.gameObject.SetActive(false);
    }

    

    void OnDestroy()
    {
        OnGotSurrender -= UIManager_OnGotSurrender;
        OnAcceptPauseRequest -= UIManager_OnAcceptPauseRequest;
        OnRecievedPauseRequest -= UIManager_OnRecievedPauseRequest;
        OnPauseRequest -= UIManager_OnPauseRequest;
        onContinueGameRequest -= UIManager_onContinueGameRequest;
        OnGotTieRequest -= UIManager_OnGotTieRequest;
    }
    #endregion

    #region EventDriven Functions
    void UIManager_OnGotTieRequest()
    {
        CustomDebug.Log("got tie request");
        recievedTieconfirm = true;
    }

    void UIManager_OnGotSurrender()
    {
        Application.LoadLevel(0);
    }

    void UIManager_onContinueGameRequest()
    {
        StartCoroutine(ClosePause());
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
        if (!chatmoving)
        {
            chatmoving = true;
            bool isActive = ChatWindow.activeSelf;
            int length = 20;
            float a = 150f / length;
            float b = 1f / length;
            Vector3 startPos = ChatWindow.transform.localPosition;
            if (!isActive)
            {
                ChatWindow.SetActive(true);
                for (int i = length; i > 0; i--)
                {
                    ChatWindow.transform.localPosition = new Vector3(startPos.x, startPos.y - a * i, startPos.z);
                    ChatGroup.alpha = b * (length - i);
                    yield return new WaitForEndOfFrame();
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    ChatWindow.transform.localPosition = new Vector3(startPos.x, startPos.y - a * i, startPos.z);
                    ChatGroup.alpha = b * (length - i - 3);
                    yield return new WaitForEndOfFrame();
                }
                ChatWindow.SetActive(false);
            }
            chatmoving = false;
            ChatWindow.transform.localPosition = startPos;
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
        float a = 150f / length;
        float b = 1f / length;

        PauseWindow.SetActive(true);
        for (int i = length; i > 0; i--)
        {
            PauseWindow.transform.localPosition = new Vector3(PauseWindow.transform.localPosition.x, 0f - a * i, PauseWindow.transform.localPosition.z);
            PauseGroup.alpha = b * (length - i - 3);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator ClosePause()
    {
        pauseopen = false;

        int length = 40;
        float a = 150f / length;
        float b = 1f / length;

        for (int i = 0; i < length; i++)
        {
            PauseWindow.transform.localPosition = new Vector3(PauseWindow.transform.localPosition.x, 0f - a * i, PauseWindow.transform.localPosition.z);
            PauseGroup.alpha = b * (length - i - 3);
            yield return new WaitForEndOfFrame();
        }
        PauseWindow.SetActive(false);
       
    }

    IEnumerator TieWait()
    {
        int length = 600;
        string msg = TieWaitText.text;
        recievedTieconfirm = false;
        for (int i = 0; i < length; i++)
        {
            if (recievedTieconfirm)
            if (i % 12 == 0)
                TieWaitText.text = "." + TieWaitText.text + ".";
            if (i % 60 == 0)
                TieWaitText.text = msg;

            yield return new WaitForEndOfFrame();
        }

        if(recievedTieconfirm)
        {
            CustomDebug.Log("Confirmed Tie");
            TieWaitText.text = "Tie Confirmed";
            yield return new WaitForSeconds(1f);
            Application.LoadLevel(0);
        }
        else
        {
            CustomDebug.Log("Tie Declined");
            TieWaitText.text = "No Tie keep playing";
            yield return new WaitForSeconds(1f);
            TieWaitText.text = msg;
            TieWaitWindow.SetActive(false);
        }
    }

    IEnumerator ToggleDropdown()
    {
        if (!dropdownMoving)
        {
            dropdownMoving = true;
            int length = 20;
            float a = 1f / length;

            if (dropDown.gameObject.activeSelf)
          {
                for (int i = length; i > 0; i--)
                {
                    dropDown.localScale = new Vector3(a * i, a * i, 1);
                    yield return new WaitForEndOfFrame();
                }
                dropDown.gameObject.SetActive(false);
            }
            else
            {
                dropDown.gameObject.SetActive(true);
                for (int i = 0; i < length; i++)
                {
                    dropDown.localScale = new Vector3(a * i, a * i, 1);
                    yield return new WaitForEndOfFrame();
                }
                dropDown.localScale = Vector3.one;
            }
            dropdownMoving = false;
        }
        
    }
    #endregion
}
