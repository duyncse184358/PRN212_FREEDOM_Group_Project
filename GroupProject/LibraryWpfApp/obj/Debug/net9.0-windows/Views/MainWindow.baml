   M S B A M L   `   `   `  ����H  DLibraryWpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:LibraryWpfApp.ViewModelsLibraryWpfApp.ViewModels  5   	   C&clr-namespace:LibraryWpfApp.ConvertersLibraryWpfApp.Converters  5   	   $    LibraryWpfApp.Views.MainWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5vm&clr-namespace:LibraryWpfApp.ViewModels     5   	   7conv&clr-namespace:LibraryWpfApp.Converters     5   	     � Title$$  Library Book Management System��5   	   $	��500��60   $	��900��6=    � WindowStartupLocation$ CenterScreen=�5   	   ���#F4F7FB � 	Resources 5
      8   2LibraryWpfApp.Converters.BoolToVisibilityConverter   BoolToVisibility  FeatureButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Triggers	 P� Property
 P� Value3  -System.Windows.Media.Effects.DropShadowEffect  DropShadowEffect   Color   
BlurRadius   ShadowDepth   Opacity��%�  &	        &	      &	 �      5   
   6)   �� 5   
   " ��6+   .�5      ��  � ��$ 10q�6'   �� 5       � ��$ 18,14q�6(   �� 5       � ��$ 15(�6)   �� 5       � ��$ SemiBold"�6+   �� 5       � �� �#2D37486+   �� 5       � �� �	#FFF1F5F96+   �� 5       � �� �	#FFCBD5E06,   �� 5       � ��$ 1q�60   �� 5       �@�Cursor$
 Hand��6'   �� 5       � ��$	 160��6)   �� 5       � ��$ 60��6*   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5   !   #��z��5   !   $ 10��5   !   ._�5      �� $��Center=�6/   $��Center=�6L   5        5!      P� 5"      	 �@J�IsMouseOver$

 True��6=   . �5#   "   ��  � �� �	#FF007ACC6?   �� 5$   "    � ��
 �����6?   �� 5%   "    � �� �	#FF007ACC6@   5&      5(      5*      5+        5,   
   $ Black��64   $ 10Q�6B   $ 2Q�6R   $
 0.10Q�6b   5-       � DataContext 5/      ,   &LibraryWpfApp.ViewModels.MainViewModel  50   
   .��53      ]� .+�55   
   �� $	��Top=�6   ���	#FF24506D$��60��6=   $ 	0,0,12,12��6I    J� Effect# �  56      ._�57      ]� .+�58      ��  �� Orientation$ 
Horizontal=�6   $��Center=�66   $��20,0,0,0q�6Q   .+�59      �� $
��📚��6    $��28(�6*   $��0,0,10,0q�68   �� 5:      $$��Library Book Management System��6    $��20(�5;       $
��Bold"�6.   
�������$��Center=�5<       5=      5>      5?      � 5B   
   .+�5C      �� 
�������$ 12��6(   $��40,10,40,27q�6:   ���#E0E7EF$��1q�5D   +   # �  5E      ._�5F      �� $��Center=�6   $��Center=�68   .+�5G      �� $��28(�5H       $
��Bold"�6.   
���   �$��Center=�5J       $��	0,30,0,10q�6=   ��5G       �� *WelcomeMessage+5J   =   �� 5K      $>��8Manage your books, users, and library operations easily.��6    $��15(�5L       
���   �$��Center=�5M       $��0,0,0,30q�6=   � 5P      $��Center=�6    $��0,10,0,0q�6=   .+�5Q      �� #��[ 6!   
���   �$��
View Books��5R   !   ��66   �� *OpenMemberBookListCommand+�� 5S      #��[ 6!   
���   �$��Manage Borrow/Return��5T   !   ��6@   �� *OpenBorrowReturnCommand+�� 5U      #��[ 6!   
���   �$��Manage Fines��5V   !   ��68   �� *OpenFineManagementCommand+�� 5W      #��[ 6!   
���   �$��Reports��5X   !   ��63   �� *OpenReportsCommand+�� 5Y      #��[ 6!   
���   �$��User Management��5Z   !   ��6;   �� *OpenUserManagementCommand+�� 5[      #��[ 6!   
���   �$��Category Management��5\   !   ��6?   �� *OpenCategoryManagementCommand+�� 5]      #��[ 6!   
���   �$��Patron Management��5^   !   ��6=   �� *OpenPatronManagementCommand+�� 5_      #��[ 6!   
���   �$��Profile��5`      ��6   �� *OpenProfileCommand+�� 5a      #��[ 6!   
���   �$��Logout��5b   !   ��62   �� *LogoutCommand+�� 5c      -   5        #��[ 5c   !   
���   �$
��Exit��5d   !   60   5e      5f      5g      5h      5i      5j      