   M S B A M L   `   `   `  ����H  DLibraryWpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:LibraryWpfApp.ViewModelsLibraryWpfApp.ViewModels  5   	   $    LibraryWpfApp.Views.UserDialog   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5vm&clr-namespace:LibraryWpfApp.ViewModels     5   	     � Title$  User Details��5   	   $	��350��6   $	��400��6+    � WindowStartupLocation$ CenterOwner=�5   	    � DataContext 5      2   ,LibraryWpfApp.ViewModels.UserDialogViewModel  5	   
    � 	Resources 5         
LabelStyle �� 
TargetType �� Property �� Value��%�   &	        �� 5   
   " ��6#   .�5      ��  � �� �	#FF3333336+   �� 5       � ��$ SemiBold"�6+   �� 5       � ��$ 0,5,0,0q�6'   5      5      .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5       $
��Auto �6   �� 5"      $
��Auto �6   �� 5$      $
��Auto �6   �� 5&      $
��Auto �6   �� 5(      $��* �6   �� 5*      $
��Auto �6   .+�5.   
   �� $��User Details��6   $��0��6(   $��20(�5/      $
��Bold"�6"   $��Center=�64   $��0,0,0,10q�6Q   �� 51   
   $��Username��6   $��1��6#   #��[  60   �� 52   
   $��2��6     ModernTextBoxStyle#��[ 6>   ��6   �� *User.UserName+6>   �� 54   
   $��Password��6   $��3��6#   #��[  60   �� 55   
   $��4��6   #��[ 6>    � ToolTip$* $Leave blank to keep current password��6j   ��6   �� *User.Password+6j   �� 57   
   $��	Full Name��6   $��5��6$   #��[  61   �� 58   
   $��6��6   #��[ 6>   ��6   �� *User.FullName+6>   �� 5:   
   $
��Role��6   $��7��6   #��[  6,   �� 5;   
   $��8��6     ModernComboBoxStyle#��[ 5<      ��5;       �� *Roles+	 �� SelectedItem	 6>   �� *	User.Role+5<      �� 5>   
   $��9��6   
 �� Orientation$
 
Horizontal=�6"   $��Right=�6;   $��0,10,0,0q�6W   .+�5?      �� -   5        $
��Save��5?        ModernButtonStyle#��[ 6$   $��0,0,10,0q�6b   �� 5@      -   5        $��Cancel��5@      #��[ 6&   6Q   5A      5B      5C      