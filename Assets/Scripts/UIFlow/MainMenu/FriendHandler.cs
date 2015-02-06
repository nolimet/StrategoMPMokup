using UnityEngine;
using System.Collections;

public class FriendHandler : MonoBehaviour {

    [SerializeField]
    private UnityEngine.UI.Text Tname = null, level = null, statistics = null, status = null;
    [SerializeField]
    private UnityEngine.UI.Image Icon = null;
    [SerializeField]
    private GameObject Online = null, Offline = null;
    [SerializeField]
    private UnityEngine.UI.Button[] unfriend;


    public void SetData(FriendData data)
    {
        foreach(UnityEngine.UI.Button b in unfriend)
        {
            b.onClick.RemoveAllListeners();
            b.onClick.AddListener(() => { FriendManager.callRemoveFriend(data); SmallWindow.singleton.close(gameObject); });
        }
        Tname.text = data.Name;
        level.text = "Level " + data.Level.ToString();
        statistics.text = data.Staticsics;
        status.text = data.status.ToString();
        Icon.sprite = data.icon;
        switch (data.status)
        {
            case friendStatus.Online:
                Offline.SetActive(false);
                Online.SetActive(true);
                break;
            case friendStatus.Offline:
                Offline.SetActive(true);
                Online.SetActive(false);
                break;
            case friendStatus.Playing:
                Offline.SetActive(false);
                Online.SetActive(true);
                break;
        }
    }
}
