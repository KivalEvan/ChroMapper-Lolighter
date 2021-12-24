// MIT License

// Copyright (c) 2020 Loloppe

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using Lolighter.Items;
using Lolighter.UserInterface;

namespace Lolighter
{
    [Plugin("Lolighter")]
    public class Lolighter
    {
        private UI _ui;
        private BeatSaberSongContainer _beatSaberSongContainer;
        private NotesContainer _notesContainer;
        private EventsContainer _eventsContainer;
        private BPMChangesContainer _bpmChangesContainer;

        [Init]
        private void Init()
        {
            SceneManager.sceneLoaded += SceneLoaded;
            _ui = new UI(this);
        }

        private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.buildIndex == 3)
            {
                _notesContainer = UnityEngine.Object.FindObjectOfType<NotesContainer>();
                _eventsContainer = UnityEngine.Object.FindObjectOfType<EventsContainer>();
                _bpmChangesContainer = UnityEngine.Object.FindObjectOfType<BPMChangesContainer>();
                _beatSaberSongContainer = UnityEngine.Object.FindObjectOfType<BeatSaberSongContainer>();

                MapEditorUI mapEditorUI = UnityEngine.Object.FindObjectOfType<MapEditorUI>();
                _ui.AddMenu(mapEditorUI);
            }
        }

        public void Light()
        {
            string environmentName = _beatSaberSongContainer.Song.EnvironmentName;
            List<MapEvent> newEvents = Methods.Light.CreateLight(_notesContainer.LoadedObjects.Cast<BeatmapNote>().ToList(), environmentName);
            // TODO: this should only remove lighting
            if (Options.Light.ClearLighting)
            {
                List<MapEvent> oldEvents = _eventsContainer.LoadedObjects.Cast<MapEvent>().ToList();
                foreach (var ev in oldEvents)
                {
                    if (Utils.EnvironmentLight.IsLightingEvent(ev))
                    {
                        _eventsContainer.DeleteObject(ev);
                    }
                }
            }
            foreach (var ev in newEvents)
            {
                _eventsContainer.SpawnObject(ev);
            }
        }

        public void Downlight()
        {
            List<MapEvent> oldEvents = _eventsContainer.LoadedObjects.Cast<MapEvent>().ToList();
            List<MapEvent> newEvents = Methods.DownLighter.Down(_eventsContainer.LoadedObjects.Cast<MapEvent>().ToList());
            foreach (var ev in oldEvents)
            {
                if (Utils.EnvironmentLight.IsLightingEvent(ev))
                {
                    _eventsContainer.DeleteObject(ev);
                }
            }
            foreach (var ev in newEvents)
            {
                _eventsContainer.SpawnObject(ev);
            }
        }

        public void Bombs()
        {
            List<BeatmapNote> oldNotes = _notesContainer.LoadedObjects.Cast<BeatmapNote>().ToList();
            List<BeatmapNote> newNotes = Methods.Bombs.CreateBomb(_notesContainer.LoadedObjects.Cast<BeatmapNote>().ToList());
            foreach (var n in oldNotes)
            {
                _notesContainer.DeleteObject(n);

            }
            foreach (var n in newNotes)
            {
                _notesContainer.SpawnObject(n);
            }
        }

        public void Inverted()
        {
            List<BeatmapNote> oldNotes = _notesContainer.LoadedObjects.Cast<BeatmapNote>().ToList();
            List<BeatmapNote> newNotes = Methods.Inverted.MakeInverted(_notesContainer.LoadedObjects.Cast<BeatmapNote>().ToList());
            foreach (var n in oldNotes)
            {
                _notesContainer.DeleteObject(n);

            }
            foreach (var n in newNotes)
            {
                _notesContainer.SpawnObject(n);
            }
        }

        public void Loloppe()
        {
            List<BeatmapNote> oldNotes = _notesContainer.LoadedObjects.Cast<BeatmapNote>().ToList();
            List<BeatmapNote> newNotes = Methods.Loloppe.LoloppeGen(_notesContainer.LoadedObjects.Cast<BeatmapNote>().ToList());
            foreach (var n in oldNotes)
            {
                _notesContainer.DeleteObject(n);

            }
            foreach (var n in newNotes)
            {
                _notesContainer.SpawnObject(n);
            }
        }

        public void Sliders()
        {
            List<BeatmapNote> oldNotes = _notesContainer.LoadedObjects.Cast<BeatmapNote>().ToList();
            List<BeatmapNote> newNotes = Methods.Sliders.MakeSliders(_notesContainer.LoadedObjects.Cast<BeatmapNote>().ToList());
            foreach (var n in oldNotes)
            {
                _notesContainer.DeleteObject(n);

            }
            foreach (var n in newNotes)
            {
                _notesContainer.SpawnObject(n);
            }
        }

        [Exit]
        private void Exit() { }
    }
}