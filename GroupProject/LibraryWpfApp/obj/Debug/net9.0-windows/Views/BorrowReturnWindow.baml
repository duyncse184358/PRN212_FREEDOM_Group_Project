   M S B A M L   `   `   `  ����H  DLibraryWpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:LibraryWpfApp.ViewModelsLibraryWpfApp.ViewModels  5   	   ,    &LibraryWpfApp.Views.BorrowReturnWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5vm&clr-namespace:LibraryWpfApp.ViewModels     5   	     � Title$#  Borrowing & Return Management��5   	   $	��600��63   $
��1000��6@    � WindowStartupLocation$ CenterScreen=�5   	   .��5
      ]� $��10q�6   .+�5   
   �� $	��Top=�6    �� Orientation$ 
Horizontal=�6*   $��0,0,0,10q�6C   .+�5      �� $	��300��6   $��0,0,10,0q�6A      ModernTextBoxStyle#��[  6S   ��6"   �� *SearchKeyword+6S   �� 5      $��Search��6   $��0,0,10,0q�6H     ModernButtonStyle#��[ 6Z   ��6&   �� *SearchCommand+6Z   5      &   System.Windows.Controls.DataGrid  5   
      AutoGenerateColumns$ False��5         
IsReadOnly$
 True��6/     ModernDataGridStyle#��[ 5      ��5      �� *
Borrowings+ �� SelectedItem 5      �� *SelectedBorrowing+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Borrowing ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6[   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6;   �� *BorrowingID+6[     5      $ 
Book Title��6%   $ 2* 6W   	 69   �� *	BookTitle+6W     5      $ Patron Name��6%   $ * 6Y   	 6:   �� *
PatronName+6Y     5      $ Borrow Date��6%   $
 Auto 6r   	 6:   �� *
BorrowDate+
 �� StringFormat$
 
dd/MM/yyyy��6r     5      $ Due Date��6%   $
 Auto 6l   	 67   �� *	DueDate+$
 
dd/MM/yyyy��6l     5      $ Return Date��6%   $
 Auto 6r   	 6:   �� *
ReturnDate+$
 
dd/MM/yyyy��6r     5      $ Status��6%   $
 Auto 6P   	 65   �� *Status+6P     5      $
 Fine��6%   $
 Auto 6b   	 63   �� *
FineAmount+$
 C��6b   4  .System.Windows.Controls.DataGridTemplateColumn  5      $ Actions��6)      CellTemplate 5       �� 5!      .R�5"      �� $ 
Horizontal=�6)   .+�5#   "   �� $��Đánh dấu mất��6)   #��[ 5&      $��2q�5'      ��5$      �� *DataContext.MarkLostCommand+ �� RelativeSource ��  �� AncestorType
 Window��5%      �� 5'      �� 5)   "   $��Đánh dấu hư hỏng��6)   #��[ 5,      $��2q�5-      ��5*      �� * DataContext.MarkDamagedCommand+ �� 
 Window��5+      �� 5-      5.      5/      51      55      56      57      