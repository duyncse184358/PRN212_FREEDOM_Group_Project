   M S B A M L   `   `   `  ����H  DLibraryWpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:LibraryWpfApp.ViewModelsLibraryWpfApp.ViewModels  5   	   $    LibraryWpfApp.Views.BookDialog   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5vm&clr-namespace:LibraryWpfApp.ViewModels     5   	     � Title$  Book Details��5   	   $	��880��6   $	��750��6+    � WindowStartupLocation$ CenterOwner=�5   	    � 	Resources 5         
LabelStyle �� 
TargetType �� Property �� Value��%�   &	        �� 5	   
   " ��6#   .�5
      ��  � �� �	#FF3333336+   �� 5       � ��$ SemiBold"�6+   �� 5       � ��$ 0,5,0,0q�6'   5      5      .��5      �� $
��Auto=�6   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5!      $
��Auto �6   �� 5#      $
��Auto �6   �� 5%      $
��Auto �6   �� 5'      $
��Auto �6   �� 5)      $
��Auto �6   �� 5+      $
��Auto �6   �� 5-      $
��Auto �6   �� 5/      $
��Auto �6   �� 51      $
��Auto �6   �� 53      $
��Auto �6   �� 55      $
��Auto �6   �� 57      $
��Auto �6   �� 59      $
��Auto �6   �� 5;      $
��Auto �6   �� 5=      $
��Auto �6   �� 5?      $
��Auto �6   �� 5A      $
��Auto �6    � ColumnDefinitions 5E   
   �� 5F      $��* �6   .+�5I   
   �� $��Book Details��6   $��0��6(   $��20(�5J      $
��Bold"�6"   $��Center=�64   $��0,0,0,10q�6Q   �� 5L   
   $��Title��6   $��1��6    #��[  6-   �� 5M   
   $��2��6     ModernTextBoxStyle#��[ 6m   ��6   �� *
Book.Title+ �� Mode$ TwoWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=�6m   �� 5O   
   $��Author��6   $��3��6!   #��[  6.   �� 5P   
   $��4��6   #��[ 6n   ��6   �� *Book.Author+$ TwoWay=�$	 PropertyChanged=�6n   �� 5R   
   $��	Publisher��6   $��5��6$   #��[  61   �� 5S   
   $��6��6   #��[ 6q   ��6   �� *Book.Publisher+$ TwoWay=�$	 PropertyChanged=�6q   �� 5U   
   $��Publication Year��6   $��7��6+   #��[  68   �� 5V   
   $��8��6   #��[ 6w   ��6   �� *Book.PublicationYear+$ TwoWay=�$	 PropertyChanged=�6w   �� 5X   
   $��Category��6   $��9��6#   #��[  60   �� 5Y   
   $��10��6   
 �� DisplayMemberPath$
 CategoryName��5Z        ModernComboBoxStyle#��[ 64   ��5Y   !   �� *
Categories+ �� SelectedItem 6D   �� *SelectedCategory+$ TwoWay=�5Z   4   �� 5\   
   $
��ISBN��6   $��11��6   #��[  6-   �� 5]   
   $��12��6   #��[ 6m   ��6    �� *	Book.Isbn+$ TwoWay=�$	 PropertyChanged=�6m   �� 5_   
   $��Total Copies��6   $��13��6'   #��[  65   �� 5`   
   $��14��6   #��[ 6w   ��6    �� *Book.NumberOfCopies+$ TwoWay=�$	 PropertyChanged=�6w   �� 5b   
   $��Available Copies��6   $��15��6+   #��[  69   �� 5c   
   $��16��6   #��[ 6F   ��6    �� *Book.AvailableCopies+6F   �� 5e   
   $��Shelf Location��6   $��17��6)   #��[  67   �� 5f   
   $��18��6   #��[ 6v   ��6    �� *Book.ShelfLocation+$ TwoWay=�$	 PropertyChanged=�6v   �� 5h   
   $��Giá tiền (VNĐ)��6   $��19��6)   #��[  67   �� 5i   
   $��20��6   #��[ 6n   ��6    �� *
Book.Price+$ TwoWay=�$	 PropertyChanged=�6n   �� 5k   
   $��Tình trạng��6   $��21��6%   #��[  63   �� 5l   
   $��22��6   #��[ 5m      ��5l   !   �� *BookStatuses+ 6F   �� *Book.Status+$ TwoWay=�5m      �� 5o   
   $��23��6    �� Orientation$ 
Horizontal=�6#   $��Right=�6<   $��0,10,0,0q�6X   .+�5p      �� -   5        $
��Save��5p        ModernButtonStyle#��[ 6$   $��0,0,10,0q�6b   $��41��6t   $	��114��6�   �� 5q      -   5        $��Cancel��5q      #��[ 6&   $��38��6e   $	��113��6q   5r      5s      5t      5u      