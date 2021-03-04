using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Aranslow.Assets
{
    internal class SpritesheetInfoLoader
    {
        private static JObject SpritesheetInfo;

        static SpritesheetInfoLoader()
        {
            try { SpritesheetInfo = JObject.Parse(File.ReadAllText(Directory.GetCurrentDirectory() + @"\Assets\SpritesheetInfo.json")); }
            catch(Exception e) { Tools.Logger.Log($"[SpritesheetInfoLoader]: Failed to load SpritesheetInfo - {e.Message}"); }
        }

        internal static int GetFrameWidthForSprite(string spriteName)
        {
            return SpritesheetInfo[spriteName].ToObject<JObject>()["IndividualFrameWidth"].ToObject<int>();
        }

        internal static int GetTotalFrameCountInSprite(string spriteName)
        {
            return SpritesheetInfo[spriteName].ToObject<JObject>()["FrameCount"].ToObject<int>();
        }
    }
}
