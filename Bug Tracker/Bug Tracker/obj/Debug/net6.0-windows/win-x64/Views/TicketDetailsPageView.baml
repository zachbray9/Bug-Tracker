   M S B A M L   `   `   `  ����F  BBug Tracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Bug_Tracker.ViewsBug_Tracker.Views  5      K*clr-namespace:Bug_Tracker.State.NavigatorsBug_Tracker.State.Navigators  5      -    'Bug_Tracker.Views.TicketDetailsPageView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Bug_Tracker.Views     5      :nav*clr-namespace:Bug_Tracker.State.Navigators     5      ] YXceed.Wpf.Toolkit.NET5, Version=4.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5        � 	Resources  5          BooleanToVisibilityConverter��%   &	        �� 5   
   6'   5      .��5      � 
�������{�6   �� *UserInputIsEnabled+ � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Ticket  /  Details��6!   $��Arial)�6;   $��16(�6N    J� Opacity$	 0.5Q�6\   $��40, 40, 40, 15q�6j   � 5   
   $��1��6   $��40, 10, 40, 40q�6    � ColumnDefinitions 5      �� 5      $��5* �6#   �� 5      $��3* �6#   .+�5       � $��0��6   $��0, 0, 80, 0q�6#    5!      �� 5"      $
��Auto �6$   �� 5#      $��2* �6$   �� 5$      $��3* �6$   .+�5(      � $��0��6    5)      �� 5*      �� 5+      .+�5.      �� $��0��6     FocusChangeableTextBox#��[ 6+   $��24(�6�    �� 	MaxLength$	 500��6�   $	��100��6�    �� TextWrapping$
 Wrap=�6�   ��6[   �� *TicketTitle+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 50      $��1��6!    �� Orientation$ 
Horizontal=�6.   $��Right=�6G   $��
0, 5, 0, 0q�6c   .+�51      ��   GenericButton#��[ 6!   $	��✓��6�   $��18(�6�   $��35��6  $��35��6  $��
0, 0, 5, 0q�6  ��6H   �� *!SaveTicketDetailsChangesCommand+y�6|   �� *!IsTicketTitleTextboxBeingEdited+	 �� 	Converter#	 [  6  �� 52      #��[ 6!   $��X��6�   $��18(�6�   $��Calibri)�6  $��35��6  $��35��6(  ��6H   �� *#!CancelTicketDetailsChangesCommand+y�6~   �� *!IsTicketTitleTextboxBeingEdited+#	 [  6(  53      54      �� 57      $��1��6   $��0, 30, 0, 20q�6*   .+�58      �� $��Description��6    $��Arial)�63   $��14(�6F   $��SemiBold"�6T   $��5, 0, 0, 10q�6j   � 59       5:      �� 5;      �� 5<      .+�5?      �� $��0��6"   #��[ 6/   $��14(�6�   $
 2500��6�   $	��220��6�   $
 Wrap=�6�   ��6_   �� *TicketDescription+$ TwoWay=�$ PropertyChanged=�6�   �� 5@      $��1��6%   $ 
Horizontal=�62   $��Right=�6K   $��
0, 5, 0, 0q�6g   .+�5A      �� #��[ 6%   $	��✓��6�   $��18(�6  $��35��6  $��35��6  $��
0, 0, 5, 0q�6'  ��6L   �� *!SaveTicketDetailsChangesCommand+y�6�   �� *'%IsTicketDescriptionTextboxBeingEdited+#	 [  6'  �� 5B      #��[ 6%   $��X��6�   $��18(�6  $��Calibri)�6  $��35��6'  $��35��62  ��6L   �� *#!CancelTicketDetailsChangesCommand+y�6�   �� *'%IsTicketDescriptionTextboxBeingEdited+#	 [  62  5C      5D      5E      � 5G      $��2��6    5H      �� 5I      $
��Auto �6(   �� 5J      $
��Auto �6(   �� 5K      $��* �6(   .+�5N      �� $��0��6    $��Comments��6-   $��12(�6=   $��Arial)�6K   $��
0, 0, 0, 5q�6^   �� 5Q      $��1��6!   $ Vertical=�6.   .+�5R      �� $��Arial)�6x   $��16(�6�   $
 Wrap=�6�   $	��Top=�6�   $
��Left=�6�   $��Stretch=�6�   $��
0, 0, 0, 5q�6  $��
0, 5, 0, 5q�6!  ��6"   �� *CommentTextBoxText+$ TwoWay=�$ PropertyChanged=�6!  � 5S      ��5T      �� 5U   "   
 �� 
TargetType"
 �6(    �� Triggers 5V   &   �� 5W   *    �� Binding 66   �� *DoesCommentTextBoxContainText+ �� Value	 False6h   .a�5X   .   ��  �� Property � y� �� Value$ 	Collapsed=�6K   5Y   +   5[   #   .+�5^      �� #��[ 6%   $
��Save��6v   
����������#7b68ee$��45��6�   $��25��6�   $
��Left=�6�   ��6L   �� *AddCommentToDbCommand+6�   5_      5`      �� 5b      $��2��6   $��0, 30, 0, 0q�6L   
������ $��Disabled=�6{   $��False��6�   ��6+   �� *
Comments+��5c      �� 5d      "
 ��6$   .�5e   "   ��  �@�FocusVisualStyle 6E   Z� �� 5f   "    � ��$ 0, 0, 0, 30q�6;   �� 5g   "    � �� 5h   &   �� 5i   *    �� 
TargetType" ��6:   .R�5j   .   �� 5k   +   5m   #    5n   "   P� 5o   &    P� Property �@J�IsMouseOver P� Value$
 True��6E   . �5p   *   ��  � ��
 ���� 6G   �� 5q   *    � ��
 ���� 6H   5r   '   5t      ��5w      �� 5x      .R�5y   "   �  5{   &   �� 5|   *   $��* �68   �� 5}   *   $
��Auto �68   �� 5~   *   $��* �68   .+�5�   &   � $��0��6+    5�   *   �� 5�   .   $
��Auto �6?   �� 5�   .   $
��Auto �6?   �� 5�   .   $
��Auto �6?   .+�5�   *   �� $��0��61   ���#00875A$��34��6V   $��34��6a    �� CornerRadius$ 17��6m   $��0, 0, 10, 0q�6   ._�5�   .   �� 
�������$��16(�6k   $��Arial)�6y   $��Center=�6�   $��Center=�6�   ��68   �� *AuthorInitials+6�   5�   +   �� 5�   *   $��1��64   $��16(�6d   $
��Bold"�6r   $��Arial)�6�   $��0, 0, 20, 0q�6�   ��6D   �� *AuthorFullName+6�   �� 5�   *   $��2��64   $��Arial)�6d   $��14(�6w   
���������6D   �� *TimeDifference+6�   5�   '   �� 5�   &   $��1��60   $��16(�6S   $��Arial)�6a   $
��Wrap=�6t   $��50, 5, 0, 20q�6�   ��6=   �� *Text+6�   �� 5�   &   $��2��60   $��Delete��6=   $��	Underline{�6K    � Cursor$
 Hand��6g   $��Arial)�6u   $��12(�6�   ���#242526$��50, 0, 0, 0q�6�    J� InputBindings 5�   *   d� 5�   .    d� MouseAction$ 	LeftClicke�6�    �� Command 6;   �� *(&DataContext.DeleteCommentFromDbCommand+ �� RelativeSource ��  �� AncestorType" �� �� CommandParameter 6�   �� 6�   y�5�   *   `� 5�   .    % !RoleOrAuthorToVisibilityConverter `� 	Converter# [ 6;   .1�5�   2   ��  �� Path$$ DataContext.CurrentProjectUser�6�    6:   ��  ListBox �� AncestorLevel$ 1��6�   �� 5�   2   $ .�6:   5�   /   5�   '   5�   #   5�      5�      5�      5�      � 5�      $��1��6   .+�5�      �� .+�5�      �� $��Arial)�6�   $��14(�6�   $��50��6  $
��Auto��6  $
��Left=�6&  $��15, 7q�6A  $��0, 0, 0, 10q�6Q  ��6   �� *75StatusOptionsRetriever.StatusOptionsDictionary.Values+$ PropertyChanged=� �� SelectedItem 6�   �� *TicketStatus+$ PropertyChanged=�$ TwoWay=�6Q  �� 5�      
���   �$��.5q�61   ._�5�      �  5�      �� 5�   "   $
��Auto �60   �� 5�   "   $
��Auto �60   �� 5�   "   $
��Auto �60   .+�5�      �� $��0��6(   $��Details��65   $��16(�6D   $��Arial)�6R   $��15, 15q�6e   �� 5�      $��1��6(   � 5�      $��2��6#    5�   "   �� 5�   &   $
��Auto �64   �� 5�   &   $
��Auto �64    5�   "   �� 5�   &   $��3* �67   �� 5�   &   $��4* �67   .+�5�   "   �� $��0��6,   $��0��69   $��Assignee��6I   $��Arial)�6Y   $��14(�6l   $��15, 15, 0, 50q�6z   �� 5�   "   $��0��6-   $��1��6:   $ Vertical=�6J   $��0, 0, 0, 15q�6a   .+�5�   &   )  #Xceed.Wpf.Toolkit.WatermarkComboBox     	Watermark$ 
Unassigned��6>     �� DisplayMemberPath$  UserFullName��6  $��Arial)�6"  $��14(�65  $	��Top=�6C  $��10, 5q�6[  $��0, 15, 15, 10q�6k  ��6V   �� *ProjectUsers+$ TwoWay=�$ PropertyChanged=� 6�   �� *
Assignee+$ TwoWay=�$ PropertyChanged=�6�  �� 5�   &   $ 
Horizontal=�61   $��Right=�6J   $��
0, 5, 0, 0q�6f   .+�5�   *   �� #��[ 61   $	��✓��6�   $��18(�6  $��35��6  $��35��6  $��
0, 0, 5, 0q�6+  ��6X   �� *!SaveTicketDetailsChangesCommand+y�6�   �� *IsAssigneeComboBoxBeingEdited+#	 [  6+  �� 5�   *   #��[ 61   $��X��6�   $��18(�6  $��Calibri)�6  $��35��6+  $��35��66  $��0, 0, 15, 0q�6B  ��6X   �� *#!CancelTicketDetailsChangesCommand+y�6�   �� *IsAssigneeComboBoxBeingEdited+#	 [  6B  5�   '   5�   #   �� 5�   "   $��1��6,   $��0��69   $��Reporter��6I   $��Arial)�6Y   $��14(�6l   $��15, 0, 0, 15q�6z   �� 5�   "   $��1��6-   $��1��6:   .+�5�   &     $ 
Unassigned��6=   $  UserFullName��6�   $��Arial)�6   $��14(�63  $	��Top=�6A  $��10, 5q�6Y  $��0, 0, 15, 15q�6i  ��6T   �� *ProjectUsers+$ TwoWay=�$ PropertyChanged=� 6�   �� *
Reporter+$ TwoWay=�$ PropertyChanged=�6�  �� 5�   &   $ 
Horizontal=�61   $��Right=�6J   $��
0, 5, 0, 0q�6f   .+�5�   *   �� #��[ 61   $	��✓��6�   $��18(�6  $��35��6  $��35��6  $��
0, 0, 5, 0q�6+  ��6X   �� *!SaveTicketDetailsChangesCommand+y�6�   �� *IsReporterComboBoxBeingEdited+#	 [  6+  �� 5�   *   #��[ 61   $��X��6�   $��18(�6  $��Calibri)�6  $��35��6+  $��35��66  $��0, 0, 15, 0q�6B  ��6X   �� *#!CancelTicketDetailsChangesCommand+y�6�   �� *IsReporterComboBoxBeingEdited+#	 [  6B  5�   '   5�   #   5�      5�      5�      �� 5�      $ 
Horizontal=�6!   $��15, 20, 0, 0q�6:   .+�5�      �� $��Created ��6$   $��12(�64   $��Arial)�6B   
�������6U   �� 5�      $��12(�6a   $��Arial)�6o   
���������6$   �� *-+TicketContainer.CurrentTicket.DateSubmitted+6�   5�      5�      5�      5�      �� 5�   
   $��2��6   #��[ 6   $
��Back��6E   $
��Left=�6�   $��40, 0, 40, 40q�6�   $��50��6�   $��25��6�   $��10q�6  ��6T   �� *ViewProjectDetailsCommand+��6�   �� *!ProjectContainer.CurrentProject+6  5�      5�      