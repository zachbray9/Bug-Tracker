   M S B A M L   `   `   `  ����F  BBug Tracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Bug_Tracker.ControlsBug_Tracker.Controls  5      K*clr-namespace:Bug_Tracker.State.NavigatorsBug_Tracker.State.Navigators  5      0    *Bug_Tracker.Controls.HomePageNavigationBar   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:Bug_Tracker.Controls     5      :nav*clr-namespace:Bug_Tracker.State.Navigators     5      .��5
      � ���#242526$
��left=�6    .+�5   
   �� 
������ $��1q�6+   ._�5      �   � RowDefinitions  5      �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $��* �6$   .+�5      � $��0��6     5      �� 5      $
��Auto �6(   �� 5      $��* �6(   .+�5      �� $��0��6    $
��🕷��6-   
������� J� Opacity$	 0.5Q�6Q   $
��Left=�6_   $��Center=�6z   $��20(�6�   $��10q�6�   �� 5      $��1��6!    �� Orientation$ 
Horizontal=�6.   $��Center=�6G   .+�5      �� $	��Hi,��6$   
�������$��Center=�6C   $��Bottom=�6`   $��20(�6{   $��5, 10, 5, 0q�6�   �� 5      
�������$��Center=�6l   $��Bottom=�6�   $��20(�6�   $��Light"�6�   $��
0, 0, 5, 0q�6�   ��6$   �� *%#Authenticator.CurrentUser.FirstName+6�   5      5      �� 5!      $��1��6   
�������$��5, 10, 5, 0q�6@   �� 5#      $��2��6   $��0, 20, 0, 0q�6*   .+�5&      	� $��Projects��6"    d� 	IsChecked$
 TrueY�65      	NavButton#��[  6F   +   %Bug_Tracker.State.Navigators.ViewType   ProjectsPage#��Z 6�   ��6i   �� *Navigator.NavigateCommand+6�   	� 5(      $��Account��6"   #��[  64      AccountPage#��Z 6�   ��6W   �� *Navigator.NavigateCommand+6�   5*      5,      5-      5.      5/      