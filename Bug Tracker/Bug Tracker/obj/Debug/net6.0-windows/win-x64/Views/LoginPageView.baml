   M S B A M L   `   `   `  ����F  BBug Tracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Bug_Tracker.ViewsBug_Tracker.Views  5      K*clr-namespace:Bug_Tracker.State.NavigatorsBug_Tracker.State.Navigators  5      %    Bug_Tracker.Views.LoginPageView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Bug_Tracker.Views     5      :nav*clr-namespace:Bug_Tracker.State.Navigators     5      ��5      �� 5   
     �� ImageSource$0  *../Assets/Images/LoginBackgroundImage.jpeg��6    j� Stretch$ UniformToFill=�6N   .��5      � {�6   �� *UserInputIsEnabled+ � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��1��6   $��1��6   $	��500��6.   $	��475��6:   
������� �� CornerRadius$ 8��6_   ._�5      �  5      �� 5      $
��Auto �6$   �� 5       $
��Auto �6$   �� 5!      $
��Auto �6$   �� 5"      $
��Auto �6$   .+�5%      �� -   5        $��
loginLabel��5%   %   $��0��6   $��
🕷 Login��67   $��Arial)�6Q   $��30(�6d   $��Center=�6r   $��Bottom=�6�   $��0, 50, 0, 15q�6�   � 5'      $��1��6    5(      �� 5)      $��* �6(   �� 5*      $��* �6(   .+�5-      �� $��0��6!   .+�5.      �� $��Email��6    $��14(�60   $��Arial)�6>   $��50, 10, 0, 0q�6Q   �� 5/      -   5         ��Name$ emailTextbox��5/   "   $
��True��6t   $��0��6�   $��14(�6�   $��Arial)�6�    �� TextAlignment$
 Left=�6�   $��Center=�6�   $��15, 0, 15, 0q�6�   $��50, 0, 50, 0q�6  $��35��6  $��Bottom=�6#  ��68   �� *Email+ �� UpdateSourceTrigger$ PropertyChanged=�6#  50      �� 52      $��1��6!   .+�53      �� $��Password��6    $��14(�63   $��Arial)�6A   $��50, 5, 0, 0q�6T   G� 54      -   5         G�Name$ passwordTextbox��54   3   $��1��6&   $
��True��6L   $��1��6]   $��14(�6j   $��Arial)�6x   $��Center=�6�   $��15, 0, 15, 0q�6�   $��50, 0, 50, 10q�6�   $��35��6�   $	��Top=�6�   55      57      �� 59      $��2��6   .+�5;      �� -   5        	 ��Name$	 loginErrorTextBlock��5;       $��14(�6]   $��Arial)�6k   $
��Wrap=�6~   
���  ��$��50, 10, 50, 20q�6�   ��6=   �� *LoginErrorText+6�   �� 5<      -   5        
 ��Name$
 loginButton��5<         GenericButton#��[  62   $��Sign In��6Y    �� 	IsDefault$
 True��6�   $
��True��6�   $��2��6�   $��35��6�   $	��Top=�6�   $��50, 0, 50, 20q�6�   ���#7b68ee
�������6  5=      � 5?      $��3��6   $��0, 30, 0, 0q�6$    5@      �� 5A      $
��Auto �6(   �� 5B      $
��Auto �6(   .+�5E      �� $��0��6!    �� Orientation$ 
Horizontal=�6.   $��Center=�6G   $��
0, 5, 0, 5q�6d   .+�5F      �� $��Create an account? ��6$   
���iii�$��Arial)�6T   �� 5G      $��Sign Up��6$   $��	Underline{�63   
���OO/�$��Arial)�6j    � Cursor$
 Hand��6~    J� InputBindings 5H      d� 5I   "    d� MouseAction$ 	LeftClicke�6]   +   %Bug_Tracker.State.Navigators.ViewType   CreateAccountPage �� CommandParameter# Z 6u    �� Command 6/   �� *Navigator.NavigateCommand+6u   5K      5L      �� 5N      $��1��6!   $ 
Horizontal=�6.   $��Center=�6G   $��
0, 5, 0, 5q�6d   $y�	Collapsed=�6x   .+�5O      �� $��Sign in as a ��6$   
���iii�$��Arial)�6N   �� 5P      $��	Demo User��6%   $��	Underline{�66   
���OO/�$��Arial)�6m   $
 Hand��6�    5Q      d� 5R   "   $ 	LeftClicke�6Z    6/   �� *LoginAsDemoUserCommand+6Z   5T      5U      5W      5X      5Y      5Z      5[      