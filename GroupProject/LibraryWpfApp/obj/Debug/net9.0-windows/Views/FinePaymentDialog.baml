   M S B A M L   `   `   `  ����H  DLibraryWpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:LibraryWpfApp.ViewModelsLibraryWpfApp.ViewModels  5   	   +    %LibraryWpfApp.Views.FinePaymentDialog   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5vm&clr-namespace:LibraryWpfApp.ViewModels     5   	     � Title$  Process Fine Payment��5   	   $	��345��6&   $	��420��63    � WindowStartupLocation$ CenterOwner=�5   	   
������� � DataContext 5	      9   3LibraryWpfApp.ViewModels.FinePaymentDialogViewModel  5
   
    � 	Resources 5         PrimaryButtonStyle �� 
TargetType �� Property �� Value  CancelButtonStyle��%�  &	        &	 Z    �� 5   
   " ��6+   .�5      ��  � �� �	#FF24506D6+   �� 5       � ��
 �����6+   �� 5       � ��$
 Bold"�6+   �� 5       � ��$
 15,8q�6(   �� 5       � ��$ 5q�6'   �� 5       � ��$ 0q�60   �� 5       �@�Cursor$
 Hand��6'   �� 5       � ��$	 120��6)   5      �� 5   
   " ��6*   .�5      ��  � �� �	#FF6C757D6+   �� 5       � ��
 �����6+   �� 5       � ��$
 Bold"�6+   �� 5       � ��$
 15,8q�6(   �� 5       � ��$ 5q�6'   �� 5        � ��$ 0q�60   �� 5!       �@�Cursor$
 Hand��6'   �� 5"       � ��$	 120��6)   5#      5$      .��5&      �� $��30q�6   .+�5(   
   �� $��0,0,0,30q�6   $��Center=�6'   .+�5)      �� $��💳 Process Fine Payment��6   $��20(�5*      $
��Bold"�6&   ���	#FF2D3748$��Center=�5,      �� 5-      $��2��6   ���	#FF24506D$	��150��5.      $��Center=�5/      $��0,8,0,0q�50      51      �� 54   
   ���	#FFF8F9FA���	#FFEEEEEE$��1q�56       �� Padding$ 20q�57      $��0,0,0,25q�58      ._�5:      �� .+�5<      ��  �� Orientation$ 
Horizontal=�6   $��0,0,0,15q�66   .+�5=      �� $��📋 Borrowing ID:��6    $	��120��5>       $��SemiBold"�5?       
���   �5@       �� 5A      $
��Bold"�5B       
���   ���5A       �� *BorrowingID+5C       5D      �� 5G      $ 
Horizontal=�6   $��0,0,0,15q�66   .+�5H      �� $��👤 Patron Name:��6    $	��120��5I       $��SemiBold"�5J       
���   �5K       �� 5L      $
��Bold"�5M       
���   ���5L       �� *
PatronName+5N       5O      �� 5R      $ 
Horizontal=�6   .+�5S      �� $��💰 Fine Amount:��6    $	��120��5T       $��SemiBold"�5U       
���   �5V       �� 5W      $��18(�5X       $
��Bold"�5Y       ���	#FFDC3545��5W       �� *
FineAmount+	 �� StringFormat$	 C��5Z       5[      5\      5]      �� 5`   
   $ 
Horizontal=�6   $��Center=�5a      .+�5b      �� $��💳 Process Payment��6   #��[  5d      ��5c      �� *ProcessPaymentCommand+5d      �� 5e      -   5        $��✖️ Cancel��5e      #��[ 5g      5h      5i      5j      