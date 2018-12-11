using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using AIFramework;
using AIFramework.Entities;

namespace CowardAgent
{
    class CowardAgentFactory : AgentFactory
    {
        int lastId = 0;
        /// <summary>
        /// create base agent
        /// </summary>
        /// <param name="propertyStorage"></param>
        /// <returns></returns>
        public override Agent CreateAgent(IPropertyStorage propertyStorage)
        {
            lastId++;
            return new CowardAgent(propertyStorage, lastId);
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
            return new CowardAgent(propertyStorage, lastId);
        }

        public override Type ProvidedAgentType
        {
            get { return typeof(CowardAgent); }
        }

        public override string Creators
        {
            get { return "Stefan : Sten4321"; }
        }

        public override void RegisterWinners(List<Agent> sortedAfterDeathTime)
        {
            //Do data collection - Perhaps used to evolutionary algoritmen
        }
    }
}
