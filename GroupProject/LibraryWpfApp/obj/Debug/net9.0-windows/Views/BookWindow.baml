   M S B A M L   `   `   `  ����H  DLibraryWpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:LibraryWpfApp.ViewModelsLibraryWpfApp.ViewModels  5   	   C&clr-namespace:LibraryWpfApp.ConvertersLibraryWpfApp.Converters  5   	   $    LibraryWpfApp.Views.BookWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5vm&clr-namespace:LibraryWpfApp.ViewModels     5   	   7conv&clr-namespace:LibraryWpfApp.Converters     5   	     � Title$  Book Management��5   	   $	��700��6!   $
��1200��6.    � WindowStartupLocation$ CenterScreen=�5   	   ���#F4F7FB � 	Resources 5
         RightAlignedText �� 
TargetType �� Property �� Value8   2LibraryWpfApp.Converters.BoolToVisibilityConverter  BoolToVisibility  EnhancedButtonStyle  ModernButtonStyle �� BasedOn �� 
TargetType �� CornerRadius	 �� Triggers
 P� Property P� Value  AddButtonStyle �� Triggers  EditButtonStyle  DeleteButtonStyle  BorrowButtonStyle  SearchTextBoxStyle 	 ModernTextBoxStyle ��Name 
 	CardStyle3  -System.Windows.Media.Effects.DropShadowEffect   Color   
BlurRadius   ShadowDepth   Opacity  BlueHeaderDataGridStyle&   System.Windows.Controls.DataGrid  ModernDataGridStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%  &	        &	 Y     &	 l     7  &	 �    7  &	 �    7  &	 �    7  &	 s    7  &	 ?    7 	 &	
 v	    &	 �
    7  �� 5   
   " ��6)   .�5      ��  �@��TextAlignment$ Right=�6.   5        5   
   6)   �� 5   
   " ��6,   8   .�5      ��  � �� �	#FF007ACC6+   �� 5       � ��
 �����6+   �� 5       � ��$
 12,8q�6(   �� 5       � ��$ 5q�6'   �� 5       � ��$ 0q�60   �� 5       � ��$ SemiBold"�6+   �� 5       �@�Cursor$
 Hand��6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5   !   #��z��5   !   $ 6��5   !   ._�5       �� $��Center=�6/   $��Center=�6L   5!      	 5"      P� 5#      
 �@J�IsMouseOver$
 True��6=   . �5$   "   ��  � �� �	#FF0066AA6?   5%      P� 5&      
 � ��$
 True��6;   . �5'   "   ��  � �� �	#FF0044886?   5(      P� 5)      
 � {�$ False��6;   . �5*   "   ��  � �� �	#FFCCCCCC6?   �� 5+   "    � �� �	#FF8888886?   5,      5.      50      51      �� 54   
   " ��6'   8   .�55      ��  � �� �	#FF28A7456+    56      P� 57      
 �@J�IsMouseOver$
 True��61   . �58      ��  � �� �	#FF1E7E3463   59      5;      �� 5=   
   " ��6(   8   .�5>      ��  � �� �	#FFFFC1076+   �� 5?       � �� �	#FF2125296+    5@      P� 5A      
 �@J�IsMouseOver$
 True��61   . �5B      ��  � �� �	#FFE0A80063   5C      5E      �� 5G   
   " ��6*   8   .�5H      ��  � �� �	#FFDC35456+    5I      P� 5J      
 �@J�IsMouseOver$
 True��61   . �5K      ��  � �� �	#FFC8233363   5L      5N      �� 5P   
   " ��6*   8   .�5Q      ��  � �� �	#FF17A2B86+    5R      P� 5S      
 �@J�IsMouseOver$
 True��61   . �5T      ��  � �� �	#FF13849663   5U      5W      �� 5Z   
   " ��6+   8   .�5[      ��  � ��$
 10,8q�6(   �� 5\       � ��$ 14(�6)   �� 5]       � �� �	#FFCBD5E06,   �� 5^       � ��$ 1q�60   �� 5_       � �� 5`      �� 5a      " ��6&   .R�5b      �� #��z��6!   #��z��5c   !   #��z��5d   !   $ 6��5e   !   ._�5f      �� $ PART_ContentHost��6+   #��z��6E   5g      	 5h      P� 5i      
 �@J�	IsFocused$
 True��6;   . �5j   "   ��  � �� �	#FF007ACC6@   �� 5k   "    � ��$ 2q�6D   5l      5n      5p      5q      �� 5t   
   " ��6"   .�5u      ��  � ��
 �����6+   �� 5v       �@��CornerRadius$ 8��6-   �� 5w       � �� �	#FFCBD5E06,   �� 5x       � ��$ 1q�60   �� 5y       �@J�Effect 5z        5{      $ Black��6'   $ 8Q�65   $ 2Q�6D   $	 0.1Q�6T   5}      5~      �� 5�   
   "  60   8   .�5�      ��  �@ ColumnHeaderStyle 5�      �� 5�      "  6   .�5�      ��  � �� �	#FF24506D67   �� 5�       � ��
 �����67   �� 5�       � ��$ SemiBold"�67   �� 5�       � ��$
 8,10q�64   �� 5�       � �� �	#FF24506D68   �� 5�       � ��$ 0,0,1,0q�6<   �� 5�       � ��$ Center=�6G   �� 5�       � ��$ Center=�6E   5�      5�      5�      5�      .��5�      � $��20q�6    � RowDefinitions 5�   
   �� 5�      $
��Auto �6   �� 5�      $��* �6   .+�5�   
   �� $��0��6   #��[
 6   $��
0,-16,0,36q�6A    �� Padding$ 20q�6U   ._�5�      �� $��	0,0,0,-21q�6   .+�5�      �� $��📚 Book Management��6   $��24(�66   $
��Bold"�6D   ���	#FF2D3748$��0,0,0,20q�5�   2   � 5�      $��0,0,0,15q�6    � ColumnDefinitions 5�      �� 5�      $��* �6+   �� 5�      $
��Auto �6+   .+�5�      �� $��0��6   #��[ 5�      $��14(�5�      $��	0,0,635,0q�6+   
���   ���5�      �� *SearchKeyword+ �� UpdateSourceTrigger$ PropertyChanged=�.��5�   2    5�       �� Binding �� RelativeSource �� Value��%  '��       7  �� 5�      " ��6$   8    5�   "   P� 5�   &   
 � ��$  ��6>   . �5�   *   ��  � ��
 �����6G   5�   '   �� 5�   &    62   �� *Text+ �� *Self+  6q   .a�5�   *   ��  � ��
 �����6G   5�   '   5�      5�      .��6-   5�      �� 5�      $��🔍 Search��6   $��15(�61   ���	#FF24506D#��[ 5�      $��498,5,498,5q�6I   ��5�      �� *SearchCommand+5�   I   5�      � 5�      $
��Left=�6   $��47��67   .+�5�      �� $��➕ Add New��6   $��15(�6P   ���	#FF24506D#��[ 5�      $��32��6D   $��98��6P   ��5�   1   �� *
AddCommand+5�   P   �� 5�      $��✏️ Edit Selected��6   $��15(�6X   ���	#FF24506D#��[ 5�      $	��132��6E   
���������5�   8   �� *EditCommand+5�   Q   �� 5�      $��🗑️ Delete Selected��6   $��15(�6]   #��[ 5�      $	��149��6G   ��5�   ;   �� *DeleteCommand+5�   G   �� 5�      $��20��6    
������ 6+   �� 5�      $��📖 Borrow Book��6   $��15(�6\   #��[ 5�      $	��121��6G   ���	#FF24506D��5�   6   �� *BorrowBookCommand+5�   S   �� 5�      $��📚 Return Book��6   $��15(�6\   #��[ 5�      $	��124��6G   ���	#FF24506D��5�   6   �� *ReturnBookCommand+5�   S   5�      5�      5�      �� 5�   
   $��1��6   #��[
 6   $ 20q�6A   ._�5�      �  5�      �� 5�      $	��93* �6'   �� 5�      $
��368* �6'   �� 5�      $	��92* �6'   �� 5�      $	��94* �6'   �� 5�      $
��471* �6'    5�      �� 5�      $
��Auto �6$   �� 5�      $��* �6$   .+�5�      �� $��0��6   $��Book Inventory��6)   $��18(�6?   $��SemiBold"�6M   ���	#FF2D3748$��
0,-55,0,49q�5�   2   $��5��6F     5�         AutoGenerateColumns$ False��5�         
IsReadOnly$
 True��6   #��[ 5�         GridLinesVisibility$ 
Horizontal=�5�         HorizontalGridLinesBrush �	#FFE0E7EF   VerticalGridLinesBrush
 ����    	RowHeight$ 35Q�5�      $��15(�5�      $��	0,-4,0,-9q�6(   $��2��6;   $��5��6M   ��5�      �� *Books+ �� SelectedItem 5�      �� *SelectedBook+  �� Mode$  TwoWay=�!   Columns! 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn"   Header$" Book ID��6-   #   Width5  /System.Windows.Controls.DataGridLengthConverter$# 80 6Y   $   MinWidth$$ 80Q�6d   1  +System.Windows.Controls.DataGridBoundColumn%   Binding% 6>   �� *BookId+6d     5�      $
" ISBN��6-   $	# 120 6T   $	$ 100Q�6`   % 6;   �� *Isbn+6`     5�      $" Title��6-   $# 2* 6V   $	$ 150Q�6a   % 6<   �� *Title+6a     5�      $" Author��6-   $# * 6X   $	$ 120Q�6b   % 6=   �� *Author+6b     5�      $" 	Publisher��6-   $# * 6^   $	$ 120Q�6h   % 6@   �� *	Publisher+6h     5�      $
" Year��6-   $# 70 6_   $$ 70Q�6j   % 6;   �� *PublicationYear+6j     5�      $" Category��6-   $	# 100 6i   $$ 90Q�6u   % 6?   �� *Category.CategoryName+6u     5�      $" Total��6-   $# 60 6_   $$ 60Q�6j   % 6<   �� *NumberOfCopies+6j     5�      $" 	Available��6-   $# 80 6d   $$ 70Q�6o   % 6@   �� *AvailableCopies+6o     5�      $" Location��6-   $# 80 6a   $$ 70Q�6l   % 6?   �� *ShelfLocation+6l     5�      $" Price��6-   $	# 100 6o   $$ 80Q�6{   % 6<   �� *Price+& �� StringFormat$& 	{0:N0} đ��6{     5�      $" Status��6-   $# 80 6X   $$ 70Q�6c   % 6=   �� *Status+6c   5�      5�      5�      5�      5�      