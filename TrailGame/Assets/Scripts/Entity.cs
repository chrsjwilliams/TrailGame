﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{

    [SerializeField]
    protected Swipe swipe;
    public Swipe.Direction direction { get; protected set; }

    public float moveSpeed { get; protected set; }
    public float arriveSpeed { get; protected set; }

    public Ink Ink { get; protected set; }
    public ColorMode CurrentColorMode { get; protected set; }

    public bool canMove { get; protected set; }
    public MapCoord coord { get; protected set; }
    public abstract void Init(MapCoord c);

    
}
