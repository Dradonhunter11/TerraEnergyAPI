using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.IO;

namespace TerraEnergyAPI.API
{
    public interface BasicDataStorage
    {
        TagCompound tag { get; }
        bool HasTagCompound();
        void SetTagCompound(TagCompound tag);
    }
}
