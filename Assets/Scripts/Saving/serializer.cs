using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public static class Serialization
{

    public static string saveFolderName = "Stratego";

    public static string SaveLocation(string worldName)
    {
        string saveLocation =  saveFolderName + "/" + worldName + "/";

        if (!Directory.Exists(saveLocation))
        {
            Directory.CreateDirectory(saveLocation);
        }

        return saveLocation;
    }

    public static string FileName()
    {
        string fileName = "token" + ".tkn";
        return fileName;
    }

    public static void Save(UserToken token)
    {
        string saveFile = SaveLocation("MP");
        saveFile += FileName();

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(saveFile, FileMode.Create, FileAccess.Write, FileShare.None);
        
        formatter.Serialize(stream, token);
        stream.Close();

    }

    public static bool Load()
    {
        string saveFile = SaveLocation("MP");
        saveFile += FileName();

        if (!File.Exists(saveFile))
            return false;

        IFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(saveFile, FileMode.Open);

        //chunk.blocks = (Block[, ,])formatter.Deserialize(stream);
        UserToken token = (UserToken)formatter.Deserialize(stream);

        User.Instance.token = token;

        stream.Close();
        return true;
    }
}
