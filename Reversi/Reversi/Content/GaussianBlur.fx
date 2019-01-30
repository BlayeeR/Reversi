sampler TextureSampler : register(s0);

float2 Offsets[15];
float Weights[15];

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 TextureCoordinates : TEXCOORD0;
};

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    float4 output = float4(0, 0, 0, 1);
    for (int i = 0; i < 15; i++)
        output += tex2D(TextureSampler, input.TextureCoordinates + Offsets[i]) * Weights[i];
    return output;
}

technique Technique1
{
    pass p0
    {
        PixelShader = compile ps_4_0_level_9_1 PixelShaderFunction();
    }
}