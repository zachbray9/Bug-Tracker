   M S B A M L   `   `   `  ���� F  BBug Tracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:Bug_Tracker.State.NavigatorsBug_Tracker.State.Navigators  _4clr-namespace:Bug_Tracker.Utility_Classes.Converters&Bug_Tracker.Utility_Classes.Converters  W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466N    HBug_Tracker.Utility_Classes.Converters.RoleOrAuthorToVisibilityConverter %  !RoleOrAuthorToVisibilityConverter  OrganizationComboBox  �� 
TargetType �� Property �� Value  GenericButton ��Name �� CornerRadius �� RelativeSource �� Path �� Triggers P� Property	 P� Value
 �� 
TargetName  TaskBoardButton �� Padding  TaskBoardListBox �� 
TargetType � RowDefinitions � ColumnDefinitions � ToolTip _� 	Placement _� VerticalOffset � Cursor J� InputBindings d� MouseAction �� Command �� AncestorType �� AncestorLevel �� CommandParameter"  FontAwesome.WPF.ImageAwesome   Icon   
Foreground `� 	Converter J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   Color   	Direction   ShadowDepth    Opacity! �� Triggers" �� Value# �� Binding$ �� ElementName  FocusChangeableTextBox% �� Triggers& ]� 
Conditions' �� Property( �� Value  AddUserButton) J� Opacity  	SearchBar* �� Mode+ �� UpdateSourceTrigger  ThinScrollViewer, ]�Name- ]� DecreaseRepeatButton. ]� IncreaseRepeatButton/ ]� Thumb�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     2nav*clr-namespace:Bug_Tracker.State.Navigators  -fa$http://schemas.fontawesome.io/icons/  =util4clr-namespace:Bug_Tracker.Utility_Classes.Converters  %�  &	        &	      &	 {     &	     &	 l    7   &	 	    &	 
    &	 �    &	 �       �� "  ��.���  �@��
IsEditable$ False����  � ��
 ���� ��  � ��
 ���� ��  � ��$ 0q��� "  ��.���  � ��$ Arial)���  � ��$	 130����  � ��$ 40����  �@�Cursor$
 Hand����  � �� �� .R��� $ ButtonBorder��$ 8��#��z��._��� #��z��$��Center=�$��Center=�$
��Bold"�$��WrapWithOverflow=�����  �� *TemplatedParent+$ Content� P�  �@J�IsMouseOver$
	 True��. ��� $
 ButtonBorder�� � ��
 ������� "  ��.���  � ��$ Arial)���  � ��
 ���� ��  � ��$ 30����  � �� �� .R��� $ ButtonBorder��
������ $ 10, 0q�._��� $��Arial)�$��Center=�����  �� *TemplatedParent+$ Content� P�  �@J�IsMouseOver$
	 True��. ��� $
 ButtonBorder�� � �� �#d4d4d4��  �@�Cursor$
 Hand���� "  ��.���  � ��$ 5q���  � ��$ Disabled=���  � ��$
 Auto=���  � ��$ False����  � �� �� "  ��.���  �@�FocusVisualStyle Z� ��  � ��$ Stretch=���  � ��$ 0, 0, 0, 10q���  � �� �� " ��.R��� ��  � �� �� .R��� $ 
ItemBorder��
�������$ 10q�$ 3��._��  �� $
��Auto ��� $��* �.+�� $��0�� �� $��* ��� $
��Auto �.+��� $��0��$
��Left=�$
��Wrap=����� *Title+�� $��1��$��26��$��26��$ 13�����#00875A$	��Top=�._��� 
�������$��Center=�$��Center=����� *AssigneeInitials+ _� ���#242526
�������$ Center=�$ 40������ *AssigneeToolTipText+� $��1�� �� $��* ��� $��* �.+��� $��0��$��View Details��$��	Underline{�$
 Hand��$
��Left=�$	��Top=�$��0, 20, 0, 0q� d� $ 	LeftClicke� �� *&$DataContext.ViewTicketDetailsCommand+ ��  ListBox$ 1�� �� �� $��1��$��20��$��20��$��Right=�$��Bottom=�
������ 
������ ���� *!DataContext.DeleteTicketCommand+ ��  ListBox$ 1������ .��  $ Trash=� �#e9edefy�`� 8   .1��� $$ DataContext.CurrentProjectUser� ��  ListBox$ 1���� $ .�   $
 Gray��$	 270Q�$	 0.2Q�$	  0.2Q�! �� $
" True��# �� *IsMouseOver+$$ 
ItemBorder��.a��� $
 
ItemBorder�� � �� �#DFE1E6�� "  ��.���  � ��$ Arial)���  �@��
IsReadOnly$
 True����  � ��
 ���� ��  � ��
 ���� ��  � ��$ 0q���  � ��$ 7q�% ]� & �� ' �@J�IsMouseOver$
( True���� ' �@��
IsReadOnly$
( True��./���  � �� �#e4e6ebP�  �@J�	IsFocused$
	 True��. ���  �@��
IsReadOnly$ False����  � ��$ 1q��� "  ��.���  � ��$ Center=���  � �� �� .R��� $ ButtonBorder��$��40��$��40�����#f4f5f7$ 20��$ 10q�._�  $ UserPlus=�$	) 0.6Q� P�  �@J�IsMouseOver$
	 True��. ��� $
 ButtonBorder�� � �� �#dfe1e6�� "  ��.���  � �� �� " ��.R��� 
���   �$	��0.5q�$ 5q�._��  �� $��* ��� $
��Auto �.+��� $��0��$��14(�$��Arial)�
������ $��0q�$��Center=�$
��Left=�����  �� *TemplatedParent+$
 Text�$* TwoWay=�$+ PropertyChanged=�  $��1��$ Search=�$��15��$��15��
 �����$��Right=�$��
5, 0, 0, 0q��� "  ��.���  � ��$ 10����  � ��
 �������  � ��
 �������  � �� �� " ��.R��  �� $��* ��� $
��Auto ��� $��* �.+��� $��0��
������ $	��▲��$��ScrollBar.LineUpCommand��]� $, 
PART_Track��$��1��- �� $��ScrollBar.PageUpCommand��. �� $��ScrollBar.PageDownCommand��/ n� #��z���� $��2��
������ $	��▼��$��ScrollBar.LineDownCommand��