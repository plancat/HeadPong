using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public bool isShooting;
    public bool isRifle = false;

    public int damage;

    [Header("Settings")]
    public GunSettings gunSettings;
    [Header("Reloade")]
    public GunReloadSettings reloadSettings;
    [Header("Component")]
    public GunComponentSettings componentSettings;
    [Header("Particle")]
    public GunParticleSettings particleSettings;
    [Header("Audio")]
    public GunAudioSettings audioSettings;

    private void Awake()
    {
        isShooting = false;
    }

    public void Shoot()
    {
        if (!isShooting)
        {
            isShooting = true;

            gunSettings.curAmmo -= 1;

            // 총 발사
            StartCoroutine(ShootRate());
            StartCoroutine(ShootFlash());

            audioSettings.mainAudioSource.clip = audioSettings.shootSound;
            audioSettings.mainAudioSource.Play();

            particleSettings.smokeParticles.Play();
            particleSettings.sparkParticles.Play();

            GunCasting();

            componentSettings.flashImageObject.SetActive(true);
        }
    }

    private void Update()
    {
        GameMng.GetInstance.BulletText.text = gunSettings.curAmmo.ToString();
        GameMng.GetInstance.MaxBulletText.text = gunSettings.maxAmmo.ToString();
    }

    private IEnumerator ShootRate()
    {
        yield return new WaitForSeconds(gunSettings.fireRate);
        isShooting = false;
    }

    private IEnumerator ShootFlash()
    {
        yield return new WaitForSeconds(0.05f);
        componentSettings.flashImageObject.SetActive(false);
    }


    public void Reload()
    {
        audioSettings.mainAudioSource.clip = audioSettings.reloadSound;
        audioSettings.mainAudioSource.Play();

        StartCoroutine(ReloadAmmo());
    }

    private IEnumerator ReloadAmmo()
    {
        yield return new WaitForSeconds(2.6f);
        if(gunSettings.maxAmmo >= gunSettings.miAmmo)
        {
            gunSettings.maxAmmo -= gunSettings.miAmmo;
            gunSettings.curAmmo = gunSettings.miAmmo;
        }
        else
        {
            gunSettings.curAmmo = gunSettings.maxAmmo;
            gunSettings.maxAmmo = 0;
        }
    }

    // 총쏘기 전 우선 호출
    public bool IsLeftAmmo()
    {
        if (gunSettings.curAmmo <= 0)
            return false;
        else
            return true;
    }

    public void GunCasting()
    {
        StartCoroutine(CastingDelay());
    }

    private IEnumerator CastingDelay()
    {
        yield return new WaitForSeconds(0.025f);
        Vector3 rayOrigin = Player.GetInstance.playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;

        if (Physics.Raycast(
            rayOrigin,
            Player.GetInstance.playerCamera.transform.forward,
            out hit,
            gunSettings.bulletDistance))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                hit.transform.SendMessage("Hit", hit.point);
            }
        }
    }
}