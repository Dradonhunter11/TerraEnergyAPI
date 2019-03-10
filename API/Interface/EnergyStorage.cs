using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraEnergyAPI.API.Interface
{
    public interface EnergyStorage
    {
        long ReceiveEnergy(long maxAmountToReceive);
        long TransfertEnergy(long maxAmountToTransfer);
        long MaxEnergy { get; set; }
        long GetCurrentEnergy();
    }
}
