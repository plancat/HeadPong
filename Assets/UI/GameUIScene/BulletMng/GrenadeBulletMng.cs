using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GrenadeBulletMng : MonoBehaviour
{
    public Text txt;


    public void GrenadeBulletReset(string Bullet)
    {
        txt.text = Bullet;
    }

    public void GrenadeBulletChange(string Bullet)
    {
        txt.text = Bullet;
    }
}