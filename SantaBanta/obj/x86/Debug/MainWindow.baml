   M S B A M L   `   `   `  ����  
SantaBanta    SantaBanta.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Icon$L  FE:\Nisarg Shah\Projects\SantaBanta\SantaBanta\Images\Pictures-icon.png��5   	    � Title$ 
MainWindow��5   	   $	��400��6   $	��600��6)   .��5      �  � RowDefinitions 5   
   �� 5      $��87 �6   6*   �� 5	      $��88 �6   6*   �� 5
      $��87 �6   6*   �� 5      $��88 �6   6*    � ColumnDefinitions 5   
   �� 5      $	��132 �6   6-   �� 5      $	��131 �6   6-   �� 5      $	��131 �6   6-   �� 5      $	��131 �6   6-   .+�5   
   �� $��0��6   $��0��6   $��Categories: ��6-   $��	0,10,0,10q�6D   �� 5   
   -   5        $��cmbCategories��5   `   $��0��6   $��1��6    $��100px��61   $
��23px��6?   $��	0,10,0,10q�6M      
Categories#���   6u    �� DisplayMemberPath$ CategoryName��5       �� SelectedValuePath$ CategoryURL��5      $	��Top=�5   2    �� SelectedValue 6   �� *
Categories+62   �� 5   
   -   5        $��0��5      $��2��6   $��100px��6.   $
��23px��6<   $	��Top=�6_   $��	0,10,0,10q�6w   .��5      �� $��List All��6   6+   5      �� 5   
   $��1��6   $��0��6   $��Sub Categories: ��6-   $��	0,10,0,10q�6H   �� 5   
   -   5        $��cmbSubCategories��5   _   $��1��6   $��1��6    $��100px��60   $
��23px��6>   $��	0,10,0,10q�6L     CategoriesContent#���  6w   $
 Name��5      $	 URL��5       $	��Top=�5!   9    6   �� *CategoriesContent+69   �� 5"   
   -   5        $��btnSubCategories��5"   J   $��1��6   $��2��6   $��100px��6.   $
��23px��6<   $	��Top=�6{   $��	0,10,0,10q�6�   .��5#      �� $)��#Download All from selected Category��6   6F   5$      �� 5&   
   -   5        $!��btnAllCategoriesAllDownload��5&   J   $��1��6   $��3��6   $��100px��6.   $
��23px��6<   $	��Top=�6�   $��	0,10,0,10q�6�   .��5'      �� $��Download All��6   6/   5(      � 5*   
   -   5        $��prgCategory��5*   G   $��2��6   $��0��6#   $��4��63   $��	10,5,10,5q�6Z   $
��23px��6m   5+      5,      