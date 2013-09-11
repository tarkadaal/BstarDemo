using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackstarDemo.WaveFun;

namespace BlackstarDemo
{
    public class StaveModel
    {
        public List<Note> Notes { get; private set; }
        public Dictionary<int, float> Staves
        {
            get
            {
                return new Dictionary<int, float>(_staveFrequencies);
            }
        }

        public StaveModel()
        {
            Notes = new List<Note>();
        }

        public void AddNoteToStave(int nearestStaveIndex)
        {
            Notes.Add(new Note
            {
                Frequency = _staveFrequencies[nearestStaveIndex],
                Duration = TimeSpan.FromMilliseconds(_noteDuration)
            });
        }

        private Dictionary<int, float> _staveFrequencies = new Dictionary<int, float>{
            {0, Pitches.F4},
            {1, Pitches.D4},
            {2, Pitches.B3},
            {3, Pitches.G3},
            {4, Pitches.E3}
        };

        private const int _noteDuration = 250;
    }
}
