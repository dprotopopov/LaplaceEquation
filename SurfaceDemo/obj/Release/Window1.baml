   M S B A M L   `   `   `  ���� [  WHelixToolkit.Wpf, Version=2014.2.15.1, Culture=neutral, PublicKeyToken=52aa3500039caf0dM8clr-namespace:HelixToolkit.Wpf;assembly=HelixToolkit.WpfHelixToolkit.Wpf  F BSurfaceDemo, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:SurfaceDemoSurfaceDemo    SurfaceDemo.Window1   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=3.5.41019.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jhelix8clr-namespace:HelixToolkit.Wpf;assembly=HelixToolkit.Wpf     +localclr-namespace:SurfaceDemo     MCustom@http://schemas.microsoft.com/winfx/2006/xaml/presentation/ribbon  -     �Name$  
thisWindow�� � Title$ Parametric surfaces��$	��720��$
��1280��
���   �
������� � 	Resources -   'HelixToolkit.Wpf.EnumToBooleanConverter   EnumToBooleanConverter��%   &	          .��]� .+�o� -    o�Name$ mainMenu��$	��Top=�.@�m� $
��File��.@�m� $��Open...��{�.  m� $��
Save as...��{�.  �� m� -   $��	Export...��m� -   $��Export stereo...���� m� -   $��E_xit��m� $
��View��.@�m� -    m�Name$ 
ViewSource��$��Source�� m� IsCheckable .  m� 	IsChecked . m� -   $��Reset camera��m� -   $ 
Fullscreen��$��Full screen�� . �� m� -	   $��Normal�� .  �� *
ViewMode+ �� ConverterParameter$ Normal�� �� 	Converter# [  	 �� Mode$	 TwoWay=�m� -
   $��Stereo�� .  �� *
ViewMode+$ Stereo��# [  $	 TwoWay=�m� -   $��Anaglyph�� .  �� *
ViewMode+$ Anaglyph��# [  $	 TwoWay=�m� $��Brush��.@�m� -   $	��Hue��m� -   $��Hue (stripes)��m� -   $��Rainbow��m� -   $��Rainbow (stripes)��m� -   $��Pattern��m� -   $��Image...��m� $��Media...��{�.  m� -   $ surfacesMenu��$��Surfaces��� $
��Left=�
 � ColumnDefinitions
 �� $��1* ��� $��2* �.+�]� -    ]�Name$ SourcePanel��.+�� $��Bottom=�$��4q�
 �� $
��Auto ��� �� $
��Auto � � RowDefinitions �� �� �� �� .+��� $��Elements in 'u' direction��$��0���� $��Elements in 'v' direction��$��1���� $��Parameter 'w'��$��2���� $��
Stereobase��$��3���� -    ��Name$ slider1��$��1��$��0�� � Minimum$ 10Q� � Maximum$	 400Q�$��	10 0 10 0q� � Value �� *	MeshSizeU+�� -   $ slider2��$��1��$��1��$ 10Q�$	 400Q�$��	10 0 10 0q� �� *	MeshSizeV+�� -   $ slider3��$��1��$��2��$ 0Q�$ 2Q�$��	10 0 10 0q� �� *
ParameterW+�� -   $ slider4��$��1��$��3��$ 0.003Q�$	 1.2Q�$��	10 0 10 0q� �� *
StereoBase+{��� *
ViewStereo+�� $��2��$��0������ *	MeshSizeU+ �� StringFormat$ {0:N0}���� $��2��$��1������ *	MeshSizeV+$ {0:N0}���� $��2��$��2������ *
ParameterW+$ {0:N2}���� $��2��$��3��{��� *
ViewStereo+���� *
StereoBase+$ {0:N3}���� -    ��Name$ 	errorList��$��Bottom=�$	��100�� �� View �� .P���  �� Width$ 40��$
��Line�� �� DisplayMemberBinding �� *Line+�� $	 300��$��Error�� �� *	ErrorText+�� -    ��Name$ source1��$!��Lucida Console, Courier New)�$ �� HorizontalScrollBarVisibility$
 Auto=�" �� VerticalScrollBarVisibility$
 Auto=� �� AcceptsReturn . �� $��Right=�$��8��$��Stretch=�
������ |�.  &    HelixToolkit.Wpf.HelixViewport3D  -     Name$ view1��$��1��   CameraRotationMode$ 	Trackball=�   ShowFrameRate . 
���   �   Title �� *
ModelTitle+   Children. $   HelixToolkit.Wpf.DefaultLights  &   SurfaceDemo.DynamicCodeSurface3D  -   surface1��$   HelixToolkit.Wpf.MeshElement3D   Fill �� *Brush+*   $HelixToolkit.Wpf.ParametricSurface3D    	MeshSizeU  �� *	MeshSizeU+!   	MeshSizeV! �� *	MeshSizeV+"   
ParameterW" �� *
ParameterW+#   Source# �� *Text+$ �� ElementName$$ source1��% �� UpdateSourceTrigger$% PropertyChanged=�#   HelixToolkit.Wpf.StereoView3D  -   &  Name$& view2��$y�Hidden=�$��1��
���   �$   HelixToolkit.Wpf.StereoControl'   CameraRotationMode$' 	Trackball=�(   
StereoBase( �� *
StereoBase+%	   HelixToolkit.Wpf.AnaglyphView3D	  -   ) 	 Name$) view3��$y�Hidden=�$��1��
���   �$' 	Trackball=�( �� *
StereoBase+6
  0Microsoft.Windows.Controls.Ribbon.RibbonCheckBox
  * 
  Label$* CheckBox��