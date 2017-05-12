using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainSceneMng : MonoBehaviour
{
    public GameObject SettingCanvas;
    public GameObject CreditCanvas;
    public GameObject MainCanvas;

    public GameObject[] MainSlot;

    public Camera mainCamera;

    int PosOfMouse;

    int OpenPage;

    // Use this for initialization
    void Start()
    {
        OpenPage = 0;
        PosOfMouse = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (OpenPage != 0)
            {
                if (OpenPage.Equals(1))
                {
                    SettingCanvas.SetActive(false);
                }
                else if (OpenPage.Equals(2))
                {
                    CreditCanvas.SetActive(false);
                }
                MainCanvas.SetActive(true);
                OpenPage = 0;
            }

        }


        Vector2 wp = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Ray2D ray = new Ray2D(wp, Vector2.zero);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("MainSlot"))
            {
                if (hit.collider.name == "GameStart")
                {
                    if (PosOfMouse != 0)
                    {
                        ChangeScale(0);
                        PosOfMouse = 0;
                    }
                }
                else if (hit.collider.name == "Setting")
                {
                    if (PosOfMouse != 1)
                    {
                        ChangeScale(1);
                        PosOfMouse = 1;
                    }
                }
                else if (hit.collider.name == "Credit")
                {
                    if (PosOfMouse != 2)
                    {
                        ChangeScale(2);
                        PosOfMouse = 2;
                    }
                }
                else if (hit.collider.name == "EXIT")
                {
                    if (PosOfMouse != 3)
                    {
                        ChangeScale(3);
                        PosOfMouse = 3;
                    }
                }
            }
        }
        else
        {
            if (PosOfMouse != 4)
            {
                ChangeScale(4);
                PosOfMouse = 4;
            }
        }
    }
    public void SettingBt()
    {
        OpenPage = 1;
    }
    public void CreditBt()
    {
        OpenPage = 2;
    }

    public void ChangeScale(int j)
    {
        for(int i = 0; i<4;i++)
        {
            MainSlot[i].transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (j != 4)
            MainSlot[j].transform.localScale = new Vector3(0.7f, 0.7f, 1f);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}