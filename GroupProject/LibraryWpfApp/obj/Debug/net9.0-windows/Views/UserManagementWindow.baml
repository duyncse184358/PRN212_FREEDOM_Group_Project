   M S B A M L   `   `   `  ����H  DLibraryWpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:LibraryWpfApp.ViewModelsLibraryWpfApp.ViewModels  5   	   .    (LibraryWpfApp.Views.UserManagementWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5vm&clr-namespace:LibraryWpfApp.ViewModels     5   	     � Title$  User Management��5   	   $	��500��6!   $	��800��6.    � WindowStartupLocation$ CenterScreen=�5   	    � DataContext 5      ,   &LibraryWpfApp.ViewModels.UserViewModel  5	   
    � 	Resources 5         RightAlignedText �� 
TargetType �� Property �� Value��%c   &	        �� 5   
   " ��6)   .�5      ��  �@��TextAlignment$ Right=�6.   5      5      .��5      ]� $��10q�6   .+�5   
   �� $	��Top=�6    �� Orientation$ 
Horizontal=�6*   $��0,0,0,10q�6C   .+�5      �� $��Add New User��6   $��0,0,10,0q�6K     ModernButtonStyle#��[ 6]   ��6,   �� *
AddCommand+6]   �� 5      $��Edit Selected User��6   $��0,0,10,0q�6R   #��[ 6d   ��62   �� *EditCommand+6d   �� 5      $��Delete Selected User��6   #��[ 6V   ��64   �� *DeleteCommand+6V   5      &   System.Windows.Controls.DataGrid  5   
      AutoGenerateColumns$ False��5      	   
IsReadOnly$
	 True��6/     ModernDataGridStyle#��[ 5      ��5      �� *Users+
 �� SelectedItem
 5      �� *SelectedUser+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *UserId+6L     5      $ Username��6%   $ * 6T    67   �� *
Username+6T     5       $ 	Full Name��6%   $ * 6U    68   �� *
FullName+6U     5!      $
 Role��6%   $
 Auto 6L    63   �� *Role+6L   5#      5$      5%      