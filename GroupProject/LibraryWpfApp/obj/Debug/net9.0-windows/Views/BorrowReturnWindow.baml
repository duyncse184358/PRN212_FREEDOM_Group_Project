   M S B A M L   `   `   `  ����H  DLibraryWpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:LibraryWpfApp.ViewModelsLibraryWpfApp.ViewModels  5   	   ,    &LibraryWpfApp.Views.BorrowReturnWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5vm&clr-namespace:LibraryWpfApp.ViewModels     5   	     � Title$(  "📊 Borrowing & Return Management��5   	   $	��700��66   $
��1200��6C    � WindowStartupLocation$ CenterScreen=�5   	   ���#F4F7FB � 	Resources 5	         	CardStyle �� 
TargetType �� Property �� Value3  -System.Windows.Media.Effects.DropShadowEffect   Color   
BlurRadius   ShadowDepth	   Opacity  EnhancedButtonStyle  ModernButtonStyle
 �� BasedOn �� 
TargetType �� CornerRadius �� Triggers P� Property P� Value  LostButtonStyle �� Triggers  DamagedButtonStyle  SearchTextBoxStyle  ModernTextBoxStyle  EnhancedDataGridStyle&   System.Windows.Controls.DataGrid  ModernDataGridStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%h	  &	        &	 D    7  &	 T    7  &	      7  &	     7  &	 �    7  �� 5   
   " ��6"   .�5      ��  � ��
 �����6+   �� 5       �@��CornerRadius$ 8��6-   �� 5       � �� �	#FFCBD5E06,   �� 5       � ��$ 1q�60   �� 5       �@J�Effect 5        5      $ Black��6'   $ 8Q�65   $ 2Q�6D   $		 0.1Q�6T   5      5      �� 5   
   " ��6,   8
   .�5      ��  � �� �	#FF24506D6+   �� 5       � ��
 �����6+   �� 5       � ��$
 12,8q�6(   �� 5       � ��$ 5q�6'   �� 5       � ��$ 0q�60   �� 5       � ��$ SemiBold"�6+   �� 5       � ��$ 13(�6)   �� 5        �@�Cursor$
 Hand��6'   �� 5!       � �� 5"      �� 5#      " ��6&   .R�5$      �� #��z��6!   #��z��5%   !   #��z��5&   !   $ 6��5'   !   ._�5(      �� $��Center=�6/   $��Center=�6L   5)       5*      P� 5+       �@J�IsMouseOver$
 True��6=   . �5,   "   ��  � �� �	#FF1A3B4D6?   5-      P� 5.       � ��$
 True��6;   . �5/   "   ��  � �� �	#FF0F2A356?   50      52      54      55      �� 58   
   " ��6(   8
   .�59      ��  � �� �	#FFDC35456+    5:      P� 5;       �@J�IsMouseOver$
 True��61   . �5<      ��  � �� �	#FFC8233363   5=      5?      �� 5A   
   " ��6+   8
   .�5B      ��  � �� �	#FFFFC1076+   �� 5C       � �� �	#FF2125296+    5D      P� 5E       �@J�IsMouseOver$
 True��61   . �5F      ��  � �� �	#FFE0A80063   5G      5I      �� 5L   
   " ��6+   8
   .�5M      ��  � ��$
 12,8q�6(   �� 5N       � ��$ 13(�6)   �� 5O       � �� �	#FFCBD5E06,   �� 5P       � ��$ 1q�60   �� 5Q       � ��$ 36��6'   5R      �� 5U   
   "  6.   8
   .�5V      ��  � ��$ 13(�6)   �� 5W       �@ ColumnHeaderStyle 5X      �� 5Y      "  6   .�5Z      ��  � �� �	#FF24506D67   �� 5[       � ��
 �����67   �� 5\       � ��$ SemiBold"�67   �� 5]       � ��$ 13(�65   �� 5^       � ��$
 8,10q�64   �� 5_       � �� �	#FF24506D68   �� 5`       � ��$ 0,0,1,0q�6<   �� 5a       � ��$ Center=�6G   �� 5b       � ��$ Center=�6E   5c      5e      5f      5g      .��5i      � $��20q�6    � RowDefinitions 5j   
   �� 5k      $
��Auto �6   �� 5l      $��* �6   .+�5p   
   �� $��0��6   #��[  6   $��0,0,0,20q�6A    �� Padding$ 20q�6S   ._�5q      �� .+�5s      �� $(��"📊 Borrowing & Return Management��6   $��30(�5t      $
��Bold"�6*   ���	#FF2D3748$��Center=�5v      $��0,0,0,20q�5w      � 5z       � ColumnDefinitions 5{      �� 5|      $��* �6+   �� 5}      $
��Auto �6+   .+�5�      �� $��0��6   #��[ 5�      $��0,0,10,0q�5�      ��5�      �� *SearchKeyword+ �� UpdateSourceTrigger$ PropertyChanged=�5�      �� 5�      $��1��6   $��🔍 Search��5�      #��[ 5�      $	��100��5�      ��5�      �� *SearchCommand+5�      5�      5�      5�      �� 5�   
   $��1��6   #��[  6   $ 20q�6A   ._�5�      �  5�      �� 5�      $
��Auto �6$   �� 5�      $��* �6$   .+�5�      �� $��0��6   $��📋 Borrowing Records��5�      $��15(�5�      $��SemiBold"�6*   ���	#FF2D3748$��0,0,0,15q�5�        5�      $��1��6      AutoGenerateColumns$ False��5�         
IsReadOnly$
 True��67   #��[ 5�         GridLinesVisibility$ 
Horizontal=�5�         HorizontalGridLinesBrush �	#FFE0E7EF   VerticalGridLinesBrush
 ����    	RowHeight$ 35Q�5�      ��5�      �� *
Borrowings+ �� SelectedItem 5�      �� *SelectedBorrowing+   Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ 60 6Y      MinWidth$ 60Q�6d   1  +System.Windows.Controls.DataGridBoundColumn    Binding  69   �� *BorrowingID+6d     5�      $ 
Book Title��6-   $ 2* 6_   $	 150Q�6j     6A   �� *	BookTitle+6j     5�      $ Patron Name��6-   $ * 6a   $	 120Q�6k     6B   �� *
PatronName+6k     5�      $ Borrow Date��6-   $	 100 6z   $	 100Q�6�     6B   �� *
BorrowDate+! �� StringFormat$! 
dd/MM/yyyy��6�     5�      $ Due Date��6-   $	 100 6t   $	 100Q�6�     6?   �� *	DueDate+$! 
dd/MM/yyyy��6�     5�      $ Return Date��6-   $	 100 6z   $	 100Q�6�     6B   �� *
ReturnDate+$! 
dd/MM/yyyy��6�     5�      $ Status��6-   $ 80 6X   $ 80Q�6c     6=   �� *Status+6c     5�      $
 Fine��6-   $ 80 6s   $ 80Q�6~     6;   �� *
FineAmount+$! 	{0:N0} đ��6~   4  .System.Windows.Controls.DataGridTemplateColumn  5�      $ Actions��61   $	 200 6B   $	 200Q�6N   "   CellTemplate" 5�      �� 5�   "   .R�5�   &   �� # �� Orientation$# 
Horizontal=�61   $��Center=�6J   .+�5�   *   �� $��	❌ Mất��61   #��[ 5�   1   $��70��5�   1   $��2q�5�   1   ��5�   1   �� *DataContext.MarkLostCommand+$ �� RelativeSource$ �� % �� AncestorType
% Window��5�   1   �� 5�   1   �� 5�   *   $��⚠️ Hỏng��61   #��[ 5�   1   $��70��5�   1   $��2q�5�   1   ��5�   1   �� * DataContext.MarkDamagedCommand+$ �� 
% Window��5�   1   �� 5�   1   5�   '   5�   #   5�      5�      5�      5�      5�      5�      