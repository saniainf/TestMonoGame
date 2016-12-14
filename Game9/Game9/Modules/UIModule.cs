using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class UIModule
    {
        public static UIModule Instance
        {
            get { return instance ?? (instance = new UIModule()); }
        }

        private static UIModule instance;

        public UIModule()
        {

        }

        public void Initialize()
        {

        }

        public void Update()
        {

        }
    }
}
