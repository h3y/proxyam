using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight;

namespace proxyam.Model
{
    public class MenuItem : ObservableObject
    {
        private string _name;
        private object _content;

        public MenuItem(string name, object content)
        {
            _name = name;
            Content = content;
        }

        public string Name
        {
            get => _name;
            set => Set(() => Name, ref _name, value);
        }

        public object Content
        {
            get => _content;
            set { Set(() => Content, ref _content, value); }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}