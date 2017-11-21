using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotScript : MonoBehaviour {
    public int speed;
    public int damage;
    
    public GameObject bonusLife;
    public GameObject bonusHealth;
    public GameObject bonusSpeed;
    public GameObject bonusDamage;
    public GameObject bonusFireRate;
    public float deathY;


    void Start () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
        GetComponent<Rigidbody2D>().transform.position = new Vector3(transform.position.x, transform.position.y, 0);
	}
	
	void OnBecomeInvisible() {
        Destroy(gameObject);
	}

    void FixedUpdate()
    {
        if (GetComponent<Rigidbody2D>().position.y > deathY)
        {
            Object.Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player" && other.tag != "wall" && other.tag != "bonus" && !other.name.Contains("Pew"))
        {
            if (other.gameObject.GetComponent<Renderer>().enabled == true)
            {
                if (other.gameObject.tag == "enemy" || other.gameObject.tag == "enemyRocket" || other.gameObject.tag == "rocket")
                {
                    //bonus item
                    int num = Random.Range(0, 100);
                    if (num >= 1 && num <= 20)  
                    {
                        num = Random.Range(1, 5);
                        if (num == 1)
                            Instantiate(bonusLife, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                        if (num == 2)
                            Instantiate(bonusHealth, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                        if (num == 3)
                            Instantiate(bonusSpeed, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                        if (num == 4)
                            Instantiate(bonusDamage, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                        if (num == 5)
                            Instantiate(bonusFireRate, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                    }
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                }
                }
                
            }
        }
    }

