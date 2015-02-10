using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerManager : MonoBehaviour
{
    #region events
    public delegate void addedPlayer(PlayerData data);
    public static event addedPlayer OnAddPlayer;
    public static void callOnAddPlayer(PlayerData data)
    {
        OnAddPlayer(data);
    }

    public delegate void AddedFriend(PlayerData data);
    public static event AddedFriend OnAddFriend;
    public static void callAddFriend(PlayerData data)
    {
        OnAddFriend(data);
    }

    #endregion
    [SerializeField]
    List<PlayerData> editPlayers = new List<PlayerData>(); //forEditor filing;
    [SerializeField]
    UnityEngine.UI.ScrollRect scrollrect;
    Dictionary<string, PlayerData> players = new Dictionary<string, PlayerData>();
    Dictionary<string, GameObject> playersInList = new Dictionary<string, GameObject>();
    [SerializeField]
    GameObject playerIconParent = null, scrollerContent = null;

    public PlayerHandler playerPopupWindow;

    void Start()
    {
        playerIconParent.SetActive(false);
        playerPopupWindow.gameObject.SetActive(false);
        OnAddPlayer += PlayerManager_OnAddPlayer;
        OnAddFriend += PlayerManager_AddFriend;
        BuildList();
    }
    #region Event Listeners
    void PlayerManager_AddFriend(PlayerData data)
    {
        removeFriend(data);
        CustomDebug.Log("Removed Friend named: " + data.Name);
    }

    void PlayerManager_OnAddPlayer(PlayerData data)
    {
        AddPlayer(data);
        scrollerContent.SendMessage("calcHeight");
    }

    #endregion

    void BuildList()
    {

        foreach (PlayerData f in editPlayers)
        {
            CustomDebug.Log(f.Name, CustomDebug.Level.Trace);
            AddPlayer(f);
        }
        scrollerContent.SendMessage("calcHeight");

        
    }

    void OnEnable()
    {
        scrollrect.verticalNormalizedPosition = 1;
    }

    void AddPlayer(PlayerData data)
    {
        players.Add(data.Name, data);
        GameObject G = (GameObject)Instantiate(playerIconParent);
        playersInList.Add(data.Name, G);
        G.SetActive(true);
        G.hideFlags = HideFlags.HideInHierarchy;
        G.transform.SetParent(scrollerContent.transform);
        G.transform.localScale = new Vector3(1, 1, 1);
        G.GetComponentInChildren<UnityEngine.UI.Text>().text = data.Name;
        G.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        G.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { playerPopupWindow.SetData(players[data.Name]); SmallWindow.singleton.open(playerPopupWindow.gameObject); });
    }

    void removeFriend(PlayerData data)
    {
        GameObject G = playersInList[data.Name];
        playersInList.Remove(data.Name);
        Destroy(G);
        players.Remove(data.Name);
        scrollerContent.SendMessage("calcHeight");
    }
}
