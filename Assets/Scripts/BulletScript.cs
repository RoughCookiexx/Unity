using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	bool _active;
	float timeLaunched;
	public float timeActive = 1;
	int _direction;
	
	// Use this for initialization
	void Start () 
	{
		_active = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Time.time > timeLaunched + timeActive)
		{
			_active = false;	
			transform.position = new Vector3(0,0, -100);
		}
		else
		{
			Vector3 position = transform.position;
			float x = position.x + ((float)_direction * .3f);
			transform.position = new Vector3(x ,position.y,position.z);
		}
	}
	
	public void Fire(int direction)
	{
		_active = true;
		timeLaunched = Time.time;
		_direction = direction;
	}
	
	public bool isActive() { return _active; }
	
	void OnCollisionEnter( Collision c )
	{
		string tag = c.collider.gameObject.tag;
		
		if(!_active)
		{
			transform.position = new Vector3(0,0, -100);
			return;	
		}
		
		// Keep track of the world parts the player is touching:
		if(tag != "Player")
		{
			_active = false;
			transform.position= new Vector3(0,0, -100);
		}
	}
}
