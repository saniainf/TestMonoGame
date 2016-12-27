using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class UIManager
    {
        public static UIManager Instance
        {
            get { return instance ?? (instance = new UIManager()); }
        }

        private static UIManager instance;

        public UIManager()
        {

        }

        public void Update()
        {

        }
    }
}
