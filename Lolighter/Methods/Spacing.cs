using System.Collections.Generic;
using System.Linq;
using static Lolighter.Items.Enum;

namespace Lolighter.Methods
{
    static class Spacing
    {
        static public List<BeatmapNote> Space(List<BeatmapNote> noteTemp, float spacing, float initial)
        {
            // Order by time
            noteTemp = noteTemp.OrderBy(o => o.Time).ToList();

            // Separate type
            List<BeatmapNote> red = new List<BeatmapNote>();
            List<BeatmapNote> blue = new List<BeatmapNote>();
            List<BeatmapNote> bomb = new List<BeatmapNote>();

            foreach (var note in noteTemp)
            {
                if (note.Type == NoteType.Red)
                {
                    red.Add(note);
                }
                else if (note.Type == NoteType.Blue)
                {
                    blue.Add(note);
                }
                else if (note.Type == NoteType.Bomb)
                {
                    bomb.Add(note);
                }
            }

            // Create the new list
            List<BeatmapNote> newNotes = new List<BeatmapNote>();

            // Add spacing here
            if (red.Count > 0)
            {
                red = AddSpacing(red, spacing, initial);
                newNotes.AddRange(red);
            }
            if (blue.Count > 0)
            {
                blue = AddSpacing(blue, spacing, initial);
                newNotes.AddRange(blue);
            }
            if (bomb.Count > 0)
            {
                newNotes.AddRange(bomb);
            }

            // Order by time
            newNotes = newNotes.OrderBy(o => o.Time).ToList();

            return newNotes;
        }

        public static List<BeatmapNote> AddSpacing(List<BeatmapNote> noteTemp, float spacing, float initial)
        {
            // Number of notes in the slider
            int count = 0;
            // Where the slider start
            int start = -1;
            // Notes
            BeatmapNote now;
            BeatmapNote previous = noteTemp[0];

            for (int i = 1; i < noteTemp.Count(); i++)
            {
                now = noteTemp[i];

                // Faster or equal to 1/10, Check for CutDirection
                if (now.Time - previous.Time <= initial + 0.01 && now.Time - previous.Time >= initial - 0.01 && (previous.CutDirection == CutDirection.Any || now.CutDirection == CutDirection.Any))
                {
                    if (start == -1)
                    {
                        start = i - 1;
                        count = 2;
                    }
                    else
                    {
                        // Add a note to the counter
                        count++;
                    }
                }
                // Modify the slider
                else if (start != -1)
                {
                    // Fix order
                    List<BeatmapNote> temp = new List<BeatmapNote>();
                    for (int j = 0; j < count; j++)
                    {
                        temp.Add(noteTemp[start + j]);
                    }

                    temp = CheckOrder(temp);

                    // Replace with the fixed order
                    for (int j = 0; j < count; j++)
                    {
                        noteTemp[start + j] = temp[j];
                    }

                    if (noteTemp[start].CutDirection != 8)
                    {
                        // For each note in the slider
                        for (int j = 0; j < count; j++)
                        {
                            // Add spacing to each
                            noteTemp[start + j].Time = noteTemp[start].Time + (spacing * j);
                        }
                    }

                    start = -1;
                }

                previous = noteTemp[i];
            }

            return noteTemp;
        }

        public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
            return list;
        }

        public static List<BeatmapNote> CheckOrder(List<BeatmapNote> notes)
        {
            // Analyse the sliders and fix the order
            int count = 0;
            // Find the arrow
            foreach (var note in notes)
            {
                if (note.CutDirection != 8)
                {
                    notes = Swap(notes, 0, count).ToList();
                    break;
                }

                count++;
            }

            // Here, we try to find a note close enough
            for (int i = 0; i < notes.Count() - 1; i++)
            {
                if (notes[i].LineIndex == notes[i + 1].LineIndex && (notes[i].LineLayer == notes[i + 1].LineLayer + 1 || notes[i].LineLayer == notes[i + 1].LineLayer - 1))
                {
                    // Do nothing
                }
                else if (notes[i].LineLayer == notes[i + 1].LineLayer && (notes[i].LineIndex == notes[i + 1].LineIndex + 1 || notes[i].LineIndex == notes[i + 1].LineIndex - 1))
                {
                    // Do nothing
                }
                else if (notes[i].LineIndex == notes[i + 1].LineIndex - 1 && notes[i].LineLayer == notes[i + 1].LineLayer - 1)
                {
                    // Do nothing
                }
                else if (notes[i].LineIndex == notes[i + 1].LineIndex + 1 && notes[i].LineLayer == notes[i + 1].LineLayer + 1)
                {
                    // Do nothing
                }
                else if (notes[i].LineIndex == notes[i + 1].LineIndex - 1 && notes[i].LineLayer == notes[i + 1].LineLayer + 1)
                {
                    // Do nothing
                }
                else if (notes[i].LineIndex == notes[i + 1].LineIndex + 1 && notes[i].LineLayer == notes[i + 1].LineLayer - 1)
                {
                    // Do nothing
                }
                else
                {
                    // Not linked
                    for (int j = 0; j < notes.Count() - 1; j++)
                    {
                        if (notes[i].LineIndex == notes[j].LineIndex && (notes[i].LineLayer == notes[j].LineLayer + 1 || notes[i].LineLayer == notes[j].LineLayer - 1))
                        {
                            notes = Swap(notes, i, j).ToList();
                        }
                        else if (notes[i].LineLayer == notes[j].LineLayer && (notes[i].LineIndex == notes[j].LineIndex + 1 || notes[i].LineIndex == notes[j].LineIndex - 1))
                        {
                            notes = Swap(notes, i, j).ToList();
                        }
                        else if (notes[i].LineIndex == notes[j].LineIndex - 1 && notes[i].LineLayer == notes[j].LineLayer - 1)
                        {
                            notes = Swap(notes, i, j).ToList();
                        }
                        else if (notes[i].LineIndex == notes[j].LineIndex + 1 && notes[i].LineLayer == notes[j].LineLayer + 1)
                        {
                            notes = Swap(notes, i, j).ToList();
                        }
                        else if (notes[i].LineIndex == notes[j].LineIndex - 1 && notes[i].LineLayer == notes[j].LineLayer + 1)
                        {
                            notes = Swap(notes, i, j).ToList();
                        }
                        else if (notes[i].LineIndex == notes[j].LineIndex + 1 && notes[i].LineLayer == notes[j].LineLayer - 1)
                        {
                            notes = Swap(notes, i, j).ToList();
                        }
                        // Now linked
                    }
                }
            }

            return notes;
        }
    }
}
