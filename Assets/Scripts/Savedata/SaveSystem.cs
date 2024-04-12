using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem
{
    public static void SaveArmy(OurHand ourHand)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/ourHand";
        FileStream stream = new FileStream(path, FileMode.Create);

        ArmyData army = new ArmyData(ourHand);
        
        formatter.Serialize(stream, army);
        stream.Close();
    }

    public static OurHand LoadArmy()
    {
        string path = Application.persistentDataPath + "/ourHand";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            
            OurHand ourHand = formatter.Deserialize(stream) as OurHand;
            stream.Close();

            return ourHand;
        }
        else
        {
            Debug.LogError("Error");
            return null;
        }
    }
}
