
using System.Collections;
using UnityEngine;


public class TimeCover
{
    public static string tostring(float t)
    {
        float fms = Mathf.FloorToInt((t * 1000) % 1000);

        float fs = Mathf.FloorToInt(t % 60);
        // print(s);
        float fm = Mathf.FloorToInt(t / 60 % 60);
        //print(m);
        float fh = Mathf.FloorToInt((t / 3600) % 24);
        //  print(h);
        float d = Mathf.FloorToInt((t / 3600 / 24));
        // print(d);

        string ms;
        string s;
        string m;
        string h;

        if (fms == 0)
            ms = "";
        else if (fms < 10)
            ms = "000" + fms;
        else if (fms < 100)
            ms = "00" + fms;
        else
            ms = "" + fms;

        if (fs < 10)
            s = "0" + fs;
        else
            s = "" + fs;

        if (fm < 10)
            m = "0" + fm;
        else
            m = "" + fm;

        if (fh < 10)
            h = "0" + fh;
        else
            h = "" + fh;


        if (d == 0 && fh == 0 && fm == 0)
            return " " + s + ":" + ms;
        else if (d == 0 && fh == 0)
            return " " + m + ":" + s + ":" + ms;
        else if (d == 0)
            return " " + h + ":" + m + ":" + s + ":" + ms;
        else
            return " " + d + ":" + h + ":" + m + ":" + s + ":" + ms;
    }
}
