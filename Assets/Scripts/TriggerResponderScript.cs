using UnityEngine;
using System.Collections;

public class TriggerResponderScript : MonoBehaviour {
	
	protected bool _triggered;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Act()
	{
		_triggered = true;	
	}
}
