using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CostTextMng : MonoBehaviour {

    public Text txt;


    public void CostReset(string Cost)
    {
        txt.text = "$ " + Cost;
    }

    public void CostChange(string Cost)
    {
        txt.text = "$ " + Cost;
    }

    public void NullCost()
    {
        txt.text = "$ ";
    }
}
