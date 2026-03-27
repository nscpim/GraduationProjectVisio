using UnityEngine;
using System;
using System.IO;

public class WebFileHandler
{
    private string dataPath = "";

    private string dataFileName = "";


    public WebFileHandler(string dataPath, string dataFileName)
    {
        this.dataPath = dataPath;
        this.dataFileName = dataFileName;
    }


    public SaveFile Load()
    {
        string fullPath = Path.Combine(dataPath, dataFileName);

        SaveFile loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }


                loadedData = JsonUtility.FromJson<SaveFile>(dataToLoad);

            }
            catch (Exception e)
            {
                Debug.LogError(e.StackTrace);
                throw;
            }
        }
        return loadedData;

    }


    public void Save(SaveFile data)
    {
        string fullPath = Path.Combine(dataPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }

        }
        catch (Exception e)
        {
            Debug.LogError(e.StackTrace);
            throw;
        }

    }
}

