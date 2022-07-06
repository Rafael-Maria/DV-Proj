using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureController : MonoBehaviour, ISaveable
{
    public enum StructureType
    {
        None, Casa, Sallon, Mina, Xerif, Estabulo, Armazem
    }

    public string structureID;
    [SerializeField]
    public StructureType type;
    public int level;
    public float xPos, yPos, zPos;
    void Start()
    {

    }

    void Update()
    {
        
    }

    public object SaveState()
    {
        return new SaveData()
        {
            structureID = this.structureID,
            type = this.type,
            level = this.level,
            xPos = this.transform.position.x,
            yPos = this.transform.position.y,
            zPos = this.transform.position.z
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        type = saveData.type;
        level = saveData.level;
        xPos = saveData.xPos;
        yPos = saveData.yPos;
        zPos = saveData.zPos;
        this.transform.position = new Vector3(xPos, yPos, zPos);
    }

    [Serializable]
    private struct SaveData
    {
        public string structureID;
        public StructureType type;
        public int level;
        public float xPos, yPos,zPos;
    }
}
