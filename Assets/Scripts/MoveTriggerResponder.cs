using UnityEngine;
using System.Collections;

public class MoveTriggerResponder : TriggerResponderScript {
	
	public Vector3 targetPosition;
	public float timeToMove = 1f;
	
	private Vector3 movePerFrame;
	private float timeMoved = 0;
	
	// Use this for initialization
	void Start () 
	{
		Vector3 position = transform.position;
		movePerFrame.x = (targetPosition.x - position.x) / timeToMove;
		movePerFrame.y = (targetPosition.y - position.y) / timeToMove;
		movePerFrame.z = (targetPosition.z - position.z) / timeToMove;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(_triggered)
		{
			if(timeMoved < timeToMove)
			{
				Debug.Log("Moving || " +transform.position + " || Clock: " +Time.time);
				transform.position += movePerFrame * Time.deltaTime;
			}
			timeMoved += Time.deltaTime;
		}
	}
}
