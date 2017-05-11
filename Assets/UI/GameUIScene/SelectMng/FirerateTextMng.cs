using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FirerateTextMng : MonoBehaviour
{
    public Text txt;


    public void FirerateReset(string Firerate)
    {
        txt.text = Firerate;
    }

    public void FirerateChange(string Firerate)
    {
        txt.text = Firerate;
    }
    public void NullFirerate()
    {
        txt.text = "";
    }
}