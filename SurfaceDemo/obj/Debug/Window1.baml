   M S B A M L   `   `   `  ����[  WHelixToolkit.Wpf, Version=2014.2.15.1, Culture=neutral, PublicKeyToken=52aa3500039caf0dM8clr-namespace:HelixToolkit.Wpf;assembly=HelixToolkit.WpfHelixToolkit.Wpf  5      F BSurfaceDemo, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:SurfaceDemoSurfaceDemo 5         SurfaceDemo.Window1   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jhelix8clr-namespace:HelixToolkit.Wpf;assembly=HelixToolkit.Wpf     5      +localclr-namespace:SurfaceDemo     5      -   5          �Name$  
thisWindow��5       � Title$ Parametric surfaces��5      $	��720��6!   $
��1280��6.   
���   �
������� � 	Resources 5	      -   'HelixToolkit.Wpf.EnumToBooleanConverter   EnumToBooleanConverter��%   &	          5
   
   6'   5      .��5      ]� .+�5   
   o� -   5        $��mainMenu��5      $	��Top=�6   .@�5      m� $
��File��6   .@�5      m� $��Open...��6   {�.  6,   m� 5      $��
Save as...��6   {�.  6/   �� 5      m� 5      -   5        $��	Export...��5      6.   m� 5      -   5        $��Export stereo...��5      65   �� 5      m� 5      -   5        $��E_xit��5      6*   5      m� 5      $
��View��6   .@�5      m� -   5        $��
ViewSource��5      $��Source��6-    m� IsCheckable .  m� 	IsChecked . 6a   m� 5      -   5        $��Reset camera��5      61   m� 5      -   5        $��
Fullscreen��5      $��Full screen��6-    . 6U   �� 5      m� 5      -	   5        $��Normal��5       .  6?   �� *
ViewMode+ �� Mode$ TwoWay=� �� 	Converter# [   �� ConverterParameter$ Normal��6�   m� 5      -
   5        $��Stereo��5       .  6>   �� *
ViewMode+$ TwoWay=�# [  $ Stereo��6�   m� 5      -   5        $��Anaglyph��5       .  6@   �� *
ViewMode+$ TwoWay=�# [  $ Anaglyph��6�   5      m� 5       $��Brush��6   .@�5!      m� -   5        $	��Hue��5!      6(   m� 5"      -   5        $��Hue (stripes)��5"      62   m� 5#      -   5        $��Rainbow��5#      6,   m� 5$      -   5        $��Rainbow (stripes)��5$      66   m� 5%      -   5        $��Pattern��5%      6,   m� 5&      -   5        $��Image...��5&      6-   m� 5'      $��Media...��6   {�.  6-   5(      m� 5)      -   5        $��surfacesMenu��5)   )   $��Surfaces��6   6)   5*      � 5+   
   $
��Left=�6    � ColumnDefinitions 5,      �� 5-      $��1* �6#   �� 5.      $��2* �6#   .+�50      ]� -   5        $��SourcePanel��50      .+�51      � $��Bottom=�6   $��4q�6/    52      �� 53      $
��Auto �6+   �� 54      �� 55      $
��Auto �6+   	 � RowDefinitions	 57      �� 58      �� 59      �� 5:      �� 5;      .+�5=      �� $��Elements in 'u' direction��6    $��0��6A   �� 5>      $��Elements in 'v' direction��6    $��1��6A   �� 5?      $��Parameter 'w'��6    $��2��65   �� 5@      $��
Stereobase��6    $��3��62   �� 5A      -   5        $��slider1��5A      $��1��6,   $��0��6<   
 � Minimum$
 10Q�6I    � Maximum$	 400Q�6V   $��	10 0 10 0q�6�    � Value 6d   �� *	MeshSizeU+6�   �� 5B      -   5        $��slider2��5B      $��1��6,   $��1��6<   $
 10Q�6I   $	 400Q�6V   $��	10 0 10 0q�6�    6d   �� *	MeshSizeV+6�   �� 5C      -   5        $��slider3��5C      $��1��6,   $��2��6<   $
 0Q�6I   $ 2Q�6U   $��	10 0 10 0q�6~    6a   �� *
ParameterW+6~   �� 5D      -   5        $��slider4��5D      $��1��6,   $��3��6<   $
 0.003Q�6I   $	 1.2Q�6Y   $��	10 0 10 0q�6�    6g   �� *
StereoBase+{�6�   �� *
ViewStereo+6�   �� 5E      $��2��6T   $��0��6d   ��6    �� *	MeshSizeU+ �� StringFormat$ {0:N0}��6d   �� 5F      $��2��6R   $��1��6b   ��6    �� *	MeshSizeV+$ {0:N0}��6b   �� 5G      $��2��6S   $��2��6c   ��6    �� *
ParameterW+$ {0:N2}��6c   �� 5H      $��2��6t   $��3��6�   {�6    �� *
ViewStereo+��6A   �� *
StereoBase+$ {0:N3}��6�   5I      �� 5J      -   5         ��Name$ 	errorList��5J      $��Bottom=�6.   $	��100��6F    �� View 5K      �� 5L      .P�5M      ��  �� Width$ 40��6-   $
��Line��68    �� DisplayMemberBinding 6F   �� *Line+�� 5N      $	 300��6-   $��Error��69    6H   �� *	ErrorText+5O      5Q      �� 5R      -   5         ��Name$ source1��5S      $!��Lucida Console, Courier New)�6&   $ �� HorizontalScrollBarVisibility$
 Auto=�5T      " �� VerticalScrollBarVisibility$
 Auto=�6:    �� AcceptsReturn . 5U   *   5V      �� 5W      $��Right=�6   $��8��67   $��Stretch=�6A   
������ |�.  6v   &    HelixToolkit.Wpf.HelixViewport3D  5Y      -   5          Name$ view1��5Y   $   $��1��63      CameraRotationMode$ 	Trackball=�5Z         ShowFrameRate . 
���   �   Title 5[      �� *
ModelTitle+   Children. 5]      $   HelixToolkit.Wpf.DefaultLights  &   SurfaceDemo.DynamicCodeSurface3D  5^      -   5        surface1��$   HelixToolkit.Wpf.MeshElement3D   Fill 5`      �� *Brush+*   $HelixToolkit.Wpf.ParametricSurface3D   	MeshSizeU 5a      �� *	MeshSizeU+   	MeshSizeV 5b      �� *	MeshSizeV+   
ParameterW 5c      �� *
ParameterW+   Source 5d      �� *Text+  �� ElementName$  source1��! �� UpdateSourceTrigger$! PropertyChanged=�5e      #   HelixToolkit.Wpf.StereoView3D  5f      -   5        "  Name$" view2��5f   !   $y�Hidden=�60   $��1��6D   
���   �$   HelixToolkit.Wpf.StereoControl#   CameraRotationMode$# 	Trackball=�6�   $   
StereoBase$ 6g   �� *
StereoBase+6�   %	   HelixToolkit.Wpf.AnaglyphView3D	  5g      -   5        % 	 Name$% view3��5g   #   $y�Hidden=�62   $��1��6F   
���   �$# 	Trackball=�6�   $ 6i   �� *
StereoBase+6�   5h      5i      5j      