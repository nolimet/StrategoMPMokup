using UnityEngine;
using System.Collections;

public class Chathandler : MonoBehaviour {

    public UnityEngine.UI.InputField inputField;
    public UnityEngine.UI.Text textField;
    [SerializeField]
    RectTransform textfieldrect;
    [SerializeField]
    UnityEngine.UI.ScrollRect scrollRect;

    public static Chathandler instance;

    //[SerializeField]
   // string[] messages;

    public delegate void ReceivedChat(string sender, string text);
    public static event ReceivedChat OnChatRecieved;
    public static void CallOnChatRecieved(string sender, string text)
    {
        OnChatRecieved(sender, text);
    }

    public delegate void SendChat(string sender, string text);
    public static event SendChat OnSendChat;
    public static void CallOnchatSend(string sender, string text)
    {
        OnSendChat(sender, text);
    }

    void Start()
    {
        OnChatRecieved += Chathandler_OnChatRecieved;
        OnSendChat += Chathandler_OnSendChat;
        textField.text = "";
        //messages = new string[8];
        instance = this;
        UnityEngine.UI.InputField.SubmitEvent submitEvent = new UnityEngine.UI.InputField.SubmitEvent();
        submitEvent.AddListener(SendChatButton);
        inputField.onEndEdit = submitEvent;
        //inputField.onSubmit.AddListener((value) => { SendChatButton(value); });
    }

    void Chathandler_OnSendChat(string sender, string text)
    {
        CustomDebug.Log(sender + " : " + text, CustomDebug.Level.Trace);
    }

    void Chathandler_OnChatRecieved(string sender, string text)
    {
        textField.text += (sender + ": " + text + "\n"); //"[" + System.DateTime.Now.ToString() + "]" +
        textfieldrect.sizeDelta = new Vector2(textfieldrect.sizeDelta.x, textField.preferredHeight);
        scrollRect.verticalNormalizedPosition = 0f;
    }

    public void SendChatButton(string value = "")
    {
        if (inputField.text != "")
            OnSendChat("player1", inputField.text);
        inputField.text = "";
    }

    /*string addedNewLine(string text)
    {
       /* messages[0] = messages[1];
        messages[1] = messages[2];
        messages[2] = messages[3];
        messages[3] = messages[4];
        messages[4] = messages[5];
        messages[5] = messages[6];
        messages[6] = messages[7];
        messages[7] = text;

        string output = "";
        int length = messages.Length;
        for (int i = 0; i < length; i++)
        {
            output += messages[i] + "\n";
        }

        return output;
    }*/

    #region Editor
/* #if UNITY_EDITOR
   void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnSendChat("player", inputField.text);
            inputField.text = "";
        }
    }
#endif*/
    #endregion
}
