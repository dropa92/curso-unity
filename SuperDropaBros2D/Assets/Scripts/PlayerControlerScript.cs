﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControlerScript : MonoBehaviour{

    public float jumpForce = 4f;    //Jump's force

    private Rigidbody2D ridgiBody;

    public LayerMask groundLayer;   //This variable checks the layer of the ground

    public Animator animator;       //The character's animation

    public float runningSpeed = 1.5f;   //This will be the speed of the character

    public static PlayerControlerScript sharedInstance; //it is used to make a singletone on this class

    public Vector3 initialPosition; //it will be the initial player's position

    private int healthPoints, manaPoints;
    


    
    //Before the game starts, the game have to be configured
    void Awake()
    {
        //we do a singletone on this class with this variable
        sharedInstance = this;

        //We reference our rigidBody with the Rigidbody2D in the Scene 
        ridgiBody = GetComponent<Rigidbody2D>();

        //we assign the position when the player starts the game to the variable initialPosition to know its first position
        initialPosition = this.transform.position;
     
	

    }   





    // Start is called before the first frame update
    public void StartGame()
    {   
	    this.healthPoints=40;
	    this.manaPoints=30;
        animator.SetBool("isAlive", true);		//This set the character's state to Alive
        animator.SetBool("isGrounded", true);		//this set the character is on the ground
        this.transform.position = initialPosition;	//this set the initial position of the character
    }




    // Update is called once per frame
    void Update()
    {

        //If the game is inside of gameplay, the character can jump
        if (GameManager.sharedInstance.currentGameState == GameState.inGame) {

            //Here, we check if the key Space is down, the character jumps
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
               
            Jump();
        }

        //Check the character is touching the ground
        animator.SetBool("isGrounded", IsTouchingTheGround());
        }
    }



    //This method is called by Unity every 0.1 second and is very usefull to make movements as run or hit
     void FixedUpdate()
    {

        //If the game is inside of gameplay, the character can move
        if (GameManager.sharedInstance.currentGameState == GameState.inGame) { 
	
	//If the Key D is down
        if (Input.GetKeyDown(KeyCode.D)) {
                if (ridgiBody.velocity.x < runningSpeed)
                {
                    
                    //We assing positive speed to the character's movement at the X axis to go right and keep the current speed at the Y axis  
                    ridgiBody.velocity = new Vector2(runningSpeed, ridgiBody.velocity.y);
                
        }
    }



	//If the Key A is down
        if (Input.GetKeyDown(KeyCode.A))
        {	
		//If the character velocity is greater than running speed
            if (ridgiBody.velocity.x > -runningSpeed)
            {
                //We assing negative speed to the character's movement at the X axis to go left and keep the current speed at the Y axis  
                ridgiBody.velocity = new Vector2(-runningSpeed, ridgiBody.velocity.y);
            }
        }

        }
    }





    /*This method makes the character jump multiplying the jump force at vector Y, and the way that the force is applied,
     * in this case is impulse
     */

    void Jump()
    {
	//If the character is touching the ground, the character jumps
        if (IsTouchingTheGround())
        {
            if(this.manaPoints>=5){
	    
	    
            ridgiBody.AddForce(Vector2.up * jumpForce *1.5f, ForceMode2D.Impulse);
	    this.manaPoints-=5;
	    }else{
	    ridgiBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

	    }
	    }
        
    }





    //Return true, if the character is touching the ground
    bool IsTouchingTheGround()
    {
        /**
         * From the character's position, throw a Raycast to down, and check if the character is touching
         * the ground until 30cm
         */
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 0.3f, groundLayer)){
            
            
            return true;
        }
        else
        {
           
            return false;
        }
    }

    //This method kill the player
    public void kill()
    {
        //Change the gameState at game manager
        GameManager.sharedInstance.GameOver();

        //change to the kill animation
        this.animator.SetBool("isAlive", false);

    	//get the current maximum distance
	float  currentMaxDistance=PlayerPrefs.GetFloat("maxDistance",0);

	//If the current maximum distance is less than the current distance, set a new record distance	
	if(currentMaxDistance<this.GetDistance()){
	
		PlayerPrefs.SetFloat("maxDistance",this.GetDistance());
	
	}
    
    }

    	//get the current distance
	public float GetDistance(){
	
	float travelledDistance= Vector2.Distance(new Vector2(initialPosition.x,0), new Vector2(this.transform.position.x,0));

	
	return travelledDistance;
	}
	public void CollectHealth(int points){
	
		this.healthPoints+=points;
		if(this.healthPoints<=40){
		this.healthPoints=40;
		}
		
	
	}

	public void CollectMana(int points){
	
		this.manaPoints+=points;

		if(manaPoints>=30){
	manaPoints=30;
	
	}
}
}
