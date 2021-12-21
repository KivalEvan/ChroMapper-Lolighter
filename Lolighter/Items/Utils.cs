using System.Collections.Generic;

namespace Lolighter.Items
{
    static class Utils
    {
        public static class EnvironmentLight
        {
            public static List<int> LightableList(string environment = "")
            {
                switch (environment)
                {
                    case "InterscopeEnvironment":
                    case "SkrillexEnvironment":
                        return new List<int>() { 0, 1, 2, 3, 4, 6, 7 };
                    case "BillieEnvironment":
                        return new List<int>() { 0, 1, 2, 3, 4, 6, 7, 10, 11 };
                    default:
                        return new List<int>() { 0, 1, 2, 3, 4 };
                }
            }
            private static List<int> LightEventType = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
            public static bool IsLightingEvent(MapEvent ev)
            {
                return LightEventType.Contains(ev.Type);
            }
        }
    }
}
