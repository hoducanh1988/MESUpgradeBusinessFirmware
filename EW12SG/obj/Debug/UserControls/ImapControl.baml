   M S B A M L   `   `   `  ����A  =EW12SG, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:EW12SG.UserControlsEW12SG.UserControls  5      %    EW12SG.UserControls.ImapControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:EW12SG.UserControls     5        � DataContext  5   5   �� .��5
      �� $��80��6   $	��150��6    � ContextMenu 5   
   �� 5      ��5      �� 5       �� 
TargetType" ��6    �� Triggers 5      �� 5       �� Binding 6*   ��  �� Path$ isconnected� �� Value true6O   .a�5   "   ��  �� Property � y� �� Value$ Visible=�6?   5      �� 5       6*   �� $ isconnected�	 false6O   .a�5   "   ��  � y�$ 	Collapsed=�6?   5      5      .@�5      m� -   5        $'��!change ip address to 192.168.88.1��5      ��5      �� 5      " m�6    .�5      ��  � {� . 6:    5      �� 5   "    6.   �� $ ip� 192.168.88.16J   .a�5    &   ��  � {� .  6B   5!   #   5#      5%      m� 5&      -   5        $��get mac address��5&      64   m� 5'      -   5        $#��get firmware build time stamp��5'      6B   5(      ._�5*   
   � $
��Left=�6   .+�5+      �� $��0,2,0,0q�6   $	��Top=�6&   $
��Left=�6>   $��60��6Y   $��55��6d   ._�5,      � .+�5-      � 	 � PointsY	 �   ��� @KL @��@KL �)� c����)� l��2
��j �2
@T� �)� z  c�
���   ���5.      �� 5/      " �6$    50   "   �� 51   &    62   �� $ isconnected� true6W   .a�52   *   ��  � ��
 � � �6A   �� 53   *    �@�ToolTip$ Network status: Connected��6D   54   '   �� 55   &    62   �� $ isconnected�	 false6W   .a�56   *   ��  � ��
 �  ��6A   �� 57   *    �@�ToolTip$" Network status: Disconnected��6D   58   '   5:      5<      �� 5=      $��/Asset/Image/internet.png��6   $��10��6?   $��10��6J   $��	10,0,14,0q�6V   �� 5>      $
��IMAP��6   $
��Bold"�6+   
�������$��10(�6P   $��	10,30,0,0q�6^   5?      5@      � 5A      $��Bottom=�6   $
��Left=�63   .+�5B      �� $��11(�6Y   $��SemiBold"�6g   ��6   �� $ ip�
 �� UpdateSourceTrigger$
 PropertyChanged=�6g   �� 5C      
��� ���$��11(�6y   $��SemiBold"�6�   ��6   �� $ accessapstring�$
 PropertyChanged=�6�   5D      5F      5G      5I      