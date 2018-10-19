using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
	//public SliderJoint2D Healthbar;
	public float Health;
	public float Stamina;

	public GameObject Self;
	public GameObject Player;
	public int Hello;
	private GameObject TextObjekt;



	public Slider healthSlider;
	public Slider staminaSlider;
	// Use this for initialization
	void Start () {
		
		Self.GetComponent<Canvas>().enabled = true;



	}
	
	// Update is called once per frame
	void Update () {
		Health = Player.GetComponent<Player> ().Health;
		Stamina = Player.GetComponent<Player> ().Stamina;


		healthSlider.value = Health;
		//Debug.Log ("Health at " + Health);
		staminaSlider.value = Stamina;

		
		
	}
}
