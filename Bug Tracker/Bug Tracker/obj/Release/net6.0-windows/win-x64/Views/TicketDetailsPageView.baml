   M S B A M L   `   `   `  ���� F  BBug Tracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Bug_Tracker.ViewsBug_Tracker.Views  K*clr-namespace:Bug_Tracker.State.NavigatorsBug_Tracker.State.Navigators  -    'Bug_Tracker.Views.TicketDetailsPageView   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1localclr-namespace:Bug_Tracker.Views     :nav*clr-namespace:Bug_Tracker.State.Navigators     ] YXceed.Wpf.Toolkit.NET5, Version=4.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit    � 	Resources      BooleanToVisibilityConverter��%   &	        �� .��� 
�������{��� *UserInputIsEnabled+ � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��Ticket  /  Details��$��Arial)�$��16(� J� Opacity$	 0.5Q�$��40, 40, 40, 15q�� $��1��$��40, 10, 40, 40q� � ColumnDefinitions �� $��5* ��� $��3* �.+�� $��0��$��0, 0, 80, 0q� �� $
��Auto ��� $��2* ��� $��3* �.+�� $��0�� �� �� .+��� $��0��  FocusChangeableTextBox#��[ $��24(� �� 	MaxLength$	 500��$	��100�� �� TextWrapping$
 Wrap=����� *TicketTitle+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� $��1�� �� Orientation$ 
Horizontal=�$��Right=�$��
0, 5, 0, 0q�.+���   GenericButton#��[ $	��✓��$��18(�$��35��$��35��$��
0, 0, 5, 0q����� *!SaveTicketDetailsChangesCommand+y��� *!IsTicketTitleTextboxBeingEdited+	 �� 	Converter#	 [  �� #��[ $��X��$��18(�$��Calibri)�$��35��$��35������ *#!CancelTicketDetailsChangesCommand+y��� *!IsTicketTitleTextboxBeingEdited+#	 [  �� $��1��$��0, 30, 0, 20q�.+��� $��Description��$��Arial)�$��14(�$��SemiBold"�$��5, 0, 0, 10q��  �� �� .+��� $��0��#��[ $��14(�$
 2500��$	��220��$
 Wrap=����� *TicketDescription+$ TwoWay=�$ PropertyChanged=��� $��1��$ 
Horizontal=�$��Right=�$��
0, 5, 0, 0q�.+��� #��[ $	��✓��$��18(�$��35��$��35��$��
0, 0, 5, 0q����� *!SaveTicketDetailsChangesCommand+y��� *'%IsTicketDescriptionTextboxBeingEdited+#	 [  �� #��[ $��X��$��18(�$��Calibri)�$��35��$��35������ *#!CancelTicketDetailsChangesCommand+y��� *'%IsTicketDescriptionTextboxBeingEdited+#	 [  � $��2�� �� $
��Auto ��� $
��Auto ��� $��* �.+��� $��0��$��Comments��$��12(�$��Arial)�$��
0, 0, 0, 5q��� $��1��$ Vertical=�.+��� $��Arial)�$��16(�$
 Wrap=�$	��Top=�$
��Left=�$��Stretch=�$��
0, 0, 0, 5q�$��
0, 5, 0, 5q����� *CommentTextBoxText+$ TwoWay=�$ PropertyChanged=�� ���� 
 �� 
TargetType"
 � �� Triggers ��  �� Binding �� *DoesCommentTextBoxContainText+ �� Value	 False.a���  �� Property � y� �� Value$ 	Collapsed=�.+��� #��[ $
��Save��
����������#7b68ee$��45��$��25��$
��Left=����� *AddCommentToDbCommand+�� $��2��$��0, 30, 0, 0q�
������ $��Disabled=�$��False������ *
Comments+���� "
 ��.���  �@�FocusVisualStyle Z� ��  � ��$ 0, 0, 0, 30q���  � �� ��  �� 
TargetType" ��.R���  P�  P� Property �@J�IsMouseOver P� Value$
 True��. ���  � ��
 ���� ��  � ��
 ���� ���� .R��  �� $��* ��� $
��Auto ��� $��* �.+�� $��0�� �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0�����#00875A$��34��$��34�� �� CornerRadius$ 17��$��0, 0, 10, 0q�._��� 
�������$��16(�$��Arial)�$��Center=�$��Center=����� *AuthorInitials+�� $��1��$��16(�$
��Bold"�$��Arial)�$��0, 0, 20, 0q����� *AuthorFullName+�� $��2��$��Arial)�$��14(�
����������� *TimeDifference+�� $��1��$��16(�$��Arial)�$
��Wrap=�$��50, 5, 0, 20q����� *Text+�� $��2��$��Delete��$��	Underline{� � Cursor$
 Hand��$��Arial)�$��12(����#242526$��50, 0, 0, 0q� J� InputBindings d�  d� MouseAction$ 	LeftClicke� �� Command �� *(&DataContext.DeleteCommentFromDbCommand+ �� RelativeSource ��  �� AncestorType" �� �� CommandParameter �� y�`�  % !RoleOrAuthorToVisibilityConverter `� 	Converter# [ .1���  �� Path$$ DataContext.CurrentProjectUser� ��  ListBox �� AncestorLevel$ 1���� $ .�� $��1��.+��� .+��� $��Arial)�$��14(�$��50��$
��Auto��$
��Left=�$��15, 7q�$��0, 0, 0, 10q����� *75StatusOptionsRetriever.StatusOptionsDictionary.Values+$ PropertyChanged=� �� SelectedItem �� *TicketStatus+$ PropertyChanged=�$ TwoWay=��� 
���   �$��.5q�._��  �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$��Details��$��16(�$��Arial)�$��15, 15q��� $��1��� $��2�� �� $
��Auto ��� $
��Auto � �� $��3* ��� $��4* �.+��� $��0��$��0��$��Assignee��$��Arial)�$��14(�$��15, 15, 0, 50q��� $��0��$��1��$ Vertical=�$��0, 0, 0, 15q�.+�)  #Xceed.Wpf.Toolkit.WatermarkComboBox     	Watermark$ 
Unassigned��  �� DisplayMemberPath$  UserFullName��$��Arial)�$��14(�$	��Top=�$��10, 5q�$��0, 15, 15, 10q����� *ProjectUsers+$ TwoWay=�$ PropertyChanged=� �� *
Assignee+$ TwoWay=�$ PropertyChanged=��� $ 
Horizontal=�$��Right=�$��
0, 5, 0, 0q�.+��� #��[ $	��✓��$��18(�$��35��$��35��$��
0, 0, 5, 0q����� *!SaveTicketDetailsChangesCommand+y��� *IsAssigneeComboBoxBeingEdited+#	 [  �� #��[ $��X��$��18(�$��Calibri)�$��35��$��35��$��0, 0, 15, 0q����� *#!CancelTicketDetailsChangesCommand+y��� *IsAssigneeComboBoxBeingEdited+#	 [  �� $��1��$��0��$��Reporter��$��Arial)�$��14(�$��15, 0, 0, 15q��� $��1��$��1��.+�  $ 
Unassigned��$  UserFullName��$��Arial)�$��14(�$	��Top=�$��10, 5q�$��0, 0, 15, 15q����� *ProjectUsers+$ TwoWay=�$ PropertyChanged=� �� *
Reporter+$ TwoWay=�$ PropertyChanged=��� $ 
Horizontal=�$��Right=�$��
0, 5, 0, 0q�.+��� #��[ $	��✓��$��18(�$��35��$��35��$��
0, 0, 5, 0q����� *!SaveTicketDetailsChangesCommand+y��� *IsReporterComboBoxBeingEdited+#	 [  �� #��[ $��X��$��18(�$��Calibri)�$��35��$��35��$��0, 0, 15, 0q����� *#!CancelTicketDetailsChangesCommand+y��� *IsReporterComboBoxBeingEdited+#	 [  �� $ 
Horizontal=�$��15, 20, 0, 0q�.+��� $��Created ��$��12(�$��Arial)�
��������� $��12(�$��Arial)�
����������� *-+TicketContainer.CurrentTicket.DateSubmitted+�� $��2��#��[ $
��Back��$
��Left=�$��40, 0, 40, 40q�$��50��$��25��$��10q����� *ViewProjectDetailsCommand+���� *!ProjectContainer.CurrentProject+