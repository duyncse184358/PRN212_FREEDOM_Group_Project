   M S B A M L   `   `   `  ����H  DLibraryWpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:LibraryWpfApp.ViewModelsLibraryWpfApp.ViewModels  5   	   &     LibraryWpfApp.Views.ReportWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5vm&clr-namespace:LibraryWpfApp.ViewModels     5   	     � Title$  Library Report��5   	   $	��600��6    $
��1000��6-    � WindowStartupLocation$ CenterScreen=�5   	    � 	Resources 5
         RightAlignedText �� 
TargetType �� Property �� Value��%c   &	        �� 5   
   " ��6)   .�5      ��  �@��TextAlignment$ Right=�6.   5      5      .��5      ]� $��10q�6   .+�5   
   �� $	��Top=�6    �� Orientation$ 
Horizontal=�6*   $��0,0,0,10q�6C   .+�5      �� $��
From Date:��6   (  "System.Windows.Controls.DatePicker  5      $��0,0,10,0q�6;     ModernDatePickerStyle#��[ 6M      SelectedDate 6   �� *
FromDate+6M   �� 5      $��To Date:��6     5      $��0,0,10,0q�69   #��[ 6K    6   �� *ToDate+6K   �� 5      $��Generate Report��6     ModernButtonStyle#��[ 6Y   ��6/   �� *GenerateReportCommand+6Y   5      �� 5   
   $��Bottom=�6   $ Vertical=�6-   $��Right=�6D   $��0,10,0,0q�6`   .+�5      �� $��0,2,0,2q�6   �� 5      $
��Bold"�6   $�Total Borrowed Books: ��6(    �� 5      �6   �� *TotalBorrowedBooksText+ �� Mode$ OneWay=�5      �� 5      $��0,2,0,2q�6   �� 5      $
��Bold"�6   $�Total Overdue Books: ��6(    �� 5       
���  ���6   �� *TotalOverdueBooksText+$ OneWay=�6J   5!      �� 5"      $��0,2,0,2q�6   �� 5#      $
��Bold"�6   $�Total Fines: ��6(    �� 5$      �6   �� *TotalFinesAmountText+$ OneWay=�5%      5&      &   System.Windows.Controls.DataGrid  5(   
   	   AutoGenerateColumns$	 False��6<   
   
IsReadOnly$

 True��6X     ModernDataGridStyle#��[ 5)      ��5(      �� *BorrowingsReport+   Columns 5*      0  *System.Windows.Controls.DataGridTextColumn  5+      ,  &System.Windows.Controls.DataGridColumn   Header$ Borrowing ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6[   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   �� *BorrowingID+6[     5,      $ 
Book Title��6%   $ 2* 6W    69   �� *	BookTitle+6W     5-      $ Patron Name��6%   $ * 6Y    6:   �� *
PatronName+6Y     5.      $ Borrow Date��6%   $
 Auto 6r    6:   �� *
BorrowDate+ �� StringFormat$ 
dd/MM/yyyy��6r     5/      $ Due Date��6%   $
 Auto 6l    67   �� *	DueDate+$ 
dd/MM/yyyy��6l     50      $ Return Date��6%   $
 Auto 6r    6:   �� *
ReturnDate+$ 
dd/MM/yyyy��6r     51      $ Status��6%   $
 Auto 6P    65   �� *Status+6P     52      $
 Fine��6%   $
 Auto 6b    63   �� *
FineAmount+$ C��6b   4  .System.Windows.Controls.DataGridCheckBoxColumn  53      $ 	Fine Paid��6)   $
 Auto 6[    6<   �� *
IsFinePaid+6[   55      56      57      