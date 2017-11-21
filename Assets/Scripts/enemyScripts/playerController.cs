using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour {
    public float speed;
    public float HSpeed;
    public float VSpeed;
    public float directionModifier;
    public static bool dead;
    
    public Text healthText;
    public Text livesText;
    public Text DeathText;
    public Text kills;
    public Text Controls;
    public int kill = -1;
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
    private bool gameOver;
    private GameObject enemy;
    public GameObject background;
    public GameObject pew;
    public GameObject enemyA;
    public GameObject enemyD;
    public GameObject bonusDamge;
    public GameObject bonusFireRate;
    public GameObject bonusHealth;
    public GameObject bonusLife;
    public GameObject bonusSpeed;
 

    private Renderer rend;
    private Rigidbody2D rb2d;

	void Start () {
        dead = false;
        gameOver = false;
        healthText.text = "Health: " + health;
        livesText.text = "Lives: " + lives;
        kills.text = "Kills: ";
        DeathText.text = "";
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
        if (gameOver)
            canAct = false;
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
                    GameObject bullet = Instantiate(pew, new Vector3(transform.position.x, (transform.position.y) + 0.8f, -0.05f), Quaternion.identity);
                    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10f);
                }
                   
            }
            }
    }

    void FixedUpdate()
    {
        healthText.text = "Health: " + health;
        livesText.text = "Lives: " + lives;
        if (gameOver) {
            background.gameObject.GetComponent<Renderer>().enabled = false;
            healthText.text = "";
            livesText.text = "";
            Controls.text = "";
            kills.color = Color.red;
            Destroy(gameObject);
            return;
        }
        
        if (canAct)
        {
            float moveHorizontal = (Input.GetAxis("Horizontal") * HSpeed) * movementMultiplier;
            float moveVertical = (Input.GetAxis("Vertical") * VSpeed) * movementMultiplier;

            if (moveVertical != 0 || moveHorizontal != 0)
            {
                rb2d.gravityScale = 0;
                rb2d.velocity = new Vector2(0, 0);
            }
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
            enemy = Instantiate(enemyA, new Vector3(-4.55f, 4f, -0.5f), Quaternion.identity);
            enemy.GetComponent<Renderer>().enabled = false;
            enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            enemy.GetComponent<Rigidbody2D>().gravityScale = 0;
            kill++;
            if (kill != -1 || kill != 0)
            {
                kills.text = "Kills: " + kill;
            }
        if (GameObject.FindGameObjectsWithTag("enemyRocket").Length == 0)
            {
                enemy = Instantiate(enemyD, new Vector3(0, 10f, -0.5f), Quaternion.identity);
                kills.text = "Kills: " + kill;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other) 
    {
    if (other.gameObject.tag == ("bonus"))
        {
            DEBUG = other.gameObject.name;
            if (other.gameObject.name.Contains(bonusDamge.gameObject.name))
                fireDamageMultiplier++;
            if (other.gameObject.name.Contains(bonusFireRate.gameObject.name))
                fireRateMultiplier++;
            if (other.gameObject.name.Contains(bonusHealth.gameObject.name))
                health = 100;
            if (other.gameObject.name.Contains(bonusLife.gameObject.name))
                lives++;
            if (other.gameObject.name.Contains(bonusSpeed.gameObject.name))
                movementMultiplier++;
            Destroy(other.gameObject);
            StartCoroutine(blink());
        }

    }

    public void Damage(int damage)
    {
        if (!invincible)
            health = health - damage;
        if (health <= 0)
        {
            lives--;
            dead = true;
            if (lives == 0) //  GAME OVER
            {
                DeathText.text = "100% DED";
                dead = true;
                gameOver = true;
            } else // DIED 
            {
                DeathText.text = "DED";
                health = 100;
                Respawn();
            }
        }
    }

    void Respawn() {
        canAct = false;
        rb2d.velocity = new Vector2(0,0);
        transform.position = new Vector3(spawnY, spawnX, 0);
        StartCoroutine(sleep(2));
        
    }


    IEnumerator sleep(float delay)
    {
        yield return new WaitForSeconds(delay);
        canAct = true;
        rend.enabled = true;
        DeathText.text = "";
        dead = false;
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
    
