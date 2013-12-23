using UnityEngine;
using System.Collections;
using System;

public class HealthScript : MonoBehaviour {
	
	public int health = 100;
	public bool good = false;
	
	public int getHealth() { return health; }
	
	// Use this for initialization
	void Start () 
	{
		if(GetComponent<DamageDealerScript>())
			good = GetComponent<DamageDealerScript>().good;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	
	
	void OnCollisionEnter( Collision c )
	{
		GameObject other = c.collider.gameObject;
		
		try
		{
		
			bool isOtherGood = other.GetComponent<DamageDealerScript>().good;
			
				//Debug.Log(gameObject + " || Health: " +health +" || HealthGood: " + good + " || " +other +": " +isOtherGood) ;
			
			if( good ^ isOtherGood)
			{
				health -= 	other.GetComponent<DamageDealerScript>().damageFactor;
				if(health <= 0)
					rigidbody.position = new Vector3 (0,0,-100);
			}
		} catch (NullReferenceException e)
		{
			return;	
		}
		
	}
}
