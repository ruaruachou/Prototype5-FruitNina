using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//记录最高分，大退后依然保留记录
public class RecordManager : MonoBehaviour
{
    public static RecordManager Instance;
    public int recordScore = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        LoadRecord();
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Backspace)) { recordScore = 0; }
    }
    [System.Serializable]
    class SaveData
    {
        public int Record;
    }
    public void SaveRecord()
    {
        SaveData data = new SaveData();
        data.Record = recordScore;

        string json=JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadRecord ()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            recordScore = data.Record;
        }
    }
}
