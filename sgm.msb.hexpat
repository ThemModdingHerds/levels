#pragma endian big

struct Pascal64String
{
    u64 length;
    char value[length];
};

Pascal64String version @ $;

struct Vector2
{
    float x;
    float y;
};

struct Vector3 : Vector2
{
    float z;
};

struct Matrix4x4
{
    float m[16];
};

struct Unknown
{
    float data[13];
};

struct Triangle
{
    s16 x;
    s16 y;
    s16 z;
};

struct Header
{
    Pascal64String textureName;
    Unknown unknown;
    Pascal64String format;
    u64 polygonSize;
    u64 polygonC;
    u64 triangleC;
    u64 boneC;
    padding[polygonSize * polygonC];
    Triangle triangles[triangleC];
    Vector3 pos;
    Vector3 rot;
    Pascal64String bones[boneC];
    Matrix4x4 boneProperties[boneC];
};

Header hds @ $;