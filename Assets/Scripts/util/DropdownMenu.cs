using UnityEngine;
using System.Collections;
namespace util
{
    public class DropdownMenu : MonoBehaviour
    {
        public static outputData dropDown(string[] list, Rect dropDownRect, outputData data)
        {
            #region Select Number of Beams
            if (GUI.Button(new Rect((dropDownRect.x - 100), dropDownRect.y, dropDownRect.width, 25), ""))
            {
                if (!data.show)
                {
                    data.show = true;
                }
                else
                {
                    data.show = false;
                }
            }

            if (data.show)
            {
                data.scrollViewVector = GUI.BeginScrollView(new Rect((dropDownRect.x - 100), (dropDownRect.y + 25), dropDownRect.width, dropDownRect.height), data.scrollViewVector, new Rect(0, 0, dropDownRect.width, Mathf.Max(dropDownRect.height, (list.Length * 25))));

                GUI.Box(new Rect(0, 0, dropDownRect.width, Mathf.Max(dropDownRect.height, (list.Length * 25))), "");

                for (int index = 0; index < list.Length; index++)
                {

                    if (GUI.Button(new Rect(0, (index * 25), dropDownRect.height, 25), ""))
                    {
                        data.show = false;
                        data.indexNumber = index;
                    }

                    GUI.Label(new Rect(5, (index * 25), dropDownRect.height, 25), list[index]);

                }

                GUI.EndScrollView();
            }
            else
            {
                GUI.Label(new Rect((dropDownRect.x - 95), dropDownRect.y, 300, 25), list[data.indexNumber]);
            }
            #endregion
            return data;
        }

        [System.Serializable]
        public class outputData
        {
            public int indexNumber = 0;
            public bool show = false;
            public Vector2 scrollViewVector = Vector2.zero;
        }
    }
}