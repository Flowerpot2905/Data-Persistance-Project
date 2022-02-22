using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    string playerName;
    public GameObject playerInput;
    public GameObject nameDisplay;
    public string persistentName;

    //create variable to store the current game over score
    public int latestScore = 0;

    //create variables to store the high score and the player name
    public int highScore;
    public string savedName;
   
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadLastPlayer();
    }
    // Start is called before the first frame update
    void Start()
    {
        //LoadLastPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StorePlayerName()
    {
        playerName = playerInput.GetComponent<TMP_InputField>().text;
        nameDisplay.GetComponent<TMP_Text>().text = "Hello " + playerName + "! Are you ready to start?";
        persistentName = playerName;
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    [System.Serializable]
    class SaveData
    {
        public string savedName;
        public int highScore;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();

        if (latestScore > highScore)
        {
            data.highScore = latestScore;
            data.savedName = persistentName;

            string json = JsonUtility.ToJson(data);

            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
        else return;
        
    }

    public void LoadLastPlayer()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            savedName = data.savedName;
            highScore = data.highScore;
        }
    }

}
   
