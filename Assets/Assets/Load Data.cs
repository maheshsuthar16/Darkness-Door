
 using UnityEngine;

[CreateAssetMenu(fileName = "Level ", menuName = "Scriptable Objects/LoadData")]
public class LoadData : ScriptableObject
{
     public string LevelName;
     public Vector2 spawnPlayerPosition;
     public Vector2 exitPosition;
     public Vector2[] coin;
     public Vector2[]  enemies;
     public GameObject  levelPrefabs;
    

}
