﻿using UnityEngine;

[System.Serializable]
public class GunSettings
{
    [Header("Ammo")]
    public int curAmmo;
    public int maxAmmo;

    private bool isNoAmmo = false;
    private bool isRelease = false;

    [Header("Fire Rate & Bullet Setting")]
    public bool isAutomaticFire = false;
    public float fireRate;

    [Space(5)]
    public float bulletDistance = 500.0f;
    public float bulletForce = 500.0f;
}