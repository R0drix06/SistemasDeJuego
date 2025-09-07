using UnityEngine;
using System.IO;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private string saveName;

    private GameData gameData = new GameData();

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        saveName = Application.dataPath + "/saveName.json";
    }

    public void LoadSave()
    {
        if (File.Exists(saveName))
        {
            string content = File.ReadAllText(saveName);
            gameData = JsonUtility.FromJson<GameData>(content);

            Debug.Log("Progreso actual: " + gameData.levelName);
        }
        else
        {
            Debug.Log("Datos no encontrados");
        }
    }

    public void SaveProgress()
    {
        GameData newData = new GameData()
        {
            levelName = IterationManager.Instance.CurrentLevel()
        };

        string cadenaJson = JsonUtility.ToJson(newData);

        File.WriteAllText(saveName, cadenaJson);

        Debug.Log("ArchivoGuardado");

    }
}
