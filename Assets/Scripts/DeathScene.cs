using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScene : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ReplayButton()
    {
        SceneManager.LoadScene("Demo");
    }

    public void MainButton()
    {
        SceneManager.LoadScene("MainScene");
    }
}