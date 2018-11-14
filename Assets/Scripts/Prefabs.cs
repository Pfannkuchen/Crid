using UnityEngine;
using System;

public class Prefabs : SingletonMonoBehaviour<Prefabs>
{
    [HideInInspector] public GameObject empty;
	[HideInInspector] public GameObject spawn;
	[HideInInspector] public GameObject wallSimple;
	[HideInInspector] public GameObject waypointX;

    void Awake()
    {
        empty = 		Resources.Load("Blocks/EmptyPrefab") as GameObject;
		spawn = 		Resources.Load("Blocks/SpawnPrefab") as GameObject;
		wallSimple = 	Resources.Load("Blocks/WallSimplePrefab") as GameObject;
		waypointX = 	Resources.Load("Blocks/WaypointXPrefab") as GameObject;
    }
}