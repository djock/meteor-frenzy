using UnityEngine;
using System.Collections;

public class Meteors : MonoBehaviour {

	public int pointsToAdd;
	public static float meteorSpeed;

	public Vector2 velocity = new Vector2(0, -2);
	public float range;

	float rotationSpeed;

	// Use this for initialization
	void Start()
	{
		GetComponent<Rigidbody2D>().velocity = velocity;
		GetComponent<Rigidbody2D>().transform.position = new Vector2(GetComponent<Rigidbody2D>().transform.position.x - range * Random.value, GetComponent<Rigidbody2D>().transform.position.y);
		GetComponent<Rigidbody2D> ().angularVelocity = rotationSpeed;
	}
	
	// Update is called once per frame
	void Update ()
	{	
		GetComponent<Rigidbody2D> ().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, meteorSpeed);

		Destroy ();
	}

	void Destroy ()
	{
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);

			if (hit.collider != null) {
				if (hit.collider.gameObject == gameObject) {	
					GameManager.Instance.AddPoints (pointsToAdd);
					GameManager.Instance.MeteorExplosion(transform.position);
					Destroy (gameObject);    
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Ground")
		{
			GameManager.Instance.MeteorExplosion(transform.position);
			Destroy(gameObject);
		}
	}
}
