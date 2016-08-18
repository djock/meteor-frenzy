using UnityEngine;
using System.Collections;

public class Meteors : MonoBehaviour
{

	public int pointsToAdd;
	public static float meteorSpeed;

	public Vector2 velocity = new Vector2 (0, -2);
	public float range;

	UI2DSprite sprite;

	void Start ()
	{
		GetComponent<Rigidbody2D> ().velocity = velocity;
		GetComponent<Rigidbody2D> ().transform.position = new Vector2 (GetComponent<Rigidbody2D> ().transform.position.x - range * Random.value, GetComponent<Rigidbody2D> ().transform.position.y);
	}

	void Update ()
	{
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, meteorSpeed);

		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hity = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);

			if (hity.collider != null) {
				DestroyMeteor (hity.collider.gameObject);
				GameManager.Instance.MeteorExplosion (hity.collider.gameObject.transform.position);
			}
		}
	}

	public void DestroyMeteor (GameObject go)
	{
		GameManager.Instance.AddPoints (pointsToAdd);
		Destroy (go);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Ground") {
			GameManager.Instance.MeteorExplosion (transform.position);
			Destroy (gameObject);
		}
	}
}
