using System;
using System.Collections.Generic;
using System.Linq;
using static Lolighter.Items.Enum;

namespace Lolighter.Methods
{
    static class Bombs
    {
        public static List<BeatmapNote> CreateBomb(List<BeatmapNote> noteTemp)
        {
            Random random = new Random();
            List<BeatmapNote> newNote = new List<BeatmapNote>();
            BeatmapNote n = new BeatmapNote(0, 0, 0, 0, 0);

            for (int i = 0; i < noteTemp.Count(); i++) //For each note
            {
                n = noteTemp[i];

                if (n.Type == NoteType.Bomb) //Skip Bomb
                {
                    continue;
                }
                else if (n.CutDirection == CutDirection.Any) //Skip Any
                {
                    continue;
                }

                switch (n.LineLayer) //Each layer, index and cut direction are handled manually.
                {
                    case Layer.Bottom:
                        switch (n.LineIndex)
                        {
                            case Index.Left:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.Left:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Middle, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.Right:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Middle, NoteType.Bomb, CutDirection.Any));
                                        break;
                                }
                                break;
                            case Index.MiddleLeft:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.UpRight:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Middle, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.DownLeft:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Middle, NoteType.Bomb, CutDirection.Any));
                                        break;
                                }
                                break;
                            case Index.MiddleRight:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.UpLeft:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Middle, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.DownRight:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Middle, NoteType.Bomb, CutDirection.Any));
                                        break;
                                }
                                break;
                            case Index.Right:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.Left:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Middle, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.Right:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Middle, NoteType.Bomb, CutDirection.Any));
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
                                            newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.Right:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.UpLeft:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.UpRight:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.DownLeft:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.DownRight:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        break;
                                }
                                break;
                            case Index.MiddleLeft:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Middle, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Middle, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.Left:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.Right:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.UpLeft:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.UpRight:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.DownLeft:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.DownRight:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        }
                                        break;
                                }
                                break;
                            case Index.MiddleRight:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Middle, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Middle, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.Left:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.Right:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.UpLeft:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.UpRight:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.DownLeft:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.DownRight:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Top, NoteType.Bomb, CutDirection.Any));
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
                                            newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.Right:
                                        if (random.Next(1) == 0)
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        }
                                        else
                                        {
                                            newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        }
                                        break;
                                    case CutDirection.UpLeft:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.UpRight:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.DownLeft:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.DownRight:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Bottom, NoteType.Bomb, CutDirection.Any));
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
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleLeft, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.Left:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Middle, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.Right:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Middle, NoteType.Bomb, CutDirection.Any));
                                        break;
                                }
                                break;
                            case Index.MiddleLeft:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Left, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        break;
                                }
                                break;
                            case Index.MiddleRight:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        break;
                                }
                                break;
                            case Index.Right:
                                switch (n.CutDirection)
                                {
                                    case CutDirection.Up:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.Down:
                                        newNote.Add(new BeatmapNote(n.Time, Index.MiddleRight, Layer.Top, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.Left:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Middle, NoteType.Bomb, CutDirection.Any));
                                        break;
                                    case CutDirection.Right:
                                        newNote.Add(new BeatmapNote(n.Time, Index.Right, Layer.Middle, NoteType.Bomb, CutDirection.Any));
                                        break;
                                }
                                break;
                        }
                        break;
                }
            }

            newNote.AddRange(noteTemp);
            List<BeatmapNote> sorted = newNote.OrderBy(o => o.Time).ToList();

            for (int i = sorted.Count() - 5; i > 4; i--) //Dumb method to remove bomb that conflict with a note.
            {
                if (sorted[i].Type == NoteType.Bomb)
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
                }
            }

            return sorted;
        }
    }
}
