   M S B A M L   `   `   `  ����F  BMMOGamePlay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MMOGamePlayMMOGamePlay  5   	       MMOGamePlay.ServerList   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:MMOGamePlay     5   	   -   5          �Name$  Server_List��5   	    � Title$ 
ServerList��5   	   $	��450��6   $	��800��6)   .��5	      � .+�5
   
   �� -   5         ��Name$ lblServerList��5
      $��SERVER LIST��6'   $��Center=�6=   $��61��6Z   $��283,33,283,0q�6f   $	��Top=�6|   $	��226��6�   ���	#FF14F1DD$��36(�6�   $
��Bold"�6�   ��6�   Z� 6�   �� 5   
   -   5         ��Name$ listServers��5      $��Center=�6(   $	��231��6E   $��	0,122,0,0q�6R   $	��Top=�6e   $	��454��6}   
���   �$��16(�6�   $
��Bold"�6�    � 	Resources 5       �� 
TargetType �� Property �� Value��%T   '��       �� 5      " ��6   .�5      ��  � ��$
 Left=�6C   5      5      ��5      �� 5       �� EndPoint$ 0.5,1&�6&   	 �� 
StartPoint$	 0.5,0&�67   .��5      � 
 � Color$
 Black��6#    � Offset$ 0Q�61   � 5      $
 	#FF69EAEA��6#   $ 1Q�65   5       �� View 5      �� 5      .P�5      �� $��Server Name��6%    �� Width$	 200��6_    �� DisplayMemberBinding 6:   �� *Key+6_   �� 5      $��
Server Url��6%   $	 350��6`    69   �� *Value+6`   �� 5      $��See Friends��6%    �� CellTemplate 5      �� 5      .R�5   "   �� -   5        $��See Friends��5   )   6?   5       5"      �� 5#      $��Connect��6%    5$      �� 5%      .R�5&   "   �� -   5        $��Connect��5&   )   6;   5'      5)      5*      5-      �� 5.   
   -   5        $ lblUname��5.      $��Welcome��6"   $
��Left=�64   $��	558,4,0,0q�6O   $	��Top=�6b   $	��210��6z   $��20(�6�   $��Italic$�6�   �� 5/   
   -   5         ��Name$ btnSeeFriends��5/      $��My friend list��6(   $
��Left=�6A   $��21��6\   $��
575,41,0,0q�6h   $	��Top=�6|   $	��127��6�   ���	#FFF7F0F0 �� IsCancel . ��50      �� 51      $ 0.5,1&�6&   $	 0.5,0&�67   .��52      � $
 Black��6#   $ 0Q�61   � 53      $
 	#FF36AFE8��6#   $ 1Q�65   54      56      57      ��58      �� 59   
    �� ImageSource$ /images/bg.jpg��6    j� Stretch$ UniformToFill=�62   5;      