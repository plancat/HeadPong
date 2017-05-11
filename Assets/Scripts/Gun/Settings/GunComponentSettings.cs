using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunComponentSettings
{
    [Header("Flash Image")]
    public GameObject flashImageObject;

    [Header("Spot Light")]
    public GameObject spotLightObject;
    public bool isSpotLight;
}