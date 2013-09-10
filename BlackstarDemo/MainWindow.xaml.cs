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
using BlackstarDemo.WaveFun;
using System.Media;

namespace BlackstarDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //PlayNotes();
        }

        private static void PlayNotes()
        {
            string filePath = @"C:\Users\DanAdmin\Desktop\test.wav";

            var notes = new List<Note> { 
                new Note{Frequency = Pitches.C4, Duration = TimeSpan.FromSeconds(.5)},
                new Note{Frequency = Pitches.D4, Duration = TimeSpan.FromSeconds(.5)},
                new Note{Frequency = Pitches.E4, Duration = TimeSpan.FromSeconds(.5)},
                new Note{Frequency = Pitches.F4, Duration = TimeSpan.FromSeconds(.5)},
                new Note{Frequency = Pitches.G4, Duration = TimeSpan.FromSeconds(.5)},
                new Note{Frequency = Pitches.A4, Duration = TimeSpan.FromSeconds(.5)},
                new Note{Frequency = Pitches.B4, Duration = TimeSpan.FromSeconds(.5)},
                new Note{Frequency = Pitches.C5, Duration = TimeSpan.FromSeconds(.5)}
            };

            WaveGenerator wave = new WaveGenerator(notes);
            wave.Save(filePath);

            SoundPlayer player = new SoundPlayer(filePath);
            player.Play();
        }

        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(null);

        }

        private void Image_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !_isDragging)
            {
                Point position = e.GetPosition(null);

                if (Math.Abs(position.X - _startPoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(position.Y - _startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    StartDrag((DependencyObject)sender, e);

                }
            }   
        }

        private void StartDrag(DependencyObject sender, MouseEventArgs e)
        {
            _isDragging = true;
            DataObject data = new DataObject(System.Windows.DataFormats.Text.ToString(), "abcd");
            DragDropEffects de = DragDrop.DoDragDrop(sender, data, DragDropEffects.Move);
            _isDragging = false;
        }

        private bool _isDragging;
        private Point _startPoint;
    }
}
