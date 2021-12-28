using System.Collections.Generic;
using System.Linq;
using Options = Lolighter.Items.Options;
using static Lolighter.Items.Enum;

namespace Lolighter.Methods
{
    static class Sliders
    {
        static public List<BeatmapNote> MakeSliders(List<BeatmapNote> noteTemp)
        {
            List<BeatmapNote> newNote = new List<BeatmapNote>();
            List<BeatmapNote> toRemove = new List<BeatmapNote>();
            BeatmapNote now = new BeatmapNote(0, 0, 0, 0, 0);
            BeatmapNote lastNote = new BeatmapNote(0, 0, 0, 0, 0);
            float secondNote = Options.Modifier.SliderPrecision > 0.0f ? 1 / Options.Modifier.SliderPrecision : 0.0f;
            float thirdNote = secondNote * 2;
            bool wrong = false;

            for (int i = noteTemp.Count() - 1; i > 0; i--) // We try to find if the current note is part of a slider or can be slidified.
            {
                now = noteTemp[i];

                if (now.Type == NoteType.Bomb) //If Bomb, skip
                {
                    continue;
                }
                else if (now.CutDirection == CutDirection.Any) //If Any, skip
                {
                    continue;
                }

                wrong = false; //If wrong, skip

                foreach (BeatmapNote temp in noteTemp) //For each note
                {
                    if (now == temp) continue; // Same note
                    else if (now.Time == temp.Time && now.Type == temp.Type && now.CutDirection == temp.CutDirection && !Options.Modifier.IsLimited) break; // Loloppe
                    else if (((now.Time - temp.Time < Options.Modifier.Limiter * 2 && now.Time - temp.Time > 0) || (temp.Time - now.Time < Options.Modifier.Limiter * 2 && temp.Time - now.Time > 0)) && temp.Type == now.Type)
                    {
                        // Already a slider, tower, stack or issue with mapping?
                        wrong = true;
                        break;
                    }
                    else if (temp.Time == now.Time && temp.Type == now.Type && now != temp)
                    {
                        // Already a slider, tower, stack or issue with mapping?
                        wrong = true;
                        break;
                    }
                    else if (now.Time == temp.Time && now.Type != temp.Type && now.CutDirection == temp.CutDirection && (now.CutDirection == CutDirection.UpLeft || now.CutDirection == CutDirection.UpRight))
                    {
                        // Diagonal double
                        wrong = true;
                        break;
                    }
                    else if (now.Time == temp.Time && temp.Type != now.Type && ((temp.LineIndex == Index.Left && now.LineIndex == Index.MiddleLeft) || (temp.LineIndex == Index.Right && now.LineIndex == Index.MiddleRight)))
                    {
                        // Collision issue
                        wrong = true;
                        break;
                    }
                }

                if (wrong) //If wrong, then skip
                {
                    continue;
                }

                switch (now.LineLayer) //Process the note into a sliders depending on layer, lane and cut direction manually. This is pretty Pepega.
                {
                    case Layer.Bottom:
                        switch (now.LineIndex)
                        {
                            case Index.Left:
                                switch (now.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Left, Layer.Middle, now.Type, CutDirection.Any));
                                        if (now.Type == NoteType.Red)
                                        {
                                            newNote.Add(new BeatmapNote(now.Time + thirdNote, Index.Left, Layer.Top, now.Type, CutDirection.Any));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(now.Time + thirdNote, Index.MiddleLeft, Layer.Top, now.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.Down:
                                        if (!noteTemp.Any(x => x != now && x.Time == now.Time && x.LineLayer == Layer.Top))
                                        {
                                            now.LineLayer = Layer.Top;
                                            newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Left, Layer.Middle, now.Type, CutDirection.Any));
                                            newNote.Add(new BeatmapNote(now.Time + thirdNote, Index.Left, Layer.Bottom, now.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.Left:
                                        if (!noteTemp.Any(x => x != now && x.Time == now.Time && x.LineIndex == Index.MiddleLeft))
                                        {
                                            now.LineIndex = Index.MiddleLeft;
                                            newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Left, Layer.Bottom, now.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.Right:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.MiddleLeft, Layer.Bottom, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpLeft:
                                        if (!noteTemp.Any(x => x != now && x.Time == now.Time && x.LineIndex == Index.MiddleLeft))
                                        {
                                            now.LineIndex = Index.MiddleLeft;
                                            newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Left, Layer.Middle, now.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.UpRight:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.MiddleRight, Layer.Top, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownLeft:
                                        if (!noteTemp.Any(x => x != now && x.Time == now.Time && x.LineIndex == Index.MiddleLeft))
                                        {
                                            now.LineIndex = Index.MiddleLeft;
                                            newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Left, Layer.Bottom, now.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.DownRight:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.MiddleLeft, Layer.Bottom, now.Type, CutDirection.Any));
                                        break;
                                }
                                break;
                            case Index.MiddleLeft:
                                switch (now.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(now.Time + thirdNote, Index.MiddleLeft, Layer.Top, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Left:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Left, Layer.Bottom, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Right:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.MiddleRight, Layer.Bottom, now.Type, CutDirection.Any));
                                        newNote.Add(new BeatmapNote(now.Time + thirdNote, Index.Right, Layer.Bottom, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpLeft:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Left, Layer.Middle, now.Type, CutDirection.Any));
                                        newNote.Add(new BeatmapNote(now.Time + thirdNote, Index.Left, Layer.Top, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpRight:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Right, Layer.Top, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownLeft:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Left, Layer.Bottom, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownRight:
                                        if (!noteTemp.Any(x => x != now && x.Time == now.Time && x.LineIndex == Index.Left && x.LineLayer == Layer.Middle))
                                        {
                                            now.LineLayer = Layer.Middle;
                                            now.LineIndex = Index.Left;
                                            newNote.Add(new BeatmapNote(now.Time + secondNote, Index.MiddleLeft, Layer.Bottom, now.Type, CutDirection.Any));
                                        }
                                        break;
                                }
                                break;
                            case Index.MiddleRight:
                                switch (now.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(now.Time + thirdNote, Index.MiddleRight, Layer.Top, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Left:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.MiddleLeft, Layer.Bottom, now.Type, CutDirection.Any));
                                        newNote.Add(new BeatmapNote(now.Time + thirdNote, Index.Left, Layer.Bottom, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Right:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Right, Layer.Bottom, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpLeft:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Left, Layer.Top, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpRight:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Right, Layer.Middle, now.Type, CutDirection.Any));
                                        newNote.Add(new BeatmapNote(now.Time + thirdNote, Index.Right, Layer.Top, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownLeft:
                                        if (!noteTemp.Any(x => x != now && x.Time == now.Time && x.LineIndex == Index.Right && x.LineLayer == Layer.Middle))
                                        {
                                            now.LineLayer = Layer.Middle;
                                            now.LineIndex = Index.Right;
                                            newNote.Add(new BeatmapNote(now.Time + secondNote, Index.MiddleRight, Layer.Bottom, now.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.DownRight:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Right, Layer.Bottom, now.Type, CutDirection.Any));
                                        break;
                                }
                                break;
                            case Index.Right:
                                switch (now.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Right, Layer.Middle, now.Type, CutDirection.Any));
                                        if (now.Type == NoteType.Red)
                                        {
                                            newNote.Add(new BeatmapNote(now.Time + thirdNote, Index.MiddleRight, Layer.Top, now.Type, CutDirection.Any));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(now.Time + thirdNote, Index.Right, Layer.Top, now.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.Down:
                                        if (!noteTemp.Any(x => x != now && x.Time == now.Time && x.LineLayer == Layer.Top))
                                        {
                                            now.LineLayer = Layer.Top;
                                            newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Right, Layer.Middle, now.Type, CutDirection.Any));
                                            newNote.Add(new BeatmapNote(now.Time + thirdNote, Index.Right, Layer.Bottom, now.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.Left:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.MiddleRight, Layer.Bottom, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Right:
                                        if (!noteTemp.Any(x => x != now && x.Time == now.Time && x.LineIndex == Index.MiddleRight))
                                        {
                                            now.LineIndex = Index.MiddleRight;
                                            newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Right, Layer.Bottom, now.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.UpLeft:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.MiddleLeft, Layer.Top, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpRight:
                                        if (!noteTemp.Any(x => x != now && x.Time == now.Time && x.LineIndex == Index.MiddleRight))
                                        {
                                            now.LineIndex = Index.MiddleRight;
                                            newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Right, Layer.Middle, now.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.DownLeft:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.MiddleRight, Layer.Bottom, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownRight:
                                        if (!noteTemp.Any(x => x != now && x.Time == now.Time && x.LineIndex == Index.MiddleRight))
                                        {
                                            now.LineIndex = Index.MiddleRight;
                                            newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Right, Layer.Bottom, now.Type, CutDirection.Any));
                                        }
                                        break;
                                }
                                break;
                        }
                        break;
                    case Layer.Middle:
                        switch (now.LineIndex)
                        {
                            case Index.Left:
                                switch (now.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Left, Layer.Top, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Left, Layer.Bottom, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpRight:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.MiddleLeft, Layer.Top, now.Type, CutDirection.Any));
                                        newNote.Add(new BeatmapNote(now.Time + thirdNote, Index.MiddleRight, Layer.Top, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownRight:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.MiddleLeft, Layer.Bottom, now.Type, CutDirection.Any));
                                        newNote.Add(new BeatmapNote(now.Time + thirdNote, Index.MiddleRight, Layer.Bottom, now.Type, CutDirection.Any));
                                        break;
                                }
                                break;
                            case Index.Right:
                                switch (now.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Right, Layer.Top, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Right, Layer.Bottom, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpLeft:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.MiddleRight, Layer.Top, now.Type, CutDirection.Any));
                                        newNote.Add(new BeatmapNote(now.Time + thirdNote, Index.MiddleLeft, Layer.Top, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownLeft:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.MiddleRight, Layer.Bottom, now.Type, CutDirection.Any));
                                        newNote.Add(new BeatmapNote(now.Time + thirdNote, Index.MiddleLeft, Layer.Bottom, now.Type, CutDirection.Any));
                                        break;
                                }
                                break;
                        }
                        break;
                    case Layer.Top:
                        switch (now.LineIndex)
                        {
                            case Index.Left:
                                switch (now.CutDirection)
                                {
                                    case CutDirection.Up:
                                        if (!noteTemp.Any(x => x != now && x.Time == now.Time && x.LineLayer == Layer.Bottom))
                                        {
                                            now.LineLayer = Layer.Bottom;
                                            newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Left, Layer.Middle, now.Type, CutDirection.Any));
                                            newNote.Add(new BeatmapNote(now.Time + thirdNote, Index.Left, Layer.Top, now.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Left, Layer.Middle, now.Type, CutDirection.Any));
                                        if (now.Type == NoteType.Red)
                                        {
                                            newNote.Add(new BeatmapNote(now.Time + thirdNote, Index.Left, Layer.Bottom, now.Type, CutDirection.Any));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(now.Time + thirdNote, Index.MiddleLeft, Layer.Bottom, now.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.Left:
                                        if (!noteTemp.Any(x => x != now && x.Time == now.Time && x.LineIndex == Index.MiddleLeft))
                                        {
                                            now.LineIndex = Index.MiddleLeft;
                                            newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Left, Layer.Top, now.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.Right:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.MiddleLeft, Layer.Top, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpLeft:
                                        if (!noteTemp.Any(x => x != now && x.Time == now.Time && x.LineLayer == Layer.Middle))
                                        {
                                            now.LineLayer = Layer.Middle;
                                            newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Left, Layer.Top, now.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.UpRight:
                                        if (!noteTemp.Any(x => x != now && x.Time == now.Time && x.LineLayer == Layer.Middle))
                                        {
                                            now.LineLayer = Layer.Middle;
                                            newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Left, Layer.Top, now.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.DownLeft:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Left, Layer.Middle, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownRight:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.MiddleRight, Layer.Bottom, now.Type, CutDirection.Any));
                                        break;
                                }
                                break;
                            case Index.MiddleLeft:
                                switch (now.CutDirection)
                                {
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(now.Time + thirdNote, Index.MiddleLeft, Layer.Bottom, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Left:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Left, Layer.Top, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Right:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.MiddleRight, Layer.Top, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpLeft:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Left, Layer.Top, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpRight:
                                        if (!noteTemp.Any(x => x != now && x.Time == now.Time && x.LineIndex == Index.Left && x.LineLayer == Layer.Middle))
                                        {
                                            now.LineIndex = Index.Left;
                                            now.LineLayer = Layer.Middle;
                                            newNote.Add(new BeatmapNote(now.Time + secondNote, Index.MiddleLeft, Layer.Top, now.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.DownLeft:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Left, Layer.Middle, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownRight:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Right, Layer.Bottom, now.Type, CutDirection.Any));
                                        break;
                                }
                                break;
                            case Index.MiddleRight:
                                switch (now.CutDirection)
                                {
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(now.Time + thirdNote, Index.MiddleRight, Layer.Bottom, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Left:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.MiddleLeft, Layer.Top, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Right:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Right, Layer.Top, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpLeft:
                                        if (!noteTemp.Any(x => x != now && x.Time == now.Time && x.LineIndex == Index.Right && x.LineLayer == Layer.Middle))
                                        {
                                            now.LineIndex = Index.Right;
                                            now.LineLayer = Layer.Middle;
                                            newNote.Add(new BeatmapNote(now.Time + secondNote, Index.MiddleRight, Layer.Top, now.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.UpRight:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Right, Layer.Top, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownLeft:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Left, Layer.Bottom, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownRight:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Right, Layer.Middle, now.Type, CutDirection.Any));
                                        break;
                                }
                                break;
                            case Index.Right:
                                switch (now.CutDirection)
                                {
                                    case CutDirection.Up:
                                        if (!noteTemp.Any(x => x != now && x.Time == now.Time && x.LineLayer == Layer.Bottom))
                                        {
                                            now.LineLayer = Layer.Bottom;
                                            newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Right, Layer.Middle, now.Type, CutDirection.Any));
                                            newNote.Add(new BeatmapNote(now.Time + thirdNote, Index.Right, Layer.Top, now.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Right, Layer.Middle, now.Type, CutDirection.Any));
                                        if (now.Type == NoteType.Blue)
                                        {
                                            newNote.Add(new BeatmapNote(now.Time + thirdNote, Index.Right, Layer.Bottom, now.Type, CutDirection.Any));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(now.Time + thirdNote, Index.MiddleRight, Layer.Bottom, now.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.Left:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.MiddleRight, Layer.Top, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Right:
                                        if (!noteTemp.Any(x => x != now && x.Time == now.Time && x.LineIndex == Index.MiddleRight))
                                        {
                                            now.LineIndex = Index.MiddleRight;
                                            newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Right, Layer.Top, now.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.UpLeft:
                                        if (!noteTemp.Any(x => x != now && x.Time == now.Time && x.LineLayer == Layer.Middle))
                                        {
                                            now.LineLayer = Layer.Middle;
                                            newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Right, Layer.Top, now.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.UpRight:
                                        if (!noteTemp.Any(x => x != now && x.Time == now.Time && x.LineLayer == Layer.Middle))
                                        {
                                            now.LineLayer = Layer.Middle;
                                            newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Right, Layer.Top, now.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.DownLeft:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.MiddleLeft, Layer.Bottom, now.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownRight:
                                        newNote.Add(new BeatmapNote(now.Time + secondNote, Index.Right, Layer.Middle, now.Type, CutDirection.Any));
                                        break;
                                }
                                break;
                        }
                        break;
                }

                lastNote = now;
            }

            noteTemp.RemoveAll(item => toRemove.Contains(item));
            newNote.AddRange(noteTemp);
            List<BeatmapNote> sorted = newNote.OrderBy(o => o.Time).ToList();

            for (int i = 0; i < sorted.Count(); i++)
            {
                if (sorted.Any(x => x != sorted[i] && x.LineIndex == sorted[i].LineIndex && x.LineLayer == sorted[i].LineLayer && x.Time == sorted[i].Time))
                {
                    sorted.Remove(sorted[i]);
                    i--;
                }
            }

            return sorted;
        }
    }
}
