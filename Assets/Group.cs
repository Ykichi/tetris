using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour {

	bool isValidGridPos(){
		foreach(Transform child in transform){
			Vector2 v = GameGrid.roundVec2(child.position);

			//Not inside border
			if(!GameGrid.insideBorder(v)){
				return false;
			}

			//Block in grid cell (and not part of same group)
			if(GameGrid.grid[(int)v.x, (int)v.y] != null && GameGrid.grid[(int)v.x, (int)v.y].parent != transform){
				return false;
			}
		}
		
		return true;
	}

	void updateGrid(){
		//Remove old children from grid
		for(int y = 0; y < GameGrid.h; ++y){
			for(int x = 0; x < GameGrid.w; ++x){
				if(GameGrid.grid[x, y] != null){
					if(GameGrid.grid[x, y].parent == transform){
						GameGrid.grid[x, y] = null;
					}
				}
			}
		}

		//Add new children to grid
		foreach(Transform child in transform){
			Vector2 v = GameGrid.roundVec2(child.position);
			GameGrid.grid[(int)v.x, (int)v.y] = child;
		}
	}

	//Time since last gravity tick
	float lastFall = 0;

	// Use this for initialization
	void Start () {
		//Default position not valid? Then it's game over
		if(!isValidGridPos()){
			Debug.Log("Game Over");
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Move left
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			//Modify position
			transform.position += new Vector3(-1, 0, 0);

			//See if valid
			if(isValidGridPos()){
				updateGrid();
			}
			else{
				transform.position += new Vector3(1, 0, 0);
			}
		}
		
		//Move right
		else if(Input.GetKeyDown(KeyCode.RightArrow)){
			//Modify position
			transform.position += new Vector3(1, 0, 0);

			//See if valid
			if(isValidGridPos()){
				updateGrid();
			}
			else{
				transform.position += new Vector3(-1, 0, 0);
			}
		}

		//Rotate
		else if(Input.GetKeyDown(KeyCode.UpArrow)){
			transform.Rotate(0, 0, -90);

			//See if valid
			if(isValidGridPos()){
				updateGrid();
			}
			else{
				transform.Rotate(0, 0, 90);
			}
		}

		//Move downwards and fall

		else if(Input.GetKeyDown(KeyCode.DownArrow) || Time.time - lastFall >= 1){
			//Modify position
			transform.position += new Vector3(0, -1, 0);

			//See if valid
			if(isValidGridPos()){
				updateGrid();
			}
			else{
				transform.position += new Vector3(0, 1, 0);

				//Clear filled horizontal lines
				GameGrid.deleteFullRaws();

				//Spawn next group
				FindObjectOfType<Spawner>().spawNext();

				//Disable script
				enabled = false;
			}

			lastFall = Time.time;
		}
	}
}
