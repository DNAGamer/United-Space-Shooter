using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_A_Controller : MonoBehaviour {

    public float health;
    public float lapTime;
    public float maxLeft;
    public float MaxRight;
    public float reload;
    public GameObject bullet;

    private bool MasterControl;
    private Vector2 P1;
    private Vector2 P2;

    void Awake() {
        P1 = new Vector2(maxLeft, transform.position.y);
        P2 = new Vector2(MaxRight, transform.position.y);
        if (gameObject.name == "fleetCommander")
            MasterControl = true;
        else if (gameObject.name.Contains("formationEnemy"))
        {
            MasterControl = false;

            var FCom = GameObject.FindGameObjectWithTag("fleetCommander").GetComponent<Enemy_A_Controller>(); // Grab all the relivent var from the master controller
            health = FCom.health;
            reload = FCom.reload;
            bullet = FCom.bullet;
            StartCoroutine(Reloader(reload)); // shoot

        }
    }

    // Update is called once per frame
    void Update() {
        if (MasterControl)
            masterControlUpdate();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.name == "fleetCommander")
            return;
        if (other.gameObject.tag == "bullet")
        {
            Destroy(gameObject);
        }
    }

    void masterControlUpdate()
    {
        if (GameObject.FindGameObjectsWithTag("fleetMember").Length == 0)
        {
            Destroy(gameObject);
        }
        {
            transform.position = Vector3.Lerp(P1, P2, Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.time / lapTime, 1f)));
        }
    }

    IEnumerator Reloader(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(delay, (delay + 0.5f)));
            if (playerController.dead == false && GetComponent<Renderer>().enabled == true)
            {
                GameObject bulletController = Instantiate(bullet, new Vector3(transform.position.x, (transform.position.y) - 1f, -0.05f), Quaternion.Euler(0, 0, 0)); //shoved the bullet into a var in case i want to do some fancy shit with bullets
            }
        }
    }
    
}
