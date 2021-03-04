using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Aranslow.Objects
{
    internal class ObjectManager
    {
        internal static List<GameObject> GameObjects;
        internal static LocalObject LocalPlayer;

        internal static bool IsPopulated = false;

        internal static void PopulateGameObjects()
        {
            if (!IsPopulated)
            {
                LocalPlayer = new LocalObject(new Point(50, 400));
                GameObjects = new List<GameObject>();

                GameObjects.Add(LocalPlayer);

                IsPopulated = true;
            }
        }
    }
}
