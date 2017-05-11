using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReboundTextMng : MonoBehaviour {

    public Text txt;


    public void ReboundReset(string Rebound)
    {
        txt.text = Rebound;
    }

    public void ReboundChange(string Rebound)
    {
        txt.text = Rebound;
    }
    public void NullRebound()
    {
        txt.text = "";
    }
}