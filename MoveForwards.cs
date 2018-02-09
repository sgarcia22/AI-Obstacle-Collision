using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwards : MonoBehaviour {

	private float speed; 
	private Rigidbody player;
	private bool noObstacle;

	// Use this for initialization
	void Start () {
		speed = 5f;
		player = gameObject.GetComponent<Rigidbody> ();
		noObstacle = true;
	}
	
	// Update is called once per frame
	void Update () {

		//Detect if an obstacle is in front of the player 
		Vector3 forwardRay = transform.TransformDirection (Vector3.forward);
		Vector3 sidewaysLeftRay = transform.TransformDirection(new Vector3(-1f, 0f, 10f));
		Vector3 sidewaysRightRay = transform.TransformDirection(new Vector3(1f, 0f, 10f));
		if (Physics.Raycast (transform.position, forwardRay, 10f) || Physics.Raycast (transform.position, sidewaysLeftRay, 10f)
			|| Physics.Raycast (transform.position, sidewaysRightRay, 10f)) {
			noObstacle = false;
		} else
			noObstacle = true;

		//If there is an obstacle move the other way
		if (!noObstacle) {
			//Have the player move sideways
			if (Physics.Raycast (transform.position, sidewaysLeftRay, 20f) /*&&
				!Physics.Raycast (transform.position, sidewaysRightRay, 5f)*/) {
				//Move to the right, change the rotation
				transform.Rotate(new Vector3(0f, 30f, 0f) * Time.deltaTime);
			}
			else if (Physics.Raycast (transform.position, sidewaysRightRay, 20f) /*&&
				Physics.Raycast (transform.position, sidewaysLeftRay, 5f)*/) {
				//Move to the left
				transform.Rotate(new Vector3(0f, -30f, 0f) * Time.deltaTime);
			}
			//There are obstacles on both sides
			else {
				//Find the normal
				player.velocity = transform.forward * speed;
			}
		} else {
			//Have player go fowards
			player.velocity = transform.forward * speed;
			//Make sure they stick to the center
			if (gameObject.transform.position.y != 0) {
				//Get the rotation distance
				float rotate =  0f - gameObject.transform.rotation.y;
				//Rotate the player
				transform.Rotate (new Vector3 (0f, rotate, 0f));
			}
		}
	}
}
