using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    public static void SaveData(Planet[] planet, GameManager gameManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gamedata.stitch";

        using(FileStream stream = new FileStream(path, FileMode.Create))
        {

            GameData data = new GameData(planet, gameManager);

            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    public static GameData LoadData()
    {
        string path = Application.persistentDataPath + "/gamedata.stitch";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {

                GameData data = formatter.Deserialize(stream) as GameData;
                stream.Close();
                return data;
            }

        }
        else
        {
            Debug.Log("No data found");
            return null;
        }
    }
}
