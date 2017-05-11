using UnityEngine;

[System.Serializable]
public class GunReloadSettings
{
    [Header("Reload Settings")]
    public Transform bulletMgazine;
    public float enableBulletTime = 1.0f;
}