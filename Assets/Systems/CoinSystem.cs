using UnityEngine;
using System.Collections;
using FYFY;


public class CoinSystem : FSystem {

	public static CoinSystem instance;

	private int coin_player;

	private string path_coin_player = "./jenaiaucuneidee.csv";

	public CoinSystem()
	{
		instance = this;
		this.coin_player = 0;

		// TODO : Chargement des données utilisateur coin
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
		this.coin_player++;
		// Debug.Log("salut onprocess"+val);
	}

	public void update_coin_panel(){
		// TODO
	}

	public int get_coin_player(){
		return this.coin_player;
	}
}