using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace TerraEnergyLibrary.API.Interface
{
    public interface EnergyHandler : EnergyConnection
    {
        long GetEnergyStored(Tile tile);
        long GetMaxEnergyStorage(Tile tile);
    }
}
