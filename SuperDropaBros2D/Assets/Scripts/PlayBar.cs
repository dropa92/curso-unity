﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum barType{

	health,
	mana

}

public class PlayBar : MonoBehaviour
{
	private Slider slider;
	public barType type;

    // Start is called before the first frame update
    void Start()
    {
	    this.slider=GetComponent<Slider>(); 


           }

    // Update is called once per frame
    void Update()
    {
		switch(this.type){
			//Obtains the value of health	
			case barType.health:
				this.slider.value=PlayerControlerScript.sharedInstance.GetHealthPoints();
				break;

				//Obtains the value of mana
			case barType.mana:
				this.slider.value=PlayerControlerScript.sharedInstance.GetManaPoints();
				break;
		
		}

            }
}