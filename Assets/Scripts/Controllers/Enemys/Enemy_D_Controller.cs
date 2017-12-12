using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_D_Controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (playerController.dead)
            Destroy(gameObject);

    }
}
