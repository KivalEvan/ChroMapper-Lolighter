using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using static Lolighter.Items.Enum;

namespace Lolighter.Items
{
    static class Utils
    {
        public static class Action
        {
            public static void Save(List<BeatmapAction> actions, string message = "Lolighter")
            {
                BeatmapActionContainer.AddAction(new ActionCollectionAction(actions, true, true, message));
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

            public static int InverseColor(int temp) //Red -> Blue, Blue -> Red
            {
                if (temp > EventLightValue.BlueFlashFade)
                    return temp - 4; //Turn to blue
                else
                    return temp + 4; //Turn to red
            }

            public static int SwapLightValue(int temp)
            {
                switch (temp)
                {
                    case EventLightValue.BlueFlashFade:
                        return EventLightValue.BlueOn;
                    case EventLightValue.RedFlashFade:
                        return EventLightValue.RedOn;
                    case EventLightValue.BlueOn:
                        return EventLightValue.BlueFlashFade;
                    case EventLightValue.RedOn:
                        return EventLightValue.RedFlashFade;
                    default:
                        return 0;
                }
            }

            public static List<int> LightEventType = new List<int>() { 0, 1, 2, 3, 4, 6, 7, 10, 11 };
            public static List<int> AuxEventType = new List<int>() { 5, 8, 9, 12, 13, 16, 17 };
            public static List<int> RingEventType = new List<int>() { 8, 9 };
            public static List<int> LaserRotationEventType = new List<int>() { 12, 13 };
            public static List<int> LaneRotationEventType = new List<int>() { 14, 15 };
            public static List<int> EnvironmentEventType = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 16, 17 };
            public static List<int> AllEventType = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 100 };
            public static bool IsEnvironmentEvent(MapEvent ev)
            {
                return EnvironmentEventType.Contains(ev.Type);
            }
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
