using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoneyTextMng : MonoBehaviour
{

    public Text txt;


    public void MoneyReset(string Money)
    {
        txt.text = Money;
    }

    public void MoneyChange(string Money)
    {
        txt.text = Money;
    }
}
