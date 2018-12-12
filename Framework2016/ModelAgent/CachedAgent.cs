using AIFramework;
using AIFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAgent
{
    class CachedAgent : Agent
    {

        FakePropertyProvider fakeStorage;
        //The original agent type
        private Type originalType;

        /// <summary>C:\Users\PalleRosendahl\Desktop\dsafsadf\ModelAgent\CachedAgent.cs
        /// Returns the type of the origional agent
        /// </summary>
        public Type OriginalType
        {
            get { return originalType; }
        }

        public override string EntityType
        {
            get { return originalType.Name; }
        }

        public CachedAgent(Agent realAgent, FakePropertyProvider propertyStorage)
            : base(propertyStorage)
        {
            fakeStorage = propertyStorage;
            //Set original agent to mathc the original agent type
            originalType = realAgent.GetType();
            //Set id to the original agent
            fakeStorage.Id = Guid.Parse(realAgent.Id.ToString());
            //Update agent information to match original agent
            UpdateInformation(realAgent);
        }
        /// <summary>
        /// Updates the information in the cached agent to match agent
        /// </summary>
        /// <param name="realAgent">Agent to use a basis for the information</param>
        public void UpdateInformation(Agent realAgent)
        {
            fakeStorage.MovementSpeed = realAgent.MovementSpeed;
            fakeStorage.ProcreationCountDown = realAgent.ProcreationCountDown;
            fakeStorage.Strength = realAgent.Strength;
            fakeStorage.Position = new AIVector(realAgent.Position.X, realAgent.Position.Y);
            fakeStorage.Defending = realAgent.Defending;
            fakeStorage.Dodge = realAgent.Dodge;
            fakeStorage.Endurance = realAgent.Endurance;
            fakeStorage.Eyesight = realAgent.Eyesight;
            fakeStorage.Health = realAgent.Health;
            fakeStorage.Hitpoints = realAgent.Hitpoints;
            fakeStorage.Hunger = realAgent.Hunger;
        }

        public override AIFramework.Actions.IAction GetNextAction(List<IEntity> otherEntities)
        {
            //Should never be called
            throw new NotImplementedException();
        }


        public override void ActionResultCallback(bool success)
        {
            //Should never be called
            throw new NotImplementedException();
        }

    }
}
