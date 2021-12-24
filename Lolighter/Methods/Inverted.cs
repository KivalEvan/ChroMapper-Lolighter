using System.Collections.Generic;
using System.Linq;
using static Lolighter.Items.Enum;

namespace Lolighter.Methods
{
    static class Inverted
    {
        static public List<BeatmapNote> MakeInverted(List<BeatmapNote> noteTemp, float Limiter, bool IsLimited)
        {
            BeatmapNote n;
            bool found;

            for (int i = noteTemp.Count() - 1; i > -1; i--) //For each note in reverse-order
            {
                n = noteTemp[i];

                if (n.Type == NoteType.Bomb) //If Bomb, skip
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
                    if (((n.Time - temp.Time < Limiter && n.Time - temp.Time > 0) || (temp.Time - n.Time < Limiter && temp.Time - n.Time > 0)) && temp.Type == n.Type)
                    {
                        found = true;
                        break;
                    }
                    else if (temp.Time == n.Time && temp.Type == n.Type && n != temp)
                    {
                        found = true;
                        break;
                    }
                    else if (temp.Time == n.Time && temp.Type != n.Type && n != temp && (temp.LineIndex == n.LineIndex || temp.LineLayer == n.LineLayer))
                    {
                        found = true;
                        break;
                    }
                }

                if (found) //If found, then skip
                {
                    continue;
                }

                switch (n.CutDirection) //Based on the cut direction, change the layer of the note.
                {
                    case CutDirection.Up:
                        n.LineLayer = Layer.Bottom;
                        break;
                    case CutDirection.Down:
                        n.LineLayer = Layer.Top;
                        break;
                    case CutDirection.Left:
                        n.LineIndex = Index.Right;
                        break;
                    case CutDirection.Right:
                        n.LineIndex = Index.Left;
                        break;
                    case CutDirection.UpLeft:
                        n.LineLayer = Layer.Bottom;
                        break;
                    case CutDirection.UpRight:
                        n.LineLayer = Layer.Bottom;
                        break;
                    case CutDirection.DownLeft:
                        n.LineLayer = Layer.Top;
                        break;
                    case CutDirection.DownRight:
                        n.LineLayer = Layer.Top;
                        break;
                    case CutDirection.Any:
                        break;
                }
            }

            return noteTemp;
        }
    }
}
