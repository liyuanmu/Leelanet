using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leelanet.Engine
{
    public interface LeelazListener
    {
        void bestMoveNotification(List<MoveData> bestMoves);
    }
}
