   M S B A M L   `   `   `  ����F  BBug Tracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Bug_Tracker.ViewsBug_Tracker.Views  5      K*clr-namespace:Bug_Tracker.State.NavigatorsBug_Tracker.State.Navigators  5      ] YMicrosoft.VisualBasic, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a\Bclr-namespace:Microsoft.VisualBasic;assembly=Microsoft.VisualBasicMicrosoft.VisualBasic 6E   (    "Bug_Tracker.Views.ProjectsPageView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Bug_Tracker.Views     5      :nav*clr-namespace:Bug_Tracker.State.Navigators     5      PiBclr-namespace:Microsoft.VisualBasic;assembly=Microsoft.VisualBasic     6E   .��5
      � 
�������  � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��Projects��6!   $��Arial)�61    J� Opacity$	 0.5Q�6D   $��16(�6R   $��40, 40, 40, 10q�6`   � 5   
   $��1��6   $��20, 20, 20, 30q�6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   .+�5      �� $��0��6      	SearchBar#��[  6&   $	��125��6�   $
��Left=�6�   $��20, 0, 0, 0q�6�   ��6I   �� *ProjectSearchQuery+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5      $��1��6     GenericButton#��[ 6%   $��+ New Project��6L   +   %Bug_Tracker.State.Navigators.ViewType   CreateNewProjectPage#��Z 6�   
����������#7b68ee$��Right=�6�   $��5, 0, 20, 0q�6  ��6d   �� *Navigator.NavigateCommand+6  5      � 5   
   $��2��6   $��40, 0q�6    5       �� 5!      $��* �6#   �� 5"      $��* �6#   �� 5#      $��* �6#   �� 5$      $��* �6#   �� 5%      $��* �6#   �� 5&      $��* �6#   .+�5)      �� $��0��6   $��Project Name��6(   $��Arial)�6<   $	 0.4Q�6O   $��20, 0, 0, 0q�6]   �� 5*      $��3��6   $�� ��6(   $��Arial)�60   $	 0.4Q�6C   �� 5+      $��5��6   $�� ��6(   $��Arial)�60   $	 0.4Q�6C   5,      �� 5.   
   $��3��6   $��40, 10, 40, 0q�6!   �� 50   
   $��4��6   
������  J� IsHitTestVisible$
 True��6�    � IsSharedSizeScope$
 True��6�   $��Stretch=�6�   $��40, 0, 40, 40q�6�   ��69   �� *ProjectSearchResults+$ TwoWay=�$ PropertyChanged=���51      �� 52      .R�53      �  54      �� 55      $��* �6/    z� SharedSizeGroup$
 Name��69   �� 56      $��* �6/   �� 57      $��* �6/   �� 58      $��* �6/   �� 59      $��* �6/   �� 5:      $��* �6/   $ Details��69   .+�5=      �� $��0��6$   $
��Left=�6|   $��10q�6�   ��64   �� *Name+$ OneWay=�$ PropertyChanged=�6�   �� 5>      $��5��6$   $��View Details��64   $��Right=�6H   $��	Underline{�6d   	 � Cursor$
	 Hand��6�   $��10q�6�   
 J� InputBindings
 5?      d� 5@   "    d� MouseAction$ 	LeftClicke�6�    �� Command 6/   �� *'%DataContext.ViewProjectDetailsCommand+ �� RelativeSource ��  �� AncestorType" �� �� CommandParameter 6�   �� 6�   5B      5C      5D      ��5G      �� 5H       �� 
TargetType" ��6   .�5I      ��  �� Property � �� �� Value$ 0q�6/   5J      5L      5M      5N      