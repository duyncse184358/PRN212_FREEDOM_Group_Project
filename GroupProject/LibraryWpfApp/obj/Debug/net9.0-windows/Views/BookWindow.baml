   M S B A M L   `   `   `  ����H  DLibraryWpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:LibraryWpfApp.ViewModelsLibraryWpfApp.ViewModels  5   	   $    LibraryWpfApp.Views.BookWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5vm&clr-namespace:LibraryWpfApp.ViewModels     5   	     � Title$  Book Management��5   	   $	��700��6!   $
��1200��6.    � WindowStartupLocation$ CenterScreen=�5   	   ���#F4F7FB � DataContext 5      ,   &LibraryWpfApp.ViewModels.BookViewModel  5	   
    � 	Resources 5         ActionButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius	 �� Triggers
 P� Property P� Value  SearchTextBoxStyle  	CardStyle3  -System.Windows.Media.Effects.DropShadowEffect   Color   
BlurRadius   ShadowDepth   Opacity  BlueHeaderDataGridStyle&   System.Windows.Controls.DataGrid=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%�  &	        &	 �    &	 �    &	 '    �� 5   
   " ��6*   .�5      ��  � ��
 �����6+   �� 5       � ��$ SemiBold"�6+   �� 5       � ��$
 12,8q�6(   �� 5       � ��$ 5,0,0,0q�6'   �� 5       � ��$	 100��6)   �� 5       � �� �	#FF24506D6+   �� 5       �@�Cursor$
 Hand��6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   $ 6��6K   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      	 5      P� 5       
 �@�Tag$	 Add��6)   . �5!      ��  � �� �	#FF28A74563   5"      P� 5#      
 �@�Tag$
 Edit��6)   . �5$      ��  � �� �	#FFFFC10763   �� 5%       � �� �#22263   5&      P� 5'      
 �@�Tag$ Delete��6)   . �5(      ��  � �� �	#FFDC354563   5)      P� 5*      
 �@�Tag$ Borrow��6)   . �5+      ��  � �� �	#FF17A2B863   5,      5.      �� 51   
   " ��6+   .�52      ��  � ��$
 10,8q�6(   �� 53       � ��$ 14(�6)   �� 54       � �� �	#FFCBD5E06,   �� 55       � ��$ 1q�60   �� 56       � ��$ 36��6'   �� 57       � ��$	 250��6&   �� 58       � ��$ Center=�62   59      �� 5<   
   " ��6"   .�5=      ��  � ��
 �����6+   �� 5>       �@��CornerRadius$ 8��6-   �� 5?       � �� �	#FFCBD5E06,   �� 5@       � ��$ 1q�60   �� 5A       �@J�Effect 5B        5C      $ Black��6'   $ 8Q�65   $ 2Q�6D   $	 0.1Q�6T   5E      5F      �� 5I   
   "  60   .�5J      ��  �@ ColumnHeaderStyle 5K      �� 5L      "  6   .�5M      ��  � �� �	#FF24506D67   �� 5N       � ��
 �����67   �� 5O       � ��$ SemiBold"�67   �� 5P       � ��$
 8,10q�64   �� 5Q       � ��$ Center=�6G   �� 5R       � ��$ Center=�6E   5S      5U      5V      5W      .��5Y      � $��20q�6    � RowDefinitions 5Z   
   �� 5[      $
��Auto �6   �� 5\      $��* �6   .+�5`   
   �� $��0��6   #��[ 6   $��
0,-16,0,36q�6A    �� Padding$ 20q�6U   ._�5a      �� .+�5b      �� $��📚 Book Management��6   $��24(�66   $
��Bold"�6D   ���	#FF2D3748$��0,0,0,20q�6m   �� 5c       �� Orientation$ 
Horizontal=�6   $��0,0,0,10q�66   .+�5d      �� #��[ 6b   $��0,0,10,0q�6�   ��6   �� *SearchKeyword+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5e      $��🔍 Search��6   #��[  6S   ���	#FF24506D��61   �� *SearchCommand+6~   �� 5f      $��✏️ Edit Selected��6   #��[  6X   
 � Tag$
 Edit��6�   ���	#FF24506D
���������68   �� *EditCommand+6�   �� 5g      $��🗑️ Delete Selected��6   #��[  6]   $ Delete��6�   $	��114��6�   ��6;   �� *DeleteCommand+6�   �� 5h      $��📖 Borrow Book��6   #��[  6\   $ Borrow��6�   ���	#FF24506D��66   �� *BorrowBookCommand+6�   �� 5i      $��📚 Return Book��6   #��[  6\   $ Borrow��6�   ���	#FF24506D��66   �� *ReturnBookCommand+6�   �� 5j      $��➕ Add New��6   #��[  6P   $	 Add��6{   ��61   �� *
AddCommand+6{   5k      5l      5m      �� 5p   
   $��1��6   #��[ 6   $ 20q�6A   ._�5q      �� .+�5r      �� $��Book Inventory��6   $��18(�62   $��SemiBold"�6@   ���	#FF2D3748$��0,0,0,10q�6m     5s         AutoGenerateColumns$ False��5u         
IsReadOnly$
 True��67   #��[ 5v         GridLinesVisibility$ 
Horizontal=�5w         HorizontalGridLinesBrush �	#FFE0E7EF   VerticalGridLinesBrush
 ����    	RowHeight$ 35Q�5z      $��15(�5{      ��5s      �� *Books+ �� SelectedItem 5t      �� *SelectedBook+ �� Mode$ TwoWay=�   Columns 5|      0  *System.Windows.Controls.DataGridTextColumn  5}      ,  &System.Windows.Controls.DataGridColumn   Header$ Book ID��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ 80 6Y   1  +System.Windows.Controls.DataGridBoundColumn    Binding  6>   �� *BookId+6Y     5~      $
 ISBN��6-   $	 120 6T     6;   �� *Isbn+6T     5      $ Title��6-   $ 2* 6V     6<   �� *Title+6V     5�      $ Author��6-   $ * 6X     6=   �� *Author+6X     5�      $ 	Publisher��6-   $ * 6^     6@   �� *	Publisher+6^     5�      $
 Year��6-   $ 70 6_     6;   �� *PublicationYear+6_     5�      $ Category��6-   $	 100 6i     6?   �� *Category.CategoryName+6i     5�      $ Total��6-   $ 60 6_     6<   �� *NumberOfCopies+6_     5�      $ 	Available��6-   $ 80 6d     6@   �� *AvailableCopies+6d     5�      $ Location��6-   $ 80 6a     6?   �� *ShelfLocation+6a     5�      $ Price��6-   $	 100 6o     6<   �� *Price+! �� StringFormat$! 	{0:N0} đ��6o     5�      $ Status��6-   $ 80 6X     6=   �� *Status+6X   5�      5�      5�      5�      5�      