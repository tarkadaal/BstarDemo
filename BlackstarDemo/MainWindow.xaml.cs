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
using System.IO;

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
            DataObject data = new DataObject(System.Windows.DataFormats.Text.ToString(), "note");
            DragDropEffects de = DragDrop.DoDragDrop(sender, data, DragDropEffects.Move);
            _isDragging = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PlayNotes(Stave.Notes);
        }

        private static void PlayNotes(List<Note> notes)
        {
            using (var stream = new MemoryStream())
            {
                WaveGenerator wave = new WaveGenerator(notes);
                wave.Save(stream);

                stream.Seek(0, SeekOrigin.Begin);
                SoundPlayer player = new SoundPlayer(stream);
                player.Play();
            }
        }

        private bool _isDragging;
        private Point _startPoint;
    }

}
