float4x4 World;
float4x4 View;
float4x4 Projection;

#define NUMLIGHTS 4

cbuffer Parameters : register(b0)
{
	float4 DiffuseColor : c0;
};

float3 LightPos[NUMLIGHTS];
float LightPower[NUMLIGHTS];
float4 LightColor[NUMLIGHTS];

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float3 Normal : TEXCOORD1;
	float3 Position3D : TEXCOORD2;
};

struct LVertex3D
{
	float4 Position: POSITION;
	float3 Normal : NORMAL;
};

float Intensity(float3 lightPos, float3 pixelPos, float3 normal)
{
	float3 dir = normalize(pixelPos - lightPos);
	return dot(-dir, normal);
}

VertexShaderOutput VertexShaderFunction(LVertex3D v)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(v.Position, World);
    float4 viewPosition = mul(worldPosition, View);

    output.Position = mul(viewPosition, Projection);
	output.Normal = normalize(mul(v.Normal, (float3x3)World));
	output.Position3D = mul(v.Position, World);

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float4 output;
    float intensity = 0.0f;

	for(int i = 0; i < NUMLIGHTS; i++)
	{
		intensity += LightPower[i] * saturate(Intensity(LightPos[i], input.Position3D, input.Normal)) * LightColor[i];
	}

	output = saturate(intensity) * DiffuseColor;

    return output;
}

technique Torches
{
    pass Pass1
    {
		AlphaBlendEnable = TRUE;
        DestBlend = INVSRCALPHA;
        SrcBlend = SRCALPHA;
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
