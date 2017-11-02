using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotScript : MonoBehaviour {
    public int speed;
    public int damage;
    public float deathY;


    void Start () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
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
        if (other.tag != "Player" && other.tag != "wall")
        {
            if (other.gameObject.GetComponent<Renderer>().enabled == true)
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
