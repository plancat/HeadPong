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

    public GunBase[] guns;
    public int gunsIdx = 0;

    bool isReload = false;

    public void SwapWeapon(bool isGrenade)
    {
        for (int i = 0; i < guns.Length; i++)
            guns[i].gameObject.SetActive(false);

        grenade.gameObject.SetActive(false);

        if (!isGrenade)
        {
            if (gunsIdx == 0)
                gunsIdx = 1;
            else
                gunsIdx = 0;

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

    IEnumerator ReloadDelay()
    {
        yield return new WaitForSeconds(gun.reloadSettings.enableBulletTime);
        isReload = false;
    }

    public void Grenade()
    {
        armAnim.SetLayerWeight(1, 0f);
        armAnim.SetLayerWeight(2, 1f);

        armAnim.SetTrigger("Throw");

        grenade.Throw();
    }
}