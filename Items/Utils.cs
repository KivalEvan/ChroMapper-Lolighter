using System.Collections.Generic;

namespace Lolighter.Items
{
    static class Utils
    {
        public static class Action
        {
            public static void Save()
            {
                //beatmapAction.Add(new BeatmapObjectPlacementAction());
                //BeatmapActionContainer.AddAction();
            }
        }
        public static class EnvironmentEvent
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

            public static List<int> LightEventType = new List<int>() { 0, 1, 2, 3, 4, 6, 7, 10, 11 };
            public static List<int> AuxEventType = new List<int>() { 5, 8, 9, 12, 13, 16, 17 };
            public static List<int> LaserRotationEventType = new List<int>() { 12, 13 };
            public static List<int> LaneRotationEventType = new List<int>() { 14, 15 };
            public static List<int> EnvironmentEventType = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 16, 17 };
            public static List<int> AllEventType = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 100 };
            public static bool IsEnvironmentEvent(MapEvent ev)
            {
                return EnvironmentEventType.Contains(ev.Type);
            }
        }
    }
}
