using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageTextMng : MonoBehaviour {

    public Text txt;


    public void DamageReset(string Damage)
    {
        txt.text = Damage;
    }

    public void DamageChange(string Damage)
    {
        txt.text = Damage;
    }
    public void NullDamage()
    {
        txt.text = "";
    }
}
