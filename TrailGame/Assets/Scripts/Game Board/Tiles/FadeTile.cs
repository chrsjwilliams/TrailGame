using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GameData
{
    public class FadeTile : Tile
    {
        private Direction direction;

        private int _fadeCount;
        private bool _diffused = false;
        
        public void Init(MapCoord mapCoord, Tile tile, Ink ink, int fadeCount)
        {
            Coord = mapCoord;
            canTraverse = true;
            sr = tile.Sprite;
            tileInk = ink;
            _fadeCounter = tile.FadeCounter;
            _fadeCount = fadeCount;
            _fadeCounter.text = _fadeCount + "";
            direction = Direction.NONE;
            _diffused = false;
            Services.EventManager.Register<SwipeEvent>(OnSwipe);
            SetColor(tileInk, isInit: true);
        }

        private void OnDestroy()
        {
            Services.EventManager.Unregister<SwipeEvent>(OnSwipe);
        }

        private void OnSwipe(SwipeEvent e)
        {
            if (AxisSwipeChange(e) && !_diffused)
            {
                _fadeCount -= 1;
                if (_fadeCount < 0)
                {
                    Ink black = new Ink(ColorMode.BLACK);
                    SetColor(black);
                    _fadeCount = 0;
                    Services.EventManager.Unregister<SwipeEvent>(OnSwipe);
                }
                _fadeCounter.text = _fadeCount + "";
            }
        }
        
        public bool AxisSwipeChange(SwipeEvent e)
        {
            return ((direction == Direction.LEFT || direction == Direction.RIGHT) &&
                    (e.gesture.CurrentDirection == Direction.UP || e.gesture.CurrentDirection == Direction.DOWN)) ||
                   ((direction == Direction.UP || direction == Direction.DOWN) &&
                    (e.gesture.CurrentDirection == Direction.LEFT || e.gesture.CurrentDirection == Direction.RIGHT)) ||
                   (direction == Direction.NONE && e.gesture.CurrentDirection != Direction.NONE);
        }

        public override void SetColor(Ink ink, bool isInit = false)
        {
            if (isInit)
            {
                base.SetColor(ink, isInit);
            }
            else
            {
                Services.EventManager.Unregister<SwipeEvent>(OnSwipe);
                _diffused = true;
                FadeCounter.DOFade(0, 0.25f)
                    .SetEase(Ease.InExpo)
                    .OnComplete(() =>
                    {
                        base.SetColor(ink, isInit);
                    });
            }
        }
    }
}