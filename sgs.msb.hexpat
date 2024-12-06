#pragma endian big

struct Pascal64String
{
    u64 length;
    char value[length];
};

struct Matrix4x4
{
    float m[16];
};

Pascal64String version @ $;
u64 count @ $;

struct Bone
{
    Pascal64String name;
    u32 parentBone;
    Matrix4x4 matrix;
};

Bone bones[count] @ $;