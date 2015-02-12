using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProfileBuilder : MonoBehaviour {

	[SerializeField]
    Image icon = null;
    [SerializeField]
    Text name=null,statics=null,lvl=null;

    void Start()
    {
       
        if (User.Instance)
        {

            UserToken token = User.Instance.token;
            if (icon)
                icon.sprite = token.icon;
            if (name)
                name.text = token.Name;
            if (lvl)
                lvl.text = "Level " + token.Level;
            if (statics)
            {
                statics.text = "";
                int l = token.Staticsics.Length;

                for (int i = 0; i < l; i++)
                {
                    statics.text += token.Staticsics[i];
                    if (i < l - 1)
                        statics.text += "\n";
                }
            }
            enabled = false;
        }
    }

    void LateUpdate()
    {
        Start();
    }
}
