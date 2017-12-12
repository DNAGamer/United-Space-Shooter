using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotScript : MonoBehaviour
{
    public int speed;
    public int damage;
    public float deathY;

    public AudioClip Plasma1;
    public AudioClip Plasma2;
    public AudioClip Plasma3;
    public AudioClip Plasma4;

    private AudioSource Audio;

    void Awake()
    {
        Audio = GetComponent<AudioSource>();
        int sound = Random.Range(1, 4);
        switch (sound)
        {
            case 1:
                Audio.PlayOneShot(Plasma1);
                break;
            case 2:
                Audio.PlayOneShot(Plasma2);
                break;
            case 3:
                Audio.PlayOneShot(Plasma3);
                break;
            case 4:
                Audio.PlayOneShot(Plasma4);
                break;
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, (speed));
    }

    void FixedUpdate()
    {
        if (GetComponent<Rigidbody2D>().position.y < deathY)
        {
            Object.Destroy(gameObject);
        }
        if (GameObject.Find("player").GetComponent<Renderer>().enabled == false)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.gameObject.GetComponent<Renderer>().enabled == true)
            {
                other.GetComponent<playerController>().Damage(10);
                Destroy(gameObject);
            }
        }
    }
}
