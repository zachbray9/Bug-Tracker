   M S B A M L   `   `   `  ���� F  BBug Tracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Bug_Tracker.ControlsBug_Tracker.Controls  K*clr-namespace:Bug_Tracker.State.NavigatorsBug_Tracker.State.Navigators  0    *Bug_Tracker.Controls.HomePageNavigationBar   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     4local"clr-namespace:Bug_Tracker.Controls     :nav*clr-namespace:Bug_Tracker.State.Navigators     .��� ���#242526$
��left=�.+��� 
������ $��1q�._��   � RowDefinitions  �� $
��Auto ��� $
��Auto ��� $��* �.+�� $��0��  �� $
��Auto ��� $��* �.+��� $��0��$
��🕷��
������� J� Opacity$	 0.5Q�$
��Left=�$��Center=�$��20(�$��10q��� $��1�� �� Orientation$ 
Horizontal=�$��Center=�.+��� $	��Hi,��
�������$��Center=�$��Bottom=�$��20(�$��5, 10, 5, 0q��� 
�������$��Center=�$��Bottom=�$��20(�$��Light"�$��
0, 0, 5, 0q����� *%#Authenticator.CurrentUser.FirstName+�� $��1��
�������$��5, 10, 5, 0q��� $��2��$��0, 20, 0, 0q�.+�	� $��Projects�� d� 	IsChecked$
 TrueY�   	NavButton#��[  +   %Bug_Tracker.State.Navigators.ViewType   ProjectsPage#��Z ���� *Navigator.NavigateCommand+	� $��Account��#��[     AccountPage#��Z ���� *Navigator.NavigateCommand+