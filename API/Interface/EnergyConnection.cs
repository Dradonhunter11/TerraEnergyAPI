using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace TerraEnergyAPI.API.Interface
{
    public interface EnergyConnection
    {
        bool CanConnect(Tile tile);
    }
}
