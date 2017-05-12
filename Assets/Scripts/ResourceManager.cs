using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private static ResourceManager instance = null;
    public static ResourceManager GetInstance
    {
        get
        {
            return instance;
        }
    }

    [System.Serializable]
    public class Resource
    {
        public GameObject prefab;
        public string resourceName;
        public int resourceLength;
    }

    public  Resource[] prefabs;

    private Dictionary<string, List<GameObject>> resources;

    public void Awake()
    {
        instance = this;

        resources = new Dictionary<string, List<GameObject>>();

        for (int i = 0; i < prefabs.Length; i++)
        {
            List<GameObject> objects = new List<GameObject>();
            for(int j = 0; j < prefabs[i].resourceLength; j++)
            {
                GameObject temp = Instantiate(prefabs[i].prefab, transform);
                temp.gameObject.SetActive(false);
                objects.Add(temp);
            }
            if(objects.Count > 0)
                resources.Add(prefabs[i].resourceName, objects);
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public GameObject GetObject(string prefabName)
    {
        for(int i = 0; i < resources[prefabName].Count; i++)
        {
            if (!resources[prefabName][i].activeSelf)
            {
                resources[prefabName][i].SetActive(true);

                return resources[prefabName][i];
            }
        }

        return null;
    }
}