using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Z")]
    public GameObject z1;
    public GameObject z2;

    public Transform[] zSpawns;

    [Space(10)]

    [Header("Item")]
    public Transform[] itemSpawns;
}