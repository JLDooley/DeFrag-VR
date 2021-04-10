Shader "ProjectCustomShaders/Basic Shader"
{
    Properties
    {
        _MainColour("Main Colour", Color)=(1,0,0,1)
    }
    SubShader
    {

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            fixed4 _MainColour;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 clipPos : SV_POSITION;
            };


            v2f vert (appdata v)
            {
                v2f o;
                o.clipPos = UnityObjectToClipPos(v.vertex);
                return o;
            }



            fixed4 frag(v2f i) : SV_Target
            {
                return _MainColour;
            }
            ENDCG
        }
    }
}