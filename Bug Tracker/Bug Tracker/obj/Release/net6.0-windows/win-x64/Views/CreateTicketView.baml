   M S B A M L   `   `   `  ���� F  BBug Tracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Bug_Tracker.ViewsBug_Tracker.Views  K*clr-namespace:Bug_Tracker.State.NavigatorsBug_Tracker.State.Navigators  (    "Bug_Tracker.Views.CreateTicketView   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1localclr-namespace:Bug_Tracker.Views     :nav*clr-namespace:Bug_Tracker.State.Navigators     .��� 
�������  � RowDefinitions  �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��
New Ticket��$��Arial)�$��16(� J� Opacity$	 0.5Q�$��40, 40, 40, 0q�� $��1�� � ColumnDefinitions �� $��* ��� $��* �.+��� $��0��$��40q�.+��� $��Title��$��Arial)�$��18(�$��SemiBold"�$��
0, 0, 0, 5q��� $��16(�$��Arial)�$��Center=�$��30�� �� 	MaxLength$	 500�� �� TextWrapping$
 Wrap=����� *TicketTitle+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=��� $��Description��$��Arial)�$��18(�$��SemiBold"�$��0, 40, 0, 5q��� $��16(�$��Arial)�$	��Top=�$
��Left=�$
 Wrap=�$	��150��$
 2500������ *TicketDescription+$ PropertyChanged=�$ TwoWay=��� $��1��$��40q�.+���  �� Orientation$ 
Horizontal=�.+��� $��Status��$��Arial)�$��18(�$��SemiBold"��� $��20, 0q�$	��100������ * StatusOptionsDictionary.Values+ �� SelectedItem �� *TicketStatus+$ TwoWay=�$ PropertyChanged=��� 
�������$	��0.6q�$	��245��$��
0, 5, 0, 0q�	 �� Padding$	 20q�._��� .+��� $ 
Horizontal=�.+��� $��Assignee��$��Arial)�$��14(��� $	��175��$��20, 0q����� *ProjectUsers+ �� *
Assignee+$ TwoWay=�$ PropertyChanged=��� $ 
Horizontal=�$��0, 40q�.+��� $��Reporter��$��Arial)�$��14(�$��0, 10, 0, 0q�� $��20, 0q� �� $
��Auto ��� $
��Auto �.+��� $��0�����#00875A
 �� CornerRadius$	
 500��$
	 7, 8q�._��� 
�������$��14(�$��Arial)����� *CurrentUser.Initials+�� $��1��$��Arial)�$��Center=�$��10, 0q����� *CurrentUser.FullName+�� $��2��$ 
Horizontal=�$��Right=�$��40, 40q�.+���    GenericButton#��[  $��Cancel��$��10, 0q�+   %Bug_Tracker.State.Navigators.ViewType   ProjectDetailsPage#��Z ���� *Navigator.NavigateCommand+�� #��[  $��Create�����#7b68ee$��10, 0q����� *AddNewTicketToDbCommand+