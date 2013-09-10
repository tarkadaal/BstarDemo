using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using BlackstarDemo.WaveFun;

namespace BlackstarDemo
{
    /// <summary>
    /// Interaction logic for StaveView.xaml
    /// </summary>
    public partial class StaveView : UserControl
    {
        public StaveView()
        {
            InitializeComponent();
            Notes = new List<Note>();
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _staveLines = new List<Line>();
            for (var i = 0; i < 5; i++)
            {
                var l = new Line
                {
                    X1 = 50,
                    X2 = ActualWidth - 50,
                    Y1 = 40 + (i * _staveLineSeparationDistance),
                    Y2 = 40 + (i * _staveLineSeparationDistance)
                };
                _staveLines.Add(l);
                MainCanvas.Children.Add(l);
            }
        }

        private void UserControl_PreviewDragEnter(object sender, DragEventArgs e)
        {
            _mousePosition = e.GetPosition(MainCanvas);
            //RemoveDragLine();
            //CreateDragLine();
            RemoveFloatingNote();
            CreateFloatingNote();
            e.Handled = true;
        }

        private void CreateDragLine()
        {
            _dragLineHorizontal = new Line
            {
                X1 = 0,
                X2 = MainCanvas.ActualWidth,
                Y1 = _mousePosition.Y,
                Y2 = _mousePosition.Y
            };
            MainCanvas.Children.Add(_dragLineHorizontal);
        }

        private void RemoveDragLine()
        {
            if (_dragLineHorizontal != null)
            {
                MainCanvas.Children.Remove(_dragLineHorizontal);
            }
        }

        private void RemoveFloatingNote()
        {
            if (_tempNote != null)
            {
                MainCanvas.Children.Remove(_tempNote);
            }
        }

        private void CreateFloatingNote()
        {
            _nearestStaveIndex = GetNearestStaveIndex();
            var targetY = GetYForNearestStaveLine();
            var targetX = GetXForNextNote();
            _tempNote = new Image();
            
           var src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(@"pack://application:,,,/Images/quarterNote.png");
            src.CacheOption = BitmapCacheOption.OnLoad;
            src.EndInit();
            _tempNote.Source = src;
            _tempNote.Stretch = Stretch.Uniform;
            MainCanvas.Children.Add(_tempNote);
            Canvas.SetLeft(_tempNote, targetX - (src.PixelWidth /2));
            Canvas.SetTop(_tempNote, targetY - (src.PixelHeight / 2));
        }

        private double GetYForNearestStaveLine()
        {
            return _staveLines[GetNearestStaveIndex()].Y1;
        }

        private int GetNearestStaveIndex()
        {
            if (_mousePosition.Y <= _staveLines[0].Y1)
            {
                return 0;
            }

            if (_mousePosition.Y >= _staveLines[4].Y1)
            {
                return 4;
            }

            var result = 0;
            for (var i = 0; i < 5; i++)
            {
                if (_mousePosition.Y >= _staveLines[i].Y1 - (_staveLineSeparationDistance / 2) &&
                    _mousePosition.Y < _staveLines[i].Y1 + (_staveLineSeparationDistance / 2))
                {
                    result = i;
                }
            }
            return result;
        }

        private double GetXForNextNote()
        {
            return 50 + 25 + (25 * Notes.Count);
        }

        private void UserControl_Drop(object sender, DragEventArgs e)
        {
            _tempNote = null;
            Notes.Add(new Note {Frequency = _staveFrequencies[_nearestStaveIndex], Duration = TimeSpan.FromMilliseconds(250)});
            e.Handled = true;
        }

        private Line _dragLineHorizontal;
        private Point _mousePosition;
        private List<Line> _staveLines;
        private int _staveLineSeparationDistance = 26;
        private Image _tempNote;
        private int _nearestStaveIndex;
        public List<Note> Notes { get; private set; }

        private Dictionary<int, float> _staveFrequencies = new Dictionary<int, float>{
            {0, Pitches.F4},
            {1, Pitches.D4},
            {2, Pitches.B3}
        };
    }
}
