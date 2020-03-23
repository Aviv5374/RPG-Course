﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.My.Saving
{
    [System.Serializable]
    public class MySerializableVector3
    {
        float x, y, z;

        public MySerializableVector3(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public Vector3 ToVector()
        {
            return new Vector3(x, y, z);
        }
    }
}
