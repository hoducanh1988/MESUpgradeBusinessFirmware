   M S B A M L   `   `   `  ����N  JMeshSwitchToLanMode, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:MeshSwitchToLanMode.UserCtrlMeshSwitchToLanMode.UserCtrl  5      ,    &MeshSwitchToLanMode.UserCtrl.ucLogItem   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <local*clr-namespace:MeshSwitchToLanMode.UserCtrl     5        � 	Resources  5
       �� 
TargetType   label_title �� Property �� Value  label_content �� Triggers �� Binding �� RelativeSource �� Path �� Value��%�  &	        &	 �    �� 5   
   " ��6   .�5      ��  � ��$ Center=�62   �� 5       � ��$
 Left=�64   �� 5       � ��$ 0,0,0,0q�6'   �� 5       � ��$	 270��6&   �� 5       � ��$ 27��6'   �� 5       � ��$ 13(�6)   �� 5       � ��$ Normal"�6+   �� 5       � �� �#7777776,   �� 5       � ��$ 0.7,0,0,0.7q�60   5      �� 5   
   " ��6   .�5      ��  � ��$ Center=�62   �� 5       � ��$
 Left=�64   �� 5       � ��$ 27��6'   �� 5       � ��$ 20(�6)   �� 5       � ��$	 200��6&   �� 5       � ��$ SemiBold"�6+   �� 5       � �� �#7777776,   �� 5        � ��$ 0.7,0,0.7,0.7q�60   �� 5!       � ��$ 10,0,0,0q�6(    5#      �� 5$       6   ��  �� *Self+$ Content�
 Passed6e   .a�5%      ��  � ��
 � � �63   5&      �� 5'       6   ��  �� *Self+$ Content�
 Failed6e   .a�5(      ��  � ��
 �  ��63   5)      �� 5*       6   ��  �� *Self+$ Content� 
Waiting...6e   .a�5+      ��  � ��
 � ���63   5,      �� 5-       6   ��  �� *Self+$ Content� None6e   .a�5.      ��  � ��
 �   �63   5/      �� 50       6   ��  �� *Self+$ Content� -6e   .a�51      ��  � ��
 �   �63   52      54      56      .��58      �� $��0,10,0,0q�6   .+�59   
   �� ���#777777$��	0,0.7,0,0q�6'   � 5:   
   .+�5;      �� #��[  6   $��Check mac format��69   �� 5<      #��[ 6   ��6;   �� $ checkMacFormat�	 �� UpdateSourceTrigger$	 PropertyChanged=�5=      � 5>   
   .+�5?      �� #��[  6   $��Find mesh ssid��69   �� 5@      #��[ 6   ��6;   �� $ findSsid�$	 PropertyChanged=�5A      � 5B   
   .+�5C      �� #��[  6   $��Connect to mesh ssid��69   �� 5D      #��[ 6   ��6;   �� $ connectSsid�$	 PropertyChanged=�5E      � 5F   
   .+�5G      �� #��[  6   $��	Login ssh��69   �� 5H      #��[ 6   ��6;   �� $ loginSsh�$	 PropertyChanged=�5I      � 5J   
   .+�5K      �� #��[  6   $��Switch mesh to lan mode��69   �� 5L      #��[ 6   ��6;   �� $ switchToLan�$	 PropertyChanged=�5M      � 5N   
   .+�5O      �� #��[  6   $��Verify mesh mode��69   �� 5P      #��[ 6   ��6;   �� $ verifyAfterSwitch�$	 PropertyChanged=�5Q      5R      5U      