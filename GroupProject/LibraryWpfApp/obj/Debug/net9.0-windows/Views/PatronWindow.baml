   M S B A M L   `   `   `  ����H  DLibraryWpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:LibraryWpfApp.ViewModelsLibraryWpfApp.ViewModels  5   	   C&clr-namespace:LibraryWpfApp.ConvertersLibraryWpfApp.Converters  5   	   &     LibraryWpfApp.Views.PatronWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5vm&clr-namespace:LibraryWpfApp.ViewModels     5   	   7conv&clr-namespace:LibraryWpfApp.Converters     5   	     � Title$  Patron Management��5   	   $	��600��6#   $
��1000��60    � WindowStartupLocation$ CenterScreen=�5   	    � 	Resources 5	         RightAlignedText �� 
TargetType �� Property �� Value��%c   &	        �� 5
   
   " ��6)   .�5      ��  �@��TextAlignment$ Right=�6.   5      5      .��5      ]� $��10q�6   .+�5   
   �� $	��Top=�6    �� Orientation$ 
Horizontal=�6*   $��0,0,0,10q�6C   .+�5      �� $	��300��6   $��0,0,10,0q�6A     ModernTextBoxStyle#��[ 6S   ��6"   �� *SearchKeyword+6S   �� 5      $��Search��6   $��0,0,10,0q�6H     ModernButtonStyle#��[ 6Z   ��6&   �� *SearchCommand+6Z   �� 5      $��Add New��6   $��0,0,10,0q�6F   #��[ 6X   ��6'   �� *
AddCommand+y�5      �� *CanManagePatrons+  BoolToVisibility �� 	Converter# [ �� 5      $��Edit Selected��6   $��0,0,10,0q�6M   #��[ 6_   ��6-   �� *EditCommand+y�5      �� *CanManagePatrons+# [ �� 5      $��Delete Selected��6   #��[ 6Q   ��6/   �� *DeleteCommand+y�5      �� *CanManagePatrons+# [ �� 5      $��View Borrowing History��6   $��20,0,0,0q�6f   #��[ 6x   ��66   �� *ViewBorrowingHistoryCommand+6x   5      &   System.Windows.Controls.DataGrid  5    
      AutoGenerateColumns$ False��5"      	   
IsReadOnly$
	 True��6/     ModernDataGridStyle#��[ 5#      ��5       �� *	Patrons+
 �� SelectedItem
 5!      �� *SelectedPatron+   Columns 5$      0  *System.Windows.Controls.DataGridTextColumn  5%      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6N   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *
PatronId+6N     5&      $ 	Full Name��6%   $ * 6U    68   �� *
FullName+6U     5'      $ Phone��6%   $
 Auto 6N    64   �� *Phone+6N     5(      $ Email��6%   $ * 6N    64   �� *Email+6N     5)      $ 
Membership��6%   $
 Auto 6\    69   �� *MembershipType+6\     5*      $ Registration Date��6%   $
 Auto 6~    6@   �� *RegistrationDate+ �� StringFormat$ 
dd/MM/yyyy��6~   5,      5-      5.      