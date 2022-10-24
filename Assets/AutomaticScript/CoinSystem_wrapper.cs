using UnityEngine;
using FYFY;

public class CoinSystem_wrapper : BaseWrapper
{
	private void Start()
	{
		this.hideFlags = HideFlags.NotEditable;
	}

	public void update_coin_panel(UnityEngine.GameObject go)
	{
		MainLoop.callAppropriateSystemMethod (system, "update_coin_panel", go);
	}

}
