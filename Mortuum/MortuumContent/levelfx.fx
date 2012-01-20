float4x4 World;
float4x4 View;
float4x4 Projection;

#define NUMLIGHTS 4

Texture2D xTexture;
float3 LightPos[NUMLIGHTS];
float LightPower[NUMLIGHTS];
float4 LightColor[NUMLIGHTS];

SamplerState TextureSampler
{
	magfilter = LINEAR;
	minfilter = LINEAR;
	mipfilter = LINEAR;
	AddressU = mirror;
	AddressV = mirror;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TexCoords : TEXCOORD0;
	float3 Normal : TEXCOORD1;
	float3 Position3D : TEXCOORD2;
};

float Intensity(float3 lightPos, float3 pixelPos, float3 normal)
{
	float3 dir = normalize(pixelPos - lightPos);
	return dot(-dir, normal);
}

VertexShaderOutput VertexShaderFunction(float4 inPos : POSITION0, float3 inNormal: NORMAL0, float2 inTexCoords : TEXCOORD0)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(inPos, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

	output.TexCoords = inTexCoords;
	output.Normal = normalize(mul(inNormal, (float3x3)World));
	output.Position3D = mul(inPos, World);

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

	output = xTexture.Sample(TextureSampler, input.TexCoords) * saturate(intensity);

    return output;
}

technique Torches
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
