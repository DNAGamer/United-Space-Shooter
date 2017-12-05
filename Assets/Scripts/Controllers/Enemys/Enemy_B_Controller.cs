using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_B_Controller : MonoBehaviour {

    public float maxLeft;
    public float maxRight;
    public float minReload;
    public float maxReload;
    public float speed;
    public GameObject rocket;

    private int health;
    public float spawnLocation;
    public float reload;


	void Awake () {
        spawnLocation = Random.Range(maxLeft, maxRight);
        transform.position = new Vector2(spawnLocation, transform.position.y);
        reload = Random.Range(minReload, maxReload);
        StartCoroutine(Reloader(reload));
    }

	void FixedUpdate () {
        if (transform.position.x != spawnLocation || transform.position.y != 2)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(spawnLocation, 2), step);
        }
    }


    IEnumerator Reloader(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            if (playerController.dead == false && GetComponent<Renderer>().enabled == true && transform.position.x == spawnLocation && transform.position.y == 2)
            {
                GameObject rocketController = Instantiate(rocket, new Vector3(transform.position.x, (transform.position.y) - 1f, -0.05f), Quaternion.Euler(0, 0, -90));
            }
        }
    }
}
