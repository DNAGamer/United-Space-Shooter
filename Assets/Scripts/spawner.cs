using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class spawner : MonoBehaviour
{
    public GameObject enemyA;
    public GameObject enemyB;
    public GameObject enemyC;
    public GameObject enemyD;


    void Start()
    {
        StartCoroutine(spawnController());
    }

    void FixedUpdate()
    {
    }

    public void EnemyA()
    {
        Debug.Log(Time.timeSinceLevelLoad + "|| Spawned EnemyA");
        GameObject enemy = GameObject.Instantiate(enemyA, new Vector3(Random.Range(-46, 46)/10, Random.Range(49, 0)/100, 0), Quaternion.identity);
    }

    public void EnemyB()
    {
        Debug.Log(Time.timeSinceLevelLoad + "|| Spawned EnemyB");

        GameObject enemy = GameObject.Instantiate(enemyB, new Vector3(0, 5, 0), Quaternion.identity);
    }

    public void EnemyC()
    {
        Debug.Log(Time.timeSinceLevelLoad + "|| Spawned EnemyC");

        GameObject enemy = GameObject.Instantiate(enemyC, new Vector3(-4.55f, 4f, -0.5f), Quaternion.identity);
        enemy.GetComponent<Renderer>().enabled = false;
        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        enemy.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    public void EnemyD()
    {
        Debug.Log(Time.timeSinceLevelLoad + "|| Spawned EnemyD");

        GameObject enemy = GameObject.Instantiate(enemyD, new Vector3(0, 0, 0), Quaternion.identity);
    }

    IEnumerator spawnController()
    {
        yield return new WaitForSeconds(2);
        while (true)
        {
            if (playerController.dead == false && GameObject.Find("player").GetComponent<Renderer>().enabled == true)
            {
                EnemyA();
                yield return new WaitForSeconds(3);
                EnemyB();
                yield return new WaitForSeconds(3);
                EnemyA();
                yield return new WaitForSeconds(3);
                EnemyC();
                yield return new WaitForSeconds(0.01f);
                EnemyB();
                yield return new WaitForSeconds(0.01f);
                EnemyB();
                yield return new WaitForSeconds(0.01f);
                EnemyB();
                yield return new WaitForSeconds(5);
            }
        }
    }
}

