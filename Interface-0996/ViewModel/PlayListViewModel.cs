using System.Collections.ObjectModel;
using Interface_0996.Model;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using Interface_0996;

// Problema com Next
namespace Interface_0996.ViewModel
{
    public class PlayListViewModel : ObservableObject
    {
        private Model.Media? _midiaAtual;
        private Model.Media _selectMedia;
        private Uri _source;
        public ObservableCollection<Media> playList { get; set; }
        public RelayCommand Pausex { get; set; }
        public RelayCommand Playx { get; set; }
        public RelayCommand Stopx { get; set; }
        public RelayCommand Openx { get; set; }
        public RelayCommand Selectx { get; set; }
        public RelayCommand Deletarx { get; set; }
        public RelayCommand Sairx { get; set; }
        private void PauseCMD()
        {
            this.MidiaAtual.MediaState = "Paused";
            WeakReferenceMessenger.Default.Send(this);
            Pausex.NotifyCanExecuteChanged();
            Playx.NotifyCanExecuteChanged();
            Stopx.NotifyCanExecuteChanged();
        }
        private void PlayCMD()
        {
            this.MidiaAtual.MediaState = "Playing";
            // this.Source = new Uri(this.MidiaAtual.Path);
            this.Source = this.MidiaAtual.Source;
            WeakReferenceMessenger.Default.Send(this);
            Pausex.NotifyCanExecuteChanged();
            Playx.NotifyCanExecuteChanged();
            Stopx.NotifyCanExecuteChanged();
        }
        private void StopCMD()
        {
            this.MidiaAtual.MediaState = "Stopped";
            WeakReferenceMessenger.Default.Send(this);
            Pausex.NotifyCanExecuteChanged();
            Playx.NotifyCanExecuteChanged();
            Stopx.NotifyCanExecuteChanged();
        }
        private void OpenCMD()
        {
            string[] paths, files;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == true)
            {
                files = ofd.SafeFileNames;
                paths = ofd.FileNames;
                for (int x = 0; x < files.Length; x++)
                {
                    Model.Media midia = new Model.Media(files[x], paths[x]);
                    this.playList.Add(midia);
                    this.MidiaAtual = midia;
                }
                PlayCMD();
            }
        }
        private void SelectCMD()
        {
            this.MidiaAtual = this.SelectMedia;
            PlayCMD();
        }
        private void DeletarCMD()
        {
            if (this.SelectMedia == this.MidiaAtual)
            {
                StopCMD();
                this.MidiaAtual = null;
            }
            this.playList.Remove(this.SelectMedia);
        }
        private void SairCMD()
        {
            System.Windows.Application.Current.Shutdown();
        }
        private bool CanPauseCMD()
        {
            if (MidiaAtual != null)
            {
            return this.MidiaAtual.MediaState == "Playing";
            }
            return false;
        }
        private bool CanPlayCMD()
        {
            if (MidiaAtual != null)
            {
            return this.MidiaAtual.MediaState != "Playing";
            }
            return false;
        }
        private bool CanStopCMD()
        {
            if (MidiaAtual != null)
            {
            return this.MidiaAtual.MediaState != "Stopped";
            }
            return false;
        }
        private bool CanSelectCMD()
        {
            if (this.SelectMedia != null && this.SelectMedia != this.MidiaAtual)
                return true;
            return false;
        }
        private bool CanDeletarCMD()
        {
            return this.SelectMedia != null;
        }
        public Model.Media? MidiaAtual
        {
            get { return _midiaAtual; }
            set
            {
                SetProperty(ref _midiaAtual, value);
                Pausex.NotifyCanExecuteChanged();
                Playx.NotifyCanExecuteChanged();
                Stopx.NotifyCanExecuteChanged();
                Deletarx.NotifyCanExecuteChanged();
                Selectx.NotifyCanExecuteChanged();
            }
        }
        public Model.Media SelectMedia
        {
            get { return _selectMedia; }
            set
            {
                SetProperty(ref _selectMedia, value);
                Selectx.NotifyCanExecuteChanged();
                Deletarx.NotifyCanExecuteChanged();
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
        public PlayListViewModel()
        {
            Pausex = new RelayCommand(PauseCMD, CanPauseCMD);
            Playx = new RelayCommand(PlayCMD, CanPlayCMD);
            Stopx = new RelayCommand(StopCMD, CanStopCMD);
            Openx = new RelayCommand(OpenCMD);
            Selectx = new RelayCommand(SelectCMD, CanSelectCMD);
            Deletarx = new RelayCommand(DeletarCMD, CanDeletarCMD);
            Sairx = new RelayCommand(SairCMD);
            playList = new ObservableCollection<Media>();
        }
    }
}