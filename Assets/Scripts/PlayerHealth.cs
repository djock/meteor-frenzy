using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.tag == "Meteor") {
			Destroy (gameObject);
			Destroy(other.gameObject);
		}

	}
}
