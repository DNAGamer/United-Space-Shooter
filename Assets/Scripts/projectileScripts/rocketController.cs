using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketController : MonoBehaviour {

    public float rotationSpeed;
    public float speed;

    void Start () {
        Destroy(gameObject, 6f);
        
    }

    void Update () {
        var player = GameObject.Find("player");
        var playerC = FindObjectOfType(typeof(playerController));
        if (playerController.dead == true)
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
