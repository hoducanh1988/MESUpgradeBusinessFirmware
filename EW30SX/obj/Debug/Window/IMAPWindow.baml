   M S B A M L   `   `   `  ����A  =EW30SX, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:EW30SXEW30SX  5   	   9!clr-namespace:EW30SX.UserControlsEW30SX.UserControls  5   	       EW30SX.IMAPWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:EW30SX     5   	   8
mycontrols!clr-namespace:EW30SX.UserControls     5   	     � Title$  
IMAPWindow��5	   	   $	��550��6   $	��900��6)    � WindowStartupLocation$ CenterScreen=�65    � 
ResizeMode$ NoResize=�6Z    � WindowStyle$
 None=�6p    � AllowsTransparency . 
������ .��5      �� -   5        $��0,0,8,8q�5       �� CornerRadius$ 10��6/   ���#007ACC$��1q�6W    J� Effect 5   
   3  -System.Windows.Media.Effects.DropShadowEffect  5         ShadowDepth$ 5Q�6      	Direction$	 320Q�6/   	   Color$	 Black��6?   
   Opacity$	
 0.5Q�6M      
BlurRadius$ 3Q�6[   ._�5   
   �� 
�������$ 10��6%   
����� �$��1,5,1,3q�6J   ._�5      �  � RowDefinitions 5      �� 5      $��50 �6$   �� 5      $��* �6$   .+�5      � $��0��6   .+�5      �� $��SemiBold"�6   
���   �$��30(�6E   $��IMAP Management��6S   �� 5      -   5        $ 3��5       � ToolTip$ Close��6@   
���  ��$��	0,-10,4,0q�6a   $��Center=�6t   $��Right=�6�   $��35��6�   $��35��6�   
�������$��1q�6�   ._�5      �� -   5        $ Close��5   2   $��	0,-10,0,0q�6B   $��Center=�6U   $��30(�6r   $��
ExtraBlack"�6�   
�������$��X��6�   5      5      �� 5      $��5q�6   $ 5��6$   
�������$��1,1,1,1q�6H   $��1��6b   ._�5      �  � 	Resources 5        �� 
TargetType �� Property �� Value��%  '��       �� 5!      " ��6$   .�5"   "   ��  � ��$ 50��6:   �� 5#   "    � ��$ Center=�6M   �� 5$   "    � ��$
 Left=�6O   �� 5%   "    � ��$ 0,0,0,1q�6D   �� 5&   "    � ��$ SemiBold"�6?   �� 5'   "    � ��$ 14(�6=   5(      5)      .+�5+      �� $��0,0,0,0q�6%   $��70��66   $	��Top=�6B   $
��Left=�6Z   .+�5,      � $��0,2,0,0q�6(   .+�5-   "   �� $��
IP Start: ��6(   �� 5.   "   $	��150��6*   ��67   ��  �� Path$ ipstart� �� UpdateSourceTrigger$ PropertyChanged=��� 5/   "   $��IP End: ��6(   �� 50   "   $	��150��6*   ��66   �� $ ipend�$ PropertyChanged=��� 51   "   $��Ping timeout: ��6(   �� 52   "   $��50��6*   ��65   �� $ pingtimeout�$ PropertyChanged=��� 53   "   $��Ping retry: ��6(   �� 54   "   $��50��6*   ��65   �� $ 	pingretry�$ PropertyChanged=�55      � 56      $��0,10,0,0q�6(   .+�57   "   	� $��50,0,0,0q�6.   $��Only show IMAP connected��6@   	� 58   "   $��20,0,0,0q�6�   $��Show all IMAP��6�    d� 	IsChecked 6.   �� $ showallimap�$ PropertyChanged=� �� Mode$ TwoWay=�6�   59      5:      �� 5;      -   5         ��Name$ _scrollview��5;   '   $��2,60,3,2q�6<   
�������$��0,1,0,0q�6a   .��5<      ��  �� Padding$ 	30,20,5,5q�6%   $��0,0,1,27q�69   
�������$	��0.5q�6^   ._�5=   "   �� -   5         ��Name$ viewIMAP��5=   *   ��5>   &   �� 5?   *   .R�5@   .   �  K� IsItemsHost . 68   5A   +   5C   #   5D      5E      �� 5G      -   5        
 � Tag$ 
buttonping��5G   6   $&�� Start Ping To Get IMAP Connected��6G   $��0,0,22,1q�6r   $��SemiBold"�6�   $��Bottom=�6�   $��Right=�6�   $	��220��6�   $��25��6�   � 5H      $��Bottom=�6$   $
��Left=�6?   $��0,-2,0,5q�6Z   .+�5I      �� $��	Progress:��6$   �� 5J      ��6$   �� $ progresscontent�$ PropertyChanged=�5K      � 5L      $��	3,0,252,1q�6&    � Minimum$ 0Q�6�   $��Bottom=�6�   $��5��6�    � Maximum 69   �� $ progressmax�$ PropertyChanged=� � Value 6�   �� $ progressvalue�$ PropertyChanged=�6�   5M      5N      5P      5Q      5R      5T      