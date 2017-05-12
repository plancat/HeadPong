using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GunMaxBulletMng : MonoBehaviour
{
    public Text txt;

    public void GunMaxBulletReset(string Bullet)
    {
        txt.text = Bullet;
    }

    public void GunMaxBulletChange(string Bullet)
    {
        txt.text = Bullet;
    }
}
