using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HandGunBulletMng : MonoBehaviour {

    public Text txt;
    

    public void HandGunBulletReset(string Bullet)
    {
        txt.text = Bullet;
    }

    public void HandGunBulletChange(string Bullet)
    {
        txt.text = Bullet;
    }
}
