   M S B A M L   `   `   `  ����M  IPOP-sf-41_2016 GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:POP_sf_41_2016_GUIPOP_sf_41_2016_GUI  5   	   (    "POP_sf_41_2016_GUI.UI.StavkaWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:POP_sf_41_2016_GUI     5   	     � Title$  StavkaWindow��5   	   $	��450��6   $	��450��6+    � 	Resources 5	         ValidationError+  %POP_sf_41_2016_GUI.ValidatorConverter  	converter��%�   &	        &	 �     �� 5
   
   .R�5      ]� .+�5      �� 
���  ��$��16(�6-   $��Right=�6;   $��Center=�6R   !!!6t   �� 5      
���  ��$��1q�6+   ._�5      �� 5      5      5        5   
   6#   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��247* �6   �� 5      $
��100* �6   �� 5      $	��73* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��dataGridNamestaj��5         SelectionMode$ Single=�6+      CanUserResizeColumns .     CanUserResizeRows .     CanUserDeleteRows .     CanUserAddRows .     CanUserSortColumns . $��Center=�6�   $	��207��6�   $��10,30,10,10q�6�   $��Center=�6  $	��422��6  	   
IsReadOnly	 . 
   Columns
 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Naziv��6D      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6T   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   ��  �� Path$ Naziv�6T     5      $ Sifra��6D   $ * 6T    6%   �� $ Sifra�6T     5      $
 Cena��6L   $ * 6[    6%   �� $ JedinicnaCena�6[     5      $ Kolicina��6P   $ * 6c    6%   �� $ KolicinaUMagacinu�6c     5       $ Tip namestaja��6K   $ * 6c    6%   �� $ TipNamestaja�6c   5"        5#   
   -   5        $��dgDodatnaUsluga��5#      $ Single=�6*   	 . $��Center=�6S   $	��207��6p   $��10,30,10,10q�6}   $��Center=�6�   $	��422��6�   
 5$        5%      $ Naziv��6D   $ * 6T    6%   �� $ Naziv�6T     5&      $
 Cena��6C   $ * 6R    6%   �� $
 Cena�6R   5(      �� 5)   
   -   5        $��
btOdustani��5)      $��Odustani��6#   $
��Left=�66   $��
319,22,0,0q�6Q   $��2��6e   $	��Top=�6r   $��94��6�   $��37��6�   $��16(�6�   6�   �� 5*   
   -   5        $��Potvrdi��5*      $��Potvrdi��6    $
��Left=�62   $��
197,22,0,0q�6M   $��2��6a   $	��Top=�6n   $��94��6�   $��37��6�   $��16(�6�    J� RenderTransformOrigin$ 0.532,0.919&�6�   �� 5+   
   -   5        $��
lbKolicina��5+      $��Kolicina��6"   $
��Left=�65   $��	19,33,0,0q�6P   $��1��6c   $	��Top=�6p   $��16(�6�   $��31��6�   $��65��6�   �� 5,   
   -   5        $��
tbKolicina��5,       =� ErrorTemplate# [  6$   $
��Left=�6l   $��33��6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��165��6�   $��
115,33,0,0q�6�   $��1��6�   $��16(�6�   .��6�   ��5-      �� 5.      $ Kolicina�6    �� UpdateSourceTrigger$ PropertyChanged=�6*    �� ValidationRules 5/      )  #POP_sf_41_2016_GUI.ValidationDouble  50      52      .��53      54      �� 55   
   $��
115,71,0,0q�6   
���  ��$
��Bold"�69   $
��Left=�57      $	��317��67   $��23��6C   $	��Top=�6O   $��1��6g   ��56      ��  �� ElementName$ 
tbKolicina��$ (Validation.Errors)� �� 	Converter# [ 57   g   �� 59   
   -   5        $��tbPopust��59      # [  6"   $
��Left=�6j   $��33��6�   $
 Wrap=�6�   $	��Top=�6�   $	��165��6�   $��
115,33,0,0q�6�   $��1��6�   $��16(�6�   .��6�   ��5:      �� 5;      $ Popust�6   $ 	LostFocus=�6(    5<      )   #POP_sf_41_2016_GUI.ValidationPopust  5=      5?      .��5@      5A      �� 5B   
   $��
115,71,0,0q�6   
���  ��$
��Bold"�69   $
��Left=�5D      $	��317��67   $��23��6C   $	��Top=�6O   $��1��6g   ��5C      �� $ tbPopust��$ (Validation.Errors)�# [ 5D   g   5E      5F      