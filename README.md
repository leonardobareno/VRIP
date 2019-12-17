# VRIP
Tomado conceptualmente a partir de <a href="https://github.com/culturevis/imageplot">ImagePlot</a> y del artículo <a href="http://manovich.net/content/04-projects/073-style-space/70_article_2011.pdf">Style Space: How to compare image sets and follow their evolution</a>, por <a href="http://manovich.net/">Lev Manovich</a> y <a href="http://lab.softwarestudies.com/">Software Studies Initiative</a>.

## Créditos
* <a href="https://scontent.oculuscdn.com/v/t64.5771-25/10000000_260630374721291_735112454761086976_n.zip?_nc_cat=111&_nc_oc=AQkCIcMtnf9moCQb1_Lou7d9d8P-6bUY9MOjj6gvTMsvOaT0eYp7OzLhe1jIthQAO90&_nc_ht=scontent.oculuscdn.com&oh=5dd90f9f76ef0a7daa245a1925f21d4f&oe=5D39C39C">Demo project OVR_UI_Demo_5_2</a> from the <a href="https://developer.oculus.com/blog/unitys-ui-system-in-vr/">Unity's UI System in VR</a> post in Oculus Developer Blog
* <a href="https://developer.oculus.com/downloads/package/oculus-utilities-for-unity-5/1.18.1/">Oculus Utilities for Unity v. 1.18.1</a>
* <a href="https://github.com/robertohuertasm/SQLite4Unity3d">SQLite4Unity3d</a>
* <a href="https://assetstore.unity.com/packages/tools/gui/ui-tools-for-unity-124299">UI Tools for Unity</a>

## Observaciones
* Elaborado con Unity 2017.4.19, Android SDK API 23 (Marshmallow) dirigido hacia Samsung Gear VR (HMD y controlador).
* Imágenes tomadas de <a href="https://www.wga.hu/">Web Gallery of Art</a> (colecciones Master y Detail).
* Revisar la BD ./src/Assets/StreamingAssets/wga_hu_bd.sqlite en caso de ser necesarias modificaciones a los campos de ruta y nombre de archivo (columna filename).
* Modificar la cadena de path en ./src/Assets/LB/Scripts/constantes.cs:27 (IMAGES_PATH).
* <a href="https://developer.oculus.com/documentation/mobilesdk/latest/concepts/mobile-submission-sig-file/">Generar el archivo de firma osig</a> y colocarlo en el directorio ./src/Assets/Plugins/Android/assets

## Descripción
<a href="doc/VRIP_Paper_ext.pdf">Paper</a>
