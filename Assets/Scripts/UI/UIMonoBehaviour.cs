﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public abstract class UIMonoBehaviour : MonoBehaviour
    {
        public abstract void Redraw(bool enable = true);
        public abstract void EnableUI(bool enable);
        public abstract void UpdateUI();
    }
}