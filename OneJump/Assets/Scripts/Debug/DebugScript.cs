using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;

public class DebugScript : MonoBehaviour
{
    public GameManager gameManager;

    [System.Serializable]
    public class Wrapper
    {
        public ClickData clickSavedData;
    }

    private void Awake()
    {
        gameManager = this.GetComponent<GameManager>();
        gameManager.saveDebugData += SaveDebugData;
    }

    [System.Serializable]
    public class ClickData
    {
        public int clicks1;
        public int clicks2;
        public int clicks3;
    }


    protected void SaveDebugData()
    {
        if (gameManager.debugMode)
        {
            ClickData data = new ClickData()
            {
                clicks1 = gameManager.min1Click,
                clicks2 = gameManager.min2Click,
                clicks3 = gameManager.min3Click,
            };

            Wrapper GameDataWrapper = new Wrapper()
            {
                clickSavedData = data
            };


            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText("H:\\Year3\\SaveFiles" + "/" + "DebugData.oj", json);
        }
    }
}
