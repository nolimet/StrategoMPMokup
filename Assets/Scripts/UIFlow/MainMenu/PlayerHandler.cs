using UnityEngine;
using System.Collections;

public class PlayerHandler : MonoBehaviour {

    [SerializeField]
    private UnityEngine.UI.Text Tname = null, level = null, statistics = null;
    [SerializeField]
    private UnityEngine.UI.Image Icon = null;
    [SerializeField]
    private UnityEngine.UI.Button addfriend;


    public void SetData(PlayerData data)
    {
            addfriend.onClick.RemoveAllListeners();
            addfriend.onClick.AddListener(() => { PlayerManager.callAddFriend(data); SmallWindow.singleton.close(gameObject); });
        Tname.text = data.Name;
        level.text = "Level " + data.Level.ToString();
        statistics.text = data.Staticsics;
        Icon.sprite = data.icon;
    }
}
