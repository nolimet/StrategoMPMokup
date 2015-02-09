using UnityEngine;
using System.Collections;

public class fakeNetwork : MonoBehaviour {

    bool willsend = false;

    void Start()
    {
        Chathandler.OnSendChat += Chathandler_OnSendChat;
        Chathandler.OnChatRecieved += Chathandler_OnChatRecieved;
        UIManager.OnAcceptPauseRequest += UIManager_OnAcceptPauseRequest;
        UIManager.OnGotSurrender += UIManager_OnGotSurrender;
        UIManager.OnPauseRequest += UIManager_OnPauseRequest;
        UIManager.OnRecievedPauseRequest += UIManager_OnRecievedPauseRequest;
        UIManager.OnRequestTie += UIManager_OnRequestTie;
        UIManager.OnSurrenderSend += UIManager_OnSurrenderSend;
        UIManager.OnGotTieRequest += UIManager_OnGotTieRequest;
    }

    void OnDestroy()
    {
        Chathandler.OnSendChat -= Chathandler_OnSendChat;
        Chathandler.OnChatRecieved -= Chathandler_OnChatRecieved;
        UIManager.OnAcceptPauseRequest -= UIManager_OnAcceptPauseRequest;
        UIManager.OnGotSurrender-= UIManager_OnGotSurrender;
        UIManager.OnPauseRequest -= UIManager_OnPauseRequest;
        UIManager.OnRecievedPauseRequest -= UIManager_OnRecievedPauseRequest;
        UIManager.OnRequestTie -= UIManager_OnRequestTie;
        UIManager.OnSurrenderSend -= UIManager_OnSurrenderSend;
        UIManager.OnGotTieRequest -= UIManager_OnGotTieRequest;
    }

    void UIManager_OnGotTieRequest()
    {
        
    }

    void UIManager_OnSurrenderSend()
    {
        UIManager.CallOnGotSurrender();
    }

    void UIManager_OnRequestTie()
    {
        if (Random.Range(0f, 100f) > 40f)
            Invoke("tieWait",Random.Range(0.2f,2f));
    }

    void UIManager_OnRecievedPauseRequest()
    {
        throw new System.NotImplementedException();
    }

    void UIManager_OnPauseRequest()
    {
        if (this == null)
        {
            return;
        }
            
        if (willsend)
            Invoke("pausepause", Random.Range(0.2f, 2f));
        else
            CustomDebug.Log("No pause", CustomDebug.Level.Info);
        willsend = !willsend;
    }

    void UIManager_OnGotSurrender()
    {
        //throw new System.NotImplementedException();
    }

    void UIManager_OnAcceptPauseRequest()
    {
        //throw new System.NotImplementedException();
    }

    void Chathandler_OnChatRecieved(string sender, string text)
    {
        
    }

    void Chathandler_OnSendChat(string sender, string text)
    {
        Chathandler.CallOnChatRecieved(sender, text);
        //Chathandler.CallOnChatRecieved("bob", "I WILL DESTORY YOU!!");
    }

    void pausepause()
    {
        UIManager.callOnAcceptPauseRequest();
    }

    void tieWait()
    {
        UIManager.callOnGotTieRequest();
    }
}
