using UnityEngine;
using System.Collections;

public class AIBlindFireStateScript : AIStateScript 
{
	GunScript g;
	
	// Use this for initialization
	void Start () 
	{
		AIScript s = ((AIScript)gameObject.GetComponent<AIScript>());
		s.AddStateScript(this);
		
		g = this.gameObject.GetComponent<GunScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void Process()
	{
		g.Fire(-1);
	}
	
	public override bool isTimeToChange()
	{
		return false;	
	}
}
