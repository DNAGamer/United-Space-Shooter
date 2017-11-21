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

    public float kills;

    public Text killText;

    void Start()
    {

    }

    void FixedUpdate()
    {

        //EnemyA
        if (GameObject.FindGameObjectsWithTag("EnemyA").Length == 0)
        {
            GameObject enemy = Instantiate(enemyA, new Vector3(0, 0, 0), Quaternion.identity);
            kills++;
            killText.text = "Kills: " + kills;
        }

        //EnemyB
        if (GameObject.FindGameObjectsWithTag("EnemyB").Length == 0)
        {
            GameObject enemy = Instantiate(enemyB, new Vector3(0, 0, 0), Quaternion.identity);
            kills++;
            killText.text = "Kills: " + kills;
        }

        //EnemyC
        if (GameObject.FindGameObjectsWithTag("EnemyC").Length == 0)
        {
            GameObject enemy = Instantiate(enemyC, new Vector3(0, 0, 0), Quaternion.identity);
            kills++;
            killText.text = "Kills: " + kills;
        }

        //EnemyD
        if (GameObject.FindGameObjectsWithTag("enemyRocket").Length == 0)
        {
            GameObject enemy = Instantiate(enemyD, new Vector3(0, 10f, -0.5f), Quaternion.identity);
            killText.text = "Kills: " + kills;
        }
        if (GameObject.FindGameObjectsWithTag("enemy").Length == 0)
        {
            GameObject enemy = Instantiate(enemyA, new Vector3(-4.55f, 4f, -0.5f), Quaternion.identity);
            enemy.GetComponent<Renderer>().enabled = false;
            enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            enemy.GetComponent<Rigidbody2D>().gravityScale = 0;
            kills++;
            if (kills != -1 || kills != 0)
            {
                killText.text = "Kills: " + kills;
            }
        }


        
    }
}
