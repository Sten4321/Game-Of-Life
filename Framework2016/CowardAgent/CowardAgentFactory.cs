using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using AIFramework;
using AIFramework.Entities;

namespace Speedy
{
    class SpeedyAgentFactory : AgentFactory
    {
        int lastId = 0;
        int cowardsMade = 0;
        int noncowards = 0;

        /// <summary>
        /// create base agent
        /// </summary>
        /// <param name="propertyStorage"></param>
        /// <returns></returns>
        public override Agent CreateAgent(IPropertyStorage propertyStorage)
        {
            lastId++;
            if (cowardsMade * 0.3 > noncowards)
            {
                noncowards++;
                cowardsMade++;
                return new Speedy(propertyStorage, lastId, cowardType.nonCoward);
            }
            else
            {
                cowardsMade++;
                return new Speedy(propertyStorage, lastId, cowardType.coward);
            }
        }

        /// <summary>
        /// procreation change stats?
        /// </summary>
        /// <param name="parent1"></param>
        /// <param name="parent2"></param>
        /// <param name="propertyStorage"></param>
        /// <returns></returns>
        public override Agent CreateAgent(Agent parent1, Agent parent2, IPropertyStorage propertyStorage)
        {
            lastId++;
            if (cowardsMade * 0.3 >= noncowards)
            {
                noncowards++;
                cowardsMade++;
                return new Speedy(propertyStorage, lastId, cowardType.nonCoward);
            }
            else
            {
                cowardsMade++;
                return new Speedy(propertyStorage, lastId, cowardType.coward);
            }
        }

        public override Type ProvidedAgentType
        {
            get { return typeof(Speedy); }
        }

        public override string Creators
        {
            get { return "Stefan : Sten4321"; }
        }

        public override void RegisterWinners(List<Agent> sortedAfterDeathTime)
        {
            //Do data collection - Perhaps used to evolutionary algoritmen
            Console.WriteLine("nonCowards: " + noncowards);
            Console.WriteLine("Cowards: " + (cowardsMade - noncowards));
        }
    }
}
