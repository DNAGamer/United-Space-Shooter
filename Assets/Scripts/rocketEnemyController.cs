using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class rocketEnemyController : MonoBehaviour {

    public float maxLeft;
    public float maxRight;
    public float minReload;
    public float maxReload;
    public float speed;
    public GameObject rocket;

    private int health;
    private float spawnLocation;
    private float reload;
    private bool shot;

	void start () {
        health = 100;
        shot = false;
	}
	
    void Update()
    {
        
    }

	void FixedUpdate () {
        if (spawnLocation == 0)
        {
            spawnLocation = Random.Range(maxLeft, maxRight);
            transform.position = new Vector2(spawnLocation, transform.position.y);
        }
        if (reload == 0)
            reload = Random.Range(minReload, maxReload);
        if (transform.position.x != spawnLocation || transform.position.y != 2)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(spawnLocation, 2), step);
        }
        if (GetComponent<Renderer>().enabled == true && shot == false && transform.position.x == spawnLocation && transform.position.y == 2)
            InvokeRepeating("Shoot", 0f, reload);
    }

    void Shoot()
    {
        float rounds = GameObject.FindGameObjectsWithTag("rocket").Length;
        var playerC = FindObjectOfType(typeof(playerController));
        if (rounds == 0 && playerController.dead == false)
        {
            GameObject rocketController = Instantiate(rocket, new Vector3(transform.position.x, (transform.position.y) - 1f, -0.05f), Quaternion.Euler(0, 0, -90));
        }
        }
}
