using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HPTextMng : MonoBehaviour
{

    public Text txt;
    

    public void HPReset(string Hp)
    {
        txt.text = Hp;
    }

    public void HPChange(string Hp)
    {
        txt.text = Hp;
    }
}
