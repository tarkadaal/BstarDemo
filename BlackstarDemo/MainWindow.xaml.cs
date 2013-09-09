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

            string filePath = @"C:\Users\DanAdmin\Desktop\test2.wav";
            WaveGenerator wave = new WaveGenerator(WaveExampleType.ExampleSineWave, Notes.G4, 2);
            wave.Save(filePath);

            SoundPlayer player = new SoundPlayer(filePath);
            player.Play();
        }
    }
}
