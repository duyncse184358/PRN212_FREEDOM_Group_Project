using System;
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
using LibraryWpfApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryWpfApp.Views
{
    /// <summary>
    /// Interaction logic for PatronWindow.xaml
    /// </summary>
    public partial class PatronWindow : Window
    {
        public PatronWindow()
        {
            InitializeComponent();
            this.DataContext = (Application.Current as App)?.Services.GetRequiredService<PatronViewModel>();
        }
    }
    }

