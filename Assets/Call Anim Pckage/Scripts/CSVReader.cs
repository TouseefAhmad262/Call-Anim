using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class CSVReader : MonoBehaviour
{
    public List<PlayerData> playerList = new List<PlayerData>();

    void Start()
    {
        LoadCSV("Assets/Call Anim Pckage/CSV's/data.csv");
    }
    void LoadCSV(string filePath)
    {
        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] values = line.Split(',');

                    // Assuming your CSV has columns in the order: Name, Number
                    string name = values[0];
                    int number = int.Parse(values[1]);

                    PlayerData player = new PlayerData(name, number);
                    playerList.Add(player);
                }
            }

            // Print data to console (for verification)
            foreach (PlayerData player in playerList)
            {
                Debug.Log($"Name: {player.Name}, Number: {player.Number}");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error reading CSV file: " + e.Message);
        }
    }
}

[System.Serializable]
public class PlayerData
{
    public string Name;
    public int Number;

    public PlayerData(string name, int number)
    {
        Name = name;
        Number = number;
    }
}