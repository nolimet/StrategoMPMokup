﻿using UnityEngine;
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
        UIManager.OnTieRequest += UIManager_OnTieRequest;
    }

    void OnDestory()
    {
        Chathandler.OnSendChat -= Chathandler_OnSendChat;
        Chathandler.OnChatRecieved -= Chathandler_OnChatRecieved;
        UIManager.OnAcceptPauseRequest -= UIManager_OnAcceptPauseRequest;
        UIManager.OnGotSurrender-= UIManager_OnGotSurrender;
        UIManager.OnPauseRequest -= UIManager_OnPauseRequest;
        UIManager.OnRecievedPauseRequest -= UIManager_OnRecievedPauseRequest;
        UIManager.OnRequestTie -= UIManager_OnRequestTie;
        UIManager.OnSurrenderSend -= UIManager_OnSurrenderSend;
        UIManager.OnTieRequest -= UIManager_OnTieRequest;
    }

    void UIManager_OnTieRequest()
    {
        throw new System.NotImplementedException();
    }

    void UIManager_OnSurrenderSend()
    {
        throw new System.NotImplementedException();
    }

    void UIManager_OnRequestTie()
    {
        throw new System.NotImplementedException();
    }

    void UIManager_OnRecievedPauseRequest()
    {
        throw new System.NotImplementedException();
    }

    void UIManager_OnPauseRequest()
    {
        if (willsend)
            StartCoroutine(pausepause());
        else
            CustomDebug.Log("No pause", CustomDebug.Level.Info);
        willsend = !willsend;
    }

    void UIManager_OnGotSurrender()
    {
        throw new System.NotImplementedException();
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

    IEnumerator pausepause()
    {
        yield return new WaitForSeconds(0.5f);
        UIManager.callOnAcceptPauseRequest();
    }
}
