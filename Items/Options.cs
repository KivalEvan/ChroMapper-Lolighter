namespace Lolighter.Items
{
    static class Options
    {
        public static class Light
        {
            private static float colorOffset = 0.0f;
            private static float colorSwap = 4.0f;

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
            public static bool IgnoreBomb { set; get; } = false;
            public static bool ClearLighting { set; get; } = false;
        }

        public static class Downlight
        {
            private static float speed = 0.5f;
            private static float spamSpeed = 0.25f;
            private static float onSpeed = 5.0f;


            public static float Speed { set => speed = value > 0.0f ? value : 0.0f; get => speed; }
            public static float SpamSpeed { set => spamSpeed = value > 0.0f ? value : 0.0f; get => spamSpeed; }
            public static float OnSpeed { set => onSpeed = value > 1.0f ? value : 1.0f; get => onSpeed; }
        }

        public static class Modifier
        {
            private static float limiter = 0.25f;
            private static float sliderPrecision = 0.03125f;

            public static bool IsLimited { set; get; } = true;
            public static float Limiter { set => limiter = value > 0.0f ? value : 0.0f; get => limiter; }
            public static float SliderPrecision { set => sliderPrecision = value > 0.0f ? 1 / value : 0.03125f; get => 1 / sliderPrecision; }
        }
    }
}
