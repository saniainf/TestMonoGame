using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    interface IComponent
    {
        bool IsRemove { get; set; }

        void Update();
    }
}
