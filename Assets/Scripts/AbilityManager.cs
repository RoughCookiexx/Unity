using UnityEngine;
using System.Collections;

public class AbilityManager : MonoBehaviour {
	
	public enum Ability { 
		WALL_JUMP= 0x01, 
		DASH= 0x02 
	};
	
	byte abilitiesUnlocked;
	
	// Use this for initialization
	void Start () 
	{
		abilitiesUnlocked = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void UnlockAbility( Ability a )
	{
		abilitiesUnlocked |= (byte)a;
	}
	
	public bool AbilityIsUnlocked(Ability a)
	{
		return ((abilitiesUnlocked & (byte)a) != 0);
	}
}
