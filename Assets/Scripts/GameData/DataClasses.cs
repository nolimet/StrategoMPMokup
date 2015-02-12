using UnityEngine;
using System.Collections;

[System.Serializable]
public class FriendData {
    public string Name = "steve superman";
    public int Level = 0;
    public string Staticsics = "";
    public friendStatus status = friendStatus.Offline;
    public Sprite icon;
    
    readonly Rect iconSize = new Rect(0, 0, 50, 50);
    readonly Vector2 pivotIcon = new Vector2(0.5f, 0.5f); //can't be consts
    public FriendData(string _name, int _level, string _staticsics, friendStatus _status, Texture2D _icon)
    {
        Name = _name;
        Level = _level;
        Staticsics = _staticsics;
        status = _status;
        icon = Sprite.Create(_icon, iconSize, pivotIcon);
    }
}

[System.Serializable]
public class PlayerData
{
    public string Name = "steve superman";
    public int Level = 0;
    public string Staticsics = "";
    public Sprite icon;

    readonly Rect iconSize = new Rect(0, 0, 50, 50);
    readonly Vector2 pivotIcon = new Vector2(0.5f, 0.5f); //can't be consts
    public PlayerData(string _name, int _level, string _staticsics, Texture2D _icon)
    {
        Name = _name;
        Level = _level;
        Staticsics = _staticsics;
        icon = Sprite.Create(_icon, iconSize, pivotIcon);
    }

}

[System.Serializable]
public class UserToken
{
    public string Name = "bob";
    public int Level = 0;
    public string[] Staticsics;
    public Sprite icon;

    readonly Rect iconSize = new Rect(0, 0, 50, 50);
    readonly Vector2 pivotIcon = new Vector2(0.5f, 0.5f); //can't be consts
    public UserToken(string _name, int _level, string[] _staticsics, Texture2D _icon)
    {
        Name = _name;
        Level = _level;
        Staticsics = _staticsics;
        icon = Sprite.Create(_icon, iconSize, pivotIcon);
    }
}

public enum friendStatus
{
    Online,
    Playing,
    Offline
}