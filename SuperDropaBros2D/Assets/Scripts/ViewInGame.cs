﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ViewInGame : MonoBehaviour
{
	public Text collectableLabel;
	public Text distance;
	public Text recordTime;
    
	// Update is called once per frame
    void Update()
    {

	    
       if(GameManager.sharedInstance.currentGameState == GameState.inGame || GameManager.sharedInstance.currentGameState == GameState.gameOver){
       
	       int currentObjects=GameManager.sharedInstance.collectedObjetcs;
		this.collectableLabel.text=currentObjects.ToString();

      if(GameManager.sharedInstance.currentGameState==GameState.inGame){
      	
	      float travelledDistance=PlayerControlerScript.sharedInstance.GetDistance();
      		this.distance.text="Metros\n"+travelledDistance.ToString("f0");
      } 
       } 

       
    }
}
