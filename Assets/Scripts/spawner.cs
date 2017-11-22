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
    static public Text killText;
    static public int kills;

    ScoreClass score = new ScoreClass();
    SpawnClass spawn = new SpawnClass();

    void Start()
    {
        StartCoroutine(spawnController());
    }

    void FixedUpdate()
    {
    }

    IEnumerator spawnController()
    {
        while (true)
        {
            if (playerController.dead == false && GameObject.Find("player").GetComponent<Renderer>().enabled == true)
            {
                spawn.EnemyA();
                yield return new WaitForSeconds(20);
            }
        }
    }
}

public class SpawnClass
{
    public spawner spawnClone;
    public spawner core;

    private ScoreClass Score = new ScoreClass();
    private GameObject enemyA;
    private GameObject enemyB;
    private GameObject enemyC;
    private GameObject enemyD;

    public void EnemyA()
    {
        core = GameObject.Instantiate(spawnClone) as spawner;

        enemyA = core.enemyA;
        GameObject enemy = GameObject.Instantiate(enemyA, new Vector3(0, 0, 0), Quaternion.identity);
        Score.setScore(Score.getScore() + 1);
    }

    public void EnemyB()
    {
        core = GameObject.Instantiate(spawnClone) as spawner;

        enemyB = core.enemyB;
        GameObject enemy = GameObject.Instantiate(enemyB, new Vector3(0, 0, 0), Quaternion.identity);
        Score.setScore(Score.getScore() + 1);
    }

    public void EnemyC()
    {
        core = GameObject.Instantiate(spawnClone) as spawner;

        enemyC = core.enemyC;
        GameObject enemy = GameObject.Instantiate(enemyA, new Vector3(-4.55f, 4f, -0.5f), Quaternion.identity);
        enemy.GetComponent<Renderer>().enabled = false;
        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        enemy.GetComponent<Rigidbody2D>().gravityScale = 0;
        Score.setScore(Score.getScore() + 1);
    }

    public void EnemyD()
    {
        GameObject enemy = GameObject.Instantiate(enemyC, new Vector3(0, 0, 0), Quaternion.identity);
        Score.setScore(Score.getScore() + 1);
    }
}


public class ScoreClass
{

    private int kills;
    private Text killText;

    public int getScore()
    {
        return (kills);
    }

    public string getKillText()
    {
        return (killText.text);
    }

    public void setScore(int points)
    {
        kills = points;
        setKillText(points);
    }

    public void setKillText(int points)
    {
        killText.text = "Kills: " + points;
    }
}
