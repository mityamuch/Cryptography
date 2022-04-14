
using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Wpf.Core;
using MyDES;

namespace DES.Client.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Fields
        private ICommand _encryptCommand ;
        private ICommand _decryptCommand ;

        private ICommand _openfileCommand;

        private string _enteredText;
        private string _encryptedText;
        private string _decryptedText;
        private byte[] _encryptedBytes;
        public string InFilePath { get; set; }
        public string OutFilePath { get; set; }

        
        private static byte[] bytesKey = ASCIIEncoding.ASCII.GetBytes("mityamu");
        private DesСontext DES = new DesСontext(bytesKey, Mode.ECB);
        #endregion
        public MainWindowViewModel()
        {

        }
        #region Properties
        public string EnteredText
        {
            get =>
                _enteredText;

            set
            {
                _enteredText = value;
                OnPropertyChanged(nameof(EnteredText));
            }
        }
        public string EncryptedText
        {
            get =>
                _encryptedText;

            private set
            {
                _encryptedText = value;
                OnPropertyChanged(nameof(EncryptedText));
            }
        }
        public string DecryptedText
        {
            get =>
                _decryptedText;

            private set
            {
                _decryptedText = value;
                OnPropertyChanged(nameof(DecryptedText));
            }
        }
        #endregion
        #region Commands
        public ICommand EncryptCommand =>
            _encryptCommand ??= new RelayCommand(_ => Encrypt());
        public ICommand DecryptCommand =>
           _decryptCommand ??= new RelayCommand(_ => Decrypt());
        public ICommand OpenFileCommand =>
           _openfileCommand ??= new RelayCommand(_ => OpenFile());

        private void Encrypt()
        {
            var s =ASCIIEncoding.ASCII.GetBytes(EnteredText);
            _encryptedBytes = DES.Encrypt(s);
            EncryptedText = ASCIIEncoding.ASCII.GetString(_encryptedBytes);
            MessageBox.Show("Зашифровано");
        }
        private void Decrypt()
        {
            var d = DES.Decrypt(_encryptedBytes);
            DecryptedText = ASCIIEncoding.ASCII.GetString(d);
            MessageBox.Show("Расшифровано");
        }
        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                InFilePath = openFileDialog.FileName;
            }
            SaveFileDialog saveFileDialog=new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                OutFilePath = saveFileDialog.FileName;
            }
            using (BinaryReader reader = new BinaryReader(File.Open(InFilePath, FileMode.Open)))
            {
                string name = reader.ReadString();

            }
            using(BinaryWriter writer = new BinaryWriter(File.Open(OutFilePath+"Encrypted", FileMode.OpenOrCreate)))
            {
                    writer.Write("");
            }

            using (BinaryWriter writer = new BinaryWriter(File.Open(OutFilePath + "Deccrypted", FileMode.OpenOrCreate)))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(OutFilePath + "Encrypted", FileMode.Open)))
                {
                    string name = reader.ReadString();






                }
            }


        }


        #endregion 
    }

}
