   M S B A M L   `   `   `  ����H  DLibraryWpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:LibraryWpfApp.ViewModelsLibraryWpfApp.ViewModels  5   	   6    0LibraryWpfApp.Views.PatronBorrowingHistoryWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5vm&clr-namespace:LibraryWpfApp.ViewModels     5   	     � Title$  Patron Borrowing History��5   	   $	��500��6*   $	��900��67    � WindowStartupLocation$ CenterScreen=�5   	    � 	Resources 5	         RightAlignedText �� 
TargetType �� Property �� Value��%c   &	        �� 5
   
   " ��6)   .�5      ��  �@��TextAlignment$ Right=�6.   5      5      .��5      ]� $��10q�6   .+�5   
   �� $	��Top=�6    �� Orientation$ Vertical=�6*   $��0,0,0,10q�6A   .+�5      �� $��Patron Borrowing History��6   $��20(�68   $
��Bold"�6F   $��Center=�6X   $��0,0,0,5q�6u   �� 5      $��Center=�6   �� 5      $�Patron: ��6   $��SemiBold"�6&    �� 5      �6   �� *Patron.FullName+ �� 5      $� (ID: ��6    �� 5      �6   �� *Patron.PatronId+ �� 5      $�, Membership: ��6    �� 5      �6   �� *Patron.MembershipType+ �� 5      $�)��6   5      5      &   System.Windows.Controls.DataGrid  5   
      AutoGenerateColumns$ False��6<      
IsReadOnly$
 True��6X   ��6   �� *BorrowingHistory+	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Borrowing ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6[   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   �� *BorrowingID+6[     5       $
 
Book Title��6%   $ 2* 6W    69   �� *	BookTitle+6W     5!      $
 Borrow Date��6%   $
 Auto 6r    6:   �� *
BorrowDate+ �� StringFormat$ 
dd/MM/yyyy��6r     5"      $
 Due Date��6%   $
 Auto 6l    67   �� *	DueDate+$ 
dd/MM/yyyy��6l     5#      $
 Return Date��6%   $
 Auto 6r    6:   �� *
ReturnDate+$ 
dd/MM/yyyy��6r     5$      $
 Status��6%   $
 Auto 6P    65   �� *Status+6P     5%      $

 Fine��6%   $
 Auto 6b    63   �� *
FineAmount+$ C��6b   4  .System.Windows.Controls.DataGridCheckBoxColumn  5&      $
 	Fine Paid��6)   $
 Auto 6[    6<   �� *
IsFinePaid+6[   5(      5)      5*      