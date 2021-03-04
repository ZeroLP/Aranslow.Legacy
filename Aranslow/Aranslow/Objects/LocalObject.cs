using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Aranslow.Objects
{
    internal class LocalObject : GameObject
    {
        internal LocalObject(Point spawnPosition, ActionState aState = ActionState.Idle) : base(GameObject.ModelType.Player, spawnPosition, aState)
        {
        }

        internal virtual void CastBasicAttack()
        {

        }
    }
}
