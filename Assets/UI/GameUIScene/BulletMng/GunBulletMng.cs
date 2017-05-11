using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GunBulletMng : MonoBehaviour
{

    public Text txt;


    public void GunBulletReset(string Bullet)
    {
        txt.text = Bullet;
    }

    public void GunBulletChange(string Bullet)
    {
        txt.text = Bullet;
    }
}
