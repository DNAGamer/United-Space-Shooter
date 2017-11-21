using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_C_Controller : MonoBehaviour
{

    public float startPosition;
    public float endPosition;
    public float timePerLap;
    public float dropTime;
    public float debugTime;
    public float shootDelay;
    public GameObject pew;

    private Vector2 P1;
    private Vector2 P2;
    private bool canAct;
    private Renderer rend;
    public float startTime = 0;
    private bool shoot = false;


    void Awake()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        P1 = new Vector2(startPosition, 4);
        P2 = new Vector2(endPosition, 4);
        canAct = false;
        Respawn();
        dropTime = Random.Range(0, dropTime);
        timePerLap = Random.Range(0.5f, timePerLap);
        shootDelay = Random.Range(0.2f, shootDelay);
    }

    void FixedUpdate()
    {
        if (GetComponent<Rigidbody2D>().gravityScale == 0 && rend.enabled == true)
        {
            float time = Time.timeSinceLevelLoad - startTime;
            debugTime = time;
            if ((time) > dropTime)
            {
                GetComponent<Rigidbody2D>().gravityScale = 2;
            }
        }
        var playerC = FindObjectOfType(typeof(playerController));
        if (transform.position.y < -6 || playerController.dead == true)
        {
            Destroy(gameObject);
        }
            

        transform.position = Vector3.Lerp(P1, P2, Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.time / timePerLap, 1f)));
        if (!shoot && rend.enabled == true)
        {
            InvokeRepeating("Shoot", 0f, shootDelay);
            shoot = true;
        }

        if (canAct)
        {
            rend.enabled = true;
        }else
        {
            rend.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<playerController>().Damage(50);
            Destroy(gameObject);
        }
    }

    void Respawn()
    {
        canAct = false;
        transform.position = new Vector3(startPosition, transform.position.y, transform.position.z);
        StartCoroutine(sleep(timePerLap));
        
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(pew, new Vector3(transform.position.x, (transform.position.y) - 1f, -0.05f), Quaternion.identity);

        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -10f);
    }
        

    IEnumerator sleep(float delay)
    {
        yield return new WaitForSeconds(delay);
        canAct = true;
        startTime = Time.timeSinceLevelLoad;
    }
}
