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

    PersistentData persistentData = new PersistentData();
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
        globalHighscoreText.text = persistentData.globalHighscore.ToString();
        sessionHighscoreText.text = volatileData.sessionHighscore.ToString();
    }

    public void IncrementScore()
    {
        ++volatileData.score;

        if (volatileData.score > volatileData.sessionHighscore)
        {
            volatileData.sessionHighscore = volatileData.score;
        }

        if (volatileData.score > persistentData.globalHighscore)
        {
            persistentData.globalHighscore = volatileData.score;
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
            formatter.Serialize(stream, persistentData);
        }
    }

    public void LoadHighscore()
    {
        try
        {
            using (FileStream stream = File.OpenRead(filePath))
            {
                persistentData = formatter.Deserialize(stream) as PersistentData;
            }
        }
        catch (FileNotFoundException)
        {
            Debug.Log("No persistent data to load");
        }
        catch (InvalidCastException)
        {
            Debug.LogWarning("Persistent data class has changed");
        }
    }
}
