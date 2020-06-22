using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraEnergyAPI.API.Interface;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace TerraEnergyAPI.API
{
    public abstract class TileEntityEnergyHandler : ModTileEntity, EnergyProvider, EnergyReceiver, BasicDataStorage
    {
        protected EnergyContainer _energyContainer = new EnergyContainer(50000);

        public EnergyContainer EnergyContainer => _energyContainer;

        public TileEntityEnergyHandler(long capacity) : this(capacity, capacity, capacity)
        {
        }

        public TileEntityEnergyHandler(long capacity, long maxTransfer) : this(capacity, maxTransfer, maxTransfer)
        {
        }

        public TileEntityEnergyHandler(long capacity, long maxTransfer, long maxReceive)
        {
            tag = new TagCompound();
            _energyContainer = new EnergyContainer(capacity, maxTransfer, maxReceive);
        }

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
            return _energyContainer.TransferEnergy(maxTransfer);
        }

        /// <summary>
        /// Send the TagCompound in binary over the network to the client, make thing easier
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="lightSend"></param>
        public sealed override void NetSend(BinaryWriter writer, bool lightSend)
        {
            MemoryStream stream = new MemoryStream(65536);
            EnergyContainer.WriteTagCompound(tag);
            TagIO.ToStream(tag, stream, true);
            writer.Write((ushort)stream.Length);
            writer.Write(stream.ToArray());

        }

        /// <summary>
        /// The client receive the tag compound from the server and sync it
        /// </summary>
        /// <param name="trueReader"></param>
        /// <param name="lightReceive"></param>
        public sealed override void NetReceive(BinaryReader trueReader, bool lightReceive)
        {
            int len = trueReader.ReadUInt16();
            MemoryStream stream = new MemoryStream(trueReader.ReadBytes(len));
            tag = TagIO.FromStream(stream, true);
            ErrorLogger.Log(tag.Count);
            EnergyContainer.ReadTagCompound(tag);
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
