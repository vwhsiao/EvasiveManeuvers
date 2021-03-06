﻿using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUps;
    public float timerMaxWaitTime;
    public float timerRate;
    private GameObject player;
    private float xRange;
    private float yRange;

    private float timer;

    void Start()
    {
        player = GameObject.Find("Player");
        xRange = 23/2;
        yRange = 14/2;
    }

    void Update()
    {
        if (!GameManager.instance.playing)
            return;
        
        timer -= timerRate;
        if (timer <= 0)
        {
            timer = timerMaxWaitTime;
            Spawn();
        }
    }

    private void Spawn()
    {
        int randint = Random.Range(0, powerUps.Length);
        float randX = Random.Range(-xRange, xRange);
        float randY = Random.Range(-yRange, yRange);
        while (player.GetComponent<Collider2D>().bounds.Contains(new Vector3(randX, randY, 0)))
        {
            randX = Random.Range(-xRange, xRange);
            randY = Random.Range(-yRange, yRange);
        }
        GameObject chosen = powerUps[randint];
//        GameObject powerUp = Instantiate(chosen, new Vector3(randX, randY, 0), chosen.transform.rotation) as GameObject;
        Instantiate(chosen, new Vector3(randX, randY, 0), chosen.transform.rotation);
    }
}
