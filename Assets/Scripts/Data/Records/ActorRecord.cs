﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    [System.Serializable]
    public class ActorRecord : Record
    {
        public int ProfileID;
        public int Level;
        public int Exp;
        public Vector3 Position;
    }
}