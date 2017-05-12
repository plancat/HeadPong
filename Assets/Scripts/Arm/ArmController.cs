using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 애니메이션 관리
public class ArmController : MonoBehaviour
{
    public Animator armAnim;

    [Header("Currnet Gun")]
    public GunBase gun;
    public GrenadeBase grenade;

    public GunBase[] handGuns;
    public GunBase[] rifles;
    public GunBase[] guns;

    public int gunsIdx = 0;

    public bool isReload = false;

    public void HandGunChange(int n, float[,] HandGunPerform)
    {
        for(int i = 0; i < handGuns.Length; i++)
            handGuns[i].gameObject.SetActive(false);

        guns[1] = handGuns[n];
        guns[1].gunSettings.miAmmo = (int)HandGunPerform[n, 1];
        guns[1].gunSettings.maxAmmo = (int)HandGunPerform[n, 2];
        guns[1].gunSettings.fireRate = HandGunPerform[n, 4];
        guns[1].damage = (int)HandGunPerform[n, 3];

        if (!grenade.gameObject.activeSelf)
        {
            if (!gun.isRifle)
            {
                gun = guns[1];
                guns[1].gameObject.SetActive(true);
            }
        }
    }

    public void RifleChange(int n, float[,] RifleGunPerform)
    {
        for (int i = 0; i < rifles.Length; i++)
            rifles[i].gameObject.SetActive(false);

        guns[0] = rifles[n];
        guns[0].gunSettings.miAmmo = (int)RifleGunPerform[n, 1];
        guns[0].gunSettings.maxAmmo = (int)RifleGunPerform[n, 2];
        guns[0].gunSettings.fireRate = RifleGunPerform[n, 4];
        guns[0].damage = (int)RifleGunPerform[n, 3];

        if (!grenade.gameObject.activeSelf)
        {
            if (gun.isRifle)
            {
                gun = guns[0];
                guns[0].gameObject.SetActive(true);
            }
        }
    }

    public void SwapWeapon(int n, bool isGrenade)
    {
        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].componentSettings.flashImageObject.gameObject.SetActive(false);
            guns[i].gameObject.SetActive(false);
        }

        grenade.gameObject.SetActive(false);

        if (!isGrenade)
        {
            gunsIdx = n;
            guns[gunsIdx].gameObject.SetActive(true);
            gun = guns[gunsIdx];

            armAnim.SetLayerWeight(2, 0);
            if (gun.isRifle)
            {
                armAnim.SetLayerWeight(1, 1);
                armAnim.Play("Draw", 1);
            }
            else
            {
                armAnim.SetLayerWeight(1, 0);
                armAnim.Play("Draw");
            }
        }
        else
        {
            grenade.gameObject.SetActive(true);
            armAnim.SetLayerWeight(1, 0);
            armAnim.SetLayerWeight(2, 1);
            armAnim.Play("Draw", 2);
        }
    }

    public void Shoot(bool isAiming)
    {
        if (!isReload)
        {
            if (!gun.isShooting)
            {
                if (gun.isRifle)
                    armAnim.SetLayerWeight(1, 1f);
                else
                    armAnim.SetLayerWeight(1, 0f);

                armAnim.SetLayerWeight(2, 0f);

                if (gun.IsLeftAmmo())
                {
                    if (isAiming)
                        armAnim.SetTrigger("Shoot");
                    else
                    {
                        if (gun.isRifle)
                            armAnim.Play("Fire", 1);
                        else
                            armAnim.Play("Fire");
                    }

                    gun.Shoot();
                }
                else
                {
                    //총알이 없을 떄
                }
            }
        }
    }

    public void Reload()
    {

        if (!grenade.gameObject.activeSelf)
        {
            if (gun.gunSettings.maxAmmo > 0)
            {
                if (!isReload)
                {
                    isReload = true;

                    if (gun.isRifle)
                        armAnim.SetLayerWeight(1, 1f);
                    else
                        armAnim.SetLayerWeight(1, 0f);

                    armAnim.SetLayerWeight(2, 0f);


                    armAnim.SetTrigger("Reload");
                    gun.Reload();

                    StartCoroutine(ReloadDelay());
                }
            }
        }
    }

    IEnumerator ReloadDelay()
    {
        yield return new WaitForSeconds(3.15f);
        isReload = false;
    }

    public void Grenade()
    {
        if (!grenade.isThrow && grenade.cur > 0)
        {
            armAnim.SetLayerWeight(1, 0f);
            armAnim.SetLayerWeight(2, 1f);

            armAnim.SetTrigger("Throw");

            grenade.Throw();
        }
    }
}