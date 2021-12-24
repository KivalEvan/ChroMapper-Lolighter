using System;
using System.Collections.Generic;
using System.Linq;
using static Lolighter.Items.Enum;

namespace Lolighter.Methods
{
    static class Loloppe
    {
        static public List<BeatmapNote> LoloppeGen(List<BeatmapNote> noteTemp)
        {
            Random random = new Random();
            List<BeatmapNote> newNote = new List<BeatmapNote>();
            BeatmapNote n = new BeatmapNote(0, 0, 0, 0, 0);

            for (int i = 0; i < noteTemp.Count(); i++) //For each note -> Big Pepega
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

                switch (n.LineLayer) //Process each note using their layer, lane and cut direction manually.
                {
                    case Layer.Bottom:
                        switch (n.LineIndex)
                        {
                            case Index.Left:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Bottom, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Bottom, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.Left:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Middle, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.Right:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Middle, n.Type, n.CutDirection));
                                        break;
                                }
                                break;
                            case Index.MiddleLeft:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Bottom, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Bottom, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.UpRight:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Middle, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.DownLeft:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Middle, n.Type, n.CutDirection));
                                        break;
                                }
                                break;
                            case Index.MiddleRight:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Bottom, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Bottom, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.UpLeft:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Middle, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.DownRight:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Middle, n.Type, n.CutDirection));
                                        break;
                                }
                                break;
                            case Index.Right:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Bottom, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Bottom, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.Left:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Middle, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.Right:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Middle, n.Type, n.CutDirection));
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
                                    case CutDirection.Left:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Top, n.Type, n.CutDirection));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Bottom, n.Type, n.CutDirection));
                                        }
                                        break;
                                    case CutDirection.Right:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Top, n.Type, n.CutDirection));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Bottom, n.Type, n.CutDirection));
                                        }
                                        break;
                                    case CutDirection.UpLeft:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Top, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.UpRight:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Bottom, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.DownLeft:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Bottom, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.DownRight:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Top, n.Type, n.CutDirection));
                                        break;
                                }
                                break;
                            case Index.MiddleLeft:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Middle, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Middle, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.Left:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Top, n.Type, n.CutDirection));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Bottom, n.Type, n.CutDirection));
                                        }
                                        break;
                                    case CutDirection.Right:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Top, n.Type, n.CutDirection));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Bottom, n.Type, n.CutDirection));
                                        }
                                        break;
                                    case CutDirection.UpLeft:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Bottom, n.Type, n.CutDirection));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Top, n.Type, n.CutDirection));
                                        }
                                        break;
                                    case CutDirection.UpRight:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Top, n.Type, n.CutDirection));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Bottom, n.Type, n.CutDirection));
                                        }
                                        break;
                                    case CutDirection.DownLeft:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Top, n.Type, n.CutDirection));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Bottom, n.Type, n.CutDirection));
                                        }
                                        break;
                                    case CutDirection.DownRight:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Bottom, n.Type, n.CutDirection));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Top, n.Type, n.CutDirection));
                                        }
                                        break;
                                }
                                break;
                            case Index.MiddleRight:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Middle, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Middle, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.Left:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Top, n.Type, n.CutDirection));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Bottom, n.Type, n.CutDirection));
                                        }
                                        break;
                                    case CutDirection.Right:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Top, n.Type, n.CutDirection));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Bottom, n.Type, n.CutDirection));
                                        }
                                        break;
                                    case CutDirection.UpLeft:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Bottom, n.Type, n.CutDirection));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Top, n.Type, n.CutDirection));
                                        }
                                        break;
                                    case CutDirection.UpRight:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Top, n.Type, n.CutDirection));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Bottom, n.Type, n.CutDirection));
                                        }
                                        break;
                                    case CutDirection.DownLeft:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Top, n.Type, n.CutDirection));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Bottom, n.Type, n.CutDirection));
                                        }
                                        break;
                                    case CutDirection.DownRight:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Bottom, n.Type, n.CutDirection));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Top, n.Type, n.CutDirection));
                                        }
                                        break;
                                }
                                break;
                            case Index.Right:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Left:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Top, n.Type, n.CutDirection));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Bottom, n.Type, n.CutDirection));
                                        }
                                        break;
                                    case CutDirection.Right:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Top, n.Type, n.CutDirection));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Bottom, n.Type, n.CutDirection));
                                        }
                                        break;
                                    case CutDirection.UpLeft:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Bottom, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.UpRight:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Top, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.DownLeft:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Top, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.DownRight:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Bottom, n.Type, n.CutDirection));
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
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Top, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Top, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.Left:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Middle, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.Right:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Middle, n.Type, n.CutDirection));
                                        break;
                                }
                                break;
                            case Index.MiddleLeft:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Top, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Top, n.Type, n.CutDirection));
                                        break;
                                }
                                break;
                            case Index.MiddleRight:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Top, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Top, n.Type, n.CutDirection));
                                        break;
                                }
                                break;
                            case Index.Right:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Top, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Top, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.Left:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Middle, n.Type, n.CutDirection));
                                        break;
                                    case CutDirection.Right:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Middle, n.Type, n.CutDirection));
                                        break;
                                }
                                break;
                        }
                        break;
                }
            }

            newNote = newNote.OrderBy(o => o.Time).ToList();

            //Here we remove notes that ended up stacked because of the silly algorithm.
            for (int i = newNote.Count() - 1; i > -1; i--) //For each note in reverse-order
            {
                foreach (var note in noteTemp) //For each note
                {
                    if (newNote[i].Time - note.Time < 0.125 && newNote[i].Time - note.Time > 0 && newNote[i].LineLayer == note.LineLayer && newNote[i].LineIndex == note.LineIndex)
                    {
                        newNote.Remove(newNote[i]);
                        break;
                    }
                    else if (newNote[i].Time == note.Time && newNote[i].LineLayer == note.LineLayer && newNote[i].LineIndex == note.LineIndex)
                    {
                        newNote.Remove(newNote[i]);
                        break;
                    }
                }
            }

            newNote.AddRange(noteTemp);
            List<BeatmapNote> sorted = newNote.OrderBy(o => o.Time).ToList();

            return sorted;
        }
    }
}
