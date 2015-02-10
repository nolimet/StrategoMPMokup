using System.Collections;
using UnityEngine;

namespace util
{
    class saveValue
    {
        public static void setBool(bool value, string name)
        {
            if (value)
                PlayerPrefs.SetInt(name, 1);
            else
                PlayerPrefs.SetInt(name, 0);
        }

        public static bool getBool(string name)
        {
            int tmp = PlayerPrefs.GetInt(name, 0);

            if (tmp == 0)
                return false;
            else
                return true;
        }
    }
}
