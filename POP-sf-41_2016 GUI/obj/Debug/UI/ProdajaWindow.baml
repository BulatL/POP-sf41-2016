   M S B A M L   `   `   `  ����M  IPOP-sf-41_2016 GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:POP_sf_41_2016_GUIPOP_sf_41_2016_GUI  5   	   )    #POP_sf_41_2016_GUI.UI.ProdajaWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:POP_sf_41_2016_GUI     5   	   -   5          � Title$  ProdajaWindow��5   	   $��805.5��6   $	��678��6.    � 	Resources 5	         ValidationError+  %POP_sf_41_2016_GUI.ValidatorConverter  	converter��%�   &	        &	 �     �� 5
   
   .R�5      ]� .+�5      �� 
���  ��$��16(�6-   $��Right=�6;   $��Center=�6R   !!!6t   �� 5      
���  ��$��1q�6+   ._�5      �� 5      5      5        5   
   6#   5      .��5      � $��0,0,0,-6q�6    � RowDefinitions 5   
   �� 5      $
��150* �6   �� 5      $
��255* �6   �� 5      $
��218* �6   �� 5      $
��104* �6   �� 5      $	��75* �6    � ColumnDefinitions 5   
   �� 5      .+�5   
   �� $
��Left=�6   $��	28,44,0,0q�6+   $	��Top=�6>   $��72��6V   $��40��6a    J� RenderTransformOrigin$ 0.444,0.525&�6m   $��Kupac:��6�   $��18(�6�   �� 5    
   $��WrapWithOverflow=�6   $��Namestaj��64   $
��Left=�6D   $��
28,100,0,0q�6_   $	��Top=�6s   $��83��6�   $��30��6�   $��18(�6�   $��1��6�   �� 5!   
   $��WrapWithOverflow=�6   $��Dodatna usluga��64   $
��Left=�6J   $��	28,77,0,0q�6e   $��2��6x   $	��Top=�6�   $��83��6�   $��50��6�   $��18(�6�   $ 
0.187,2.24&�6�   �� 5"   
   -   5        $��tbKupac��5"       =� ErrorTemplate# [  6!   $
��Left=�6i   $��40��6�   $��
116,44,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��306��6�   $��16(�6�   .��6�   ��5#      �� 5$       �� Path$ Kupac�6    �� UpdateSourceTrigger$ PropertyChanged=�6'   	 �� ValidationRules	 5%      )  #POP_sf_41_2016_GUI.ValidationString  5&      5(      .��5)      5*      �� 5+   
   $��
116,89,0,0q�6   
���  ��$
��Bold"�69   $
��Left=�5-      $	��333��67   $��31��6C   $	��Top=�6O   ��5,      �� 
 �� ElementName$
 tbKupac��$ (Validation.Errors)� �� 	Converter# [ 5-   O   �� 5/   
   -   5        $��	btnDodajN��5/      $��Dodaj��6"   $
��Left=�62   $	��Top=�6M   $��94��6e   $��
566,46,0,0q�6p   $��37��6�   $��16(�6�   $��1��6�   �� 50   
   -   5        $��
btnObrisiN��50      $��Obrisi��6#   $	��Top=�64   $��566,133,0,0q�6L   $��36��6a   $��16(�6m   $
��Left=�6{   $��94��6�   $��1��6�   &   System.Windows.Controls.DataGrid  51   
   -   5        $��
dgNamestaj��51      $
��Left=�6%   $	��206��6@   $��
116,10,0,0q�6M   $��1��6a   $	��Top=�6n   $	��430��6�   $��16(�6�      AutoGenerateColumns .     
IsReadOnly .    Columns 52      0  *System.Windows.Controls.DataGridTextColumn  53      ,  &System.Windows.Controls.DataGridColumn   Header$ Naziv��6D      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6T   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� $ Naziv�6T     54      $ Kolicina��6G   $ * 6Z    6%   �� $ Kolicina�6Z     55      $
 Cena��6I   $ * 6X    6%   �� $ 
UkupnaCena�6X   57        58   
   -   5        $��dgDodatnaUsluga��58      $
��Left=�6*   $	��151��6E   $��
116,10,0,0q�6R   $��2��6f   $	��Top=�6s   $	��430��6�   $��16(�6�    .   .  59        5:      $ Naziv��6D   $ * 6T    6%   �� $ Naziv�6T     5;      $
 Cena��6C   $ * 6R    6%   �� $
 Cena�6R   5=      �� 5>   
   $��WrapWithOverflow=�6   $��Cena sa pdv-om��64   $
��Left=�6J   $��	28,30,0,0q�6e   $��3��6x   $	��Top=�6�   $	��188��6�   $��48��6�   $��18(�6�   �� 5?   
   -   5        $��	tbCenaPDV��5?      $��WrapWithOverflow=�6%   $
��Left=�6g   $��
248,30,0,0q�6�   $��3��6�   $	��Top=�6�   $	��178��6�   $��48��6�   $��18(�6�   ��6E   �� $ UkupanIznos�6�   �� 5@   
   -   5        $��
btnDodajDU��5@      $��Dodaj��6#   $
��Left=�63   $	��Top=�6N   $��94��6f   $��
566,26,0,0q�6q   $��2��6�   $��37��6�   $��16(�6�   $ 3.739,2.568&�6�   �� 5A   
   -	   5        $��btnObrisiDU��5A      $��Obrisi��6$   $	��Top=�65   $��
566,96,0,0q�6M   $��2��6a   $��36��6n   $��16(�6z   $
��Left=�6�   $��94��6�   6�   �� 5B   
   -
   5        $��btnOdustani��5B      $��Odustani��6$   $
��Left=�67   $��
368,10,0,0q�6R   $	��Top=�6f   $��94��6~   $��36��6�   $��4��6�   $��16(�6�   $ 	0.5,0.611&�6�   �� 5C   
   -   5        $��
btnPotvrdi��5C      $��Potvrdi��6#   $
��Left=�65   $��
194,10,0,0q�6P   $	��Top=�6d   $��94��6|   $��36��6�   $��4��6�   $��16(�6�   ��5D      �� 5E       �� 
TargetType" ��6   .�5F      ��  �� Property � {� �� Value .  62    �� Triggers 5G      ^� 5H       ^� 
Conditions 5I      �� 5J   "    �� Binding 6,   �� $ (Validation.HasError)�$
 tbKupac�� �� Value	 false6p   .0�5L      ��  � {� . 6:   5M      5O      5Q      �� 5R   
   $��Prodavac��6   $
��Left=�6#   $��
490,10,0,0q�6>   $	��Top=�6R   $��14(�6j   �� 5S   
   -   5         ��Name$ lblImeProdavaca��5S      $
��Left=�6X   $��
581,10,0,0q�6s   $	��Top=�6�   $��79��6�   $��26��6�   $��14(�6�   ��6)   �� $ ProdavacKorisnickoIme�6�   �� 5T   
   $��Datum��6   $
��Left=�6    $��
490,50,0,0q�6;   $	��Top=�6O   $��14(�6g   �� 5U   
   -   5        $ lblDatumProdaje��5U      $�� ��6)   $
��Left=�64   $��
581,50,0,0q�6O   $	��Top=�6c   $��79��6{   $��14(�6�   �� 5V   
   $��Broj racuna��6   $
��Left=�6&   $��
490,94,0,0q�6A   $	��Top=�6U   $��83��6m   $��14(�6x   �� 5W   
   -   5        $��lblBrRacuna��5W      $
��Left=�6E   $��
581,96,0,0q�6`   $	��Top=�6t   $��79��6�   $��14(�6�   ��6#   �� $ BrRacuna�6�   5Y      5Z      