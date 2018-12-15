using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	// the player instance that delivers all informations
	public Player player;

	// the panel containing the speech bubble
	private GameObject talking;
	// the text object displaying the speech
	private Text talkingText;
	// the moment of the last bubble displayed, 0 if not displayed
	private int speechDisplayedTime = 0;

	// health slider
	private Slider healthSlider;
	// stamina slider
	private Slider staminaSlider;
	// the text object displaying the money player has
	private Text moneyText;

	// Use this for initialization
	void Start ()
	{
		
		GetComponent<Canvas> ().enabled = true;

		talking = transform.Find ("Talking").gameObject;
		talkingText = transform.Find ("Talking/Text").gameObject.GetComponent<Text> ();
		talking.SetActive (false);

		healthSlider = transform.Find ("HealthSlider").gameObject.GetComponent<Slider> ();
		staminaSlider = transform.Find ("StaminaSlider").gameObject.GetComponent<Slider> ();
		moneyText = transform.Find ("Money/Text").gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	// Display status information
	void Update ()
	{
		moneyText.text = player.money + " $";

//		Health = player.health;
		healthSlider.value = player.health;

//		Stamina = player.stamina;
		staminaSlider.value = player.stamina;

		string lastTalk = player.lastTalk;
		// if the player has been talked to in the last round
		if (lastTalk.Length > 0) {
			// display bubble with text
			player.lastTalk = "";
			talking.SetActive (true);
			talkingText.text = lastTalk;
			// remember when
			speechDisplayedTime = (int)Time.time;
		} else {
			// no new text, check whether bubble should be hidden again
			if (speechDisplayedTime > 0) {
				if ((int)Time.time - speechDisplayedTime > 15) {
					speechDisplayedTime = 0;
					talking.SetActive (false);
					talkingText.text = "";
				}
			}
		}	
			
		
	}
}
