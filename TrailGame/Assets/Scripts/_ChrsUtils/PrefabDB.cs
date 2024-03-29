﻿using UnityEngine;
using GameData;
using GameScreen;

[CreateAssetMenu (menuName = "Prefab DB")]
public class PrefabDB : ScriptableObject
{

    [SerializeField] private Player _player;
    public Player Player
    {
        get { return _player; }
    }

    [SerializeField] private GameObject[] _scenes;
    public GameObject[] Scenes
    {
        get { return _scenes; }
    }

    [SerializeField] private Tile _tile;
    public Tile Tile
    {
        get { return _tile; }
    }

    [SerializeField] private PumpTile _pumpTile;
    public PumpTile PumpTile
    {
        get { return _pumpTile; }
    }
}
