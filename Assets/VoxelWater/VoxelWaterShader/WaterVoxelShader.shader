//Shader "Custom/WaterVoxelShader"
//{
//    Properties
//    {
//        _MainTex ("Texture", 2D) = "white" { }
//        _Speed ("Speed", Range(1, 10)) = 2
//        _Amplitude ("Amplitude", Range(0.1, 5.0)) = 1.0
//    }
    
//    SubShader
//    {
//        Tags { "RenderType"="Opaque" }
//LOD 100
        
//        CGPROGRAM
//        #pragma surface surf Lambert vertex:vert
        
//sampler2D _MainTex;
//fixed4 _Color;
//float _Speed;
//float _Amplitude;
        
//struct Input
//{
//    float2 uv_MainTex;
//};
        
//void vert(inout appdata_full v)
//{
//            // Move the vertex position in the y-axis using sine function
//    v.vertex.y += _Amplitude * sin(_Time.y * _Speed);
//}
        
//void surf(Input IN, inout SurfaceOutput o)
//{
//    fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
//    o.Albedo = c.rgb;
//    o.Alpha = c.a;
//}
        
//        ENDCG
//    }
    
//FallBack"Diffuse"
//}










//Shader "Custom/WaterVoxelShader"
//{
//    Properties
//    {
//        _MainTex ("Texture", 2D) = "white" { }
//        _GlobalSpeed ("Global Speed", Range(1, 10)) = 2
//        _GlobalAmplitude ("Global Amplitude", Range(0.1, 5.0)) = 1.0
//        _ID ("Object ID", Range(0, 10)) = 0
//    }
    
//    SubShader
//    {
//        Tags { "RenderType"="Opaque" }
//LOD 100
        
//        CGPROGRAM
//        #pragma surface surf Lambert vertex:vert
        
//sampler2D _MainTex;
//float _GlobalSpeed;
//float _GlobalAmplitude;
//float _ID;
        
//struct Input
//{
//    float2 uv_MainTex;
//};
        
////void vert(inout appdata_full v)
////{
////            // Mueve el vértice en el eje Y usando la función senoidal y un valor único para cada objeto
////    v.vertex.y += _GlobalAmplitude * sin(_Time.y * (_GlobalSpeed + _ID + v.vertex.x)) +
////                          _GlobalAmplitude * sin(_Time.y * (_GlobalSpeed * 1.3 + _ID + v.vertex.z));

////            // Restaura la posición original en los otros ejes para evitar la inclinación
////    v.vertex.x = v.vertex.x;
////    v.vertex.z = v.vertex.z;
////}

//void vert(inout appdata_full v)
//{
//    // Mueve el vértice en el eje Y usando la función senoidal y un valor único para cada objeto
//    v.vertex.y += _GlobalAmplitude * sin(_Time.y * (_GlobalSpeed + _ID));

//    // Restaura la posición original en los otros ejes para evitar la inclinación
//    v.vertex.x = v.vertex.x;
//    v.vertex.z = v.vertex.z;
//}

        
//void surf(Input IN, inout SurfaceOutput o)
//{
//            // Utiliza un color constante (azul) en lugar de _Color
//    float3 baseColor = float3(0.2, 0.3, 1.0);
            
//    fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
//    o.Albedo = c.rgb * baseColor;
//    o.Alpha = c.a;
//}
//        ENDCG
//    }
    
//FallBack"Diffuse"
//}














Shader "Custom/WaterVoxelShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" { }
        _GlobalSpeed ("Global Speed", Range(1, 10)) = 2
        _GlobalAmplitude ("Global Amplitude", Range(0.1, 5.0)) = 1.0
        _ID ("Object ID", Range(0, 10)) = 0
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
LOD 100
        
        CGPROGRAM
        #pragma surface surf Lambert vertex:vert
        
sampler2D _MainTex;
float _GlobalSpeed;
float _GlobalAmplitude;
float _ID;
        
struct Input
{
    float2 uv_MainTex;
    float3 worldPos;
};
        
void vert(inout appdata_full v)
{
            // Mueve el vértice en el eje Y usando la función senoidal y un valor único para cada objeto
    v.vertex.y += _GlobalAmplitude * sin(_Time.y * (_GlobalSpeed + _ID));

            // Restaura la posición original en los otros ejes para evitar la inclinación
    v.vertex.x = v.vertex.x;
    v.vertex.z = v.vertex.z;
}

void surf(Input IN, inout SurfaceOutput o)
{
    // Utiliza las coordenadas del mundo para obtener la altura
    float height = IN.worldPos.y + 3;

    // Normaliza la altura para obtener un valor entre 0 y 1
    float normalizedHeight = (height - _GlobalAmplitude) / (_GlobalAmplitude * 2.0);

    // Utiliza un color que varía según la altura
    float3 color1 = float3(0.075, 0.071, 0.749); // Color #1312bf
    float3 color2 = float3(0.051, 0.862, 0.992); // Color #0adcfd
    float3 baseColor = lerp(color1, color2, normalizedHeight*0.2);

    fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
    o.Albedo = c.rgb * baseColor;
    o.Alpha = c.a;
}


        ENDCG
    }
    
FallBack"Diffuse"
}