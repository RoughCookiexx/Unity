using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {
	
	
	public float timeBetweenShots;
	float lastTimeShot = 0;
	
	public int numBullets;
	public GameObject bulletPrefab;
	GameObject[] bulletPrefabArray;
	
	// Use this for initialization
	void Start () {
		
		Vector3 position = new Vector3(0f, -6f, 10f);
		Quaternion rotation = Quaternion.identity;
		Instantiate(bulletPrefab, position, rotation);
		
		bulletPrefabArray = new GameObject[numBullets];
		for(int i = 0; i < bulletPrefabArray.Length; i++)
		{				
			bulletPrefabArray[i] = (GameObject)Instantiate(bulletPrefab, position, rotation);
		}
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	// Fire the gun
	public void Fire(int direction)
	{
		// If 
		if(Time.time < lastTimeShot + timeBetweenShots)
			return;
		
		Vector3 position = gameObject.transform.position;
		position.x += (.6f * direction); 
		for(int i = 0; i < bulletPrefabArray.Length; i++)
		{
			GameObject bullet = bulletPrefabArray[i];
			
			if(!bullet.GetComponent<BulletScript>().isActive())
			{
				bullet.transform.position = position;
//				bullet.transform.velocity = new Vector3(20f * direction, 0, 0);
				bullet.GetComponent<BulletScript>().Fire(direction);
				
				lastTimeShot = Time.time;
				return;
			}
		}
		
	}
}
