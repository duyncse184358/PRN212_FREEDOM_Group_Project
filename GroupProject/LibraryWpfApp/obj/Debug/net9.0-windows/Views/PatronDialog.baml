   M S B A M L   `   `   `  ����H  DLibraryWpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:LibraryWpfApp.ViewModelsLibraryWpfApp.ViewModels  5   	   &     LibraryWpfApp.Views.PatronDialog   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5vm&clr-namespace:LibraryWpfApp.ViewModels     5   	     � Title$  Patron Details��5   	   $	��550��6    $	��400��6-    � WindowStartupLocation$ CenterOwner=�5   	    � 	Resources 5	         
LabelStyle �� 
TargetType �� Property �� Value��%�   &	        �� 5
   
   " ��6#   .�5      ��  � �� �	#FF3333336+   �� 5       � ��$ SemiBold"�6+   �� 5       � ��$ 0,5,0,0q�6'   5      5      .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5       $
��Auto �6   �� 5"      $
��Auto �6   �� 5$      $
��Auto �6   �� 5&      $
��Auto �6   �� 5(      $
��Auto �6   �� 5*      $
��Auto �6   �� 5,      $
��Auto �6   �� 5.      $��* �6   �� 50      $
��Auto �6   .+�54   
   �� $��Patron Details��6   $��0��6*   $��20(�55      $
��Bold"�6"   $��Center=�64   $��0,0,0,10q�6Q   �� 57   
   $��	Full Name��6   $��1��6$   #��[  61   �� 58   
   $��2��6     ModernTextBoxStyle#��[ 6@   ��6   �� *Patron.FullName+6@   �� 5:   
   $��Address��6   $��3��6"   #��[  6/   �� 5;   
   $��4��6   #��[ 6?   ��6   �� *Patron.Address+6?   �� 5=   
   $��Phone��6   $��5��6    #��[  6-   �� 5>   
   $��6��6   #��[ 6=   ��6   �� *Patron.Phone+6=   �� 5@   
   $��Email��6   $��7��6    #��[  6-   �� 5A   
   $��8��6   #��[ 6=   ��6   �� *Patron.Email+6=   �� 5C   
   $��Membership Type��6   $��9��6*   #��[  67   �� 5D   
   $��10��6     ModernComboBoxStyle#��[ 5E      ��5D   !   �� *MembershipTypes+ �� SelectedItem 6I   �� *Patron.MembershipType+5E      �� 5G   
   $��Registration Date��6   $��11��6,   #��[  6:   (  "System.Windows.Controls.DatePicker  5H   
   $��12��6     ModernDatePickerStyle#��[ 6T      SelectedDate 6#   �� *Patron.RegistrationDate+6T   �� 5J   
   $��13��6   	 �� Orientation$	 
Horizontal=�6#   $��Right=�6<   $��0,10,0,0q�6X   .+�5K      �� -   5        $
��Save��5K      $��0,0,10,0q�67     ModernButtonStyle#��[ 6I   $��59��6t   $��41��6   �� 5L      -   5        $��Cancel��5L      #��[ 6:   $��41��6e   5M      5N      5O      