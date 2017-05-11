using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmmoTextMng : MonoBehaviour {

    public Text txt;


    public void AmmoReset(string Ammo,string MaxAmmo)
    {
        txt.text = Ammo+ "/" + MaxAmmo;
    }

    public void AmmoChange(string Ammo, string MaxAmmo)
    {
        txt.text = Ammo + "/" + MaxAmmo;
    }

    public void NullAmmo()
    {
        txt.text = "  /  ";
    }
}
