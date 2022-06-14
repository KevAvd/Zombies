using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Engine
{
    enum AnimationType
    {
        NORMAL,
        REVERSE,
        LOOP
    }

    enum GraphicState
    {
        BACKGROUND,
        LAYER_1,
        LAYER_2,
        LAYER_3,
        UI,
        HIDDEN,
    }

    enum PhysicState
    {
        NORMAL,
        NOCLIP
    }
}
