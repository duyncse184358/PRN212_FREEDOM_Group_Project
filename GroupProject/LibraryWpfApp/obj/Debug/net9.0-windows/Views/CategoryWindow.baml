   M S B A M L   `   `   `  ����H  DLibraryWpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:LibraryWpfApp.ViewModelsLibraryWpfApp.ViewModels  5   	   (    "LibraryWpfApp.Views.CategoryWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5vm&clr-namespace:LibraryWpfApp.ViewModels     5   	     � Title$  Category Management��5   	   $	��500��6%   $	��800��62    � WindowStartupLocation$ CenterScreen=�5   	    � DataContext 5      0   *LibraryWpfApp.ViewModels.CategoryViewModel  5	   
    � 	Resources 5         RightAlignedText �� 
TargetType �� Property �� Value��%c   &	        �� 5   
   " ��6)   .�5      ��  �@��TextAlignment$ Right=�6.   5      5      .��5      ]� $��10q�6   .+�5   
   �� $	��Top=�6    �� Orientation$ 
Horizontal=�6*   $��0,0,0,10q�6C   .+�5      �� $��Add New��6   $��0,0,10,0q�6F     ModernButtonStyle#��[ 6X   ��6'   �� *
AddCommand+6X   �� 5      $��Edit Selected��6   $��0,0,10,0q�6M   #��[ 6_   ��6-   �� *EditCommand+6_   �� 5      $��Delete Selected��6   #��[ 6Q   ��6/   �� *DeleteCommand+6Q   5      &   System.Windows.Controls.DataGrid  5   
      AutoGenerateColumns$ False��5      	   
IsReadOnly$
	 True��6/     ModernDataGridStyle#��[ 5      ��5      �� *
Categories+
 �� SelectedItem
 5      �� *SelectedCategory+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6P   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *
CategoryId+6P     5      $
 Name��6%   $ * 6T    63   �� *CategoryName+6T   5!      5"      5#      