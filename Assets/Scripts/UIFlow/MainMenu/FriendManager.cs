using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FriendManager : MonoBehaviour
{

    public delegate void addedFriend(FriendData data);
    public static event addedFriend OnAddFriend;
    public static void callOnAddFriend(FriendData data)
    {
        OnAddFriend(data);
    }

    public delegate void removedFriend(FriendData data);
    public static event removedFriend OnRemoveFriend;
    public static void callRemoveFriend(FriendData data)
    {
        OnRemoveFriend(data);
    }
    [SerializeField]
    List<FriendData> editFriends = new List<FriendData>(); //forEditor filing;

    Dictionary<string, FriendData> friends = new Dictionary<string, FriendData>();
    Dictionary<string, GameObject> friendsInList = new Dictionary<string, GameObject>();
    [SerializeField]
    GameObject friendIconParent = null, scrollerContent = null;
    [SerializeField]
    UnityEngine.UI.ScrollRect scrollrect;

    public FriendHandler friendPopupWindow;

    void Start()
    {
        friendIconParent.SetActive(false);
        friendPopupWindow.gameObject.SetActive(false);
        OnAddFriend += FriendManager_OnAddFriend;
        OnRemoveFriend += FriendManager_OnRemoveFriend;
        PlayerManager.OnAddFriend += PlayerManager_OnAddFriend;
        BuildList();
    }

    void PlayerManager_OnAddFriend(PlayerData data)
    {
        throw new System.NotImplementedException();
    }

    void FriendManager_OnRemoveFriend(FriendData data)
    {
        removeFriend(data);
        CustomDebug.Log("Removed Friend named: " + data.Name);
    }

    void FriendManager_OnAddFriend(FriendData data)
    {
        AddFriend(data);
        scrollerContent.SendMessage("calcHeight");
    }

    void BuildList()
    {
        
        foreach (FriendData f in editFriends)
        {
            CustomDebug.Log(f.Name, CustomDebug.Level.Trace);
            AddFriend(f);
        }
        scrollerContent.SendMessage("calcHeight");
    }

    void OnEnable()
    {
        scrollrect.verticalNormalizedPosition = 1;
    }

    void AddFriend(FriendData data)
    {
        friends.Add(data.Name, data);
        GameObject G = (GameObject)Instantiate(friendIconParent);
        friendsInList.Add(data.Name, G);
        G.SetActive(true);
        G.name = data.Name;
        G.transform.SetParent(scrollerContent.transform);
        G.transform.localScale = new Vector3(1, 1, 1);
        G.GetComponentInChildren<UnityEngine.UI.Text>().text = data.Name;
        UnityEngine.UI.Button B = G.GetComponent<UnityEngine.UI.Button>();
        B.onClick.RemoveAllListeners();
        B.onClick.AddListener(() => { friendPopupWindow.SetData(friends[data.Name]); SmallWindow.singleton.open(friendPopupWindow.gameObject); });
        if(data.status == friendStatus.Offline)
            B.image.color = Color.gray; 
        
    }

    void removeFriend(FriendData data)
    {
        GameObject G = friendsInList[data.Name];
        friendsInList.Remove(data.Name);
        Destroy(G);
        friends.Remove(data.Name);
        scrollerContent.SendMessage("calcHeight");

    }
}
