using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PasswordHoarder.ViewModels.Commands;

namespace PasswordHoarder.Views.Controls
{
    /// <summary>
    /// Interaction logic for ErasableTextBox.xaml
    /// </summary>
    public partial class ErasableTextBox : UserControl
    {

        public static readonly DependencyProperty DpText =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(ErasableTextBox));

        public string Text
        {
            get => (string)GetValue(DpText);
            set => SetValue(DpText, value);
        }
        public ErasableTextBox()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            TextSource.Text = "";
        }

        private void TextSource_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Text = TextSource.Text;
        }
    }
}
