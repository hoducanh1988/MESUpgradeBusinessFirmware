   M S B A M L   `   `   `  ����A  =EW12SG, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:EW12SGEW12SG  5   	       EW12SG.InputMacAddress   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:EW12SG     5   	     � Title$  InputMacAddress��5   	   $	��250��6!   $	��500��6.    � WindowStartupLocation$ CenterScreen=�6:    � 
ResizeMode$ NoResize=�6_    � WindowStyle$
 None=�6u    � AllowsTransparency . 
������  � 	Resources 5
       �� 
TargetType �� Property �� Value��%b   '��       �� 5   
   " ��6   .�5      ��  �@�UseLayoutRounding . 62   5      5      .��5      �� -   5        $��0,0,8,8q�5      	 �� CornerRadius$	 10��6/   ���#007ACC$��1q�6W   
 J� Effect
 5   
   3  -System.Windows.Media.Effects.DropShadowEffect  5         ShadowDepth$ 5Q�6      	Direction$	 320Q�6/      Color$ Black��6?      Opacity$	 0.5Q�6M      
BlurRadius$ 3Q�6[   ._�5   
   �� 
�������$	 10��6%   
����� �$��1,5,1,3q�6J   ._�5      � .+�5      ��  J� Opacity$	 0.3Q�6   $��0,5,40,0q�6&   $	��250��68   $	��220��6D   $	��top=�6Q   $��Right=�6i   $��/Asset/Image/barcode.png��6�   �� 5      .+�5      � .+�5      �� -   5         ��Name$ lblDut��5       $
��Bold"�60   
���   �$��30(�6U   $�� ��6c   �� 5      -   5        $	 3��5   !    � ToolTip$ Close��6D   
���  ��$��	0,-10,4,0q�6e   $��Center=�6x   $��Right=�6�   $��35��6�   $��35��6�   
�������$��1q�6�   ._�5      �� -   5        $ Close��5   6   $��	0,-10,0,0q�6F   $��Center=�6Y   $��30(�6v   $��
ExtraBlack"�6�   
�������$��X��6�   5      5      �� 5!      
���   �$��0,0,0,10q�6/   $��17(�6A   $��Input Mac Address :��6O   $��SemiBold"�6m   �� 5"      -   5         ��Name$ txtMac��5"   n   
������ $ 1Q�67   $��5,0,10,0q�6C   $��55��6~   $��40(�6�   $
��Bold"�6�   $��Center=�6�   $��0,0,0,0q�6�   $��0,0,0,1q�6�   .��6�   
 5#        5$      $ 5Q�6/   $	 270Q�6?   $	 Red��6O   $	 0.8Q�6[   $ 25Q�6i   .��5%   *   5&      �� 5'      -   5         ��Name$ 	tbMessage��5'       $��14(�63   $
��Wrap=�6A   
���  ��$�� ��6f   $��5,10,5,0q�6n   5(      5)      5*      5+      5/      