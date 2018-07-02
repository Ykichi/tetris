using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject[] groups;

	public void spawNext(){
		//Random index
		int i = Random.Range(0,groups.Length);

		//Spawn group at current position
		Instantiate(groups[i], transform.position, Quaternion.identity);
	}

	// Use this for initialization
	void Start () {
		//Spwan initial group
		spawNext();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
