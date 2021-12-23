namespace Lolighter.Items
{
    static class Options
    {
        private static float colorOffset = 0.0f;
        private static float colorSwap = 4.0f;

        public static float downlightSpeed = 0.5f;
        public static float downlightSpamSpeed = 0.25f;
        public static float downlightOnSpeed = 5;

        public static float ColorOffset { set => colorOffset = value > 0.0f ? value : 0.0f; get => colorOffset; }
        public static float ColorSwap { set => colorSwap = value > 1.0f ? value : 1.0f; get => colorSwap; }
        public static bool AllowBackStrobe { set; get; } = true;
        public static bool AllowNeonStrobe { set; get; } = true;
        public static bool AllowSideStrobe { set; get; } = true;
        public static bool AllowExtraStrobe { set; get; } = false;
        public static bool AllowExtra2Strobe { set; get; } = false;
        public static bool AllowFade { set; get; } = true;
        public static bool AllowSpinZoom { set; get; } = true;
        public static bool AllowBoostColor { set; get; } = false;
        public static bool NerfStrobes { set; get; } = false;
        public static bool OnlyCommonEvent { set; get; } = false;
        public static bool ClearLighting { set; get; } = false;

        public static float DownlightSpeed { set => downlightSpeed = value > 0.0f ? value : 0.0f; get => downlightSpeed; }
        public static float DownlightSpamSpeed { set => downlightSpamSpeed = value > 0.0f ? value : 0.0f; get => downlightSpamSpeed; }
        public static float DownlightOnSpeed { set => downlightOnSpeed = value > 1.0f ? value : 1.0f; get => downlightOnSpeed; }
    }
}
