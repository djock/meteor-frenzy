using UnityEngine;
using System.Collections;

public class CloudSpawn : MonoBehaviour {

	[SerializeField] private float displacement;
	private Vector3 initialPosition;

	private Transform thisTransform;

	
	public void OnTriggerExit(Collider collision)
	{
		if (collision.gameObject.name == "DestroyCloud")
		{
			Debug.Log("Cloud meets with destroy collider");
				DisableObject ();
		}
	}
	
	private void Awake()
	{
		thisTransform = this.transform;
		this.initialPosition = thisTransform.position;

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
