using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager GetInstance
    {
        get
        {
            return instance;
        }
    }

    public SpawnManager spawnManager;
    
    public int stage;
    public int score;
    public float time;

    private void Awake()
    {
        instance = this;

        stage = 0;
        time = 40;
        score = 0;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    private void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            time = 40;

            stage += 1;
        }
    }

    public void StageStart()
    {

    }

    public void StageEnd()
    {

    }
}