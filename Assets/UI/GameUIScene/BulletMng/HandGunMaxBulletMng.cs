using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HandGunMaxBulletMng : MonoBehaviour {

    public Text txt;
    

    public void HandGunMaxBulletReset(string Bullet)
    {
        txt.text = Bullet;
    }

    public void HandGunMaxBulletChange(string Bullet)
    {
        txt.text = Bullet;
    }
}
