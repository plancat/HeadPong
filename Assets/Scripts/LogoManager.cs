using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogoManager : MonoBehaviour {

    public Image kwcLogoImage;
    private bool isFadeOut = false;

    private void Awake()
    {
        isFadeOut = false;
        var white = Color.white;
        white.a = 0.0f;

        kwcLogoImage.color = white;

        StartCoroutine("LogoFade");
    }

    IEnumerator LogoFade()
    {
        while (true)
        {
            if (!isFadeOut)
            {
                var color = kwcLogoImage.color;
                color.a += 1.0f * Time.deltaTime;

                if (color.a >= 0.9f)
                    isFadeOut = true;

                kwcLogoImage.color = color;
            }
            else
            {
                var color = kwcLogoImage.color;
                color.a -= 1.0f * Time.deltaTime;

                if (color.a <= 0.1f) {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
                }

                kwcLogoImage.color = color;
            }

            yield return null;
        }
    }
}