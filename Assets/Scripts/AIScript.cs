using UnityEngine;
using System.Collections;

public class AIScript : MonoBehaviour {
	
	public ArrayList stateList;
	private AIStateScript activeState;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( ((AIStateScript)activeState).isTimeToChange() )
		{
			
		}
		
		activeState.Process();
	}
	
	public void AddStateScript(AIStateScript s)
	{
		if(stateList == null)
		{
			stateList = new ArrayList();
			activeState = s;
		}
		stateList.Add(s);
	}
	
}
