using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using static Lolighter.Items.Enum;
using static Lolighter.Items.Utils;
using Options = Lolighter.Items.Options;

// i might want to change/fix some of these algorithm
namespace Lolighter.Methods
{
    static class Light
    {
        static public List<MapEvent> CreateLight(List<BeatmapNote> noteTempo, string environment = "DefaultEnvironment")
        {
            float last = new float(); //Var to stop spin-stack and also used as time check.
            float[] time = new float[4]; //Now, before, before-before, before-before-before, in this order.
            //0.0D = Default value for float, similar to NULL for int.
            int[] light = new int[3]; //Now, before, before-before.
            int lastLight = 0;
            float offset;
            float firstNote = 0;
            float timer = 0; //Timer start on the first note.
            int count; //Light counter, stop at maximum.
            int maximum = 2; //Maximum number of light per same time
            int color; //Set color start value.
            int lastCut = 0;
            int currentSpeed = 3;
            bool boostFlipFlop = false;
            float lastSpeed = 0;
            float nextfloat = 0;
            bool firstSlider = false;
            BeatmapNote nextSlider = new BeatmapNote(0, 0, 0, 0, 0);
            List<int> sliderLight = Options.Light.OnlyCommonEvent ? EnvironmentEvent.LightableList() : EnvironmentEvent.LightableList(environment);
            sliderLight.Remove(2);
            sliderLight.Remove(3);
            int sliderIndex = 0;
            float sliderNoteCount = 0;
            bool wasSlider = false;
            List<int> pattern = Options.Light.OnlyCommonEvent ? EnvironmentEvent.LightableList() : EnvironmentEvent.LightableList(environment);
            int maxPattern = pattern.Count;
            int patternIndex = 0;
            int patternCount = 0;
            List<MapEvent> eventTempo = new List<MapEvent>();

            void ResetTimer() //Pretty much reset everything necessary.
            {
                if (Options.Light.AllowFade)
                {
                    color = EventLightValue.BlueFlashFade; //Blue Fade
                }
                else
                {
                    color = EventLightValue.BlueOn; //Blue On
                }
                foreach (BeatmapNote note in noteTempo) //For each note
                {
                    if (note.Type == NoteType.Red || note.Type == NoteType.Blue) //Find the first note to know where to start the timer
                    {
                        firstNote = note.Time;
                        break;
                    }
                }
                offset = firstNote;
                count = 0;
                for (int i = 0; i < 2; i++)
                {
                    time[i] = 0.0f;
                    light[i] = 0;
                }
                time[2] = 0.0f;
                time[3] = 0.0f;
            }

            int Inverse(int temp) //Red -> Blue, Blue -> Red
            {
                if (temp > EventLightValue.BlueFlashFade)
                    return temp - 4; //Turn to blue
                else
                    return temp + 4; //Turn to red
            }

            void TimerDuration() //Check the checkpoint
            {
                timer = time[0];
                if (timer >= Options.Light.ColorOffset + Options.Light.ColorSwap + offset) //If the timer is above offset + ColorOffset + ColorSwap (From the interface), then it's time to change color.
                {
                    int swapTime = (int)((time[0] - time[1]) / Options.Light.ColorSwap) + 1; //We get the number of "beat" since the last time it entered here this way.
                    for (int i = 0; i < swapTime; i++) //For each time that it need to swap. (Dumb fix for a dumb method)
                    {
                        color = Inverse(color); //Swap color
                        offset += Options.Light.ColorSwap; //Offset incremented
                    }
                }
            }

            void CreateGenericLight(int speed) //Receive laser speed as var.
            {
                if (time[0] == time[1]) //Same beat
                {
                    if (count < maximum) //Maximum laser is 2
                    {
                        count++;
                    }
                }
                else
                {
                    count = 0; //Reset the count, we are moving forward (in time).
                    for (int i = 0; i < 2; i++)
                    {
                        if (light[i] != 0 && time[0] - time[1] <= 2.5) //TODO: Re-add as an option left/right laser strobe.
                        {
                            //ev = new MapEvent((time[0] - (time[0] - time[1]) / 2), light[i], 0);
                            //eventTempo.Add(ev);
                        }
                        light[i] = 0;
                    }
                }

                if (count == maximum) //If count reach the maximum, we skip this.
                {
                    return;
                }

                if (light[0] != 0)
                {
                    light[1] = light[0];
                }

                if (lastLight == 2) //We swap between laser
                {
                    light[0] = 3;
                }
                else
                {
                    light[0] = 2;
                }

                switch (light[0]) //Add laser + speed
                {
                    case 2:
                        eventTempo.Add(new MapEvent(time[0], EventType.LightLeftLasers, color));
                        eventTempo.Add(new MapEvent(time[0], EventType.RotatingLeftLasers, speed));
                        break;
                    case 3:
                        eventTempo.Add(new MapEvent(time[0], EventType.LightRightLasers, color));
                        eventTempo.Add(new MapEvent(time[0], EventType.RotatingRightLasers, speed));
                        break;
                }

                lastLight = light[0];
            }

            ResetTimer();

            foreach (BeatmapNote note in noteTempo) //Process specific light (Side/Neon) using time.
            {
                float now = note.Time;
                time[0] = now;

                //Here we process Spin and Zoom
                if (now == firstNote && time[1] == 0.0D && Options.Light.AllowSpinZoom) //If we are processing the first note, add spin + zoom to it.
                {
                    eventTempo.Add(new MapEvent(now + Options.Light.ColorOffset, EventType.RotationAllTrackRings, 0));
                    eventTempo.Add(new MapEvent(now + Options.Light.ColorOffset, EventType.RotationSmallTrackRings, 0));
                }
                else if (now >= Options.Light.ColorOffset + Options.Light.ColorSwap + offset && now > firstNote && Options.Light.AllowSpinZoom) //If we are reaching the next threshold of the timer
                {
                    //Add a spin at timer.
                    eventTempo.Add(new MapEvent(offset + Options.Light.ColorOffset, EventType.RotationAllTrackRings, 0));
                    if (count == 0) //Only add zoom (and boost) every 2 spin.
                    {
                        eventTempo.Add(new MapEvent(offset + Options.Light.ColorOffset, EventType.RotationSmallTrackRings, 0));
                        if (Options.Light.AllowBoostColor)
                        {
                            eventTempo.Add(new MapEvent(offset + Options.Light.ColorOffset, EventType.LightBoost, boostFlipFlop ? 1 : 0));
                            boostFlipFlop = !boostFlipFlop;
                        }
                        count = 1;
                    }
                    else
                    {
                        count--;
                    }
                }
                //If there's a quarter between two float parallel notes and timer didn't pass the check.
                else if (time[1] - time[2] == 0.25 && time[3] == time[2] && time[1] == now && timer < offset && Options.Light.AllowSpinZoom)
                {
                    eventTempo.Add(new MapEvent(now, EventType.RotationAllTrackRings, 0));
                }

                TimerDuration();

                if ((now == time[1] || (now - time[1] <= 0.02 && time[1] != time[2])) && (time[1] != 0.0D && now != last)) //If not same note, same beat, apply once.
                {
                    if (!Options.Light.NerfStrobes) //Off event
                    {
                        if (now - last >= 0.5)
                        {
                            if (Options.Light.AllowBackStrobe) //Back Top Laser
                            {
                                eventTempo.Add(new MapEvent(now + 0.25f, EventType.LightBackTopLasers, 0));
                            }
                            if (Options.Light.AllowNeonStrobe) //Neon Light
                            {
                                eventTempo.Add(new MapEvent(now + 0.25f, EventType.LightTrackRingNeons, 0));
                            }
                            if (Options.Light.AllowSideStrobe) //Side Light
                            {
                                eventTempo.Add(new MapEvent(now + 0.25f, EventType.LightBottomBackSideLasers, 0));
                            }
                            if (pattern.Contains(EventType.LightLeftExtraLight) && Options.Light.AllowExtraStrobe) //Extra Light
                            {
                                eventTempo.Add(new MapEvent(now + 0.25f, EventType.LightLeftExtraLight, 0));
                                eventTempo.Add(new MapEvent(now + 0.25f, EventType.LightRightExtraLight, 0));
                            }
                            if (pattern.Contains(EventType.LightLeftExtra2Light) && Options.Light.AllowExtra2Strobe) //Extra2 Light
                            {
                                eventTempo.Add(new MapEvent(now + 0.25f, EventType.LightLeftExtra2Light, 0));
                                eventTempo.Add(new MapEvent(now + 0.25f, EventType.LightRightExtra2Light, 0));
                            }
                        }
                        else
                        {
                            if (Options.Light.AllowBackStrobe) //Back Top Laser
                            {
                                eventTempo.Add(new MapEvent(now - (now - last) / 2, EventType.LightBackTopLasers, 0));
                            }
                            if (Options.Light.AllowNeonStrobe) //Neon Light
                            {
                                eventTempo.Add(new MapEvent(now - (now - last) / 2, EventType.LightTrackRingNeons, 0));
                            }
                            if (Options.Light.AllowSideStrobe) //Side Light
                            {
                                eventTempo.Add(new MapEvent(now - (now - last) / 2, EventType.LightBottomBackSideLasers, 0));
                            }
                            if (pattern.Contains(EventType.LightLeftExtraLight) && Options.Light.AllowExtraStrobe) //Extra Light
                            {
                                eventTempo.Add(new MapEvent(now - (now - last) / 2, EventType.LightLeftExtraLight, 0));
                                eventTempo.Add(new MapEvent(now - (now - last) / 2, EventType.LightRightExtraLight, 0));
                            }
                            if (pattern.Contains(EventType.LightLeftExtra2Light) && Options.Light.AllowExtra2Strobe) //Extra2 Light
                            {
                                eventTempo.Add(new MapEvent(now - (now - last) / 2, EventType.LightLeftExtra2Light, 0));
                                eventTempo.Add(new MapEvent(now - (now - last) / 2, EventType.LightRightExtra2Light, 0));
                            }
                        }
                    }
                    eventTempo.Add(new MapEvent(now, EventType.LightBackTopLasers, color));
                    eventTempo.Add(new MapEvent(now, EventType.LightBottomBackSideLasers, color));
                    eventTempo.Add(new MapEvent(now, EventType.LightTrackRingNeons, color));
                    if (pattern.Contains(EventType.LightLeftExtraLight))
                    {
                        eventTempo.Add(new MapEvent(now, EventType.LightLeftExtraLight, color));
                        eventTempo.Add(new MapEvent(now, EventType.LightRightExtraLight, color));
                    }
                    if (pattern.Contains(EventType.LightLeftExtra2Light))
                    {
                        eventTempo.Add(new MapEvent(now, EventType.LightLeftExtra2Light, color));
                        eventTempo.Add(new MapEvent(now, EventType.LightRightExtra2Light, color));
                    }

                    last = now;
                }

                for (int i = 3; i > 0; i--) //Keep the timing of up to three notes before.
                {
                    time[i] = time[i - 1];
                }
            }

            int Swap(int temp)
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

            if (Options.Light.NerfStrobes)
            {
                float lastTimeTop = 100;
                float lastTimeNeon = 100;
                float lastTimeSide = 100;

                foreach (var x in eventTempo)
                {
                    if (x.Type == EventType.LightBackTopLasers)
                    {
                        if (x.Time - lastTimeTop <= 1)
                        {
                            x.Value = Swap(x.Value);
                        }
                        lastTimeTop = x.Time;
                    }
                    else if (x.Type == EventType.LightTrackRingNeons)
                    {
                        if (x.Time - lastTimeNeon <= 1)
                        {
                            x.Value = Swap(x.Value);
                        }
                        lastTimeNeon = x.Time;
                    }
                    else if (x.Type == EventType.LightBottomBackSideLasers)
                    {
                        if (x.Time - lastTimeSide <= 1)
                        {
                            x.Value = Swap(x.Value);
                        }
                        lastTimeSide = x.Time;
                    }
                }
            }

            ResetTimer();

            foreach (BeatmapNote note in noteTempo) //Process all note using time.
            {
                time[0] = note.Time;

                TimerDuration();

                if (wasSlider)
                {
                    if (sliderNoteCount != 0)
                    {
                        sliderNoteCount--;
                        lastCut = note.CutDirection; //For the spin check.

                        for (int i = 3; i > 0; i--) //Keep the timing of up to three notes before.
                        {
                            time[i] = time[i - 1];
                        }
                        continue;
                    }
                    else
                    {
                        wasSlider = false;
                    }
                }

                if (time[2] == 0.0D) //No third note processed yet.
                {
                    if (time[1] == 0.0D) //No second note processed yet.
                    {
                        time[1] = time[0]; //Skip first note.
                        continue;
                    }
                    else //The second note is processed a very specific way.
                    {
                        if (!firstSlider)
                        {
                            eventTempo.Add(new MapEvent(time[0], EventType.LightRightLasers, color));
                            eventTempo.Add(new MapEvent(0, EventType.RotatingRightLasers, 1));
                            eventTempo.Add(new MapEvent(time[1], EventType.LightLeftLasers, color));
                            eventTempo.Add(new MapEvent(0, EventType.RotatingLeftLasers, 1));
                        }
                        time[2] = time[1];
                        time[1] = time[0];
                        continue;
                    }
                }

                if (firstSlider)
                {
                    firstSlider = false;
                    continue;
                }

                // Find the next float
                if (time[0] >= nextfloat)
                {
                    for (int i = noteTempo.FindIndex(n => n == note); i < noteTempo.Count() - 1; i++)
                    {
                        if (noteTempo[i].Time == noteTempo[i - 1].Time)
                        {
                            nextfloat = noteTempo[i].Time;
                            break;
                        }
                    }
                }

                // Find the next slider (1/8 minimum)
                if (time[0] >= nextSlider.Time)
                {
                    sliderNoteCount = 0;

                    for (int i = noteTempo.FindIndex(n => n == note); i < noteTempo.Count() - 1; i++)
                    {
                        // Between 1/8 and 0, same cut direction or dots
                        if (noteTempo[i].Time - noteTempo[i - 1].Time <= 0.125 && noteTempo[i].Time - noteTempo[i - 1].Time > 0 && (noteTempo[i].CutDirection == noteTempo[i - 1].CutDirection || noteTempo[i].CutDirection == 8))
                        {
                            // Search for the last note of the slider
                            if (sliderNoteCount == 0)
                            {
                                // This is the first note of the slider
                                nextSlider = noteTempo[i - 1];
                            }
                            sliderNoteCount++;
                        }
                        else if (sliderNoteCount != 0)
                        {
                            break;
                        }
                    }
                }

                // Slider time
                if (nextSlider == note)
                {
                    // Take a light between neon, side or backlight and strobes it via On/Flash
                    if (sliderIndex == -1)
                    {
                        int old = sliderLight[sliderIndex + 1];

                        do
                        {
                            sliderLight.Shuffle();
                        } while (sliderLight[2] == old);

                        sliderIndex = 2;
                    }

                    // Place light
                    if (Options.Light.AllowFade)
                    {
                        eventTempo.Add(new MapEvent(time[0], sliderLight[sliderIndex], color - 2));
                        eventTempo.Add(new MapEvent(time[0] + 0.125f, sliderLight[sliderIndex], color - 1));
                        eventTempo.Add(new MapEvent(time[0] + 0.25f, sliderLight[sliderIndex], color - 2));
                        eventTempo.Add(new MapEvent(time[0] + 0.375f, sliderLight[sliderIndex], color - 1));
                    }
                    else
                    {
                        eventTempo.Add(new MapEvent(time[0], sliderLight[sliderIndex], color));
                        eventTempo.Add(new MapEvent(time[0] + 0.125f, sliderLight[sliderIndex], color + 1));
                        eventTempo.Add(new MapEvent(time[0] + 0.25f, sliderLight[sliderIndex], color));
                        eventTempo.Add(new MapEvent(time[0] + 0.375f, sliderLight[sliderIndex], color + 1));
                    }
                    eventTempo.Add(new MapEvent(time[0] + 0.5f, sliderLight[sliderIndex], 0));

                    sliderIndex--;

                    // Spin goes brrr
                    if (Options.Light.AllowSpinZoom)
                    {
                        eventTempo.Add(new MapEvent(time[0], EventType.RotationAllTrackRings, 0));
                        for (int i = 0; i < 8; i++)
                        {
                            eventTempo.Add(new MapEvent(time[0] + 0.5f - (0.5f / 8 * i), EventType.RotationAllTrackRings, 0));
                        }
                    }

                    wasSlider = true;
                }
                // Not a float
                else if (time[0] != nextfloat)
                {
                    if (time[0] - time[1] >= lastSpeed + 0.02 || time[0] - time[1] <= lastSpeed - 0.02 || patternCount == 20) // New speed or 20 notes of the same pattern
                    {
                        int old = 0;
                        // New pattern
                        if (patternIndex != 0)
                        {
                            old = pattern[patternIndex - 1];
                        }
                        else
                        {
                            old = pattern[maxPattern - 1];
                        }

                        do
                        {
                            pattern.Shuffle();
                        } while (pattern[0] == old);
                        patternIndex = 0;
                        patternCount = 0;
                    }

                    // Place the next light
                    eventTempo.Add(new MapEvent(time[0], pattern[patternIndex], color));

                    if (time[0] - time[1] < 0.25)
                    {
                        currentSpeed = 7;
                    }
                    else if (time[0] - time[1] >= 0.25 && time[0] - time[1] < 0.5)
                    {
                        currentSpeed = 5;
                    }
                    else if (time[0] - time[1] >= 0.5 && time[0] - time[1] < 1)
                    {
                        currentSpeed = 3;
                    }
                    else
                    {
                        currentSpeed = 1;
                    }

                    if (pattern[patternIndex] == 2)
                    {
                        eventTempo.Add(new MapEvent(time[0], EventType.RotatingLeftLasers, currentSpeed));
                    }
                    else if (pattern[patternIndex] == 3)
                    {
                        eventTempo.Add(new MapEvent(time[0], EventType.RotatingRightLasers, currentSpeed));
                    }

                    // Place off event
                    if (noteTempo[noteTempo.Count() - 1] != note)
                    {
                        if (noteTempo[noteTempo.FindIndex(n => n == note) + 1].Time == nextfloat)
                        {
                            if (noteTempo[noteTempo.FindIndex(n => n == note) + 1].Time - time[0] <= 2)
                            {
                                float value = (noteTempo[noteTempo.FindIndex(n => n == note) + 1].Time - noteTempo[noteTempo.FindIndex(n => n == note)].Time) / 2;
                                eventTempo.Add(new MapEvent(noteTempo[noteTempo.FindIndex(n => n == note)].Time + value, pattern[patternIndex], 0));
                            }
                        }
                        else
                        {
                            eventTempo.Add(new MapEvent(noteTempo[noteTempo.FindIndex(n => n == note) + 1].Time, pattern[patternIndex], 0));
                        }
                    }

                    // Pattern have 5 notes in total (5 lights available)
                    if (patternIndex < maxPattern - 1)
                    {
                        patternIndex++;
                    }
                    else
                    {
                        patternIndex = 0;
                    }

                    patternCount++;
                    lastSpeed = time[0] - time[1];
                }
                else if (time[0] - time[1] < 0.25) //Lower than fourth
                {
                    if (time[0] != last && time[0] != time[1] && note.Type != 3 && note.CutDirection != 8 && note.CutDirection != lastCut && Options.Light.AllowSpinZoom && !Options.Light.NerfStrobes) //Spin
                    {
                        last = time[0];
                        eventTempo.Add(new MapEvent(time[0], EventType.RotationAllTrackRings, 0));
                        for (int i = 0; i < 8; i++)
                        {
                            eventTempo.Add(new MapEvent(time[0] - ((time[0] - time[1]) / 8 * i), EventType.RotationAllTrackRings, 0));
                        }
                    }

                    if (time[0] == time[1])
                    {
                        CreateGenericLight(currentSpeed);
                    }
                    else
                    {
                        CreateGenericLight(currentSpeed = 7);
                    }
                }
                else if (time[0] - time[1] >= 0.25 && time[0] - time[1] < 0.5) //Quarter to half
                {
                    CreateGenericLight(currentSpeed = 5);
                }
                else if (time[0] - time[1] >= 0.5 && time[0] - time[1] < 1) //Half to 1
                {

                    CreateGenericLight(currentSpeed = 3);
                }
                else if (time[0] - time[1] >= 1) //1 and above
                {
                    CreateGenericLight(currentSpeed = 1);
                }

                lastCut = note.CutDirection; //For the spin check.

                for (int i = 3; i > 0; i--) //Keep the timing of up to three notes before.
                {
                    time[i] = time[i - 1];
                }
            }

            // Sort lights
            eventTempo = eventTempo.OrderBy(o => o.Time).ToList();

            // Remove fused
            for (int i = 1; i < eventTempo.Count() - 1; i++)
            {
                // Very close to eachother
                if (eventTempo.Any(e => e.Time == eventTempo[i].Time && e.Type == eventTempo[i].Type && e != eventTempo[i]))
                {
                    // Off event
                    if (eventTempo[i].Value == 0 || eventTempo[i].Value == 4)
                    {
                        eventTempo.Remove(eventTempo[i]);
                        i--;
                    }
                }
            }

            return eventTempo;
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
