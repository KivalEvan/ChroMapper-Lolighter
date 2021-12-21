using Lolighter.Items;
using System.Collections.Generic;
using System.Linq;
using static Lolighter.Items.Options;
using EventLightValue = Lolighter.Items.Enum.EventLightValue;
using EventType = Lolighter.Items.Enum.EventType;

namespace Lolighter.Methods
{
    class DownLighter
    {
        static public List<MapEvent> Down(List<MapEvent> light)
        {
            // Turns all long strobes into pulse (alternate between fade and on)
            // Remove fast off
            // Remove fast color switch
            // Automatically set on Backlight during long period of nothing
            // Remove spin/zoom spam

            // Sort the list
            light.Sort((x, y) => x.Time.CompareTo(y.Time));

            // Sort each of them per type
            List<MapEvent> Back = new List<MapEvent>(light.Where(x => x.Type == EventType.LightBackTopLasers));
            List<MapEvent> Neon = new List<MapEvent>(light.Where(x => x.Type == EventType.LightTrackRingNeons));
            List<MapEvent> Side = new List<MapEvent>(light.Where(x => x.Type == EventType.LightBottomBackSideLasers));
            List<MapEvent> Left = new List<MapEvent>(light.Where(x => x.Type == EventType.LightLeftLasers));
            List<MapEvent> Right = new List<MapEvent>(light.Where(x => x.Type == EventType.LightRightLasers));
            List<MapEvent> LeftSpeed = new List<MapEvent>(light.Where(x => x.Type == EventType.RotatingLeftLasers));
            List<MapEvent> RightSpeed = new List<MapEvent>(light.Where(x => x.Type == EventType.RotatingRightLasers));
            List<MapEvent> Spin = new List<MapEvent>(light.Where(x => x.Type == EventType.RotationAllTrackRings));
            List<MapEvent> Zoom = new List<MapEvent>(light.Where(x => x.Type == EventType.RotationSmallTrackRings));

            // Send them to the algorithm
            Back = Mod(Back, DownlightSpeed);
            Neon = Mod(Neon, DownlightSpeed);
            Side = Mod(Side, DownlightSpeed);
            Left = Mod(Left, DownlightSpeed);
            Right = Mod(Right, DownlightSpeed);

            // Spin/Zoom, we want to remove spam
            Spin = Spam(Spin, DownlightSpamSpeed);
            Zoom = Spam(Zoom, DownlightSpamSpeed);

            // Put back together the list
            light = new List<MapEvent>();
            light.AddRange(Back);
            light.AddRange(Neon);
            light.AddRange(Side);
            light.AddRange(Left);
            light.AddRange(Right);
            light.AddRange(LeftSpeed);
            light.AddRange(RightSpeed);

            // Turn On an Event if no light for a while.
            light = On(light, DownlightOnSpeed);

            // Put back together the list
            light.AddRange(Spin);
            light.AddRange(Zoom);

            // Sort the list
            light.Sort((x, y) => x.Time.CompareTo(y.Time));

            return light;
        }

        static List<MapEvent> On(List<MapEvent> light, double onSpeed)
        {
            for (int i = light.Count() - 1; i > 0; i--)
            {
                MapEvent previous = light[i - 1];
                MapEvent now = light[i];

                // If no light for a long duration, we turn on something.
                if (now.Time - previous.Time >= onSpeed)
                {
                    if (previous.Value < 4)
                    {
                        previous.Value = EventLightValue.BlueOn;
                    }
                    else
                    {
                        previous.Value = EventLightValue.RedOn;
                    }
                }
            }

            return light;
        }

        static List<MapEvent> Spam(List<MapEvent> light, double spamSpeed)
        {
            for (int i = light.Count() - 1; i > 0; i--)
            {
                MapEvent previous = light[i - 1];
                MapEvent now = light[i];

                // We remove spam under that speed
                if (now.Time - previous.Time <= spamSpeed)
                {
                    light.Remove(previous);
                }
            }

            return light;
        }

        static List<MapEvent> Mod(List<MapEvent> light, double speed)
        {
            bool fastColorSwap = false;

            for (int i = light.Count() - 1; i > 0; i--)
            {
                MapEvent previous = light[i - 1];
                MapEvent now = light[i];

                // The light are pretty close
                if (now.Time - previous.Time <= speed)
                {
                    // One of them is an Off event
                    if (now.Value == 4 || now.Value == 0)
                    {
                        light.Remove(now);
                        if (i == light.Count())
                        {
                            i--;
                        }
                        now = light[i];
                        previous = light[i - 1];
                    }
                    if (previous.Value == 4 || previous.Value == 0)
                    {
                        light.Remove(previous);
                        if (i == light.Count())
                        {
                            i--;
                        }
                        now = light[i];
                        previous = light[i - 1];
                    }
                    // If one light is Blue and the other is Red, we inverse one of them.
                    if ((now.Value < 4 && previous.Value > 3) || (now.Value > 3 && previous.Value < 4) && now.Value != 4 && now.Value != 0 && previous.Value != 0 && previous.Value != 4)
                    {
                        if (fastColorSwap)
                        {
                            now.Value = Inverse(now.Value);
                            fastColorSwap = false;
                        }
                        else
                        {
                            fastColorSwap = true;
                        }
                    }
                }
                else
                {
                    fastColorSwap = false;
                }
            }

            // Now with fast stuff removed.
            for (int i = 1; i < light.Count(); i++)
            {
                MapEvent previous = light[i - 1];
                MapEvent now = light[i];

                // Swap light between Fade and On if they are close.
                if (now.Time - previous.Time <= speed && now.Value == previous.Value)
                {
                    if (now.Value == EventLightValue.BlueFlashFade || now.Value == EventLightValue.RedFlashFade || now.Value == EventLightValue.BlueOn || now.Value == EventLightValue.RedOn)
                    {
                        now.Value = Swap(now.Value);
                    }
                }
            }

            return light;
        }

        static int Inverse(int temp) //Red -> Blue, Blue -> Red
        {
            if (temp > EventLightValue.BlueFlashFade)
                return temp - 4; //Turn to blue
            else
                return temp + 4; //Turn to red
        }

        static int Swap(int temp)
        {
            if (temp == EventLightValue.BlueFlashFade)
                return EventLightValue.BlueOn;
            if (temp == EventLightValue.RedFlashFade)
                return EventLightValue.RedOn;
            if (temp == EventLightValue.BlueOn)
                return EventLightValue.BlueFlashFade;
            if (temp == EventLightValue.RedOn)
                return EventLightValue.RedFlashFade;

            return 0;
        }
    }
}
