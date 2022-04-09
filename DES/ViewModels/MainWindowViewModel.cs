
using Microsoft.Win32;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Wpf.Core;

namespace DES.Client.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private ICommand _encryptCommand ;
        private ICommand _decryptCommand ;

        private ICommand _openfileCommand;

        private string _enteredText;
        private string _encryptedText;
        private string _decryptedText;
        public string FilePath { get; set; }

        static byte[] bytes = ASCIIEncoding.ASCII.GetBytes("ZeroCool");
        public MainWindowViewModel()
        {

        }
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

        public ICommand EncryptCommand =>
            _encryptCommand ??= new RelayCommand(_ => Encrypt());
        public ICommand DecryptCommand =>
           _decryptCommand ??= new RelayCommand(_ => Decrypt());
        public ICommand OpenFileCommand =>
           _openfileCommand ??= new RelayCommand(_ => OpenFile());

        private void Encrypt()
        {
            MessageBox.Show("Зашифровано");
            EncryptedText = EncryptReady(EnteredText);
        }
        private void Decrypt()
        {
            MessageBox.Show("Расшифровано");
            DecryptedText = DecryptReady(EncryptedText);
        }

        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
            }


        }


        public static string EncryptReady(string originalString)
        {
            if (String.IsNullOrEmpty(originalString))
            {
                throw new ArgumentNullException
                       ("The string which needs to be encrypted can not be null.");
            }
           
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream,cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(originalString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }
        public static string DecryptReady(string cryptedString)
        {
            if (String.IsNullOrEmpty(cryptedString))
            {
                throw new ArgumentNullException
                   ("The string which needs to be decrypted can not be null.");
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream
                    (Convert.FromBase64String(cryptedString));
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();
        }
    }
}
