   M S B A M L   `   `   `  ����F  BBug Tracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Bug_Tracker.ViewsBug_Tracker.Views  5      %    Bug_Tracker.Views.LoginPageView   5      \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Bug_Tracker.Views     5      ��5
      �� 5   
     �� ImageSource$0  *../Assets/Images/LoginBackgroundImage.jpeg��6   .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��1��6   $��1��6   $	��500��6.   $	��525��6:   
������� �� CornerRadius$ 6��6_   ._�5      �  5      �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5       $
��Auto �6$   �� 5!      $
��Auto �6$   .+�5$      �� -   5        $��
loginLabel��5$   %   $��0��6   $��Bug Tracker Login��67   $��DM Sans)�6S   $��30(�6h   $��Center=�6v   $��Bottom=�6�   $��0, 50, 0, 15q�6�   � 5&      $��1��6    5'      �� 5(      $��* �6(   �� 5)      $��* �6(   .+�5,      �� -   5         ��Name$ usernameTextbox��5,   +   $��0��6   $��14(�6D   $��DM Sans)�6R    �� TextAlignment$
 Left=�6g   $��25, 20, 25, 0q�6|   $��50, 10, 50, 0q�6�   $��50��6�   $��Bottom=�6�   �� 5.      -   5        $ passwordTextbox��5.   +   $��1��6   $��14(�6s   $��DM Sans)�6�   $
 Left=�6�   $��25, 20, 25, 0q�6�   $��50, 0, 50, 10q�6�   $��50��6�   $	��Top=�6�   ��6D   �� *CurrentViewModel.PasswordText+.��6�   ��5/      �� 50       �� 
TargetType" ��6$   .�51   "   ��  �� Property � �� �� Value
 �����6?   �� 52   "    � ��$ Password��69   	 �� Triggers	 53   "   P� 54   &   
 P� Property
 �@J�	IsFocused P� Value$
 True��6C   . �55   *   ��  � ��$  ��6A   �� 56   *    � ��
 �   �6G   57   '   59      .��5:   )   5;      5<      �� 5>      -   5         ��Name$ loginButton��5>   &   $��2��6   $��SIGN IN��6;   $��50��6M   $	��Top=�6Y   $��50, 0, 50, 20q�6q   
������
�������6�   � 5@      $��3��6   $��0, 50, 0, 0q�6$    5A      �� 5B      $
��Auto �6(   �� 5C      $
��Auto �6(   �� 5D      $
��Auto �6(   .+�5G      �� $��0��6!    �� Orientation$ 
Horizontal=�6.   $��Center=�6G   $��
0, 5, 0, 5q�6d   .+�5H      �� $��Forgot your ��6$   
���iii�68   �� 5I      $��	Password?��6$   $��	Underline{�65   
���OO/� � Cursor$
 Hand��6l   5J      �� 5L      $��1��6!   $ 
Horizontal=�6.   $��Center=�6G   $��
0, 5, 0, 5q�6d   .+�5M      �� $��Create an account? ��6$   
���iii�6?   �� 5N      $��Sign Up��6$   $��	Underline{�63   
���OO/�$
 Hand��6j   5O      �� 5Q      $��2��6!   $ 
Horizontal=�6.   $��Center=�6G   $��
0, 5, 0, 5q�6d   .+�5R      �� $��Sign in as a ��6$   
���iii�69   �� 5S      $��	Demo User��6$   $��	Underline{�65   
���OO/�$
 Hand��6l   5T      5V      5W      5X      5Y      5Z      