using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BlackstarDemo
{
    /// <summary>
    /// Subclass of BinaryWriter that does not close the underlying stream when it is disposed.
    /// </summary>
    public class SafeBinaryWriter: BinaryWriter
    {
        public SafeBinaryWriter(Stream stream):base(stream) { }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.BaseStream.Flush();
            }
        }
    }
}
