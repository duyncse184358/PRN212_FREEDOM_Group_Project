   M S B A M L   `   `   `  ����H  DLibraryWpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:LibraryWpfApp.ViewModelsLibraryWpfApp.ViewModels  5   	   *    $LibraryWpfApp.Views.ReturnBookDialog   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5vm&clr-namespace:LibraryWpfApp.ViewModels     5   	     � Title$  Return Book��5   	   $	��450��6   $	��450��6*    � WindowStartupLocation$ CenterOwner=�5   	    � DataContext 5      8   2LibraryWpfApp.ViewModels.ReturnBookDialogViewModel  5	   
    � 	Resources 5         
LabelStyle �� 
TargetType �� Property �� Value��%�   &	        �� 5   
   " ��6#   .�5      ��  � �� �	#FF3333336+   �� 5       � ��$ SemiBold"�6+   �� 5       � ��$ 0,5,0,0q�6'   5      5      .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5       $
��Auto �6   �� 5"      $��* �6   �� 5$      $
��Auto �6   .+�5(   
   �� $��Return Book��6   $��0��6'   $��20(�5)      $
��Bold"�6"   $��Center=�64   $��0,0,0,10q�6Q   �� 5+   
   $$��Enter Book ID or Borrowing ID:��6   $��1��69   #��[  6F   �� 5,   
   $��2��6     ModernTextBoxStyle#��[ 68   ��6   �� *	IDInput+68   �� 5-   
   $��Search��6   $��3��6"   $��0,0,0,10q�6Z     ModernButtonStyle#��[ 6l   ��6/   �� *SearchBorrowingCommand+6l   �� 5/      $��4��6   y�6   �� *SelectedBorrowingInfo+  NullToVisibilityConverter �� 	Converter# [ .+�50      �� $��Borrowing Details:��6   #��[  6)   �� 51      $
��Bold"�6A   $��0,0,0,2q�6S   ��6   �� *!SelectedBorrowingInfo.BookTitle+6S   �� 52      $��0,0,0,2q�6B   ��6   �� *" SelectedBorrowingInfo.PatronName+6B   �� 53      $��0,0,0,2q�6e   ��6   �� *" SelectedBorrowingInfo.BorrowDate+	 �� StringFormat$	 Borrowed: dd/MM/yyyy��6e   �� 54      $��0,0,0,2q�6]   ��6   �� *SelectedBorrowingInfo.DueDate+$	 Due: dd/MM/yyyy��6]   �� 55      $��0,0,0,2q�6X   ��6   �� *SelectedBorrowingInfo.Status+$	 Status: {0}��6X   �� 56      
���  ��$��0,0,0,10q�6m   ��6   �� *" SelectedBorrowingInfo.FineAmount+$	 Fine: {0:C}��6m   �� 59      
 �� Orientation$
 
Horizontal=�6   $��Right=�6*   $��0,10,0,0q�6F   .+�5:   
   �� $��Confirm Return��6   $��0,0,10,0q�6S   #��[ 6e   $��56��6�   $	��127��6�   ��6*   �� *ConfirmReturnCommand+6�   �� 5;   
   -   5        $��Cancel��5;      #��[ 66   $��53��6a   $��77��6m   5<      5=      5?      5@      