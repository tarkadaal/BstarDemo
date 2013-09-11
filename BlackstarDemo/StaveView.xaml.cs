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
            _model = new StaveModel();
        }

        public List<Note> Notes { get { return _model.Notes; } }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _staveLines = new List<Line>();
            for (var i = 0; i < _model.Staves.Count; i++)
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

            RemoveFloatingNote();
            CreateFloatingNote();
            e.Handled = true;
        }

        private void RemoveFloatingNote()
        {
            if (_noteImage != null)
            {
                MainCanvas.Children.Remove(_noteImage);
            }
        }

        private void CreateFloatingNote()
        {
            _nearestStaveIndex = GetNearestStaveIndex();
            var targetY = GetYForNearestStaveLine();
            var targetX = GetXForNextNote();
            _noteImage = new Image();

            var src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(@"pack://application:,,,/Images/quarterNote.png");
            src.CacheOption = BitmapCacheOption.OnLoad;
            src.EndInit();
            _noteImage.Source = src;
            _noteImage.Stretch = Stretch.Uniform;
            MainCanvas.Children.Add(_noteImage);
            Canvas.SetLeft(_noteImage, targetX - (src.PixelWidth / 2));
            Canvas.SetTop(_noteImage, targetY - (src.PixelHeight / 2));
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

            if (_mousePosition.Y >= _staveLines[_staveLines.Count - 1].Y1)
            {
                return _staveLines.Count - 1;
            }

            var result = 0;
            for (var i = 0; i < _staveLines.Count - 1; i++)
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
            return 50 + 25 + (25 * _model.Notes.Count);
        }

        private void UserControl_Drop(object sender, DragEventArgs e)
        {
            _noteImage = null;
            _model.AddNoteToStave(_nearestStaveIndex);
            e.Handled = true;
        }



        private Point _mousePosition;
        private List<Line> _staveLines;
        private int _staveLineSeparationDistance = 26;
        private Image _noteImage;
        private int _nearestStaveIndex;

        private StaveModel _model;
    }
}
