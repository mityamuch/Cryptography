
using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Wpf.Core;
using MyDES;
using System.Threading.Tasks;

namespace DES.Client.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Fields
        private ICommand _encryptCommand ;
        private ICommand _decryptCommand ;
        private ICommand _openfileCommand;

        private readonly CipherСontext _cipherContext;

        private string _enteredText;
        private string _encryptedText;
        private string _decryptedText;
        private byte[] _encryptedBytes;
        public string InFilePath { get; set; }
        public string OutFilePath { get; set; }

       
        private static byte[] bytesKey = ASCIIEncoding.ASCII.GetBytes("mityamu");
        private ICrypto DESImplementation;

        #endregion
        public MainWindowViewModel()
        {
            DESImplementation = new MyDES.DES(bytesKey);
            _cipherContext = new CipherСontext(DESImplementation);
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
           _openfileCommand ??= new RelayCommand(async _ => await OpenFileAsync());

        private void Encrypt()
        {
            var s =ASCIIEncoding.ASCII.GetBytes(EnteredText);
            _encryptedBytes = _cipherContext.Encrypt(s);
            EncryptedText = ASCIIEncoding.ASCII.GetString(_encryptedBytes);
            MessageBox.Show("Зашифровано");
        }
        private void Decrypt()
        {
            var d = _cipherContext.Decrypt(_encryptedBytes);
            DecryptedText = ASCIIEncoding.ASCII.GetString(d);
            MessageBox.Show("Расшифровано");
        }
        private async Task OpenFileAsync()
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


            using (StreamReader reader = new StreamReader(InFilePath))
            {
                using (StreamWriter writer = new StreamWriter(OutFilePath+"Encrypted", true))
                {
                    string? line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        var s = ASCIIEncoding.ASCII.GetBytes(line);
                        var encrypted = _cipherContext.Decrypt(s);
                        await writer.WriteLineAsync(ASCIIEncoding.ASCII.GetString(encrypted));
                    }
                }

            }

            using (StreamReader reader = new StreamReader(OutFilePath + "Encrypted"))
            {
                using (StreamWriter writer = new StreamWriter(OutFilePath + "Decrypted", true))
                {
                    string? line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        var s = ASCIIEncoding.ASCII.GetBytes(line);
                        var decrypted = _cipherContext.Encrypt(s);
                    }
                }
            }


        }


        #endregion 
    }

}
