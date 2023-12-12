using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.Examples;
using UnityEditor;
using Sirenix.Utilities;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

namespace GameData
{
    [CreateAssetMenu(fileName = "MapData", menuName = "ScriptableObjects/MapData", order = 1)]
    public class MapData : SerializedScriptableObject
    {
        public bool finished;
        public string mapName;
        [TextArea] public string MapGoal;
        public GameBoard.ColorType BoardType;
        public Vector2 BoardSize;

        public Vector2 PlayerStartPos;
        public List<Vector2> ImpassableMapCoords;
        public List<TileData> tileData;



        public List<ColorGoal> colorGoals;


        [System.Serializable]
        public struct ColorGoal
        {
            public ColorMode colorMode;
            public int amount;
        }

        public bool HasTileData(Vector2 coord)
        {
            foreach (TileData tile in tileData)
            {
                if (tile.coord == coord)
                    return true;
            }

            return false;
        }

        public TileData GetTile(Vector2 coord)
        {
            foreach (TileData tile in tileData)
            {
                if (tile.coord == coord)
                    return tile;
            }

            return new TileData { coord = new Vector2(-1, -1) };
        }
    }

    [System.Serializable]
    public struct TileData 
    {
        [TableColumnWidth(30, Resizable = false)]
        public Vector2 coord;
        
        [HorizontalGroup("Group 1"), LabelWidth(100)]
        public bool hasCustomInk;
        [ShowIf("hasCustomInk")]
        [HorizontalGroup("Group 1"), LabelWidth(50)]
        public Ink ink;
        [ShowIf("hasCustomInk")]
        [HorizontalGroup("Group 1"), LabelWidth(50)]
        public bool isPumpTile;

        [HorizontalGroup("Group 2"), LabelWidth(100)]
        public bool isPivotTile;
        [ShowIf("isPivotTile")]
        [HorizontalGroup("Group 2"), LabelWidth(100)]
        public Direction PivotDirection;

        [HorizontalGroup("Group 3"), LabelWidth(100)]
        public bool isWrapTile;
        [ShowIf("isWrapTile")]
        [HorizontalGroup("Group 3"), LabelWidth(100)]
        public Direction WrapDirection;

        [HorizontalGroup("Group 4"), LabelWidth(100)]
        public bool isInvertTile;

        [HorizontalGroup("Group 5"), LabelWidth(100)]
        public bool isFillTile;
        [ShowIf("isFillTile")]
        [HorizontalGroup("Group 5"), LabelWidth(50)]
        public FillType fillType;
        [HorizontalGroup("Group 5"), LabelWidth(50)]
        [ShowIf("isFillTile")]
        public ColorMode fillColor;

        
    }
}