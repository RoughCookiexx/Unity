using UnityEngine;
using System.Collections;

public class HealthGuiScript : MonoBehaviour {
	
	public GameObject target;
	public GameObject healthPiece;
	
	public int numOfHealthPieces = 9;
	public int healthPieceSize = 9;
	
	private GameObject[] healthPieces;
	private float healthPerPiece;
	private float x = 0.0235f;
	private float yStart = 0.74f;
	private float stackHeight =0.016f;
	
	// Use this for initialization
	void Start () 
	{
		int health = target.GetComponent<HealthScript>().getHealth();
		
		healthPerPiece = health / (float) (numOfHealthPieces);
		
		healthPieces = new GameObject[numOfHealthPieces];
		
		for(int i = 0; i < numOfHealthPieces; i++)
		{
			float y = yStart+(stackHeight * i);
			GameObject healthPieceI = (GameObject)Instantiate(healthPiece, new Vector3 (x, (y), 10), Quaternion.identity);
			
			//healthPieceI.guiTexture.pixelInset.Set (12+5, -112+5 + (i*7), 0, 0);
			healthPieces[i] = healthPieceI;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		int health = target.GetComponent<HealthScript>().getHealth();
		
		int numOfPieces = (int) ((float) health / healthPerPiece);
		
		for(int i = 0; i < numOfHealthPieces; i++)
		{
			healthPieces[i].SetActive(false);
		}
		if(health > 0 
			&& numOfPieces <= 0)
			numOfPieces = 1;
		for(int i = 0; i < numOfPieces; i++)
		{
			healthPieces[i].SetActive(true);
		}
		
	}
}
