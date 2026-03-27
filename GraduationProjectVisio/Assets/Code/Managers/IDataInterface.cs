using UnityEngine;

public interface IDataInterface
{
    void LoadData(SaveFile data);

    void SaveData(ref SaveFile data);
}
