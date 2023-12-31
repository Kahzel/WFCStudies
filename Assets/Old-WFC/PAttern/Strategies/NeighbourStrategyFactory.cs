﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace WaveFunctionCollapse
{
    public class NeighbourStrategyFactory
    {
        Dictionary<string, Type> strategies;

        public NeighbourStrategyFactory()
        {
            LoadTypesIFindNeighbourStrategy();
        }

        private void LoadTypesIFindNeighbourStrategy()
        {
            strategies = new Dictionary<string, Type>();
            Type[] typesInThisAssembly = Assembly.GetExecutingAssembly().GetTypes();

            foreach(var type in typesInThisAssembly)
            {
                if (type.GetInterface(typeof(IFindNeighbourStrategy).ToString()) != null)
                {
                    strategies.Add(type.Name.ToLower(), type);
                }
            }
        }

        internal IFindNeighbourStrategy CreateInstance(string nameOfStrategy)
        {
            Type t = GetTypeToCreate(nameOfStrategy) ?? GetTypeToCreate("more");
            return Activator.CreateInstance(t) as IFindNeighbourStrategy;
        }

        private Type GetTypeToCreate(string nameOfStrategy)
        {
            foreach (var possibleStrategy in strategies)
            {
                if (possibleStrategy.Key.Contains(nameOfStrategy)){
                    return possibleStrategy.Value;
                }
            }
            return null;
        }
    }
}