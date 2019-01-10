using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraEnergyLibrary.API.Interface;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace TerraEnergyLibrary.API
{
    abstract class TileEntityEnergyHandler : ModTileEntity, EnergyProvider, EnergyReceiver
    {

        protected EnergyContainer _energyContainer = new EnergyContainer(50000);

        public sealed override TagCompound Save()
        {
            TagCompound tag = new TagCompound();
            _energyContainer.Save(tag);
            return base.Save();
        }



        public sealed override void Load(TagCompound tag)
        {
            _energyContainer.Load(tag);
        }

        

        public virtual bool CanConnect(Tile tile)
        {
            return true;
        }

        public long GetEnergyStored(Tile tile)
        {
            return _energyContainer.GetCurrentEnergy();
        }

        public long GetMaxEnergyStorage(Tile tile)
        {
            return _energyContainer.MaxEnergy;
        }

        public long ReceiveEnergy(Tile tile, long maxTransfer)
        {
            return _energyContainer.ReceiveEnergy(maxTransfer);
        }

        public long TransferEnergy(Tile tile, long maxTransfer)
        {
            return _energyContainer.TransfertEnergy(maxTransfer);
        }

        public virtual void NewSave(TagCompound tag)
        {

        }

        public virtual void NewLoad(TagCompound tag)
        {

        }
    }
}
