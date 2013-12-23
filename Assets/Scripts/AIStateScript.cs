using UnityEngine;
using System.Collections;

public class AIStateScript : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		AIScript s = ((AIScript)gameObject.GetComponent<AIScript>());
		s.AddStateScript(this);
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public virtual bool isTimeToChange()
	{
		return false;
	}
	
	public virtual void Process()
	{
		
	}
}
