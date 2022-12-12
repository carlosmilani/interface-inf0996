using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Interface_0996.Model
{
    public class Media : ObservableObject
    {
        private string _fileName;
        private Uri _source;
        private double _position;
        private double _volume;
        private string _mediaState = "Stopped";
        public string FileName
        {
            get { return _fileName; }
            set
            {
                SetProperty(ref _fileName, value);
            }
        }        
        public Uri Source
        {
            get { return _source; }
            set
            {
                SetProperty(ref _source, value);
            }
        }
        public double Position
        {
            get { return _position; }
            set
            {
                SetProperty(ref _position, value);
            }
        }
         public double Volume
        {
            get { return _volume; }
            set
            {
                SetProperty(ref _volume, value);
            }
        }
        public string MediaState
        {
            get { return _mediaState; }
            set
            {
                SetProperty(ref _mediaState, value);
            }
        }
        public Media(string file, string path)
        {
            this._fileName = file;
            this._source = new Uri(path);
        }
    }
}