   M S B A M L   `   `   `  ����H  DLibraryWpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:LibraryWpfApp.ViewModelsLibraryWpfApp.ViewModels  5   	   (    "LibraryWpfApp.Views.CategoryDialog   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5vm&clr-namespace:LibraryWpfApp.ViewModels     5   	     � Title$  Category Details��5   	   $	��180��6"   $	��400��6/    � WindowStartupLocation$ CenterOwner=�5   	    � DataContext 5      6   0LibraryWpfApp.ViewModels.CategoryDialogViewModel  5	   
    � 	Resources 5         
LabelStyle �� 
TargetType �� Property �� Value��%�   &	        �� 5   
   " ��6#   .�5      ��  � �� �	#FF3333336+   �� 5       � ��$ SemiBold"�6+   �� 5       � ��$ 0,5,0,0q�6'   5      5      .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5"   
   �� $��Category Details��6   $��0��6,   $��20(�5#      $
��Bold"�6"   $��Center=�64   $��0,0,0,10q�6Q   �� 5%   
   $
��Name��6   $��1��6   #��[  6,   �� 5&   
   $��2��6     ModernTextBoxStyle#��[ 6F   ��6   �� *Category.CategoryName+6F   �� 5(   
   $��3��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   $��0,10,0,0q�6W   .+�5)      �� -   5        $
��Save��5)      $��0,0,10,0q�67     ModernButtonStyle#��[ 6I   �� 5*      -   5        $��Cancel��5*      #��[ 6:   5+      5,      5-      