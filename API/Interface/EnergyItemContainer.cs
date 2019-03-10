using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace TerraEnergyAPI.API.Interface
{

    public interface EnergyItemContainer
    {
        long ReceiveEnergy(BasicDataStorage betterModItem, long maxReceive);
        long TransferEnergy(BasicDataStorage container, long maxTransfer);
        long GetStoredEnergy(BasicDataStorage container);
        long GetMaximumStorage(BasicDataStorage container);
    }
}
