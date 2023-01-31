﻿using R2API;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArsonistMod.Modules
{
    public static class Damage
    {
        internal static DamageAPI.ModdedDamageType arsonistStickyDamageType;

        internal static void SetupModdedDamage()
        {
            arsonistStickyDamageType = DamageAPI.ReserveDamageType();
        }
    }
}