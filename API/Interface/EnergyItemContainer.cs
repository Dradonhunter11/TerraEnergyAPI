using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace TerraEnergyLibrary.API.Interface
{

    public interface EnergyItemContainer
    {
        long ReceiveEnergy(BetterModItem betterModItem, long maxReceive);
        long TransferEnergy(BetterModItem container, long maxTransfer);
        long GetStoredEnergy(BetterModItem container);
        long GetMaximumStorage(BetterModItem container);
    }
}
