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

    private void Start()
    {
        instance = this;

        stage = 1;
        time = 40;
        score = 0;

        Spawn();
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

            Spawn();

            GameMng.GetInstance.StageAni();
        }

        GameMng.GetInstance.StageTime.text = ((int)time).ToString();
        GameMng.GetInstance.StageShow.text = "STAGE " + stage.ToString();
        GameMng.GetInstance.HPText.text = Player.GetInstance.health.ToString();
        GameMng.GetInstance.MoneyText.text = Player.GetInstance.cash.ToString();
    }

    private void Spawn()
    {
        for (int i = 0; i < 5 * stage; i++)
        {
            spawnManager.ZSpawn(stage);
        }
        spawnManager.ISpawn();
    }
}