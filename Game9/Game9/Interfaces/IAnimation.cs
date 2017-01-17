using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    interface IAnimation
    {
        bool IsPlay { get; set; }
        DrawComponent RootDC { set; }

        void Play();
        void Stop();
        void Pause();
        void Update();
    }
}
