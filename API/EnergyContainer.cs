using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraEnergyLibrary.API.Interface;
using Terraria.ModLoader.IO;

namespace TerraEnergyLibrary.API
{
    class EnergyContainer : EnergyStorage
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

        public long GetCurrentEnergy()
        {
            return _currentEnergy;
        }

        public void SetMaxTransferRate(long maxTransfer)
        {
            MaxTransfer = maxTransfer;
            MaxReceiver = maxTransfer;

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

        public void SetEnergyStored(long energyToSet)
        {
            _currentEnergy = energyToSet;
            if (_currentEnergy > MaxEnergy)
            {
                _currentEnergy = MaxEnergy;
            }
        }

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

        public long ReceiveEnergy(long maxAmountToReceive)
        {
            long energyToReceive = Math.Min(_energyCapcity-_currentEnergy, Math.Min(_maxReceive, maxAmountToReceive));
            return energyToReceive;
        }

        public long TransfertEnergy(long maxAmountToTransfer)
        {
            long energyToTransfer = Math.Min(_currentEnergy, Math.Min(_maxTransfer, maxAmountToTransfer));
            return energyToTransfer;
        }

        
    }
}
