   M S B A M L   `   `   `  ����F  BBug Tracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Bug_Tracker.ViewsBug_Tracker.Views  5      K*clr-namespace:Bug_Tracker.State.NavigatorsBug_Tracker.State.Navigators  5      (    "Bug_Tracker.Views.CreateTicketView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Bug_Tracker.Views     5      :nav*clr-namespace:Bug_Tracker.State.Navigators     5      .��5
      � 
�������  � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��
New Ticket��6!   $��Arial)�63   $��16(�6F    J� Opacity$	 0.5Q�6T   $��40, 40, 40, 0q�6b   � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   .+�5      �� $��0��6   $��40q�6)   .+�5      �� $��Title��6   $��Arial)�6)   $��18(�6<   $��SemiBold"�6J   $��
0, 0, 0, 5q�6`   �� 5      $��16(�6   $��Arial)�6w   $��Center=�6�   $��30��6�    �� 	MaxLength$	 500��6�    �� TextWrapping$
 Wrap=�6�   ��6(   �� *TicketTitle+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�6�   �� 5      $��Description��6   $��Arial)�6/   $��18(�6B   $��SemiBold"�6P   $��0, 40, 0, 5q�6f   �� 5      $��16(�6   $��Arial)�6}   $	��Top=�6�   $
��Left=�6�   $
 Wrap=�6�   $	��150��6�   $
 2500��6�   ��6(   �� *TicketDescription+$ PropertyChanged=�$ TwoWay=�6�   5      �� 5!      $��1��6   $��40q�6)   .+�5"      ��  �� Orientation$ 
Horizontal=�6   .+�5#      �� $��Status��6    $��Arial)�6.   $��18(�6A   $��SemiBold"�6O   �� 5$      $��20, 0q�6   $	��100��6.   ��6:   �� * StatusOptionsDictionary.Values+ �� SelectedItem 6q   �� *TicketStatus+$ TwoWay=�$ PropertyChanged=�5%      �� 5'      
�������$	��0.6q�6,   $	��245��6B   $��
0, 5, 0, 0q�6O   	 �� Padding$	 20q�6c   ._�5(      �� .+�5)      �� $ 
Horizontal=�6%   .+�5*      �� $��Assignee��6(   $��Arial)�68   $��14(�6K   �� 5+      $	��175��6�   $��20, 0q�6�   ��6'   �� *ProjectUsers+ 6L   �� *
Assignee+$ TwoWay=�$ PropertyChanged=�6�   5,      �� 5.      $ 
Horizontal=�6%   $��0, 40q�6>   .+�5/      �� $��Reporter��6(   $��Arial)�68   $��14(�6K   $��0, 10, 0, 0q�6Y   � 50      $��20, 0q�6#    51   "   �� 52   &   $
��Auto �67   �� 53   &   $
��Auto �67   .+�56   "   �� $��0��6)   ���#00875A
 �� CornerRadius$	
 500��6N   $
	 7, 8q�6a   ._�57   &   �� 
�������$��14(�6i   $��Arial)�6w   ��60   �� *CurrentUser.Initials+6w   58   #   �� 5:   "   $��1��6,   $��Arial)�6b   $��Center=�6u   $��10, 0q�6�   ��6<   �� *CurrentUser.FullName+6�   5;      5<      5=      5>      5?      5@      �� 5B   
   $��2��6   $ 
Horizontal=�6"   $��Right=�6;   $��40, 40q�6W   .+�5C      ��    GenericButton#��[  6   $��Cancel��6<   $��10, 0q�6M   +   %Bug_Tracker.State.Navigators.ViewType   ProjectDetailsPage#��Z 6�   ��6\   �� *Navigator.NavigateCommand+6�   �� 5D      #��[  6   $��Create��6<   ���#7b68ee$��10, 0q�6�   ��6M   �� *AddNewTicketToDbCommand+6�   5E      5F      5G      