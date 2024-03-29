using System;
using System.Collections;
using System.Collections.Generic;
using GameData;
using GameScreen;
using UnityEngine;

public class MapSelectSceneScript : Scene<TransitionData>
{

    [SerializeField] private Transform _mapContent;
    [SerializeField] private MapButton _mapButtonPrefab;

    [SerializeField] private Transform _tileTypeTestContent;


    internal override void OnEnter(TransitionData data)
    {
        // load levels
        foreach(MapData mapData in Services.MapManager.Maps)
        {
            MapButton mapButton = Instantiate(_mapButtonPrefab, _mapContent);
            bool finished = Convert.ToBoolean(PlayerPrefs.GetInt(mapData.name));
            MapButton.MapStatus status = finished ? MapButton.MapStatus.COMPLETED : MapButton.MapStatus.NOT_COMPLETED;
            mapButton.Init(mapData, status);
            mapButton.Pressed += OnMapSelected;
        }

        foreach (MapData mapData in Services.MapManager.TileTestMaps)
        {
            MapButton mapButton = Instantiate(_mapButtonPrefab, _tileTypeTestContent);
            bool finished = Convert.ToBoolean(PlayerPrefs.GetInt(mapData.name));
            MapButton.MapStatus status = finished ? MapButton.MapStatus.COMPLETED : MapButton.MapStatus.NOT_COMPLETED;
            mapButton.Init(mapData, status);
            mapButton.Pressed += OnMapSelected;
        }

    }

    internal override void OnExit()
    {

    }

    public void OnMapSelected(MapData data)
    {
        TransitionData tData = new TransitionData();
        tData.SelecetdMap = data;
        Services.Scenes.Swap<GameSceneScript>(tData);
    }

    public void BackButtonPressed()
    {
        Services.Scenes.Swap<TitleSceneScript>();
    }
}
