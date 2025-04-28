using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;

public class SaveSystem : MonoBehaviour
{

    [System.Serializable]
    private class GameSaveWrapper
    {
        public List<PlanetSaveData> planets;
        public GameManagerSaveData gameManger;
    }

    public class GameManagerSaveData
    {
        public int mineral1Amount;
        public int mineral2Amount;
        public int mineral3Amount;
    }


    public void SaveData(string filename)
    {

        Planet[] planets = FindObjectsOfType<Planet>();
        List<PlanetSaveData> planetsData = new List<PlanetSaveData>();

        foreach (Planet planet in planets)
        {
            planetsData.Add(planet.SaveState());
        }

        GameManager gameManager = FindObjectOfType<GameManager>();
        GameManagerSaveData gameManagerSaveData = new GameManagerSaveData()
        {
            mineral1Amount = gameManager.mineral1Amount,
            mineral2Amount = gameManager.mineral2Amount,
            mineral3Amount = gameManager.mineral3Amount
        };

        GameSaveWrapper wrapper = new GameSaveWrapper()
        {
            planets = planetsData,
            gameManger = gameManagerSaveData
        };

        string json = JsonUtility.ToJson(wrapper, true);
        Debug.Log($"Saved the data at: {Application.persistentDataPath}/{filename}");
    }

    public void LoadData(string filename)
    {
        GameManager gameManager = FindObjectOfType<GameManager>();

        string path = GetFilePath(filename);

        if (!File.Exists(path))
        {
            Debug.LogError("There is no save file: " + path);
            return;
        }

        string json = File.ReadAllText(path);
        GameSaveWrapper wrapper = JsonUtility.FromJson<GameSaveWrapper>(json);

        Planet[] currentPlanets = FindObjectsOfType<Planet>();


        if(wrapper.planets.Count == currentPlanets.Length) //If everything is oki doki, then load file, no warning.
        {
            for(int i =0; i< currentPlanets.Length; i++)//Loads however many planets are currently in scene.
            {
                currentPlanets[i].LoadPlanetData(wrapper.planets[i]);
            }
            gameManager.mineral1Amount = wrapper.gameManger.mineral1Amount;
            gameManager.mineral2Amount = wrapper.gameManger.mineral2Amount;
            gameManager.mineral3Amount = wrapper.gameManger.mineral3Amount;
        }
        else if( wrapper.planets.Count > currentPlanets.Length) //Checks if more planet than currently in game, which is bad bad.
        {
            Debug.LogWarning($"There are more planets in save file {wrapper.planets.Count} than current {currentPlanets.Length} ");


            for (int i = 0; i < currentPlanets.Length; i++)//Loads however many planets are currently in scene.
            {
                currentPlanets[i].LoadPlanetData(wrapper.planets[i]);
            }

            gameManager.mineral1Amount = wrapper.gameManger.mineral1Amount;
            gameManager.mineral2Amount = wrapper.gameManger.mineral2Amount;
            gameManager.mineral3Amount = wrapper.gameManger.mineral3Amount;
        }
        else
        {
            Debug.LogWarning($"There are fewer planets in save file {wrapper.planets.Count} than current {currentPlanets.Length} ");

            for (int i = 0; i < currentPlanets.Length; i++)//Loads however many planets are currently in scene.
            {
                currentPlanets[i].LoadPlanetData(wrapper.planets[i]);
            }

            gameManager.mineral1Amount = wrapper.gameManger.mineral1Amount;
            gameManager.mineral2Amount = wrapper.gameManger.mineral2Amount;
            gameManager.mineral3Amount = wrapper.gameManger.mineral3Amount;
        }

    }

    private string GetFilePath(string filename)
    {
        return Path.Combine(Application.persistentDataPath, filename);
    }
}
