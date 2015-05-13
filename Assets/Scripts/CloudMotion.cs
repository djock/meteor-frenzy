using UnityEngine;
using System.Collections;

public class CloudMotion : MonoBehaviour {

	public float scrollSpeed;

	public GameObject startingPoint;

	Transform thisTransform;

	// Use this for initialization
	void Start () {
		thisTransform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		float newPosition = Time.deltaTime * scrollSpeed;
		this.thisTransform.position = thisTransform.position + Vector3.left * newPosition;
	}

	public void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.name == "DestroyCloud")
		{
			Debug.Log("Destroy Cloud");
			//thisTransform.position.x = startingPoint.transform.position.x;
		}
	}
}
