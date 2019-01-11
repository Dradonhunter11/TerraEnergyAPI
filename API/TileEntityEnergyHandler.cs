using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraEnergyLibrary.API.Interface;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace TerraEnergyLibrary.API
{
    abstract class TileEntityEnergyHandler : ModTileEntity, EnergyProvider, EnergyReceiver, BasicDataStorage
    {

        protected EnergyContainer _energyContainer = new EnergyContainer(50000);

        public TagCompound tag { get; internal set; }

        public sealed override TagCompound Save()
        {
            TagCompound tag = new TagCompound();
            _energyContainer.Save(tag);
            NewSave(tag);
            return tag;
        }



        public sealed override void Load(TagCompound tag)
        {
            _energyContainer.Load(tag);
            NewLoad(tag);
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

        public sealed override void NetSend(BinaryWriter writer, bool lightSend)
        {
            var stream = new MemoryStream();
            TagIO.ToStream(tag, stream);
            writer.Write(stream.ToArray());
        }

        public sealed override void NetReceive(BinaryReader reader, bool lightReceive)
        {
            tag = TagIO.Read(reader);
        }

        public virtual void NewSave(TagCompound tag)
        {

        }

        public virtual void NewLoad(TagCompound tag)
        {

        }

        
        public bool HasTagCompound()
        {
            return tag != null;
        }

        public void SetTagCompound(TagCompound tag)
        {
            this.tag = tag;
        }
    }
}
