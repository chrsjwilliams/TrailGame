﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GameData;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MapManager : MonoBehaviour
{
    [SerializeField] string mapLabel;
    [SerializeField] string betaMapsLabel;
    [SerializeField] string tileTestLabel;
    TaskManager tm = new TaskManager();

    [SerializeField] private List<MapData> _maps = new List<MapData>();
    public ReadOnlyCollection<MapData> Maps { get { return _maps.AsReadOnly(); } }

    [SerializeField] private List<MapData> _tileTestMaps = new List<MapData>();
    public ReadOnlyCollection<MapData> TileTestMaps { get { return _tileTestMaps.AsReadOnly(); } }

    public void Init()
    {
        LoadMapsAddressables(() => { Debug.Log("Finished Loading Normal Maps"); });
        LoadTileTestMapsAddressables(() => { Debug.Log("Finished Loading Tile Test Maps"); });

    }

    public void LoadMapsAddressables(Action callback)
    {

        var op = Addressables.LoadResourceLocationsAsync(mapLabel);
        List<MapData> loadedMaps = new List<MapData>();
        op.Completed += ophandle =>
        {
            var result = ophandle.Result;
            
            var leftToLoad = ophandle.Result.Count;
            foreach (var readLocation in ophandle.Result)
            {
                var loaderOp = Addressables.LoadAssetAsync<MapData>(readLocation);
                loaderOp.Completed += opHandle =>
                {
                    if (!loadedMaps.Contains(loaderOp.Result))
                    {

                        loadedMaps.Add(loaderOp.Result);
                    }

                    leftToLoad--;
                    if (leftToLoad == 0)
                    {
                        _maps = loadedMaps.OrderBy(x => x.name).ToList();
                        callback?.Invoke();
                    }
                };
            }
        };
    }

    public void LoadTileTestMapsAddressables(Action callback)
    {

        var op = Addressables.LoadResourceLocationsAsync(tileTestLabel);
        List<MapData> loadedMaps = new List<MapData>();
        op.Completed += ophandle =>
        {
            var result = ophandle.Result;

            var leftToLoad = ophandle.Result.Count;
            foreach (var readLocation in ophandle.Result)
            {
                var loaderOp = Addressables.LoadAssetAsync<MapData>(readLocation);
                loaderOp.Completed += opHandle =>
                {
                    if (!loadedMaps.Contains(loaderOp.Result))
                    {

                        loadedMaps.Add(loaderOp.Result);
                    }

                    leftToLoad--;
                    if (leftToLoad == 0)
                    {
                        _tileTestMaps = loadedMaps.OrderBy(x => x.name).ToList();
                        callback?.Invoke();
                    }
                };
            }
        };
    }


    void Update()
    {
        tm.Update();
    }
}

public struct MapInfo
{

}
