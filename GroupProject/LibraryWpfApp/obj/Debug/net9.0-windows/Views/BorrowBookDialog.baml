   M S B A M L   `   `   `  ����  LibraryWpfApp*    $LibraryWpfApp.Views.BorrowBookDialog   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Borrow Book��5   	   $	��250��6   $	��400��6*    � WindowStartupLocation$ CenterOwner=�5   	   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��Borrow Book��6   $��0��6'   $��20(�5      $
��Bold"�6"   $��Center=�64   $��0,0,0,10q�6Q   �� 5   
   $��Book Title:��6   $��1��6&   �� 5   
   $��2��6   $
��Bold"�6E   $��0,0,0,10q�6W   ��6!   �� *BookToBorrow.Title+6W   �� 5   
   $��Select Patron:��6   $��3��6)   �� 5   
   $��4��6    �� DisplayMemberPath$ FullName��5         ModernComboBoxStyle#��[  60   ��5       �� *	Patrons+ �� SelectedItem 6@   �� *SelectedPatron+5   0   �� 5   
   $��6��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   $��0,10,0,0q�6W   .+�5      �� $��Confirm Borrow��6   $��0,0,10,0q�5        ModernButtonStyle#��[ 5      ��5      �� *ConfirmBorrowCommand+5      �� 5       -   5        $��Cancel��5       #��[ 5"      5#      5$      5%      