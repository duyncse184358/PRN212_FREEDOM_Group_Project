   M S B A M L   `   `   `  ����  LibraryWpfApp*    $LibraryWpfApp.Views.BorrowBookDialog   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  📖 Borrow Book��5   	   $	��498��6    $	��450��6-    � WindowStartupLocation$ CenterOwner=�5   	   ���#F4F7FB � 	Resources 5         	CardStyle �� 
TargetType �� Property �� Value3  -System.Windows.Media.Effects.DropShadowEffect   Color   
BlurRadius   ShadowDepth	   Opacity��%N  &	        �� 5
   
   " ��6"   .�5      ��  � ��
 �����6+   �� 5       �@��CornerRadius$ 8��6-   �� 5       � �� �	#FFCBD5E06,   �� 5       � ��$ 1q�60   �� 5       �@J�Effect 5        5      $ Black��6'   $ 8Q�65   $ 2Q�6D   $		 0.1Q�6T   5      5      5      .��5      � $��20q�6   .+�5   
   �� #��[  6   
 �� Padding$
 25q�64   ._�5      �� .+�5      �� $��📖 Borrow Book��6   $��20(�5      $
��Bold"�6*   ���	#FF2D3748$��Center=�5      $��0,0,0,20q�5      �� 5"      $��0,0,0,15q�6   .+�5#      �� $��Book Title:��6     
LabelStyle#��[ 62   �� 5$      $
��Bold"�5%       ���	#FFE6E6E6 �� Padding$ 10q�5'       $��0,0,0,10q�5(       ��5$       �� *BookToBorrow.Title+5(       5)      �� 5,      $��0,0,0,10q�6   .+�5-      �� $��
Patron ID:��6   #��[ 61   �� 5.        ModernTextBoxStyle#��[ 5/      $��0,0,0,5q�50      ��5.      �� *
PatronId+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�50      51      �� 54      $��0,0,0,20q�6   .+�55      �� $��Patron Name:��6   #��[ 63   �� 56      $
��Bold"�57       ���	#FFE6E6E6$ 10q�59       ���	#FF2D3748$��0,0,0,10q�5;       ��56       �� *
PatronName+��5<      �� 5=      " ��6$   .�5>   "   ��  � �� 69   �� *
PatronName+ �� Triggers 5?   "   �� 5@   &    �� Binding 62   �� *
PatronName+ �� Value  6Q   .a�5A   *   ��  � ��$! Enter Patron ID to see name��6A   �� 5B   *    � �� �	#FF9CA3AF6G   �� 5C   *    � ��$ Italic$�6F   5D   '   �� 5E   &    62   �� *
PatronName+ 6Q   Z� .a�5F   *   ��  � ��$! Enter Patron ID to see name��6A   �� 5G   *    � �� �	#FF9CA3AF6G   �� 5H   *    � ��$ Italic$�6F   5I   '   5K      5M      5N      �� 5Q       �� Orientation$ 
Horizontal=�6   $��Center=�5R      .+�5S      �� $��✅ Confirm Borrow��6     ModernButtonStyle#��[ 5U      $	��130��5V      ���	#FF24506D$��0,0,10,0q�5W      $��25��6/   ��5T      �� *ConfirmBorrowCommand+5W   /   �� 5X      -   5        $��
❌ Cancel��5X      #��[ 5Z      ���	#FF6C757D$	��100��5\      $��25��6)   5]      5^      5_      5`      5a      