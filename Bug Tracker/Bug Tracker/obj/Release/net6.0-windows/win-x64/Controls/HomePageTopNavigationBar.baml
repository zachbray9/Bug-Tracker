   M S B A M L   `   `   `  ���� F  BBug Tracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Bug_Tracker.ControlsBug_Tracker.Controls  K*clr-namespace:Bug_Tracker.State.NavigatorsBug_Tracker.State.Navigators  3    -Bug_Tracker.Controls.HomePageTopNavigationBar   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     4local"clr-namespace:Bug_Tracker.Controls     Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  4nav*clr-namespace:Bug_Tracker.State.Navigators    � 	Resources      BooleanToVisibilityConverter��%   &	        �� .��� ���#242526$��Stretch=�$	��Top=�$��50��.+���  �� Orientation$ 
Horizontal=�$��Right=�$��10, 10, 20, 10q�.+���  � Cursor$
 Hand������ *OpenAccountPopupCommand+����  �� 
TargetType" ��.���  �� Property � �� �� Value �� .R�� .+��� $��24��$��24�� �� CornerRadius$ 12�����#00875A._��� 
�������$��10(�$��Arial)�$��Center=�$��Center=����� *$"Authenticator.CurrentUser.Initials+�  � 	StaysOpen$ False�� � AllowsTransparency$
 True��$��Bottom=�$	��325��$	��185��	 � HorizontalOffset$
	 -300��
 � VerticalOffset$
 5������ *IsAccountPopupOpen+.���� ���� " ��.���  � �� �� .R��� 
�������$ 4��
�������$	��0.5q�._��� .+��� ���#f4f5f7$ 4�� �� Padding$ 15q�$��5q�._��  � ColumnDefinitions �� $��1* ��� $��3* �.+��� $��0�����#00875A$��60��$��60��$ 30��._��� $��24(�$��Arial)�
�������$��Center=�$��Center=����� *$"Authenticator.CurrentUser.Initials+�� $��1��$��15, 0, 0, 0q�$��Center=�.+��� $��20(�$��Arial)�$��
0, 0, 0, 5q����� *$"Authenticator.CurrentUser.FullName+�� $��Arial)�
����������� *!Authenticator.CurrentUser.Email+�� +   %Bug_Tracker.State.Navigators.ViewType   AccountPage#��Z ���� *Navigator.NavigateCommand+���� " ��.���  � �� �� .R���  ��Name$ ManageAccountButtonBorder��
������ $ 20, 10q�._��� $ 
Horizontal=�.+�"  FontAwesome.WPF.ImageAwesome     Icon$ UserOutline=�$��18��$��18�� J� SnapsToDevicePixels$
 True�� � UseLayoutRounding$
 True��$��Center=��� $��Manage Account��$��14(�$��Arial)�$��Center=�$��20, 0, 0, 0q� �� Triggers P�  P� Property �@J�IsMouseOver P� Value$
 True��. ���  �� 
TargetName$ ManageAccountButtonBorder�� � �� �#f4f5f7�� ���� *LogoutCommand+���� " ��.���  � �� �� .R��� $ LogoutButtonBorder��
������ $ 20, 10q�._��� $ 
Horizontal=�.+�  $ SignOut=�$��18��$��18��$
 True��$
 True��$��Center=��� $��Log out��$��14(�$��Arial)�$��Center=�$��20, 0, 0, 0q� P�  �@J�IsMouseOver$
 True��. ��� $ LogoutButtonBorder�� � �� �#f4f5f7