﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LibraryWpfApp.Views
{
    /// <summary>
    /// Interaction logic for ReturnBookDialog.xaml
    /// </summary>
    public partial class ReturnBookDialog : Window
    {
        public ReturnBookDialog()
        {
            InitializeComponent();

        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
