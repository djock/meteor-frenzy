using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

	void OnTriggerEnter2D (Collider2D other)
	{
		NGUITools.Destroy (other.gameObject);
		NGUITools.SetActive (gameObject, false);
	}

	void OnDisable ()
	{
		GameManager.Instance.IsPlayerAlive ();
	}
}
