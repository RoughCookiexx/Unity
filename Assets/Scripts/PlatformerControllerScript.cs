using UnityEngine;
using System.Collections;
using System;


// Should be called "WallJumperController"
public class PlatformerControllerScript : MonoBehaviour {
	
	const byte  ON_GROUND = 0x01;
	const byte  ON_WALL = 0x02;
	const byte  IN_AIR = 0x04;
	const byte  SHOOT = 0x08;
	const byte  DASH = 0x10;
	const byte  RUN = 0x20;
	const byte  JUMP = 0x40;
	
	int numGroundCollisions = 0;
	int numWallCollisiotns = 0;
	float speed = 25f;
	static float MAX_SPEED = 12f;
	byte actionFlags;
	byte prevActionFlags;
	bool fireButtonHeldDown = false;
	
	AbilityManager abilityManager;
	
	// Use this for initialization
	void Start () 
	{
		actionFlags = 0;
		prevActionFlags = 0;
		abilityManager = gameObject.GetComponent<AbilityManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log("AF: " +actionFlags +" || ON_WALL: " +ON_WALL + " || AF & ON_WALL: " +(actionFlags & ON_WALL));
		prevActionFlags = actionFlags;
		actionFlags = 0x00;
		
		HandleMovement();
		SetAnimination();
		FireGun();
			
	}
	
	// Check if the fire button was pressed then fire the gun.
	void FireGun()
	{
			
		float fireInput = Input.GetAxisRaw( "Fire1" );
		if(fireInput != 0 && !fireButtonHeldDown)
		{
			this.GetComponent<GunScript>().Fire(this.GetComponent<SpriteSheetAnimation>().getDirection());
			fireButtonHeldDown = true;
		}
		if(fireInput == 0)
			fireButtonHeldDown = false;
	}
	
	void HandleMovement()
	{
		
		// Get the input on the x-axis:
		float xInput = Input.GetAxisRaw( "Horizontal" );
		
		// Also get the sprite sheet animation script:
		SpriteSheetAnimation ssa = this.GetComponent<SpriteSheetAnimation>();
		
		// If the player is not pressing a direction button:
		if(xInput == 0)
		{			
			rigidbody.AddForce(new Vector3(rigidbody.velocity.x * -3, 0,0));			
		}
		
		// Go left or right:
		if(xInput != 0)
		{
			// Set the flag for animations:
			actionFlags |= RUN;
			
			// Make sure the sprite is facing the right direction:
			if(xInput > 0 
					&& ssa.direction != 0)
			{
				ssa.ChangeDirections(0);	
			}
			else if(xInput < 0
					&& ssa.direction != 1)
			{
				ssa.ChangeDirections(1);
			}
			
			// If the body is not moving faster than max speed:
			if(Math.Abs(rigidbody.velocity.x) <= MAX_SPEED)
			{				
				// Apply a force to the body:
				rigidbody.AddForce( new Vector3( speed * xInput, 0, 0 ));
			}
		}
				
		
		
		// Jump:
		if(Input.GetButtonDown("Jump"))
		{ 
			// Jump off the ground:
			if(numGroundCollisions > 0)
			{
				this.rigidbody.AddForce(new Vector3(0f,500f,0f));
				actionFlags |= JUMP;
			}
			
			// Or jump off of a wall:
			else if(true//abilityManager.AbilityIsUnlocked(AbilityManager.Ability.WALL_JUMP) 
				&& numWallCollisiotns > 0)
			{
				this.rigidbody.AddForce(new Vector3(xInput * -600f ,500f,0f));
				actionFlags |= JUMP;
			}
		}
		
		
	}
	
	void SetAnimination()
	{
		// Get the sprite sheet animation script:
		SpriteSheetAnimation ssa = this.GetComponent<SpriteSheetAnimation>();
		
		// Set the flags for animations:
		if(numGroundCollisions > 0)
		{
			actionFlags |= ON_GROUND;
		}
		if(numWallCollisiotns > 0)
		{
			actionFlags |= ON_WALL;	
		}
		
		// This mess right here switches the animation actions:
		{
			int action = ssa.currentAction;
			switch(action) 
			{
				case 1 : // WALL SLIDE
			{
				if( (actionFlags & JUMP) != 0)
					ssa.ChangeAction(3, 2);
				else if ( (actionFlags & ON_GROUND) != 0)
					ssa.ChangeAction(5);
				else if(( actionFlags & ON_WALL) == 0)
					ssa.ChangeAction(3,4);
					break;
			}
				case 3 : // JUMP
			{
				if((actionFlags & ON_GROUND) != 0)
				{
					if((actionFlags & RUN) != 0)
						ssa.ChangeAction(7);
					else 
						ssa.ChangeAction(5);
				}
				else if((actionFlags & ON_WALL) != 0)
					ssa.ChangeAction(1);
				
					break;
			}
				case 5 : // STAND
			{
				if((actionFlags & JUMP) != 0)
					ssa.ChangeAction(3);
				else if((actionFlags & RUN) != 0)
				{
					ssa.ChangeAction(7);
				}
				break;
			}
				case 7 : // RUN 
			{
				if((actionFlags & JUMP) != 0
					|| (actionFlags & ON_GROUND) == 0)
					ssa.ChangeAction(3);
				else if((actionFlags & RUN) != 0) // STOP RUNNING
					ssa.ChangeAction(7);				
				else if((actionFlags & RUN) == 0)
					ssa.ChangeAction(5);
				//Debug.Log("AF: " +actionFlags +" || RUN: " +RUN + " || AF & RUN: " +(actionFlags & RUN));
					break;
			}
				
				default :
					ssa.ChangeAction(3, 4); // Not touching a floor or wall? Must be falling.
					break;
			}
		}
	}
	
	
	void OnCollisionEnter( Collision c )
	{
		string tag = c.collider.gameObject.tag;
		
		// Keep track of the world parts the player is touching:
		if(tag == "Ground")
		{
			numGroundCollisions++;
		}
		if(tag == "Wall" )
		{
			numWallCollisiotns++;
		}
	}
	
	void OnCollisionExit( Collision c )
	{
		string tag = c.collider.gameObject.tag;
		
		// Keep track of the world parts the player is touching:
		if(tag == "Ground")
		{
			numGroundCollisions--;
		}
		if(tag == "Wall" )
		{
			numWallCollisiotns--;
		}
	}
}
