using UnityEngine;
using System.Collections;

public class DestroyMeteors : MonoBehaviour
{

	public int pointsToAdd;

	void Update ()
	{	
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
}
