using System.Collections.Generic;
using System.Linq;
using static Lolighter.Items.Enum;

namespace Lolighter.Methods
{
    static class Sliders
    {
        static public List<BeatmapNote> MakeSliders(List<BeatmapNote> noteTemp, float Limiter, bool IsLimited)
        {
            List<BeatmapNote> newNote = new List<BeatmapNote>();
            List<BeatmapNote> toRemove = new List<BeatmapNote>();
            BeatmapNote n = new BeatmapNote(0, 0, 0, 0, 0);
            BeatmapNote lastNote = new BeatmapNote(0, 0, 0, 0, 0);
            bool found = false;

            for (int i = noteTemp.Count() - 1; i > -1; i--) //For each note in reverse-order
            {
                n = noteTemp[i];

                if (n.Type == NoteType.Bomb) //If Bomb, skip
                {
                    continue;
                }
                else if (n.CutDirection == CutDirection.Any) //If Any, skip
                {
                    continue;
                }

                found = false;

                foreach (BeatmapNote temp in noteTemp) //For each note
                {
                    if (n.Time == temp.Time && n.Type == temp.Type && n.CutDirection == temp.CutDirection && !IsLimited)
                    {
                        //Loloppe notes
                        break;
                    }
                    else if (((n.Time - temp.Time < Limiter * 2 && n.Time - temp.Time > 0) || (temp.Time - n.Time < Limiter * 2 && temp.Time - n.Time > 0)) && temp.Type == n.Type)
                    {
                        found = true;
                        break;
                    }
                    else if (temp.Time == n.Time && temp.Type == n.Type && n != temp)
                    {
                        found = true;
                        break;
                    }
                    else if (n.Time == temp.Time && n.Type != temp.Type && n.CutDirection == temp.CutDirection && (n.CutDirection == CutDirection.UpLeft || n.CutDirection == CutDirection.UpRight))
                    {
                        found = true;
                        break;
                    }
                    else if (n.Time == temp.Time && temp.Type != n.Type && ((temp.LineIndex == Index.Left && n.LineIndex == Index.MiddleLeft) || (temp.LineIndex == Index.Right && n.LineIndex == Index.MiddleRight)))
                    {
                        found = true;
                        break;
                    }
                }

                if (found) //If found, then skip
                {
                    continue;
                }

                switch (n.LineLayer) //Process the note into a sliders depending on layer, lane and cut direction manually. This is pretty Pepega.
                {
                    case Layer.Bottom:
                        switch (n.LineIndex)
                        {
                            case Index.Left:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Left, Layer.Middle, n.Type, CutDirection.Any));
                                        if (n.Type == NoteType.Red)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time + 0.0625f, Index.Left, Layer.Top, n.Type, CutDirection.Any));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time + 0.0625f, Index.MiddleLeft, Layer.Top, n.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.Down:
                                        n.LineLayer = Layer.Top;
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Left, Layer.Middle, n.Type, CutDirection.Any));
                                        newNote.Add(new BeatmapNote(n.Time + 0.0625f, Index.Left, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Left:
                                        n.LineIndex = Index.MiddleLeft;
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Left, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Right:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.MiddleLeft, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpLeft:
                                        n.LineIndex = 1;
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Left, Layer.Middle, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpRight:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.MiddleRight, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownLeft:
                                        n.LineIndex = Index.MiddleLeft;
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Left, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownRight:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.MiddleLeft, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                }
                                break;
                            case Index.MiddleLeft:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(n.Time + 0.0625f, Index.MiddleLeft, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Left:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Left, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Right:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.MiddleRight, Layer.Bottom, n.Type, CutDirection.Any));
                                        newNote.Add(new BeatmapNote(n.Time + 0.0625f, Index.Right, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpLeft:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Left, Layer.Middle, n.Type, CutDirection.Any));
                                        newNote.Add(new BeatmapNote(n.Time + 0.0625f, Index.Left, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpRight:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Right, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownLeft:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Left, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownRight:
                                        n.LineLayer = Layer.Middle;
                                        n.LineIndex = Index.Left;
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.MiddleLeft, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                }
                                break;
                            case Index.MiddleRight:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(n.Time + 0.0625f, Index.MiddleRight, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Left:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.MiddleLeft, Layer.Bottom, n.Type, CutDirection.Any));
                                        newNote.Add(new BeatmapNote(n.Time + 0.0625f, Index.Left, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Right:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Right, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpLeft:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Left, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpRight:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Right, Layer.Middle, n.Type, CutDirection.Any));
                                        newNote.Add(new BeatmapNote(n.Time + 0.0625f, Index.Right, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownLeft:
                                        n.LineLayer = Layer.Middle;
                                        n.LineIndex = Index.Right;
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.MiddleRight, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownRight:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Right, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                }
                                break;
                            case Index.Right:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Right, Layer.Middle, n.Type, CutDirection.Any));
                                        if (n.Type == NoteType.Red)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time + 0.0625f, Index.MiddleRight, Layer.Top, n.Type, CutDirection.Any));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time + 0.0625f, Index.Right, Layer.Top, n.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.Down:
                                        n.LineLayer = Layer.Top;
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Right, Layer.Middle, n.Type, CutDirection.Any));
                                        newNote.Add(new BeatmapNote(n.Time + 0.0625f, Index.Right, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Left:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.MiddleRight, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Right:
                                        n.LineIndex = Index.MiddleRight;
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Right, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpLeft:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.MiddleLeft, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpRight:
                                        n.LineIndex = Index.MiddleRight;
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Right, Layer.Middle, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownLeft:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.MiddleRight, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownRight:
                                        n.LineIndex = Index.MiddleRight;
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Right, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                }
                                break;
                        }
                        break;
                    case Layer.Middle:
                        switch (n.LineIndex)
                        {
                            case Index.Left:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Left, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Left, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpRight:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.MiddleLeft, Layer.Top, n.Type, CutDirection.Any));
                                        newNote.Add(new BeatmapNote(n.Time + 0.0625f, Index.MiddleRight, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownLeft:
                                        break;
                                    case CutDirection.DownRight:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.MiddleLeft, Layer.Bottom, n.Type, CutDirection.Any));
                                        newNote.Add(new BeatmapNote(n.Time + 0.0625f, Index.MiddleRight, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                }
                                break;
                            case Index.MiddleLeft:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Left:
                                        n.LineIndex = Index.Left;
                                        break;
                                    case CutDirection.Right:
                                        n.LineIndex = Index.Right;
                                        break;
                                }
                                break;
                            case Index.MiddleRight:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Left:
                                        n.LineIndex = Index.Left;
                                        break;
                                    case CutDirection.Right:
                                        n.LineIndex = Index.Right;
                                        break;
                                }
                                break;
                            case Index.Right:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Right, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Right, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpLeft:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.MiddleRight, Layer.Top, n.Type, CutDirection.Any));
                                        newNote.Add(new BeatmapNote(n.Time + 0.0625f, Index.MiddleLeft, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownLeft:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.MiddleRight, Layer.Bottom, n.Type, CutDirection.Any));
                                        newNote.Add(new BeatmapNote(n.Time + 0.0625f, Index.MiddleLeft, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                }
                                break;
                        }
                        break;
                    case Layer.Top:
                        switch (n.LineIndex)
                        {
                            case Index.Left:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        n.LineLayer = Layer.Bottom;
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Left, Layer.Middle, n.Type, CutDirection.Any));
                                        newNote.Add(new BeatmapNote(n.Time + 0.0625f, Index.Left, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Left, Layer.Middle, n.Type, CutDirection.Any));
                                        if (n.Type == NoteType.Red)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time + 0.0625f, Index.Left, Layer.Bottom, n.Type, CutDirection.Any));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time + 0.0625f, Index.MiddleLeft, Layer.Bottom, n.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.Left:
                                        n.LineIndex = Index.MiddleLeft;
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Left, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Right:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.MiddleLeft, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpLeft:
                                        n.LineLayer = Layer.Middle;
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Left, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpRight:
                                        n.LineLayer = Layer.Middle;
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Left, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownLeft:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Left, Layer.Middle, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownRight:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.MiddleRight, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                }
                                break;
                            case Index.MiddleLeft:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time + 0.0625f, Index.MiddleLeft, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Left:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Left, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Right:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.MiddleRight, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpLeft:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Left, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpRight:
                                        n.LineIndex = Index.Left;
                                        n.LineLayer = Layer.Middle;
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.MiddleLeft, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownLeft:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Left, Layer.Middle, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownRight:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Right, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                }
                                break;
                            case Index.MiddleRight:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time + 0.0625f, Index.MiddleRight, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Left:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.MiddleLeft, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Right:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Right, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpLeft:
                                        n.LineIndex = Index.Right;
                                        n.LineLayer = Layer.Middle;
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.MiddleRight, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpRight:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Right, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownLeft:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Left, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownRight:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Right, Layer.Middle, n.Type, CutDirection.Any));
                                        break;
                                }
                                break;
                            case Index.Right:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        n.LineLayer = Layer.Bottom;
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Right, Layer.Middle, n.Type, CutDirection.Any));
                                        newNote.Add(new BeatmapNote(n.Time + 0.0625f, Index.Right, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Right, Layer.Middle, n.Type, CutDirection.Any));
                                        if (n.Type == NoteType.Blue)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time + 0.0625f, Index.Right, Layer.Bottom, n.Type, CutDirection.Any));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time + 0.0625f, Index.MiddleRight, Layer.Bottom, n.Type, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.Left:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.MiddleRight, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.Right:
                                        n.LineIndex = Index.MiddleRight;
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Right, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpLeft:
                                        n.LineLayer = Layer.Middle;
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Right, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.UpRight:
                                        n.LineLayer = Layer.Middle;
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Right, Layer.Top, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownLeft:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.MiddleLeft, Layer.Bottom, n.Type, CutDirection.Any));
                                        break;
                                    case CutDirection.DownRight:
                                        newNote.Add(new BeatmapNote(n.Time + 0.03125f, Index.Right, Layer.Middle, n.Type, CutDirection.Any));
                                        break;
                                }
                                break;
                        }
                        break;
                }

                lastNote = n;
            }

            noteTemp.RemoveAll(item => toRemove.Contains(item));
            newNote.AddRange(noteTemp);
            List<BeatmapNote> sorted = newNote.OrderBy(o => o.Time).ToList();

            //Here, we try to remove stacked notes that has been created.
            for (int i = sorted.Count() - 5; i > 4; i--) //For each note in reverse-order
            {
                if (sorted[i].CutDirection == CutDirection.Any) //If it's a dot
                {
                    if (sorted[i].Time - sorted[i - 1].Time <= 0.25 && sorted[i].LineLayer == sorted[i - 1].LineLayer && sorted[i].LineIndex == sorted[i - 1].LineIndex)
                    {
                        sorted.Remove(sorted[i]);
                    }
                    else if (sorted[i].Time - sorted[i - 2].Time <= 0.25 && sorted[i].LineLayer == sorted[i - 2].LineLayer && sorted[i].LineIndex == sorted[i - 2].LineIndex)
                    {
                        sorted.Remove(sorted[i]);
                    }
                    else if (sorted[i].Time - sorted[i - 3].Time <= 0.25 && sorted[i].LineLayer == sorted[i - 3].LineLayer && sorted[i].LineIndex == sorted[i - 3].LineIndex)
                    {
                        sorted.Remove(sorted[i]);
                    }
                    else if (sorted[i].Time - sorted[i - 4].Time <= 0.25 && sorted[i].LineLayer == sorted[i - 4].LineLayer && sorted[i].LineIndex == sorted[i - 4].LineIndex)
                    {
                        sorted.Remove(sorted[i]);
                    }
                    else if (sorted[i + 1].Time - sorted[i].Time <= 0.25 && sorted[i].LineLayer == sorted[i + 1].LineLayer && sorted[i].LineIndex == sorted[i + 1].LineIndex)
                    {
                        sorted.Remove(sorted[i]);
                    }
                    else if (sorted[i + 2].Time - sorted[i].Time <= 0.25 && sorted[i].LineLayer == sorted[i + 2].LineLayer && sorted[i].LineIndex == sorted[i + 2].LineIndex)
                    {
                        sorted.Remove(sorted[i]);
                    }
                    else if (sorted[i + 3].Time - sorted[i].Time <= 0.25 && sorted[i].LineLayer == sorted[i + 3].LineLayer && sorted[i].LineIndex == sorted[i + 3].LineIndex)
                    {
                        sorted.Remove(sorted[i]);
                    }
                    else if (sorted[i + 4].Time - sorted[i].Time <= 0.25 && sorted[i].LineLayer == sorted[i + 4].LineLayer && sorted[i].LineIndex == sorted[i + 4].LineIndex)
                    {
                        sorted.Remove(sorted[i]);
                    }
                    else if (sorted[i].Time - sorted[i - 1].Time <= 0.25 && sorted[i - 1].CutDirection == 8 && sorted[i].LineIndex == sorted[i - 1].LineIndex && sorted[i].Type != sorted[i - 1].Type)
                    {
                        sorted.Remove(sorted[i - 1]);
                    }
                }
            }

            return sorted;
        }
    }
}
