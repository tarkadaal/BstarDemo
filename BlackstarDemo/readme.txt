README for Demo
-------------------------

This is an extremely simple music synthesizer. Drag and drop notes from the image on to the stave, and hit Play.

This uses code taken from MSDN to help with generating the WAV files. I've modified it to cope with different frequencies, handle disposable objects better, and to generate multiple consecutive notes at the same pitch as a single wave. The original is available here:
http://blogs.msdn.com/b/dawate/archive/2009/06/24/intro-to-audio-programming-part-3-synthesizing-simple-wave-audio-using-c.aspx

There are a number of limitations - currently it only plays notes on the bars, not inbetween (so, only EGBDF), and there's no way to play sharps and flats. It's an octave too low for the treble clef, there's no way to play rests, etc. If you look in the TODO.txt file, you'll see what else I had in mind.

Likewise, I was hoping to improve the structure of the code. This application was iteratively developed. My goal was to get it working, and then restructure it. Currently the GUI is too closely coupled to the program logic. As a specific example, PlayNotes shouldn't be in MainWindow.xaml.cs. It should be moved into a class in the WaveFun namespace. There's also little in the way of MVVM at the moment. The first thing I'd want to fix is the click handler for the play button. That should be bound to an ICommand object.

This project is under source control at:
http://github.com/tarkadaal/BstarDemo