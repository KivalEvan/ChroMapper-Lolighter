using System.Collections.Generic;
using System.Linq;
using Options = Lolighter.Items.Options;
using Utils = Lolighter.Items.Utils;
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
            // Automatically set on Backlight during long period of nothing
            // Remove spin/zoom spam

            // Sort the list (it's already sorted so let's not)
            //light.Sort((x, y) => x.Time.CompareTo(y.Time));

            // Sort each of them per type
            Dictionary<int, List<MapEvent>> mapEvents = new Dictionary<int, List<MapEvent>>(17);
            foreach (var type in Utils.EnvironmentEvent.AllEventType)
            {
                mapEvents.Add(type, new List<MapEvent>(light.Where(x => x.Type == type)));
            }

            // Send them to the algorithm
            foreach (var type in Utils.EnvironmentEvent.LightEventType)
            {
                mapEvents[type] = Mod(mapEvents[type], Options.Downlight.Speed);
            }

            // Spin/Zoom, we want to remove spam
            mapEvents[EventType.RotationAllTrackRings] = Spam(mapEvents[EventType.RotationAllTrackRings], Options.Downlight.SpamSpeed);
            mapEvents[EventType.RotationSmallTrackRings] = Spam(mapEvents[EventType.RotationSmallTrackRings], Options.Downlight.SpamSpeed);

            // Put back together the list
            light = new List<MapEvent>();
            foreach (var type in Utils.EnvironmentEvent.LightEventType)
            {
                light.AddRange(mapEvents[type]);
            }
            light.AddRange(mapEvents[EventType.RotatingLeftLasers]);
            light.AddRange(mapEvents[EventType.RotatingRightLasers]);

            // Turn On an Event if no light for a while.
            light = On(light, Options.Downlight.OnSpeed);

            // Put back together the list
            foreach (var type in Utils.EnvironmentEvent.AuxEventType.Except(Utils.EnvironmentEvent.LaserRotationEventType))
            {
                light.AddRange(mapEvents[type]);
            }

            // Sort the list (no need to it'll be sorted as it is placed)
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
                    light.Remove(now);
                }
            }

            return light;
        }

        static List<MapEvent> Mod(List<MapEvent> light, double speed)
        {
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
                    }
                    else if (previous.Value == 4 || previous.Value == 0)
                    {
                        light.Remove(previous);
                    }
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
    }
}
