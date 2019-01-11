using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraEnergyLibrary.API.Interface;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace TerraEnergyLibrary.API
{
    abstract class ItemEnergyContainer : ModItem, EnergyItemContainer, BetterModItem
    {
        public override bool CloneNewInstances
        {
            get => false;
        }

        public readonly string ENERGY = "Energy";
        protected long _capacity;
        protected long maxReceive;
        protected long maxTransfer;
        
        protected TagCompound tagCompound = new TagCompound();

        public long Capacity
        {
            get => _capacity;
            set => _capacity = value;
        }
        public TagCompound tag { get; internal set; }

        public sealed override TagCompound Save()
        {
            TagCompound tag = new TagCompound();
            NewSave(tag);
            tag[ENERGY] = tagCompound.GetAsLong(ENERGY);
            return tagCompound;
        }



        public sealed override void Load(TagCompound tag)
        {
            tagCompound[ENERGY] = tag.GetAsLong(ENERGY);
            NewLoad(tag);
        }

        public bool HasTagCompound()
        {
            return tag != null;
        }

        public void SetTagCompound(TagCompound tag)
        {
            this.tag = tag;
        }

        public long ReceiveEnergy(BetterModItem container, long maxReceive)
        {
            if (!container.HasTagCompound() || !container.tag.ContainsKey(ENERGY))
            {
                container.SetTagCompound(new TagCompound());
            }

            long stored = Math.Min(container.tag.GetAsLong(ENERGY), GetMaximumStorage(container));
            long energyReceived = Math.Min(_capacity - stored, Math.Min(this.maxReceive, maxReceive));

            stored += energyReceived;
            container.tag[ENERGY] = stored;

            return energyReceived;
        }

        public long TransferEnergy(BetterModItem container, long maxTransfer)
        {
            if (container.tag == null || !container.tag.ContainsKey(ENERGY))
            {
                return 0;
            }

            long stored = Math.Min(container.tag.GetAsLong(ENERGY), GetMaximumStorage(container));
            long energyExtracted = Math.Min(stored, Math.Min(this.maxTransfer, maxTransfer));

            stored -= energyExtracted;
            container.tag[ENERGY] = stored;
            return energyExtracted;
        }

        public long GetStoredEnergy(BetterModItem container)
        {
            if (container.tag == null || !container.tag.ContainsKey(ENERGY))
            {
                return 0;
            }

            return Math.Min(container.tag.GetAsLong(ENERGY), GetMaximumStorage(container));
        }

        public long GetMaximumStorage(BetterModItem container)
        {
            return Capacity;
        }

        public virtual void NewSave(TagCompound tag)
        {

        }

        public virtual void NewLoad(TagCompound tag)
        {

        }
    }
}
