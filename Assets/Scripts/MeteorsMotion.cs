using UnityEngine;
using System.Collections;

public class MeteorsMotion : MonoBehaviour {

	public float meteorSpeed;
	
	public Vector2 velocity = new Vector2(4, -2);
	public float range;
	
	public float rotationSpeed;
	
	// Use this for initialization
	void Start()
	{
		GetComponent<Rigidbody2D>().velocity = velocity;
		GetComponent<Rigidbody2D>().transform.position = new Vector2(GetComponent<Rigidbody2D>().transform.position.x - range * Random.value, GetComponent<Rigidbody2D>().transform.position.y);
		GetComponent<Rigidbody2D> ().angularVelocity = rotationSpeed;
	}
	
	void Update()
	{
		GetComponent<Rigidbody2D> ().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, meteorSpeed);
	}
}
