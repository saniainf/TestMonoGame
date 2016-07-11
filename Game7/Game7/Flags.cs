using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game7
{
    class Flags
    {
        public FlagEntity[] FlagsArray;

        public Flags()
        {
            FlagsArray = new FlagEntity[3];
            FlagsArray[0] = new FlagEntity(FlagTypes.Active, true, false);
            FlagsArray[1] = new FlagEntity(FlagTypes.Move, false, true);
            FlagsArray[2] = new FlagEntity(FlagTypes.OnHit, false, true);
        }

        public bool Pop(FlagTypes _type)
        {
            bool v = false;
            foreach (FlagEntity fe in FlagsArray)
            {
                if (fe.Type == _type)
                {
                    v = fe.Value;
                    break;
                }
            }
            return v;
        }

        public void Push(FlagTypes _type, bool _value)
        {
            foreach (FlagEntity fe in FlagsArray)
            {
                if (fe.Type == _type)
                {
                    fe.Value = _value;
                    break;
                }
            }
        }

        public void Clean()
        {
            foreach (FlagEntity fe in FlagsArray)
            {
                if (fe.Clean)
                    fe.Value = false;
            }
        }
    }
}
