using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialScene : MonoBehaviour
{
    public Text time;
    public float timeScale;

    private void Awake()
    {
        timeScale = 7f;

    }

    private void Update()
    {
        timeScale -= Time.deltaTime;
        if (timeScale <= 0)
        {
            SceneManager.LoadScene("Demo");
        }
        time.text = ((int)timeScale).ToString();
    }
}