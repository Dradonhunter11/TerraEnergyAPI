using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace TerraEnergyAPI.API.Interface
{
    public interface EnergyProvider : EnergyHandler
    {
        long TransferEnergy(Tile tile, long maxTransfer);
    }
}
