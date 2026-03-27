using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class SaveLoadManager : Manager
{
    private SaveFile saveData;
    public List<IDataInterface> savedObjects;


    private WebFileHandler dataHandler;

    public override void Start()
    {
        this.savedObjects = GameManager.instance.FindAllObjectsToSave();
       // this.dataHandler = new WebFileHandler(Application.persistentDataPath, GameManager.instance.fileName);
    }

    public void NewSave()
    {
        this.saveData = new SaveFile();
    }




    public void LoadSave()
    {
      //  this.saveData = dataHandler.Load();


        if (this.saveData == null)
        {
            Debug.Log("No save file found");
            NewSave();
        }

        foreach (IDataInterface savedObject in savedObjects)
        {
            savedObject.LoadData(saveData);
        }


    }



    public bool SaveData(string fileName, string jsonData)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        //idbfs is the webbrowsers storage
        string path = Path.Combine("idbfs" + Application.productName + this.saveData.name);
        if (!File.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        path = Path.Combine(path, fileName);
#else
#endif

        foreach (IDataInterface savedObject in savedObjects)
        {
            savedObject.SaveData(ref saveData);
        }

        dataHandler.Save(saveData);
        return false;
    }
}
