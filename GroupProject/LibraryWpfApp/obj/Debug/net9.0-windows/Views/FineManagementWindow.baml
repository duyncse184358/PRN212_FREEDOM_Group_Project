   M S B A M L   `   `   `  ����H  DLibraryWpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:LibraryWpfApp.ViewModelsLibraryWpfApp.ViewModels  5   	   .    (LibraryWpfApp.Views.FineManagementWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5vm&clr-namespace:LibraryWpfApp.ViewModels     5   	     � Title$  Fine Management��5   	   $	��600��6!   $
��1000��6.    � WindowStartupLocation$ CenterScreen=�5   	    � 	Resources 5         RightAlignedText �� 
TargetType �� Property �� Value��%c   &	        �� 5	   
   " ��6)   .�5
      ��  �@��TextAlignment$ Right=�6.   5      5      .��5      ]� $��10q�6   .+�5   
   �� $��Fine Management��6   $��22(�6+   $
��Bold"�69   $��0,0,0,10q�6K   $	��Top=�6]   �� 5   
   $	��Top=�6    �� Orientation$ 
Horizontal=�6*   $��0,0,0,10q�6C   .+�5      �� $	��300��6   $��0,0,10,0q�6f    � ToolTip$ Search by patron name��6x   ��6"   �� *SearchKeyword+ �� UpdateSourceTrigger$ PropertyChanged=�6x   �� 5      $��Search��6   $��0,0,10,0q�6H   $��51��6Z   ���	#FF24506D
���������6&   �� *SearchCommand+6|   �� 5      $��	Mark Paid��6   $��0,0,10,0q�6M   $��67��6_   ���	#FF24506D
���������6)   �� *MarkPaidCommand+6�   �� 5      $��Mark Unpaid��6   $��0,0,10,0q�6Q   $��86��6c   ���	#FF24506D
���������6+   �� *MarkUnpaidCommand+6�   5      �� 5   
   $	��Top=�6   $ 
Horizontal=�6*   $��0,0,0,10q�6C   .+�5      �� $��Total fines: ��6   $
��Bold"�6-   $��15(�6?   �� 5      $��5,0,20,0q�65   $��15(�6G   ��6   �� *Fines.Count+6G   �� 5      $��Total unpaid: ��6   $
��Bold"�6.   $��15(�6@   �� 5       $��5,0,0,0q�6E   $��15(�6V   ��6   �� *TotalUnpaid+	 �� StringFormat$	 C��6V   5!      &   System.Windows.Controls.DataGrid  5$   
   -   5        
   AutoGenerateColumns$
 False��5&         
IsReadOnly$
 True��6/   ��5$      �� *Fines+ �� SelectedItem 5%      �� *SelectedFine+ �� Mode$ TwoWay=�   Columns 5(      0  *System.Windows.Controls.DataGridTextColumn  5)      ,  &System.Windows.Controls.DataGridColumn   Header$ Fine ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6Q   1  +System.Windows.Controls.DataGridBoundColumn   Binding 66   �� *FineID+6Q     5*      $ Borrowing ID��6%   $
 Auto 6[    6;   �� *BorrowingID+6[     5+      $ Patron Name��6%   $ * 6Y    6:   �� *
PatronName+6Y     5,      $ Amount��6%   $
 Auto 6`    65   �� *Amount+$	 C��6`     5-      $
 Date��6%   $
 Auto 6i    63   �� *
FineDate+$	 
dd/MM/yyyy��6i     5.      $
 Type��6%   $
 Auto 6P    63   �� *
FineType+6P     50      $ 	Late Days��6%   $
 Auto 6U    68   �� *
LateDays+6U     52      $ Due Date��6%   $
 Auto 6p    67   �� *FineDueDate+$	 
dd/MM/yyyy��6p   4  .System.Windows.Controls.DataGridCheckBoxColumn  54      $
 Paid��6)   $
 Auto 6R    67   �� *IsPaid+6R   57      58      59      