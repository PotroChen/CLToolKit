using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppRoot : MonoBehaviour {

	public static ResLoader GlobalResLoader = new ResLoader();
	void Awake()
	{
		DontDestroyOnLoad(gameObject);	
		ResMgr.Init();	
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
