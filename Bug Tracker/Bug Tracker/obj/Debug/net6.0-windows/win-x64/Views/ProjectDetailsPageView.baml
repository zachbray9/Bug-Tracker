   M S B A M L   `   `   `  ����F  BBug Tracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Bug_Tracker.ViewsBug_Tracker.Views  5      L HBugTracker.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhFclr-namespace:BugTracker.Domain.Enumerables;assembly=BugTracker.DomainBugTracker.Domain.Enumerables 5      K*clr-namespace:Bug_Tracker.State.NavigatorsBug_Tracker.State.Navigators  5      .    (Bug_Tracker.Views.ProjectDetailsPageView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Bug_Tracker.Views     5      WenumFclr-namespace:BugTracker.Domain.Enumerables;assembly=BugTracker.Domain     5      :nav*clr-namespace:Bug_Tracker.State.Navigators     5        � 	Resources  5          BooleanToVisibilityConverter��%   &	        �� 5   
   6'   5      .��5      � .+�5   
   �  � IsSharedSizeScope$
 True��6   
������� � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��* �6    �� 5      $
��Auto �6    .+�5      �  � 	StaysOpen$ False��6X   $��Center=�6�    � VerticalOffset$
 -200��6�   $	��400��6�   $	��425��6�   ��6   �� *IsPopupOpen+ �� UpdateSourceTrigger$ PropertyChanged=� � DataContext 6j   �� *AddUserViewModel+.��5      1   +Bug_Tracker.Views.AddUserToProjectPopupView  5      � 5      $��0��6    5      �� 5      $
��Auto �6$   �� 5       $
��Auto �6$   �� 5!      $
��Auto �6$   .+�5$      �� $��0��6    �� Orientation$ 
Horizontal=�6*   $��40, 40, 40, 10q�6C   .+�5%      �� $��Projects  /  ��6    $��Arial)�65    J� Opacity$	 0.5Q�6H   $��16(�6V   $��Center=�6d   �� 5&      $��Arial)�6V   $	 0.5Q�6i   $��16(�6w   $��Center=�6�   ��6    �� *&$ProjectContainer.CurrentProject.Name+6�   5'      �� 5)      $��2��6   $��
Task Board��6)   $��Arial)�6;   $��20(�6N   $
��Bold"�6\   $��40, 10, 40, 50q�6n   5+      � 5.      $��1��6   	 � ColumnDefinitions	 5/      �� 50      $
��Auto �6'   �� 51      $
��Auto �6'   .+�54      �� -   5        
 ��Name$
 TaskFilterTextBox��54      $��0��65     	SearchBar#��[ 6E   $��40��6�   $	��120��6�   $
��Left=�6�   $��Center=�6�   $��40, 0, 20, 0q�6  ��6h   �� *TicketFilterQuery+ �� Mode$ TwoWay=�$ PropertyChanged=�6  �� 56      $��1��6   $ 
Horizontal=�6-   $��Center=�6F   $
��0, 5q�6a   .+�57      �� $
��Auto=�6#   $��Disabled=�6H   .��58      �� $ 
Horizontal=�6%   .+�59      �� $��Center=�6P   $
��Left=�6k   ��6+   �� *ProjectUsers+��5:   "   �� 5;   &   .R�5<   *   �� $ 
Horizontal=�65   5=   '   ��5?   "   �� 5@   &   .R�5A   *   �� ���#00875A �� CornerRadius$	 500��6F   $��Center=�6Y    �� Padding$
 7, 8q�6t   $��0, 0, 10, 0q�6�   ._�5B   .   �� 
�������$��14(�6i   $��Arial)�6w   ��68   �� *UserInitials+ � ToolTip 5C   2   _� 5D   6   ���#242526
������� _� 	Placement$ Center=�6�    _� VerticalOffset$ 40��6�   ��6>   �� *UserFullName+6�   5F   /   5G   +   5H   '   5J      5K      5L      �� 5N        AddUserButton#��[ 6    � Cursor$
 Hand��6p   ��6D   �� *OpenAddUserPopupCommand+ 5O      _� 5P      $��Add User��6&   ���#242526
�������$ Center=�6a   $ 40��6t   5R      5S      5T      �� 5V      $��2��6   $ 
Horizontal=�6&   $��30, 20q�6?   $
��Left=�6O   .+�5X      � $��10, 0, 10, 0q�6   $	��300��6-   .+�5Y      �� ���#e9edef$ 3��62   $
��0.25q�6C   ���#333333$ 5q�6p   $
��Auto��6|   ._�5Z      �  5[      �� 5\   "   $
��Auto �60   �� 5]   "   $��* �60   .+�5`      �� $��0��6(   $��To Do��65   $��Arial)�6B   $	 0.6Q�6U   $��10, 10q�6c   � 5b      $��1��6#    5c   "   �� 5d   &   $��* �64   �� 5e   &   $
��Auto �64   .+�5h   "   �� $��0��6*     TaskBoardListBox#��[ 67   
������ 
������ ��6a   �� *FilteredToDoTickets+$ TwoWay=�$ PropertyChanged=�5j   #   �� 5l   "   $��1��6)     TaskBoardButton#��[ 66   $��+ Create Task��6_   *  $BugTracker.Domain.Enumerables.Status   ToDo#��Z 6�   $	��Top=�6�   ��6w   �� *CreateNewTicketCommand+6�   5m      5n      5o      5p      � 5s      $��10, 0, 10, 0q�6   $	��300��6-   .+�5t      �� ���#e9edef$ 3��62   $
��0.25q�6C   ���#333333$ 5q�6p   $
��Auto��6|   ._�5u      �  5v      �� 5w   "   $
��Auto �60   �� 5x   "   $��* �60   .+�5{      �� $��0��6(   $��In Progress��65   $��Arial)�6H   $	 0.6Q�6[   $��10, 10q�6i   � 5}      $��1��6#    5~   "   �� 5   &   $��* �64   �� 5�   &   $
��Auto �64   .+�5�   "   �� $��0��6*   #��[ 67   
������ 
������ ��6a   �� *FilteredInProgressTickets+$ TwoWay=�$ PropertyChanged=�5�   #   �� 5�   "   $��1��6)   #��[ 66   $��+ Create Task��6_      
InProgress#��Z 6�   $	��Top=�6�   ��6w   �� *CreateNewTicketCommand+6�   5�      5�      5�      5�      � 5�      $��10, 0, 10, 0q�6   $	��300��6-   .+�5�      �� ���#e9edef$ 3��62   $
��0.25q�6C   ���#333333$ 5q�6p   $
��Auto��6|   ._�5�      �  5�      �� 5�   "   $
��Auto �60   �� 5�   "   $��* �60   .+�5�      �� $��0��6(   $
��Done��65   $��Arial)�6A   $	 0.6Q�6T   $��10, 10q�6b   � 5�      $��1��6#    5�   "   �� 5�   &   $��* �64   �� 5�   &   $
��Auto �64   .+�5�   "   �� $��0��6*   #��[ 67   
������ 
������ ��6a   �� *FilteredDoneTickets+$ TwoWay=�$ PropertyChanged=�5�   #   �� 5�   "   $��1��6)   #��[ 66   $��+ Create Task��6_      Done#��Z 6�   $	��Top=�6�   ��6w   �� *CreateNewTicketCommand+6�   5�      5�      5�      5�      5�      �� 5�      $��3��6     GenericButton#��[ 6"   +   %Bug_Tracker.State.Navigators.ViewType   ProjectsPage#��Z 6w   $
��Back��6�   $
��Left=�6�   $��50��6�   $��25��6�   $��10q�6�   $��40, 0, 40, 40q�6�   ��6I   �� *Navigator.NavigateCommand+6�   5�      � 5�   
   -   5         �Name$ Overlay��5�      ���	#80000009y�67   �� *AddUserViewModel.IsPopupOpen+$ PropertyChanged=� �� 	Converter# [  5�      5�      5�      