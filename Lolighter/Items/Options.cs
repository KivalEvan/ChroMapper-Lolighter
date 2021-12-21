namespace Lolighter.Items
{
    static class Options
    {
        public static float ColorOffset { set; get; } = 0;
        public static float ColorSwap { set; get; } = 4;
        public static bool AllowBackStrobe { set; get; } = true;
        public static bool AllowNeonStrobe { set; get; } = true;
        public static bool AllowSideStrobe { set; get; } = true;
        public static bool AllowFade { set; get; } = true;
        public static bool AllowSpinZoom { set; get; } = true;
        public static bool AllowBoostColor { set; get; } = false;
        public static bool NerfStrobes { set; get; } = false;
        public static bool OnlyCommonEvent { set; get; } = false;
        public static bool ClearLighting { set; get; } = false;
        public static float DownlightSpeed { set; get; } = 0.5f;
        public static float DownlightSpamSpeed { set; get; } = 0.25f;
        public static float DownlightOnSpeed { set; get; } = 5;
    }
}
