using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game7
{
    enum FlagTypes { Active, Move, OnHit };

    class FlagEntity
    {
        public FlagTypes Type;
        public bool Value;
        public bool Clean;

        public FlagEntity(FlagTypes _type, bool _value, bool clean)
        {
            Type = _type;
            Value = _value;
            Clean = clean;
        }
    }
}
