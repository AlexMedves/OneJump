using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    public GameManager gameManager;
    public UpgradeManager upgradeManager;

    [System.Serializable]
    private class GameSaveWrapper
    {
        public GameManagerSaveData gameMangerData;
        public List<PlanetSaveData> planets;
    }

    [System.Serializable]
    public class GameManagerSaveData
    {
        public int mineral1Amount;
        public int mineral2Amount;
        public int mineral3Amount;
    }


    private void Awake()
    {
        gameManager = this.GetComponent<GameManager>();
        upgradeManager = FindObjectOfType<UpgradeManager>();
    }


    public void SaveData(string filename)
    {

        Planet[] planets = FindObjectsOfType<Planet>();
        List<PlanetSaveData> planetsData = new List<PlanetSaveData>();

        foreach (Planet planet in planets)
        {
            planetsData.Add(planet.SaveState());
        }

        
        GameManagerSaveData gameManagerSaveData = new GameManagerSaveData()
        {
            mineral1Amount = gameManager.mineral1Amount,
            mineral2Amount = gameManager.mineral2Amount,
            mineral3Amount = gameManager.mineral3Amount,

        };


        GameSaveWrapper wrapper = new GameSaveWrapper()
        {
            planets = planetsData,
            gameMangerData = gameManagerSaveData,
        };

        string json = JsonUtility.ToJson(wrapper, true);

        //File.WriteAllText(Application.persistentDataPath + "/" + filename, json);   Go back to this
        File.WriteAllText("H:\\Year3\\SaveFiles" + "/" + filename, json);

        //Debug.Log($"Saved the data at: {Application.persistentDataPath}/{filename}");
    }

    public void LoadData(string filename)
    {


        string path = "H:\\Year3\\SaveFiles" + "/" + filename; //This needs to be changed to GetFilePath(Path) or whatever.
        Debug.Log(path);

        if (!File.Exists(path))
        {
            Debug.LogError("There is no save file: " + path);
            return;
        }

        string json = File.ReadAllText(path);
        GameSaveWrapper wrapper = JsonUtility.FromJson<GameSaveWrapper>(json);

        Planet[] currentPlanets = FindObjectsOfType<Planet>();

        gameManager.mineral1Amount = wrapper.gameMangerData.mineral1Amount;
        gameManager.mineral2Amount = wrapper.gameMangerData.mineral2Amount;
        gameManager.mineral3Amount = wrapper.gameMangerData.mineral3Amount;



        if (wrapper.planets.Count == currentPlanets.Length) //If everything is oki doki, then load file, no warning.
        {
            for(int i =0; i< currentPlanets.Length; i++)//Loads however many planets are currently in scene.
            {
                currentPlanets[i].LoadPlanetData(wrapper.planets[i]);
            }
        }
        else if( wrapper.planets.Count > currentPlanets.Length) //Checks if more planet than currently in game, which is bad bad.
        {
            Debug.LogWarning($"There are more planets in save file {wrapper.planets.Count} than current {currentPlanets.Length} ");


            for (int i = 0; i < currentPlanets.Length; i++)//Loads however many planets are currently in scene.
            {
                currentPlanets[i].LoadPlanetData(wrapper.planets[i]);
            }
        }
        else
        {
            Debug.LogWarning($"There are fewer planets in save file {wrapper.planets.Count} than current {currentPlanets.Length} ");

            for (int i = 0; i < currentPlanets.Length; i++)//Loads however many planets are currently in scene.
            {
                currentPlanets[i].LoadPlanetData(wrapper.planets[i]);
            }
        }

    }

    public string GetFilePath(string filename)
    {
        return Path.Combine(Application.persistentDataPath, filename);
    }
}
