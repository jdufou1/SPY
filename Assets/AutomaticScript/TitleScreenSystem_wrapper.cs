using UnityEngine;
using FYFY;

public class TitleScreenSystem_wrapper : BaseWrapper
{
	public GameData prefabGameData;
	public UnityEngine.GameObject mainMenu;
	public UnityEngine.GameObject skinMenu;
	public UnityEngine.GameObject skins;
	public UnityEngine.GameObject campagneMenu;
	public UnityEngine.GameObject compLevelButton;
	public UnityEngine.GameObject cList;
	public UnityEngine.GameObject robotKyle;
	public System.String pathFileParamFunct;
	public System.String pathFileParamRequiermentLibrary;
	public System.String[] textures_available;
	public System.Int32[] textures_prices;
	private void Start()
	{
		this.hideFlags = HideFlags.NotEditable;
		MainLoop.initAppropriateSystemField (system, "prefabGameData", prefabGameData);
		MainLoop.initAppropriateSystemField (system, "mainMenu", mainMenu);
		MainLoop.initAppropriateSystemField (system, "skinMenu", skinMenu);
		MainLoop.initAppropriateSystemField (system, "skins", skins);
		MainLoop.initAppropriateSystemField (system, "campagneMenu", campagneMenu);
		MainLoop.initAppropriateSystemField (system, "compLevelButton", compLevelButton);
		MainLoop.initAppropriateSystemField (system, "cList", cList);
		MainLoop.initAppropriateSystemField (system, "robotKyle", robotKyle);
		MainLoop.initAppropriateSystemField (system, "pathFileParamFunct", pathFileParamFunct);
		MainLoop.initAppropriateSystemField (system, "pathFileParamRequiermentLibrary", pathFileParamRequiermentLibrary);
		MainLoop.initAppropriateSystemField (system, "textures_available", textures_available);
		MainLoop.initAppropriateSystemField (system, "textures_prices", textures_prices);
	}

	public void showCampagneMenu()
	{
		MainLoop.callAppropriateSystemMethod (system, "showCampagneMenu", null);
	}

	public void launchLevel()
	{
		MainLoop.callAppropriateSystemMethod (system, "launchLevel", null);
	}

	public void backFromCampagneMenu()
	{
		MainLoop.callAppropriateSystemMethod (system, "backFromCampagneMenu", null);
	}

	public void quitGame()
	{
		MainLoop.callAppropriateSystemMethod (system, "quitGame", null);
	}

	public void showSkinMenu()
	{
		MainLoop.callAppropriateSystemMethod (system, "showSkinMenu", null);
	}

	public void backToMain()
	{
		MainLoop.callAppropriateSystemMethod (system, "backToMain", null);
	}

	public void LogName(System.Int32 skin_index)
	{
		MainLoop.callAppropriateSystemMethod (system, "LogName", skin_index);
	}

	public void write_current_skin_index()
	{
		MainLoop.callAppropriateSystemMethod (system, "write_current_skin_index", null);
	}

	public void write_current_player_coins()
	{
		MainLoop.callAppropriateSystemMethod (system, "write_current_player_coins", null);
	}

	public void achat_skin(System.Int32 index)
	{
		MainLoop.callAppropriateSystemMethod (system, "achat_skin", index);
	}

	public void add_skin(System.Int32 skin_value)
	{
		MainLoop.callAppropriateSystemMethod (system, "add_skin", skin_value);
	}

	public void write_current_skins()
	{
		MainLoop.callAppropriateSystemMethod (system, "write_current_skins", null);
	}

}
