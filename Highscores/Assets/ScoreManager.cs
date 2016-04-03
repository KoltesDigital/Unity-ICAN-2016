using UnityEngine;
using UnityEngine.UI;

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text sessionHighscoreText;
    public Text globalHighscoreText;

    public bool autoSave = true;

    PersistantData persistantData = new PersistantData();
    VolatileData volatileData = new VolatileData();

    BinaryFormatter formatter = new BinaryFormatter();

    string filePath;

    void Start ()
    {
        filePath = Path.Combine(Application.persistentDataPath, "persistant.dat");

        LoadHighscore();
        UpdateTexts();
    }

    void UpdateTexts()
    {
        scoreText.text = volatileData.score.ToString();
        globalHighscoreText.text = persistantData.globalHighscore.ToString();
        sessionHighscoreText.text = volatileData.sessionHighscore.ToString();
    }

    public void IncrementScore()
    {
        ++volatileData.score;

        if (volatileData.score > volatileData.sessionHighscore)
        {
            volatileData.sessionHighscore = volatileData.score;
        }

        if (volatileData.score > persistantData.globalHighscore)
        {
            persistantData.globalHighscore = volatileData.score;
            if (autoSave)
            {
                SaveHighscore();
            }
        }

        UpdateTexts();
    }

    public void ResetScore()
    {
        volatileData.score = 0;
        UpdateTexts();
    }

    public void SaveHighscore()
    {
        using (FileStream stream = File.OpenWrite(filePath))
        {
            formatter.Serialize(stream, persistantData);
        }
    }

    public void LoadHighscore()
    {
        try
        {
            using (FileStream stream = File.OpenRead(filePath))
            {
                persistantData = formatter.Deserialize(stream) as PersistantData;
            }
        }
        catch (FileNotFoundException)
        {
            Debug.Log("No persistant data to load");
        }
        catch (InvalidCastException)
        {
            Debug.LogWarning("Persistant data class has changed");
        }
    }
}
