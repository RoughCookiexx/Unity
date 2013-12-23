using UnityEngine;
using System.Collections;

public class Camera2DScript : MonoBehaviour {
	
	public GameObject target;
	public float xBounds = 3f;
	public float yBounds = 3f;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 targetPos = target.transform.position;
		Vector3 thisPos = transform.position;
		
		float x = thisPos.x;
		float y = thisPos.y;
		
		float targetX = target.transform.position.x;
		float targetY = target.transform.position.y;
		
		if(Mathf.Abs(x - targetX) > xBounds)
		{
			if(targetX > x)
			{
				x += targetX- (x + xBounds);
			}
			else 
			{
				x += targetX- (x - xBounds);
			}
		}
		
		if(Mathf.Abs(y - targetY) > yBounds)
		{
			if(targetY > y)
			{
				y += targetY- (y + yBounds);
			}
			else 
			{
				y += targetY- (y - yBounds);
			}
		}
		transform.position = new Vector3(x,y,-10);
		transform.LookAt(targetPos);
	}
}
