using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	// Die Spielerinstanz, welche alle Informationen liefert
	public Player player;

	// Feld, welches die Sprechblase beinhaltet
	private GameObject talking;
	// Textobjekt, welches den gesprochenem Text anzeigt
	private Text talkingText;
	// Zeitpunkt, als die letzte Blase angezeigt wurde, 0 wenn keine angezeigt wird
	private int speechDisplayedTime = 0;
	// Darstellungszeit der Sprechblase
	private const int speechDisplayDuration = 15;

	// Gesundheit Schieberegler
	private Slider healthSlider;
	// Ausdauer Schieberegler
	private Slider staminaSlider;
	// Textobjekt welches das Geld anzeigt das der Spieler hat.
	private Text moneyText;

	// Initialisierung
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
	
	// Update wird pro frame einmal aufgerufen
	// Statusinformationen aus Spieler übernehmen und anzeigen
	void Update ()
	{
		moneyText.text = player.money + " $";
		healthSlider.value = player.health;
		staminaSlider.value = player.stamina;

		string lastTalk = player.lastTalk;
		// wenn mit dem Spieler seit letztem Mal gesprochen wurde
		if (lastTalk.Length > 0) {
			// anzeigen der Sprechblase
			player.lastTalk = "";
			talking.SetActive (true);
			talkingText.text = lastTalk;
			// Zeitpunkt des Anzeigens merken
			speechDisplayedTime = (int)Time.time;
		} else {
			// kein neuer Text, überprüfe ob die Sprechblase wieder versteckt werden soll
			if (speechDisplayedTime > 0) {
				if ((int)Time.time - speechDisplayedTime > speechDisplayDuration) {
					speechDisplayedTime = 0;
					talking.SetActive (false);
					talkingText.text = "";
				}
			}
		}	
	}
}
