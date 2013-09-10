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
        }

        private void UserControl_PreviewDragEnter(object sender, DragEventArgs e)
        {
            _isDragging = true;
            _mousePosition = e.GetPosition(MainCanvas);
            RemoveDragLine();
            _dragLine = new Line
            {
                X1 = 0,
                X2 = MainCanvas.ActualWidth,
                Y1 = _mousePosition.Y,
                Y2 = _mousePosition.Y
            };
            MainCanvas.Children.Add(_dragLine);
            e.Handled = true;
        }

        private void RemoveDragLine()
        {
            if (_dragLine != null)
            {
                MainCanvas.Children.Remove(_dragLine);
            }
        }

        private void UserControl_Drop(object sender, DragEventArgs e)
        {
            _isDragging = false;
            RemoveDragLine();
            Background = Brushes.Pink;
            e.Handled = true;
        }

        private Line _dragLine;
        private Point _mousePosition;
        private bool _isDragging;



    }
}
