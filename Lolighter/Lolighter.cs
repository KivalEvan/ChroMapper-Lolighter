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
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine.SceneManagement;
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
            List<MapEvent> events = Methods.Light.CreateLight(_notesContainer.LoadedObjects.Cast<BeatmapNote>().ToList(), environmentName);
            foreach (var ev in events)
            {
                _eventsContainer.SpawnObject(ev);
            }
        }

        [Exit]
        private void Exit() { }
    }
}