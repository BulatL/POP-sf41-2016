   M S B A M L   `   `   `  ����M  IPOP-sf-41_2016 GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:POP_sf_41_2016_GUIPOP_sf_41_2016_GUI  5   	   *    $POP_sf_41_2016_GUI.UI.KorisnikWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:POP_sf_41_2016_GUI     5   	     � Title$  KorisnikWindow��5   	   $	��400��6    $��306.667��6-    � 	Resources 5	         ValidationError+  %POP_sf_41_2016_GUI.ValidatorConverter  	converter��%�   &	        &	 �     �� 5
   
   .R�5      ]� .+�5      �� 
���  ��$��16(�6-   $��Right=�6;   $��Center=�6R   !!!6t   �� 5      
���  ��$��1q�6+   ._�5      �� 5      5      5        5   
   6#   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��111* �6   �� 5      $
��188* �6    � RowDefinitions 5   
   �� 5      $	��70* �6   �� 5      $	��70* �6   �� 5      $	��70* �6   �� 5      $	��70* �6   �� 5      $	��70* �6   �� 5      $	��50* �6   .+�5!   
   �� $	��Ime��6   $
��Left=�6   $��	10,13,0,0q�69   $	��Top=�6L    J� RenderTransformOrigin$ 0.026,-0.182&�6d   $��29��6�   $��39��6�   $��14(�6�   �� 5"   
   $��Prezime��6   $
��Left=�6"   $��	10,13,0,0q�6=   $��1��6P   $	��Top=�6]   $ -0.184,0.038&�6u   $��26��6�   $��52��6�   $��14(�6�   �� 5#   
   $��WrapWithOverflow=�6   $��Korisnicko ime��64   $
��Left=�6J   $��	10,16,0,0q�6e   $��2��6x   $	��Top=�6�   $��33��6�   $��91��6�   $��14(�6�   �� 5$   
   $��Lozinka��6   $
��Left=�6"   $��10,8,0,0q�6=   $��3��6O   $	��Top=�6\   $��26��6t   $��59��6�   $��14(�6�   �� 5%   
   $��Tip korisnika��6   $
��Left=�6(   $��	10,18,0,0q�6C   $��4��6V   $	��Top=�6c   $��31��6{   $��91��6�   $��14(�6�   $ 0.439,0.577&�6�   �� 5'   
   -   5        $��tbIme��5'       =� ErrorTemplate# [  6   $��1��6g   $
��Left=�6w   $��23��6�   $��	10,13,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��129��6�   $��14(�6�   .��6�   ��5(      �� 5)       �� Path$	 Ime�6    �� UpdateSourceTrigger$ PropertyChanged=�6%   	 �� ValidationRules	 5*      )  #POP_sf_41_2016_GUI.ValidationString  5+      5-      .��5.      5/      �� 50   
   $��1��6   $��	10,41,0,0q�6$   
���  ��$
��Bold"�6H   $
��Left=�52      $	��168��67   $��23��6C   $	��Top=�6O   ��51      �� 
 �� ElementName$
 tbIme��$ (Validation.Errors)� �� 	Converter# [ 52   O   �� 54   
   -   5        $��	tbPrezime��54      # [  6#   $��1��6k   $
��Left=�6{   $��23��6�   $��	10,13,0,0q�6�   $��1��6�   $
 Wrap=�6�   $	��Top=�6�   $	��129��6�   $��14(�6�   .��6	  ��55      �� 56      $ Prezime�6   $ PropertyChanged=�6)   	 57        58      5:      .��5;      5<      �� 5=   
   $��1��6   $��	10,41,0,0q�6$   
���  ��$
��Bold"�6H   $
��Left=�5?      $	��168��67   $��23��6C   $	��Top=�6O   $��1��6g   ��5>      �� $
 	tbPrezime��$ (Validation.Errors)�# [ 5?   g   �� 5A   
   -   5        $��tbKorisnickoIme��5A      # [  6)   $��1��6q   $
��Left=�6�   $��23��6�   $��	10,13,0,0q�6�   $��2��6�   $
 Wrap=�6�   $	��Top=�6�   $	��129��6�   $��14(�6  .��6  ��5B      �� 5C      $ KorisnickoIme�6   $ PropertyChanged=�6/   	 5D        5E      5G      .��5H      5I      �� 5J   
   $��1��6   $��	10,41,0,0q�6$   
���  ��$
��Bold"�6H   $
��Left=�5L      $	��168��67   $��23��6C   $	��Top=�6O   $��2��6g   ��5K      �� $
 tbKorisnickoIme��$ (Validation.Errors)�# [ 5L   g   �� 5N   
   -   5        $��
tbPassword��5N      # [  6$   $��1��6l   $
��Left=�6|   $��	10,14,0,0q�6�   $��3��6�   $	��Top=�6�   $ 
-1.833,0.2&�6�   $	��129��6�   $��22��6�   $��14(�6
  .��6  ��5O      �� 5P      $ Lozinka�6   $ PropertyChanged=�6)   	 5Q        5R      5T      .��5U      5V      �� 5W   
   $��1��6   $��	10,41,0,0q�6$   
���  ��$
��Bold"�6H   $
��Left=�5Y      $	��168��67   $��23��6C   $	��Top=�6O   $��3��6g   ��5X      �� $
 
tbPassword��$ (Validation.Errors)�# [ 5Y   g   �� 5[   
   -   5        $��cbTipKorisnika��5[   >   $��1��6T   $
��Left=�6d   $��	10,18,0,0q�6   $��4��6�   $	��Top=�6�   $	��129��6�   $��26��6�   $��14(�6�    �� SelectedItem 6   �� $ TipKorisnika�6�   �� 5\   
   -   5        $��btnOdustani��5\      $��Odustani��6$   $��1��67   $
��Left=�6G   $��
110,16,0,0q�6b   $��5��6v   $	��Top=�6�   $��75��6�   $��22��6�   �� 5]   
   -   5        $��
btnPotvrdi��5]      $��Potvrdi��6#   $��1��65   $
��Left=�6E   $��	23,16,0,0q�6`   $��5��6s   $	��Top=�6�   $��75��6�   $��22��6�   ��5^      �� 5_       �� 
TargetType" ��6   .�5`      ��  �� Property � {� �� Value .  62    �� Triggers 5a      ^� 5b       ^� 
Conditions 5c      �� 5d   "    �� Binding 6,   �� $ (Validation.HasError)�$
 tbIme�� �� Value	 false6n   �� 5e   "    6,   �� $ (Validation.HasError)�$
 	tbPrezime��	 false6r   �� 5f   "    6,   �� $ (Validation.HasError)�$
 tbKorisnickoIme��	 false6x   �� 5g   "    6,   �� $ (Validation.HasError)�$
 
tbPassword��	 false6s   .0�5i      ��  � {� . 6:   5j      5l      5n      5p      5q      