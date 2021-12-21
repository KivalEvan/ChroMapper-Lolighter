using System.Collections.Generic;

namespace Lolighter.Items
{
    static class Enum
    {
        public static class Index
        {
            public const int Left = 0;
            public const int MiddleLeft = 1;
            public const int MiddleRight = 2;
            public const int Right = 3;
        }

        public static class Layer
        {
            public const int Bottom = 0;
            public const int Middle = 1;
            public const int Top = 2;
        }

        public static class CutDirection
        {
            public const int Up = 0;
            public const int Down = 1;
            public const int Left = 2;
            public const int Right = 3;
            public const int UpLeft = 4;
            public const int UpRight = 5;
            public const int DownLeft = 6;
            public const int DownRight = 7;
            public const int Any = 8;
        }

        public static class NoteType
        {
            public const int Red = 0;
            public const int Blue = 1;
            public const int Bomb = 3;
        }

        public static class ObstacleType
        {
            public const int Full = 0;
            public const int Crouch = 1;
        }

        public static class EventType
        {
            public const int LightBackTopLasers = 0;
            public const int LightTrackRingNeons = 1;
            public const int LightLeftLasers = 2;
            public const int LightRightLasers = 3;
            public const int LightBottomBackSideLasers = 4;
            public const int LightBoost = 5;
            public const int LightLeftExtraLasers = 6;
            public const int LightRightExtraLasers = 7;
            public const int RotationAllTrackRings = 8;
            public const int RotationSmallTrackRings = 9;
            public const int LightLeftExtraLight = 10;
            public const int LightRightExtraLight = 11;
            public const int RotatingLeftLasers = 12;
            public const int RotatingRightLasers = 13;
            public const int RotationEarlyLane = 14;
            public const int RotationLateLane = 15;
            public const int ExtraLeftEvent = 16;
            public const int ExtraRightEvent = 17;
            public const int BPMChange = 100;
        }

        public static class EventLightValue
        {
            public const int Off = 0;
            public const int BlueOn = 1;
            public const int BlueFlashStay = 2;
            public const int BlueFlashFade = 3;
            public const int BlueTransition = 4;
            public const int RedOn = 5;
            public const int RedFlashStay = 6;
            public const int RedFlashFade = 7;
            public const int RedTransition = 8;
        }

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
        }
    }
}
