using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public static Builder instance { private set; get; }
    public string selectedObject;
    Ray PointerRay;
    RaycastHit PointerHit;
    enum BuildMode { Building, Selecting, Free, Deleting} [SerializeField] BuildMode buildMode = new BuildMode();

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        SwitchMode();
        Building();

        if (Input.GetKeyDown(KeyCode.P))
        {
            selectedObject = "casa";
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            selectedObject = "sallon";
        }
    }
    void SwitchMode()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            buildMode = BuildMode.Building;
            Debug.Log("Building!");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            buildMode = BuildMode.Selecting;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            buildMode = BuildMode.Free;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            buildMode = BuildMode.Deleting;
        }

    }
    void Building()
    {
        if (buildMode != BuildMode.Building) return;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray rayCast = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 4f));
            Debug.Log(screenToWorld);
            GameObject obj = Instantiate(PrefabDatabase.instance.RequestPrefab(selectedObject), screenToWorld, Quaternion.identity);
            //DataManager.instance.AddStructure(obj);
        }
    }

    void DeleteStructure()
    {
        
        if (buildMode != BuildMode.Deleting) return;
        if (isRayHitting())
        {
            if(PointerHit.collider.tag == "Structure" && Input.GetKeyDown(KeyCode.Mouse0))
            {
                DataManager.instance.RemoveStructure(PointerHit.collider.gameObject.name);
                Destroy(PointerHit.collider.gameObject);
            }
        }
    }

    bool isRayHitting()
    {
        PointerRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(PointerRay, out PointerHit);
    }
}
