using UnityEngine;
using FYFY;

/// <summary>
/// This system executes new currentActions
/// </summary>
public class CurrentActionExecutor : FSystem {
	
	private Family f_ice = FamilyManager.getFamily(new AnyOfTags("Ice"));
	private Family f_lava = FamilyManager.getFamily(new AnyOfTags("Lava"));


	
	private Family f_wall = FamilyManager.getFamily(new AllOfComponents(typeof(Position)), new AnyOfTags("Wall", "Door"), new AnyOfProperties(PropertyMatcher.PROPERTY.ACTIVE_IN_HIERARCHY));
	private Family f_activableConsole = FamilyManager.getFamily(new AllOfComponents(typeof(Activable),typeof(Position),typeof(AudioSource)));
    private Family f_newCurrentAction = FamilyManager.getFamily(new AllOfComponents(typeof(CurrentAction), typeof(BasicAction)));
	private Family f_player = FamilyManager.getFamily(new AllOfComponents(typeof(ScriptRef)), new AnyOfTags("Player"));

	protected override void onStart()
	{
		f_newCurrentAction.addEntryCallback(onNewCurrentAction);
		Pause = true;
	}

	protected override void onProcess(int familiesUpdateCount)
	{
		// count inaction if a robot have no CurrentAction
		foreach (GameObject robot in f_player)
			if (robot.GetComponent<ScriptRef>().executableScript.GetComponentInChildren<CurrentAction>() == null)
				robot.GetComponent<ScriptRef>().nbOfInactions++;
		Pause = true;
	}

	// each time a new currentAction is added, 
	private void onNewCurrentAction(GameObject currentAction) {
		Pause = false; // activates onProcess to identify inactive robots
		
		CurrentAction ca = currentAction.GetComponent<CurrentAction>();	

		// process action depending on action type
		switch (currentAction.GetComponent<BasicAction>().actionType){
			case BasicAction.ActionType.Forward:
				ApplyForward(ca.agent);
				break;
			case BasicAction.ActionType.TurnLeft:
				ApplyTurnLeft(ca.agent);
				break;
			case BasicAction.ActionType.TurnRight:
				ApplyTurnRight(ca.agent);
				break;
			case BasicAction.ActionType.TurnBack:
				ApplyTurnBack(ca.agent);
				break;
			case BasicAction.ActionType.Wait:
				break;
			case BasicAction.ActionType.Activate:
				Position agentPos = ca.agent.GetComponent<Position>();
				foreach ( GameObject actGo in f_activableConsole){
					if(actGo.GetComponent<Position>().x == agentPos.x && actGo.GetComponent<Position>().y == agentPos.y){
						actGo.GetComponent<AudioSource>().Play();
						// toggle activable GameObject
						if (actGo.GetComponent<TurnedOn>())
							GameObjectManager.removeComponent<TurnedOn>(actGo);
						else
							GameObjectManager.addComponent<TurnedOn>(actGo);
					}
				}
				ca.agent.GetComponent<Animator>().SetTrigger("Action");
				break;
		}
		// notify agent moving
		if (ca.agent.CompareTag("Drone") && !ca.agent.GetComponent<Moved>())
			GameObjectManager.addComponent<Moved>(ca.agent);
	}

	private void ApplyForward(GameObject go){

		bool oniceblock = onIce(go.GetComponent<Position>().x, go.GetComponent<Position>().y);
		//bool checkobstacle = checkObstacle(go.GetComponent<Position>().x, go.GetComponent<Position>().y);
		//Debug.Log("Check obstacle test : " + checkobstacle);
		Debug.Log("Ice block check : " + oniceblock);

		int x = go.GetComponent<Position>().x;
		int y = go.GetComponent<Position>().y;

		switch (go.GetComponent<Direction>().direction){
			case Direction.Dir.North:
				y = y - 1;
				while(onIce(x,y) && !checkObstacle(x, y-1)){
					y = y-1;
				}
				break;
			case Direction.Dir.South:
				y = y + 1;
				while(onIce(x,y) && !checkObstacle(x, y+1)){
					y = y+1;
				}
				break;
			case Direction.Dir.East:
				x = x + 1;
				while(onIce(x,y) && !checkObstacle(x+1, y)){
					x = x + 1;
				}
				break;
			case Direction.Dir.West:
				x = x - 1;
				while(onIce(x,y) && !checkObstacle(x-1, y)){
					x = x - 1;
				}
				break;
		}
		if(!(checkObstacle(x, y) || onLava(x, y))){
			go.GetComponent<Position>().x = x;
			go.GetComponent<Position>().y = y;
		}
	}

	private void ApplyTurnLeft(GameObject go){
		switch (go.GetComponent<Direction>().direction){
			case Direction.Dir.North:
				go.GetComponent<Direction>().direction = Direction.Dir.West;
				break;
			case Direction.Dir.South:
				go.GetComponent<Direction>().direction = Direction.Dir.East;
				break;
			case Direction.Dir.East:
				go.GetComponent<Direction>().direction = Direction.Dir.North;
				break;
			case Direction.Dir.West:
				go.GetComponent<Direction>().direction = Direction.Dir.South;
				break;
		}
	}

	private void ApplyTurnRight(GameObject go){
		switch (go.GetComponent<Direction>().direction){
			case Direction.Dir.North:
				go.GetComponent<Direction>().direction = Direction.Dir.East;
				break;
			case Direction.Dir.South:
				go.GetComponent<Direction>().direction = Direction.Dir.West;
				break;
			case Direction.Dir.East:
				go.GetComponent<Direction>().direction = Direction.Dir.South;
				break;
			case Direction.Dir.West:
				go.GetComponent<Direction>().direction = Direction.Dir.North;
				break;
		}
	}

	private void ApplyTurnBack(GameObject go){
		switch (go.GetComponent<Direction>().direction){
			case Direction.Dir.North:
				go.GetComponent<Direction>().direction = Direction.Dir.South;
				break;
			case Direction.Dir.South:
				go.GetComponent<Direction>().direction = Direction.Dir.North;
				break;
			case Direction.Dir.East:
				go.GetComponent<Direction>().direction = Direction.Dir.West;
				break;
			case Direction.Dir.West:
				go.GetComponent<Direction>().direction = Direction.Dir.East;
				break;
		}
	}

	private bool checkObstacle(int x, int z){
		foreach( GameObject go in f_wall){
			if(go.GetComponent<Position>().x == x && go.GetComponent<Position>().y == z)
				return true;
		}
		return false;
	}

	private bool onIce(int x, int y){
		// On ignore la glace si l'utilisateur a le skin bleu
		if(PlayerPrefs.GetInt("currentSkinIndex", 0) == 1)
			return false;
		foreach( GameObject go in f_ice){
			if(go.GetComponent<Position>().x == x && go.GetComponent<Position>().y == y)
				return true;
		}
		return false;
	}

	private bool onLava(int x, int y){
		// On ignore la lave si l'utilisateur a le skin de feu
		if(PlayerPrefs.GetInt("currentSkinIndex", 0) == 2)
			return false;

		foreach( GameObject go in f_lava){
			if(go.GetComponent<Position>().x == x && go.GetComponent<Position>().y == y)
				return true;
		}
		return false;
	}
}
