   M S B A M L   `   `   `  ����H  DLibraryWpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:LibraryWpfApp.ViewModelsLibraryWpfApp.ViewModels  5   	   (    "LibraryWpfApp.Views.CategoryDialog   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5vm&clr-namespace:LibraryWpfApp.ViewModels     5   	     � Title$  Category Details��5   	   $	��220��6"   $	��420��6/    � WindowStartupLocation$ CenterOwner=�5   	   ���#F4F7FB � DataContext 5	      6   0LibraryWpfApp.ViewModels.CategoryDialogViewModel  5
   
    � 	Resources 5         	CardStyle �� 
TargetType �� Property �� Value3  -System.Windows.Media.Effects.DropShadowEffect   Color   
BlurRadius	   ShadowDepth
   Opacity  DialogLabelStyle  DialogTextBoxStyle  ModernTextBoxStyle �� BasedOn  DialogButtonStyle  ModernButtonStyle��%_  &	        &	 E    &	     7  &	 �    7  �� 5   
   " ��6"   .�5      ��  � ��
 �����6+   �� 5       �@��CornerRadius$ 8��6-   �� 5       � �� �	#FFCBD5E06,   �� 5       � ��$ 1q�60   �� 5       �@J�Effect 5        5      $ Black��6'   $ 8Q�65   $	 2Q�6D   $

 0.08Q�6T   5      5      �� 5   
   " ��6)   .�5      ��  � ��$ 13(�6)   �� 5       � ��$ SemiBold"�6+   �� 5       � �� �	#FF2D37486+   �� 5       � ��$ 0,0,0,6q�6'   5       �� 5"   
   " ��6+   8   .�5#      ��  � ��$ 13(�6)   �� 5$       � ��$
 10,6q�6(   �� 5%       � ��$ 34��6'   5&      �� 5(   
   " ��6*   8   .�5)      ��  � ��$ 13(�6)   �� 5*       � ��$ SemiBold"�6+   �� 5+       � ��$
 18,8q�6(   �� 5,       � �� �	#FF24506D6+   �� 5-       � ��
 �����6+   �� 5.       � ��$ 0q�60   �� 5/       � ��$ 0,0,8,0q�6'   �� 50       �@�Cursor$
 Hand��6'   �� 51       � ��$ 34��6'   52      53      .��55      � ���#F4F7FB$��
0,10,0,-39q�6    .+�56   
   �� #��[  6    �� Padding$ 28q�64   $��20q�6A   ._�57      �� .+�59      �� $��Category Details��6   $��15(�5:      $
��Bold"�5;      ���	#FF2D3748$��Center=�5=      $��0,0,0,18q�5>      �� 5@      $
��Name��6   #��[ 6(   �� 5A      #��[ 6A   $��0,0,0,18q�6m   $��26��6   ��6   �� *Category.CategoryName+6   �� 5C       �� Orientation$ 
Horizontal=�6   $��Right=�66   .+�5D      �� -   5        $
��Save��5D      #��[ 6?   $��35��6j   �� 5E      -   5        $��Cancel��5E      #��[ 6B   ���	#FF6C757D$��0q�6�   5F      5G      5H      5I      5J      