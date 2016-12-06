using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    interface IBehavior
    {
        bool IsRemove { get; set; }

        void Initialize();

        void Update();
    }
}
