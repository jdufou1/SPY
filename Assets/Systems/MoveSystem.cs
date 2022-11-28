﻿using UnityEngine;
using FYFY;

/// <summary>
/// Manage position and Direction component to move agent accordingly
/// </summary>
public class MoveSystem : FSystem {

	private Family f_movable = FamilyManager.getFamily(new AllOfComponents(typeof(Position),typeof(Direction)));

	public float turnSpeed;
	public float moveSpeed;
	public AudioClip footSlow;
	public AudioClip footSpeed;
	private GameData gameData;

	protected override void onStart()
	{
		GameObject go = GameObject.Find("GameData");
		if (go != null)
			gameData = go.GetComponent<GameData>();
		foreach (GameObject movable in f_movable)
			initAgentDirection(movable);
		f_movable.addEntryCallback(initAgentDirection);
	}

	private void initAgentDirection(GameObject agent)
    {
		switch (agent.GetComponent<Direction>().direction)
		{
			case Direction.Dir.North:
				agent.transform.rotation = Quaternion.Euler(0, -90, 0);
				break;
			case Direction.Dir.East:
				agent.transform.rotation = Quaternion.Euler(0, 0, 0);
				break;
			case Direction.Dir.West:
				agent.transform.rotation = Quaternion.Euler(0, 180, 0);
				break;
			case Direction.Dir.South:
				agent.transform.rotation = Quaternion.Euler(0, 90, 0);
				break;
		}
	}

	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {

		foreach (GameObject go in f_movable)
		{
			// Manage position
			if (Mathf.Abs(go.transform.localPosition.z / 3 - go.GetComponent<Position>().x) > 0.01f || Mathf.Abs(go.transform.localPosition.x / 3 - go.GetComponent<Position>().y) > 0.01f)
			{
				go.transform.localPosition = Vector3.MoveTowards(go.transform.localPosition, new Vector3(go.GetComponent<Position>().y * 3, go.transform.localPosition.y, go.GetComponent<Position>().x * 3), moveSpeed * gameData.gameSpeed_current * Time.deltaTime);
				if (go.GetComponent<Animator>() && go.tag == "Player")
				{
					if(PlayerPrefs.GetInt("lava",0) == 1)
					{
						//Debug.Log("Oh la glissadeeeee !");
						go.GetComponent<Animator>().SetFloat("Walk", -1f);
						go.GetComponent<Animator>().SetFloat("Run", -1f);
						go.GetComponent<Animator>().SetFloat("Slide", -1f);
						go.GetComponent<Animator>().SetFloat("Burn", 1f);
					}
					else if(PlayerPrefs.GetInt("ice",0) == 1)
					{
						//Debug.Log("Oh la glissadeeeee !");
						go.GetComponent<Animator>().SetFloat("Walk", -1f);
						go.GetComponent<Animator>().SetFloat("Run", -1f);
						go.GetComponent<Animator>().SetFloat("Burn", -1f);
						go.GetComponent<Animator>().SetFloat("Slide", 1f);
					}
					else if (gameData.gameSpeed_current == gameData.gameSpeed_default)
					{
						go.GetComponent<Animator>().SetFloat("Run", -1f);
						go.GetComponent<Animator>().SetFloat("Slide", -1f);
						go.GetComponent<Animator>().SetFloat("Burn", -1f);
						go.GetComponent<Animator>().SetFloat("Walk", 1f);
					}
					else
					{
						go.GetComponent<Animator>().SetFloat("Walk", -1f);
						go.GetComponent<Animator>().SetFloat("Slide", -1f);
						go.GetComponent<Animator>().SetFloat("Burn", -1f);
						go.GetComponent<Animator>().SetFloat("Run", 1f);
					}
				}
			}
			else
			{
				if (go.GetComponent<Animator>() && go.tag == "Player")
				{
					go.GetComponent<Animator>().SetFloat("Walk", -1f);
					go.GetComponent<Animator>().SetFloat("Run", -1f);
					go.GetComponent<Animator>().SetFloat("Slide", -1f);
					go.GetComponent<Animator>().SetFloat("Burn", -1f);
				}
			}

			// Manage orientation
			Quaternion target = Quaternion.Euler(0, 0, 0);
			switch (go.GetComponent<Direction>().direction)
			{
				case Direction.Dir.North:
					target = Quaternion.Euler(0, -90, 0);
					break;
				case Direction.Dir.East:
					target = Quaternion.Euler(0, 0, 0);
					break;
				case Direction.Dir.West:
					target = Quaternion.Euler(0, 180, 0);
					break;
				case Direction.Dir.South:
					target = Quaternion.Euler(0, 90, 0);
					break;
			}
			if (target.eulerAngles.y != go.transform.eulerAngles.y)
			{
				go.transform.rotation = Quaternion.RotateTowards(go.transform.rotation, target, turnSpeed * gameData.gameSpeed_current * Time.deltaTime);
				if (go.GetComponent<Animator>() && go.tag == "Player")
					go.GetComponent<Animator>().SetFloat("Rotate", 1f);
			}
			else
				if (go.GetComponent<Animator>() && go.tag == "Player")
					go.GetComponent<Animator>().SetFloat("Rotate", -1f);
		}
	}
}