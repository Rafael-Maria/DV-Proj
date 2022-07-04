using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrefabDatabase : MonoBehaviour
{
    public static PrefabDatabase instance { private set; get; }
    private void Awake()
    {
        instance = this;
    }

    public List<PrefabItem> prefabItems = new List<PrefabItem>();

    public GameObject RequestPrefab(string prefabID)
    {
        PrefabItem item = prefabItems.Where(p => p.prefabID == prefabID).First();
        return item.prefabGameObject;
    }
}

[System.Serializable]
public class PrefabItem
{
    public GameObject prefabGameObject;
    public string prefabID;
}