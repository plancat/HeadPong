using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GrenadeMaxBulletMng : MonoBehaviour
{
    public Text txt;


    public void GrenadeMaxBulletReset(string Bullet)
    {
        txt.text = Bullet;
    }

    public void GrenadeMaxBulletChange(string Bullet)
    {
        txt.text = Bullet;
    }
}
