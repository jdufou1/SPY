using System;
using System.IO;

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using FYFY;

using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class CoinSystem : FSystem {

	private Family f_coinDisplay = FamilyManager.getFamily(new AllOfComponents(typeof(CoinDisplay)));

	public static CoinSystem instance;

	public CoinSystem()
	{
		instance = this;
	}
	
	// Use to init system before the first onProcess call
	protected override void onStart(){


	}

	// Use to update member variables when system pause. 
	// Advice: avoid to update your families inside this function.
	protected override void onPause(int currentFrame) {

	}

	// Use to update member variables when system resume.
	// Advice: avoid to update your families inside this function.
	protected override void onResume(int currentFrame){
		
	}

	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {
		int coins = read_current_player_coins(); //PlayerPrefs.GetInt("playerCoins", 0);
		GameObject text_field = f_coinDisplay.First(); // acces au premier game object
		TextMeshProUGUI text = text_field.GetComponent<TextMeshProUGUI>(); // acces au component text
		text.text = coins + "$"; // modification du tex
	}

	public int read_current_player_coins()
	{
		int player_coins = 0;
		string path = "Assets/Resources/current_player_coins.txt";
		if(File.Exists(path)){
			// si le fichier existe, on prend la derniere valeur enregistrée
			string[] lines = File.ReadAllLines(path);
			player_coins =  int.Parse(lines[0]);
		}
		// sinon on prend le skin par défaut
		return player_coins;
	}

}