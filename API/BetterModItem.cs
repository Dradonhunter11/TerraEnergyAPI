﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.IO;

namespace TerraEnergyLibrary.API
{
    public interface BetterModItem
    {
        TagCompound tag { get; }
        bool HasTagCompound();
        void SetTagCompound(TagCompound tag);
    }
}
