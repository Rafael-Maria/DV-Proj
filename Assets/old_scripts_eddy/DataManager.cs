using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager instance { get; set; }
    public StructureDB structureDB;
    private void Start()
    {
        instance = this;
        LoadData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void AddStructure(GameObject obj)
    {
        Structure s = new Structure();
        s.prefabID = Builder.instance.selectedObject;
        s.structureID = s.prefabID + structureDB.structures.Count + ToString();
        obj.name = s.structureID;
        s.position = obj.transform.position;
        structureDB.structures.Add(s);
    }

    public void RemoveStructure(string structureID)
    {
        Structure s = structureDB.structures.Where(x => x.structureID == structureID).First();
        if(s != null)
            structureDB.structures.Remove(s);
    }

    public void SaveData()
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(DataManager));
        FileStream stream = new FileStream(Application.persistentDataPath + "/saves/data.xml", FileMode.Create);
        xmlSerializer.Serialize(stream, structureDB);
        stream.Close();
    }

    public void LoadData()
    {
        if (!File.Exists((Application.persistentDataPath + "/saves/data.xml"))) return;
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(DataManager));
        FileStream stream = new FileStream(Application.persistentDataPath + "/saves/data.xml", FileMode.Open);
        structureDB = xmlSerializer.Deserialize(stream) as StructureDB;
        stream.Close();

        foreach(Structure s in structureDB.structures)
        {
            GameObject g = Instantiate(PrefabDatabase.instance.RequestPrefab(s.prefabID), s.position, Quaternion.identity);
            g.name = s.structureID;
        }
    }
}


[System.Serializable]
public class StructureDB
{
    public List<Structure> structures = new List<Structure>();
}

[System.Serializable]
public class Structure
{
    public string prefabID;
    public string structureID;
    public Vector3 position;
}
