using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketController : MonoBehaviour {

    public float rotationSpeed;
    public float speed;
    public float startTime = 0;
    public float debugTime;

    void Start () {

    }

    void Update () {
        float time = Time.timeSinceLevelLoad - startTime;
        debugTime = time;

        if ((time) > 6f)
        {
            Destroy(gameObject);
        }
            var player = GameObject.Find("player");
        if (player.GetComponent<Renderer>().enabled == false)
            Destroy(gameObject);

        Vector3 vectorToTarget = player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);

        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
    }
}
