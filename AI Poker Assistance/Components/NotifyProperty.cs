using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsControlLibrary.Components
{
    public class NotifyProperty : INotifyPropertyChanged
    {
        private string _variableText;

        // Событие, которое срабатывает при изменении свойства
        public event PropertyChangedEventHandler PropertyChanged;

        public string VariableText
        {
            get => _variableText;
            set
            {
                if (_variableText != value)
                {
                    _variableText = value;
                    OnPropertyChanged(nameof(VariableText)); // Вызываем событие при изменении значения
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
