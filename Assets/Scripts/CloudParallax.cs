using UnityEngine;
using System.Collections;

public class CloudParallax : MonoBehaviour {

	public float scrollSpeed;
	[SerializeField] private float displacement;

	Transform thisTransform;
	Vector3 initialPosition;

	private void Awake()
	{
		thisTransform = this.transform;
		this.initialPosition = thisTransform.position;

	}
	// Update is called once per frame
	void Update () {
		float newPosition = Time.deltaTime * scrollSpeed;
		this.thisTransform.position = thisTransform.position + Vector3.left * newPosition;
	}

	public void OnTriggerExit(Collider collision)
	{
		if (collision.gameObject.name == "DestroyCloud")
		{
			Debug.Log("Cloud meets with destroy collider");
			DisableObject ();
		}
	}

	private void DisableObject()
	{
		Vector3 finalPosition = thisTransform.position;
		finalPosition.x += displacement;

		thisTransform.position = finalPosition;
	}

	public void ResetPosition()
	{
		thisTransform.position = this.initialPosition;
	}
}

