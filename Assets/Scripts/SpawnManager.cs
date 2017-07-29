using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Z")]
    public Transform[] zSpawns;

    [Space(10)]

    [Header("Item")]
    public Transform[] iSpawns;

    public void ZSpawn(int stage)
    {
        Transform position = zSpawns[Random.Range(0, zSpawns.Length)];

        int zIdx = Random.Range(1, 4);
        GameObject z = ResourceManager.GetInstance.GetObject("Z" + zIdx.ToString());
        if (z != null)
        {
            if (zIdx == 1)
                z.GetComponent<Enemy>().health = 1 + stage;
            else if (zIdx == 2)
                z.GetComponent<Enemy>().health = stage;
            else
                z.GetComponent<Enemy>().health = 2 + stage;

            z.GetComponent<Enemy>().Live();
            z.transform.position = position.position;
            z.gameObject.SetActive(true);
        }
    }

    public void ISpawn()
    {
        Transform position = iSpawns[Random.Range(0, iSpawns.Length)];

        GameObject i = ResourceManager.GetInstance.GetObject("Heal");
        if (i != null)
        {
            i.transform.position = position.position;
            i.gameObject.SetActive(true);
        }
    }
}