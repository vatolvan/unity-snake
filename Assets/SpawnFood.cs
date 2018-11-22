﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour {
    public GameObject foodPrefab;

    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;

	// Use this for initialization
	void Start () {
        InvokeRepeating("Spawn", 3, 4);
	}

    void Spawn () {
        int y = (int)Random.Range(borderTop.position.y, 
                                  borderBottom.position.y);
        int x = (int)Random.Range(borderLeft.position.x, 
                                  borderRight.position.x);

        Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity);
    }

}
