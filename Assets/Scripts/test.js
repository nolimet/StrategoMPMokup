#pragma strict

static var lineMaterial : Material;

static function CreateLineMaterial() {
	if( !lineMaterial ) {
		lineMaterial = new Material( "Shader \"Lines/Colored Blended\" {" +
			"SubShader { Pass { " +
			"    Blend SrcAlpha OneMinusSrcAlpha " +
			"    ZWrite Off Cull Off Fog { Mode Off } " +
			"    BindChannels {" +
			"      Bind \"vertex\", vertex Bind \"color\", color }" +
			"} } }" );
		lineMaterial.hideFlags = HideFlags.HideAndDontSave;
		lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
	}
}

function OnPostRender() {
	CreateLineMaterial();
	// set the current material
	lineMaterial.SetPass( 0 );
	GL.Begin( GL.LINES );
	GL.Color( Color(1,1,1,0.5) );
	GL.Vertex3( 0, 0, 0 );
	GL.Vertex3( 9, 0, 0 );
	GL.Vertex3( 0, 9, 0 );
	GL.Vertex3( 9, 9, 0 );
	GL.Color( Color(0,0,0,0.5) );
	GL.Vertex3( 0, 0, 0 );
	GL.Vertex3( 0, 9, 0 );
	GL.Vertex3( 9, 0, 0 );
	GL.Vertex3( 9, 9, 0 );
	GL.End();
}