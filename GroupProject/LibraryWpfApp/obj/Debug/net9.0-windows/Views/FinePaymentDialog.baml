   M S B A M L   `   `   `  ����H  DLibraryWpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:LibraryWpfApp.ViewModelsLibraryWpfApp.ViewModels  5   	   +    %LibraryWpfApp.Views.FinePaymentDialog   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5vm&clr-namespace:LibraryWpfApp.ViewModels     5   	     � Title$  Process Fine Payment��5   	   $	��280��6&   $	��400��63    � WindowStartupLocation$ CenterOwner=�5   	    � DataContext 5      9   3LibraryWpfApp.ViewModels.FinePaymentDialogViewModel  5	   
   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��Process Fine Payment��6   $��0��60   $��20(�5      $
��Bold"�6"   $��Center=�64   $��0,0,0,10q�6Q   �� 5   
   $��Borrowing ID:��6   $��1��6(   �� 5    
   $��2��6   $
��Bold"�6>   $��0,0,0,5q�6P   ��6!   �� *BorrowingID+6P   �� 5"   
   $��Patron Name:��6   $��3��6'   �� 5#   
   $��4��6   $
��Bold"�6=   $��0,0,0,5q�6O   ��6!   �� *
PatronName+6O   �� 5%   
   $��Fine Amount:��6   $��5��6'   �� 5&   
   $��6��6   $��18(�6M   $
��Bold"�6[   
���  ��$��0,0,0,10q�6~   ��6!   �� *
FineAmount+ �� StringFormat$ C��6~   �� 5(   
   $��7��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   $��0,10,0,0q�6W   .+�5)      �� $��Process Payment��6   $��0,0,10,0q�5+   	      ModernButtonStyle#��[  5,   	   ��5*   	   �� *ProcessPaymentCommand+5,   	   �� 5-      -   5        $��Cancel��5-      #��[  6:   5.      5/      50      