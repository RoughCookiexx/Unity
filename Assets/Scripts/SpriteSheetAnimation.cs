using UnityEngine;
using System.Collections;

public class SpriteSheetAnimation : MonoBehaviour {
	
	public int columns = 9;
	public int rows = 8;
	public float frameRate = .1f;
	public int[] framesPerAction;
	public int[] pauseFrame;		// These three should be combined into a single array of 'Actions'
	public int[] nextAction;
	public int direction = 1;  // Should this be an enum?
	public int currentAction = 0;
	Vector2 offset;
	float timeLastDrawn;
	int framesSinceChange;
	int index;
	
	// Use this for initialization
	void Start () {
		offset = new Vector2(0f,0.5f);
		timeLastDrawn = Time.time;
		index = 0;
		framesSinceChange = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		// IF it's time to change frames:
		if(Time.time > (timeLastDrawn + frameRate) ) 
		{
			// Add to the offset on the sprite sheet:
			offset.x = index * ( (float) 1/(float) columns );
			
			// Set the index:
			if(index != pauseFrame[currentAction])
				index++;
			
			// Set the index to zero if it goes over the edge of the sprite sheet:
			if (index >= framesPerAction[currentAction])
			{
				// If the end of this
				if(nextAction[currentAction] != -1)
					currentAction = nextAction[currentAction+direction];
				index = 0;
			}
			
			//Debug.Log("Index: " +index +" || Current Action: " +currentAction +" || FramesInAction: " +framesPerAction[currentAction] + " || Direction: " +direction);
			
			// Swap the frames:
			renderer.material.SetTextureOffset("_MainTex", offset);
			
			// Keep track of the time the frames are swapped:
			timeLastDrawn = Time.time;
			framesSinceChange++;
		}
		
	}
	
	// Change the action number:
	 public void ChangeAction(int actionNumber)
	{
		if(framesSinceChange >= 1
			&& actionNumber != currentAction)
		{
			offset.y =  (float)(actionNumber-direction)/(float)rows;			
			currentAction = actionNumber;
			index = 0;
			framesSinceChange = 0;
		}
	}
	
	// Change the action number:
	 public void ChangeAction(int actionNumber, int startFrame)
	{
		if(framesSinceChange >= 1
			&& actionNumber != currentAction)
		{
			offset.y =  (float)(actionNumber-direction)/(float)rows;			
			currentAction = actionNumber;
			index = startFrame;
			framesSinceChange = 0;
		}
	}
	
	public void ContinueAnimation()
	{
		index++;	
	}
	
	// Change the direction the sprite is facing:
	public void ChangeDirections(int dir)
	{
		direction = dir;
			offset.y =  (float)(currentAction-direction)/(float)rows;
		
	}
	
	public int getDirection() 
	{ 
		if(direction == 0)
			return 1;
		else return -1;
	}
}
