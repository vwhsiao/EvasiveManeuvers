using UnityEngine;
using System.Collections;

using System;

public class waveSpawner : MonoBehaviour
{
    private GameObject projectile;
    private GameManager gameManager;

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform Projectile;
        public int count;
        public float rate;

    }
    public Transform[] RandomSpawnPoints;
    public Wave[] waves;
    private int nextWave = 0;

    public int NextWave
    {
        get { return nextWave + 1; }
    }

    public float timeBetweenWaves = 5f;
    public float waveCountDown;
    public float WaveCountDown
    {
        get { return waveCountDown; }
    }

    public float searchCountDown = 1f;
    public SpawnState state = SpawnState.COUNTING;
    public SpawnState State
    {
        get { return state; }
    }

    void Start()
    {
        waveCountDown = timeBetweenWaves;

    }

    void Update()
    {
        if (state == SpawnState.WAITING)
        {
           if(!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountDown <= 0)
        {
            if(state != SpawnState.SPAWNING )

            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");


        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETE! Looping...");

        }
        else
        {
            nextWave++;
        }

    }

    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0f)
        {
            Debug.Log("setting search to 1");

            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                Debug.Log("all enemies dead");
                return false;
            }

        }
        return true;

    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);

        state = SpawnState.SPAWNING;
        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.Projectile);
            Debug.Log("PROJECT IS AT:" + RandomSpawnPoints.GetValue(1));
            yield return new WaitForSeconds(1f / _wave.rate);
        }
        state = SpawnState.WAITING;

        yield break;
    }
    void SpawnEnemy(Transform _Projectile)
    {
 
        Transform _sp = RandomSpawnPoints[UnityEngine.Random.Range(0, RandomSpawnPoints.Length)];
        Instantiate(_Projectile, _sp.position, _sp.rotation);
    }
}
