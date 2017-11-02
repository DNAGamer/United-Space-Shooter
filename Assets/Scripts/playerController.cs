using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
    public float speed;
    public float HSpeed;
    public float VSpeed;
    public float directionModifier;
    public GameObject pew;
    public GameObject enemyA;
    public string DEBUG;

    public int defaultEnemyDamage;
    public int defaultBulletDamage;

    public int lives;
    public int health;
    public float spawnX;
    public float spawnY;
    public float maxBullets;

    public int fireRateMultiplier;
    public int fireDamageMultiplier;
    public int movementMultiplier;
    private bool canAct;
    private bool invincible;

    private Renderer rend;
    private Rigidbody2D rb2d;

	void Start () {
        
        rb2d = GetComponent<Rigidbody2D>();
        rend = GetComponent<Renderer>();
        fireRateMultiplier = 1;
        fireDamageMultiplier = 1;
        fireRateMultiplier = 1;
        movementMultiplier = 1;
        canAct = false;
        Respawn();
	}

    void Update()
    {
        if (!canAct)
        {
            rend.enabled = false;
        }else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                float rounds = GameObject.FindGameObjectsWithTag("bullet").Length;
                if (rounds <= (maxBullets * fireRateMultiplier))
                {
                    GameObject bullet = Instantiate(pew, new Vector2(transform.position.x, (transform.position.y) +0.8f ), Quaternion.identity);
                    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10f);

                }
            }
            }
    }

	void FixedUpdate ()
        {
        if (canAct)
        {
            float moveHorizontal = (Input.GetAxis("Horizontal") * HSpeed) * movementMultiplier;
            float moveVertical = (Input.GetAxis("Vertical") * VSpeed) * movementMultiplier;

            if (moveVertical != 0 || moveHorizontal != 0)
                rb2d.gravityScale = 0;
            else
                rb2d.gravityScale = 4;  
            if (moveVertical <= 0)
            {
                moveVertical = moveVertical * directionModifier;
            }

            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rb2d.velocity = movement;
        }
        if (GameObject.FindGameObjectsWithTag("enemy").Length == 0){
            GameObject enemy = Instantiate(enemyA, new Vector2(-4.55f, 4f), Quaternion.identity);
            enemy.GetComponent<Renderer>().enabled = false;
            enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            enemy.GetComponent<Rigidbody2D>().gravityScale = 0;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DEBUG = "TOUCHING";
        if (other.gameObject.CompareTag("enemy"))
            Destroy(other.gameObject);
            Damage(other.gameObject.name);

    }

    void Damage(string hostile)
    {
        if (!invincible)
        {
            health = health - defaultEnemyDamage;
            if (health <= 0)
            {
                if (lives <= 0)
                {/*gameover*/}
                else
                {
                    Respawn();
                    health = 100;
                    lives--;
                }
            }
            else
            {
                StartCoroutine(blink());
            }
        }
       }

    void Respawn() {
        canAct = false;
        rb2d.velocity = new Vector2(0,0);
        transform.position = new Vector3(spawnY, spawnX, -0.1F);
        StartCoroutine(sleep(2));
        
    }


    IEnumerator sleep(float delay)
    {
        yield return new WaitForSeconds(delay);
        canAct = true;
        rend.enabled = true;
    }

    IEnumerator blink()
    {
        invincible = true;
        for (int i = 0; i < 4; i++)
        {
            rend.enabled = false;
            yield return new WaitForSeconds(0.2f);
            rend.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
        invincible = false;
    }
}
    
