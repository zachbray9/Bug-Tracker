   M S B A M L   `   `   `  ���� F  BBug Tracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Bug_Tracker.ViewsBug_Tracker.Views  K*clr-namespace:Bug_Tracker.State.NavigatorsBug_Tracker.State.Navigators  )    #Bug_Tracker.Views.CreateAccountView   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1localclr-namespace:Bug_Tracker.Views     :nav*clr-namespace:Bug_Tracker.State.Navigators     ����   �� ImageSource$0  *../Assets/Images/LoginBackgroundImage.jpeg�� j� Stretch$ UniformToFill=�.��� {��� *UserInputIsEnabled+.+��� $	��500��$	��550��
������� �� CornerRadius$ 8��._��  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $��* ��� $��* ��� $��* ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��Create Account��$��Center=�$��24(�$��Arial)�$��0, 20, 0, 0q��� $��1��$9��3Create an account to view and manage your projects.��$��Center=�$��12(�$��Arial)��� $��2�� �� Orientation$ 
Horizontal=�.+��� .+��� $��
First Name��$
��Left=�$��12(�$��Arial)�$��50, 10, 50, 0q��� $��14(�$��Arial)� �� TextAlignment$
 Left=�$
��Left=�$��Center=�$��50, 0, 25, 0q�$��15, 0, 15, 0q�$��35��$	��175������ *	FirstName+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� .+��� $��	Last Name��$
��Left=�$��12(�$��Arial)�$��25, 10, 50, 0q��� $��14(�$��Arial)�$
 Left=�$
��Left=�$��Center=�$��25, 0, 50, 0q�$��15, 0, 15, 0q�$��35��$	��175������ *
LastName+$ TwoWay=�$ PropertyChanged=��� $��3��.+��� $��Email��$
��Left=�$��12(�$��Arial)�$��50, 0, 50, 0q��� $��14(�$��Arial)�$
 Left=�$
��Left=�$��Center=�$��50, 0, 50, 0q�$��15, 0, 15, 0q�$��35������ *Email+$ TwoWay=�$ PropertyChanged=��� $��4��.+��� $��Password��$
��Left=�$��12(�$��Arial)�$��50, 0, 50, 0q��� $��14(�$��Arial)�$
 Left=�$
��Left=�$��Center=�$��50, 0, 50, 0q�$��15, 0, 15, 0q�$��35������ *
Password+$ TwoWay=�$ PropertyChanged=��� $��5��.+��� $��Confirm Password��$
��Left=�$��12(�$��Arial)�$��50, 0, 0, 0q��� $��14(�$��Arial)�$
 Left=�$
��Left=�$��Center=�$��50, 0, 50, 0q�$��15, 0, 15, 0q�$��35������ *ConfirmPassword+$ TwoWay=�$ PropertyChanged=��� $��6��.+��� -    ��Name$ createAccountErrorTextBox��$��12(�$��Arial)�$
��Wrap=�
���  ��$��50, 15, 50, 15q����� *CreateAccountErrorText+�� -   	 ��Name$	 createAccountButton��   GenericButton#��[  $��Create Account��$��Center=����#7b68ee
�������$��50, 0, 50, 20q����� *CreateAccountCommand+�� $��7��$ 
Horizontal=�$��Center=�$��Bottom=�$��0, 30, 0, 40q�.+��� $��Already have an account? ���� $��Sign in��$��	Underline{�
 � Cursor$

 Hand�� J� InputBindings d� +   %Bug_Tracker.State.Navigators.ViewType   	LoginPage �� CommandParameter# Z  d� MouseAction$ 	LeftClicke� �� Command �� *Navigator.NavigateCommand+