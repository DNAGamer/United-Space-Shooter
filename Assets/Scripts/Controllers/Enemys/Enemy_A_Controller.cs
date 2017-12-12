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

    public bool MasterControl;
    private Vector2 P1;
    private Vector2 P2;

    void Awake() {
        lapTime = Random.Range(1, lapTime);
        reload = Random.Range(reload * 0.5f, reload * 1.5f);
        P1 = new Vector2(maxLeft, transform.position.y);
        P2 = new Vector2(MaxRight, transform.position.y);
        if (gameObject.name.Contains("fleetCommander"))
            MasterControl = true;
        else if (gameObject.name.Contains("formationEnemy"))
        {
            MasterControl = false;
            var FCom = transform.parent.GetComponent<Enemy_A_Controller>();
            health = FCom.health;
            reload = FCom.reload;
            bullet = FCom.bullet;
            StartCoroutine(Reloader(reload)); // shoot

        }
    }

    // Update is called once per frame
    void Update() {
        if (playerController.dead)
            Destroy(gameObject);
        if (MasterControl)
            masterControlUpdate();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.name.Contains("fleetCommander"))
            return;
        if (other.gameObject.tag == "bullet")
        {
            Debug.Log(gameObject.name + "Died");
            Destroy(gameObject);
        }
    }

    void masterControlUpdate()
    {
        int children = 0;
        foreach (Transform child in transform)
            children++;
        if (children == 0)
            Destroy(gameObject);
        else{
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
