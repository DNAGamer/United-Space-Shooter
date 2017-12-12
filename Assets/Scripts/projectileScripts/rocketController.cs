using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketController : MonoBehaviour {

    public float rotationSpeed;
    public float speed;

    public AudioClip missile1;
    public AudioClip missile2;
    public AudioClip missile3;

    private AudioSource Audio;

    void Start () {
        Audio = GetComponent<AudioSource>();
        int sound = 3;
        switch (sound)
        {
            case 1:
                Audio.PlayOneShot(missile1);
                break;
            case 2:
                Audio.PlayOneShot(missile2);
                break;
            case 3:
                Audio.PlayOneShot(missile3);
                break;
        }
        Destroy(gameObject, 4f);
        
    }

    void Update () {
        var player = GameObject.Find("player");
        if (playerController.dead == true || player.GetComponent<Renderer>().enabled == false)
            Destroy(gameObject);

        Vector3 vectorToTarget = player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);

        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<playerController>().Damage(20);
            Destroy(gameObject);
        }
    }

}
