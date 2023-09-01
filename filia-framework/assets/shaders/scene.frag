#version 330 core

struct Material
{
    vec4 ambientColor;
    vec4 diffuseColor;
    vec4 specularColor;
    sampler2D diffuseTexture;
};

in vec2 fUv;

uniform Material material;

out vec4 FragColor;

void main()
{
    //vec4 test = texture(uTexture0, fUv);
    vec4 result = texture(material.diffuseTexture, fUv) * material.diffuseColor;//(material.diffuseColor * texture(material.diffuseTexture, fUv));// + material.specularColor;
    FragColor = result;//vec4(result.x + 1, 1, 1, 1);//vec4(test.x + 1000, test.y + 1000, test.z + 1000, test.w + 1000) * vec4(1000, 1000, 1000, 1000);
    //FragColor = material.diffuseColor;//texture(material.diffuseTexture, fUv) * material.diffuseColor;
}