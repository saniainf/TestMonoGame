﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    interface IEntity
    {
        bool IsRemove { get; set; }

        void Update();
    }
}
