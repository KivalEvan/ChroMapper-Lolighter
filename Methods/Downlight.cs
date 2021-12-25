using Lolighter.Items;
using System.Collections.Generic;
using System.Linq;
using Options = Lolighter.Items.Options;
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

            // Sort the list (it's already sorted so let's not)
            //light.Sort((x, y) => x.Time.CompareTo(y.Time));

            // Sort each of them per type
            // TODO: why this isnt in dictionary? idk but i need to do that
            List<MapEvent> Back = new List<MapEvent>(light.Where(x => x.Type == EventType.LightBackTopLasers));
            List<MapEvent> Neon = new List<MapEvent>(light.Where(x => x.Type == EventType.LightTrackRingNeons));
            List<MapEvent> Side = new List<MapEvent>(light.Where(x => x.Type == EventType.LightBottomBackSideLasers));
            List<MapEvent> Left = new List<MapEvent>(light.Where(x => x.Type == EventType.LightLeftLasers));
            List<MapEvent> Right = new List<MapEvent>(light.Where(x => x.Type == EventType.LightRightLasers));
            List<MapEvent> Boost = new List<MapEvent>(light.Where(x => x.Type == EventType.LightBoost));
            List<MapEvent> LeftExtra = new List<MapEvent>(light.Where(x => x.Type == EventType.LightLeftExtraLight));
            List<MapEvent> RightExtra = new List<MapEvent>(light.Where(x => x.Type == EventType.LightRightExtraLight));
            List<MapEvent> LeftExtra2 = new List<MapEvent>(light.Where(x => x.Type == EventType.LightLeftExtra2Light));
            List<MapEvent> RightExtra2 = new List<MapEvent>(light.Where(x => x.Type == EventType.LightRightExtra2Light));
            List<MapEvent> LeftSpeed = new List<MapEvent>(light.Where(x => x.Type == EventType.RotatingLeftLasers));
            List<MapEvent> RightSpeed = new List<MapEvent>(light.Where(x => x.Type == EventType.RotatingRightLasers));
            List<MapEvent> Spin = new List<MapEvent>(light.Where(x => x.Type == EventType.RotationAllTrackRings));
            List<MapEvent> Zoom = new List<MapEvent>(light.Where(x => x.Type == EventType.RotationSmallTrackRings));
            List<MapEvent> RotationEarly = new List<MapEvent>(light.Where(x => x.Type == EventType.RotationEarlyLane));
            List<MapEvent> RotationLate = new List<MapEvent>(light.Where(x => x.Type == EventType.RotationLateLane));
            List<MapEvent> Extra1 = new List<MapEvent>(light.Where(x => x.Type == EventType.ExtraEvent1));
            List<MapEvent> Extra2 = new List<MapEvent>(light.Where(x => x.Type == EventType.ExtraEvent2));

            // Send them to the algorithm
            Back = Mod(Back, Options.Downlight.Speed);
            Neon = Mod(Neon, Options.Downlight.Speed);
            Side = Mod(Side, Options.Downlight.Speed);
            Left = Mod(Left, Options.Downlight.Speed);
            Right = Mod(Right, Options.Downlight.Speed);
            LeftExtra = Mod(LeftExtra, Options.Downlight.Speed);
            RightExtra = Mod(RightExtra, Options.Downlight.Speed);
            LeftExtra2 = Mod(LeftExtra2, Options.Downlight.Speed);
            RightExtra2 = Mod(RightExtra2, Options.Downlight.Speed);

            // Spin/Zoom, we want to remove spam
            Spin = Spam(Spin, Options.Downlight.SpamSpeed);
            Zoom = Spam(Zoom, Options.Downlight.SpamSpeed);

            // Put back together the list
            light = new List<MapEvent>();
            light.AddRange(Back);
            light.AddRange(Neon);
            light.AddRange(Side);
            light.AddRange(Left);
            light.AddRange(Right);
            light.AddRange(LeftExtra);
            light.AddRange(RightExtra);
            light.AddRange(LeftExtra2);
            light.AddRange(RightExtra2);
            light.AddRange(LeftSpeed);
            light.AddRange(RightSpeed);
            light.AddRange(Extra1);
            light.AddRange(Extra2);

            // Turn On an Event if no light for a while.
            light = On(light, Options.Downlight.OnSpeed);

            // Put back together the list
            light.AddRange(Boost);
            light.AddRange(Spin);
            light.AddRange(Zoom);
            light.AddRange(RotationEarly);
            light.AddRange(RotationLate);

            // Sort the list no need to it'll be sorted as it is placed
            //light.Sort((x, y) => x.Time.CompareTo(y.Time));

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
