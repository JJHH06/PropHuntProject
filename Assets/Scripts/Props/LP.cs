﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LP : MonoBehaviour
{

    public int life = 10;
    
    public void getHit()
    {
        life--;
        if (life <= 0)
            Destroy(gameObject);
    }
}
