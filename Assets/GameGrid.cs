﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour {

	//The grid itself
	public static int w = 10;
	public static int h = 20;
	public static Transform[,] grid = new Transform[w, h];
	public static Vector2 roundVec2(Vector2 v){
		return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
	}
	public static bool insideBorder(Vector2 pos){
		return ((int)pos.x >= 0 && (int)pos.x < w && (int)pos.y >= 0);
	}

	public static void deleteRaw(int y){
		for(int x = 0; x < w; ++x){
			Destroy(grid[x, y].gameObject);
			grid[x, y] = null;
		}
	}

	public static void decreseRaw(int y){
		for(int x = 0; x < w; ++x){
			if(grid[x, y] != null){
				//Move one towards bottom
				grid[x, y - 1] = grid[x, y];
				grid[x, y] = null;

				//Update block position
				grid[x, y - 1].position += new Vector3(0, -1, 0);
			}
		}
	}

	public static void decreseRawAbove(int y){
		for(int i = y; i < h; ++i){
			decreseRaw(i);
		}
	}

	public static bool isRowFull(int y){
		for(int x = 0; x < w; ++x){
			if(grid[x, y] == null){
				return false;
			}
		}
		return true;
	}

	public static void deleteFullRaws(){
		for(int y = 0; y < h; ++y){
			if(isRowFull(y)){
				deleteRaw(y);
				decreseRawAbove(y + 1);
				--y;
			}
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
