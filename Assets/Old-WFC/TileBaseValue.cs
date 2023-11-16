using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WaveFunctionCollapse
{
    public class TileBaseValue : IValue<TileBase>
    {
        public TileBase value
        {
            get => this.tileBase;
            set => this.tileBase = value;
        }

        private TileBase tileBase;

        public TileBaseValue(TileBase tile)
        {
            this.tileBase = tile;
        }
        public bool Equals(IValue<TileBase> x, IValue<TileBase> y)
        {
            return x == y;
        }

        public int GetHashCode(IValue<TileBase> obj)
        {
            return this.tileBase.GetHashCode();
        }

        public bool Equals(IValue<TileBase> other)
        {
            return other.value == this.value;
        }

        
    }
    
}
