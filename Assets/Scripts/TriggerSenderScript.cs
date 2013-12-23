using UnityEngine;
using System.Collections;

public class TriggerSenderScript : MonoBehaviour {
	
	public GameObject activator;
	public TriggerResponderScript responder;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	
	void OnTriggerEnter( Collider c)
	{
		GameObject other = c.gameObject;
		
		if(other == activator)
		{
			responder.Act();
			Debug.Log("Triggering: " +responder);
		}
		Debug.Log(other);
		
	}
}
