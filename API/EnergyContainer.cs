using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraEnergyAPI.API.Interface;
using Terraria.ModLoader.IO;

namespace TerraEnergyAPI.API
{
    public class EnergyContainer : EnergyStorage
    {
        protected long _energyCapcity;
        protected long _currentEnergy;
        protected long _maxReceive;
        protected long _maxTransfer;

        public EnergyContainer(long capacity) : this(capacity, capacity, capacity)
        {
        }

        public EnergyContainer(long capacity, long maxTransfer) : this(capacity, maxTransfer, maxTransfer)
        {
        }

        public EnergyContainer(long capacity, long maxTransfer, long maxReceive)
        {
            this._energyCapcity = capacity;
            this.MaxTransfer = maxTransfer;
            this.MaxReceiver = maxReceive;
        }

        public long MaxReceiver
        {
            get => _maxReceive;
            set => _maxReceive = value;
        }

        public long MaxTransfer
        {
            get => _maxTransfer;
            set => _maxTransfer = value;
        }

        public long MaxEnergy
        {
            get => _energyCapcity;
            set
            {
                _energyCapcity = value;
                if (_currentEnergy > _energyCapcity)
                {
                    _currentEnergy = _energyCapcity;
                }
            }
        }

        /// <summary>
        /// Give the current energy amount
        /// </summary>
        /// <returns></returns>
        public long GetCurrentEnergy()
        {
            return _currentEnergy;
        }

        /// <summary>
        /// Set the max transfer/receiving rate of the container
        /// </summary>
        /// <param name="maxTransfer"></param>
        public void SetMaxTransferRate(long maxTransfer)
        {
            MaxTransfer = maxTransfer;
            MaxReceiver = maxTransfer;

        }

        /// <summary>
        /// Mainly used for multiplayer.
        /// Currently bugged out, you cannot replace a value if it's already in there. Might be fixed in future tml version? Otherwise use the fixer
        /// </summary>
        /// <param name="tag"></param>
        public void WriteTagCompound(TagCompound tag)
        {
            tag["Energy"] = _currentEnergy;
        }

        /// <summary>
        /// Mainly used for multiplayer.
        /// Currently bugged out, you cannot replace a value if it's already in there. Might be fixed in future tml version? Otherwise use the fixer
        /// </summary>
        /// <param name="tag"></param>
        public void ReadTagCompound(TagCompound tag)
        {
            _currentEnergy = tag.GetAsLong("Energy");
        }

        public void Save(TagCompound tag)
        {
            if (_currentEnergy < 0)
            {
                _currentEnergy = 0;
            }
            tag.Add("currentEnergy", _currentEnergy);
        }

        public void Load(TagCompound tag)
        {
            _currentEnergy = tag.GetLong("currentEnergy");
            if (_currentEnergy > _energyCapcity)
            {
                _currentEnergy = _energyCapcity;
            }
        }

        /// <summary>
        /// Allow to set current energy level in the container
        /// </summary>
        /// <param name="energyToSet"></param>
        public void SetEnergyStored(long energyToSet)
        {
            _currentEnergy = energyToSet;
            if (_currentEnergy > MaxEnergy)
            {
                _currentEnergy = MaxEnergy;
            }
        }

        /// <summary>
        /// Basically a Receive and Transfer method in one!
        /// </summary>
        /// <param name="energy"></param>
        public void ModifyEnergy(int energy)
        {
            this._currentEnergy += energy;
            if (_currentEnergy > _energyCapcity)
            {
                _currentEnergy = _energyCapcity;
            } else if (_currentEnergy < 0)
            {
                _currentEnergy = 0;
            }
        }

        /// <summary>
        /// The amount of energy that the container will receive from another container
        /// </summary>
        /// <param name="maxAmountToReceive"></param>
        /// <returns></returns>
        public long ReceiveEnergy(long maxAmountToReceive)
        {
            long energyToReceive = Math.Min(_energyCapcity-_currentEnergy, Math.Min(_maxReceive, maxAmountToReceive));
            _currentEnergy += energyToReceive;
            return energyToReceive;
        }

        /// <summary>
        /// The amount of energy that the container will transfer to another container
        /// </summary>
        /// <param name="maxAmountToTransfer"></param>
        /// <returns></returns>
        public long TransferEnergy(long maxAmountToTransfer)
        {
            long energyToTransfer = Math.Min(_currentEnergy, Math.Min(_maxTransfer, maxAmountToTransfer));
            _currentEnergy += energyToTransfer;
            return energyToTransfer;
        }

        
    }
}
