using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using GameData;

namespace GameScreen
{
    public class GameScreenUIController : MonoBehaviour
    {

        [SerializeField] ColorGoal_UI colorGoalPrefab;
        [SerializeField] Transform colorGoalParent;
        [SerializeField, ReadOnly] List<ColorGoal_UI> colorGoals = new List<ColorGoal_UI>();
        [SerializeField] RectTransform canvas;
        [SerializeField] GameOverBanner gameOverBanner;

        MapData map;

        [SerializeField] private bool uiSet = false;

        public void SetGameUI(MapData mapData)
        {
            gameOverBanner.HideBanner();


            map = mapData;
            for (int i = 0; i < mapData.colorGoals.Count; i++)
            {
                var colorGoal = Instantiate(colorGoalPrefab, colorGoalParent);

               
                colorGoal.Init(Services.ColorManager.GetColor(mapData.colorGoals[i].colorMode),
                                Services.Board.CurrentFillAmount[(int)mapData.colorGoals[i].colorMode],mapData.colorGoals[i].amount);
                float xPos = Screen.width * ((i + 1) / (float)(mapData.colorGoals.Count + 1));
                colorGoal.rectTransform.localPosition = new Vector3(xPos, 0, 0);

                colorGoals.Add(colorGoal);
            }

            uiSet = true;
        }

        private void OnDestroy()
        {
  
        }

        public void DestroyGameUI()
        {

        }

        public bool IsGoalMet()
        {
            if (!uiSet) return false;

            for (int i = 0; i < colorGoals.Count; i++)
            {
                if (!colorGoals[i].IsGoalMet())
                    return false;
            }

            gameOverBanner.ShowBanner();

            return true;
        }

        void Update()
        {
            if (!uiSet) return;

            for (int i = 0; i < colorGoals.Count; i++)
            {
                colorGoals[i].UpdateText(Services.Board.CurrentFillAmount[(int)map.colorGoals[i].colorMode],map.colorGoals[i].amount);
                
            }
        }

    }
}