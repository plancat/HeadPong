using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageTextMng : MonoBehaviour {

    public Text txt;


    public void StageReset(string Stage)
    {
        txt.text = "Stage" + Stage;
    }

    public void StageChange(string Stage)
    {
        txt.text = "Stage" +Stage;
    }
}
